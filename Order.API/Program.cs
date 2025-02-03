using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OfferSale.DataAccess.Repository;
using Order.API.Exception;
using Order.API.Logging.Serilog;
using Order.API.Models;
using Order.ApplicationService.Handlers.CommandHandler;
using Order.ApplicationService.Handlers.CommandHandler.CustomerOrders;
using Order.ApplicationService.Handlers.QueryHandler.CustomerOrders;
using Order.ApplicationService.Services;
using Order.ApplicationService.Services.Interfaces;
using Order.Caching.Redis.Services;
using Order.API.Filters;
using Order.DataAccess.Repository.Interfaces;
using Order.Queue.RabbitMQ.Consumer;
using Order.Queue.RabbitMQ.Producer;
using Order.Repository.Context;
using Serilog;
using StackExchange.Redis;
using System.Reflection;
using TechBuddy.Middlewares.ExceptionHandling;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddAuthentication("ApiKey")
       .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>("ApiKey", null);

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));

builder.Services.AddSingleton<RabbitMqService>();
builder.Services.AddHostedService<RabbitMqConsumerService>();

builder.Services.AddAuthorization();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddAutoMapper(typeof(Program).Assembly);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<CustomerOrderPostreDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), e =>
        {
            e.EnableRetryOnFailure(2, TimeSpan.FromSeconds(30), null);
            e.MigrationsAssembly("Order.DataAccess");
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }));

builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
    opt.RegisterServicesFromAssemblies(typeof(CreateCustomerOrderCommandHandler).Assembly);
    opt.RegisterServicesFromAssemblies(typeof(CreateProductCommandHandler).Assembly);
    opt.RegisterServicesFromAssemblies(typeof(GetCustomerOrderByIdQueryHandler).Assembly);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_orderOrigin", policy =>
    {
        policy
         .AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer Order API", Version = "v1" });

    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Name = "X-API-KEY",
        Type = SecuritySchemeType.ApiKey,
        In = ParameterLocation.Header,
        Description = "API Key for accessing the API"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "ApiKey"
                    }
                },
                new string[] { }
            }
    });

    c.DocumentFilter<CustomDocumentFilter>();

    c.SupportNonNullableReferenceTypes();
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddTransient<ICustomerOrderService, CustomerOrderService>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddScoped<ISenderService, SmsService>();
builder.Services.AddScoped<ISenderService, EmailService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IRedisCacheService, RedisCacheService>();

var app = builder.Build();

app.UseMiddleware<RequestResponseLoggingMiddleware>();


app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer Order API"); });

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.AddTBExceptionHandlingMiddleware(opt =>
    {
        opt.IsDevelopment = false;
        opt.ContentType = "application/json";

        opt.ExceptionTypeList.Add<Exception>();
        opt.DefaultMessage = "Default error message";
        opt.DefaultHttpStatusCode = System.Net.HttpStatusCode.NotFound;

        opt.UseResponseModelCreator<CustomExceptionResponseModelCreator>();
    }
);



app.Run();

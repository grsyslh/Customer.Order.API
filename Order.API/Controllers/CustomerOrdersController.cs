using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.API.Models;
using Order.Contract.Request.Command.CustomerOrders;
using Order.Contract.Request.Query.CustomerOrders;
using Order.Domain.Entity;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "ApiKey")]
    public class CustomerOrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerOrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerOrder(Guid id)
        {
            var result = await _mediator.Send(new GetCustomerOrderByIdQueryRequest { Id = id });
            return Ok(result);
        }

        [HttpGet("GetCustomerOrdersByCustomerId")]
        public async Task<IActionResult> GetCustomerOrdersByCustomerId([FromQuery] GetCustomerOrdersByCustomerIdQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateCustomerOrder(CreateCustomerOrderCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCustomerOrder(UpdateCustomerOrderCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteCustomerOrder(DeleteCustomerOrderCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}

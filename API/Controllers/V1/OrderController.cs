using API.Controllers.V1.Base;
using Application.Contract.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using E = Domain.Entity;
using API.Models.V1;

namespace API.Controllers.V1
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IValidator<Order> _validator;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IValidator<Order> validator, IMapper mapper)
        {
            _orderService = orderService;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<IEnumerable<Order>>(await _orderService.RetrieveAll()));
        }

        [HttpGet("id:int")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            ThrowIfFalse(id > 0);

            var entity = await _orderService.RetrieveById(id);

            if (entity == default)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Order>(entity));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            // TODO: User CQR to separate a create command with other crud commands respective queries

            ThrowIfFalse(order.Id == 0);

            await _validator.ValidateAndThrowAsync(order);

            var entity = _mapper.Map<E.Order>(order);

            return CreatedAtAction(default, _mapper.Map<Order>(await _orderService.Create(entity)));
        }

        [HttpPatch]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] Order order)
        {
            await _validator.ValidateAndThrowAsync(order);

            var entity = _mapper.Map<E.Order>(order);

            return Ok(_mapper.Map<Order>(await _orderService.Update(entity)));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAll() => Ok(await _orderService.DeleteAll());
    }
}

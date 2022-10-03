﻿using Microsoft.AspNetCore.Mvc;
using ReverseAnalytics.Domain.DTOs.Order;
using ReverseAnalytics.Domain.DTOs.OrderItem;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace Reverse_Analytics.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly ILogger<OrdersController> _logger;

        const int pageSize = 15;

        public OrdersController(IOrderService service, ILogger<OrdersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        #region CRUD

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersAsync(int pageSize = pageSize, int pageNumber = 1)
        {
            try
            {
                var orders = await _service.GetAllOrdersAsync(pageSize, pageNumber);

                if (orders is null || orders.Count() < 1)
                {
                    return Ok("There is no Orders.");
                }

                return Ok(orders);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while retrieving Orders.", ex.Message);
                return StatusCode(500, "There was an error retrieving Order. Please, try agian later.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrderByIdAsync(int id)
        {
            try
            {
                var order = await _service.GetOrderByIdAsync(id);

                if (order is null)
                {
                    return NotFound($"There is no Order with id: {id}.");
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while retrieving Order with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Order with id: {id}. Please, try again later.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrderAsync([FromBody] OrderForCreate orderToCreate)
        {
            try
            {
                if (orderToCreate is null)
                {
                    return BadRequest("Order to create cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Order to create is not valid.");
                }

                var createdOrder = await _service.CreateOrderAsync(orderToCreate);

                if (orderToCreate is null)
                {
                    return StatusCode(500, "Something went wrong while creating new Order. Please, try again.");
                }

                return Ok(createdOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while creating new Order.", ex.Message);
                return StatusCode(500, $"There was an error creating new Order. Please, try again later.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrderAsync([FromBody] OrderForUpdate orderToUpdate, int id)
        {
            try
            {
                if (orderToUpdate is null)
                {
                    return BadRequest("Order to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Order to update is not valid.");
                }

                if (orderToUpdate.Id != id)
                {
                    return BadRequest($"Route id: {orderToUpdate.Id} does not match with route id: {id}.");
                }

                await _service.UpdateOrderAsync(orderToUpdate);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while updating Order with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error updating Order with id: {id}. Please, try again later.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderAsync(int id)
        {
            try
            {
                await _service.DeleteOrderAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while deleting Order with id: {id}.", ex.Message);
                return StatusCode(500, "There was an error deleting Order with id: {id}. Please, try again later.");
            }
        }

        #endregion

        #region Order Items

        [HttpGet("{id}/items")]
        public async Task<ActionResult<IEnumerable<OrderItemDto>>> GetOrderItemsAsync(int id, int pageSize = pageSize, int pageNumber = 1)
        {
            try
            {
                var orderItems = await _service.GetAllOrderItemsAsync(id, pageSize, pageNumber);

                if(orderItems?.Count() < 1)
                {
                    return Ok($"There is Items in Order with id: {id}.");
                }

                return Ok(orderItems);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error retrieving Order Items for Order with id: {id}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Order Items for Order with id: {id}. Please, try again later.");
            }
        }

        [HttpGet("{id}/items/{orderItemId}")]
        public async Task<ActionResult<OrderItemDto>> GetOrderItemByIdAsync(int id, int orderItemId)
        {
            try
            {
                var orderItem = await _service.GetOrderItemByIdAsync(id, orderItemId);

                if(orderItem is null)
                {
                    return NotFound($"There is no Order Item with id: {orderItemId}.");
                }

                return Ok(orderItem);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while retrieving Order Item with Order id: {id} and Order Item id: {orderItemId}.", ex.Message);
                return StatusCode(500, $"There was an error retrieving Order Item with id: {orderItemId}. Please, try again later.");
            }
        }

        [HttpPost("{id}/items")]
        public async Task<ActionResult<OrderItemDto>> CreateOrderItem([FromBody] OrderItemForCreate orderItemToCreate)
        {
            try
            {
                if(orderItemToCreate is null)
                {
                    return BadRequest("Order item to create cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Order Item to create is not valid.");
                }

                var createdOrderItem = await _service.CreateOrderItemAsync(orderItemToCreate);

                return Ok(createdOrderItem);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error while creating new Order Item.", ex.Message);
                return StatusCode(500, "There was an error creating new Order Item. Please, try again later.");
            }
        }

        [HttpPut("{id}/items/{orderItemId}")]
        public async Task<ActionResult> UpdateOrderItemAsync([FromBody] OrderItemForUpdate orderItemToUpdate, int id, int orderItemId)
        {
            try
            {
                if(orderItemToUpdate is null)
                {
                    return BadRequest("Order Item to update cannot be null.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Order Item to update is not valid.");
                }

                if(orderItemId != orderItemToUpdate.Id)
                {
                    return BadRequest($"Order Item id: {orderItemToUpdate.Id} does not match with route id: {orderItemId}");
                }

                await _service.UpdateOrderItemAsync(orderItemToUpdate);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while updating Order Item with id: {orderItemId}.", ex.Message);
                return StatusCode(500, "There was an error updating Order Item with id: {orderItemId}. Please, try again later.");
            }
        }

        [HttpDelete("{id}/items/{orderItemId}")]
        public async Task<ActionResult> DeleteOrderItemAsync(int id, int orderItemId)
        {
            try
            {
                await _service.DeleteOrderItemAsync(orderItemId);

                return NoContent();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error while deleting Order Item with id: {orderItemId}.", ex.Message);
                return StatusCode(500, $"There was an error deleting Order Item with id: {orderItemId}. Please, try again later.");
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleAPI.Entities;
using ExampleAPI.Repositories.Abstracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExampleAPI.Controllers;

[Route("api/[controller]")]
public class OrderItemsController : Controller
{
    private IOrderItemRepository _orderItemRepository;
    private IStockRepository _stockRepository;

    public OrderItemsController(IOrderItemRepository orderRepository, IStockRepository stockRepository)
    {
        _orderItemRepository = orderRepository;
        _stockRepository = stockRepository;
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        return Ok(_orderItemRepository.GetAll());
    }

    [HttpGet("GetById/{id}")]
    public IActionResult Get(Guid id)
    {
        return Ok(_orderItemRepository.Get(user => user.Id == id));
    }

    [HttpPost("Add")]
    public IActionResult Add([FromBody] OrderItem orderItem)
    {
        var stock = _stockRepository.Get(stock => stock.ProductId == orderItem.ProductId);
        if (stock.Count >= orderItem.Quantity)
        {
            stock.Count -= orderItem.Quantity;
            _stockRepository.Update(stock);
            return Ok(_orderItemRepository.Add(orderItem));
            
        }
        return BadRequest("Stok adedi yetersiz");
    }

    [HttpPut("Update")]
    public IActionResult Update([FromBody] OrderItem orderItem)
    {
        return Ok(_orderItemRepository.Update(orderItem));
    }

    [HttpDelete("DeleteById/{id}")]
    public IActionResult Delete(Guid id)
    {
        var orderItem = _orderItemRepository.Get(orderItem => orderItem.Id == id);
        if (orderItem == null) return BadRequest("OrderItem not found");
        return Ok(_orderItemRepository.Delete(orderItem));
    }
}


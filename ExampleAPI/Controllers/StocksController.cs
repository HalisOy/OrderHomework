using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampleAPI.Entities;
using ExampleAPI.Repositories.Abstracts;
using ExampleAPI.Repositories.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExampleAPI.Controllers;

[Route("api/[controller]")]
public class StocksController : Controller
{
    private IStockRepository _stockRepository;

    public StocksController(IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        return Ok(_stockRepository.GetAll());
    }

    [HttpGet("GetAllWithProduct")]
    public IActionResult GetAllWithProducts()
    {
        return Ok(_stockRepository
            .GetAll(include: stock => stock.Include(s => s.Product))
            );
    }

    [HttpGet("GetById/{id}")]
    public IActionResult Get(Guid id)
    {
        return Ok(_stockRepository.Get(stock => stock.Id == id));
    }

    [HttpPost("Add")]
    public IActionResult Add([FromBody] Stock stock)
    {
        return Ok(_stockRepository.Add(stock));
    }

    [HttpPut("Update")]
    public IActionResult Update([FromBody] Stock stock)
    {
        Stock oldStock = _stockRepository.Get(s => s.Id == stock.Id);
        if (oldStock == null) return BadRequest("Stock data not found");
        oldStock.Count += stock.Count;
        return Ok(_stockRepository.Update(oldStock));
    }

    [HttpDelete("DeleteById/{id}")]
    public IActionResult Delete(Guid id)
    {
        var stock = _stockRepository.Get(stock => stock.Id == id);
        if (stock == null) return BadRequest("Stock data not found");
        return Ok(_stockRepository.Delete(stock));
    }
}


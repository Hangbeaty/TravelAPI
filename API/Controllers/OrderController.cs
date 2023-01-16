using API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrderController : ControllerBase
  {
    private readonly IConfiguration _configuration;
    private readonly DOAN5Context _context;
    public OrderController(IConfiguration configuration, DOAN5Context context)
    {
      _configuration = configuration;
      _context = context;
    }
    [HttpGet]
    public IActionResult Index()
    {
      var result = _context.Orders.ToList();
      return Ok(result);
    }
    [HttpPost]
    public IActionResult Post(Order request)
    {


      _context.Orders.Add(request);
      var res = _context.SaveChanges();

      return StatusCode(201, request);
    }
    [HttpPut]
    public IActionResult Update(Order request)
    {
      Order newOrder = _context.Orders.Find(request.OrderId);
      if (newOrder == null) return null;

      newOrder.OrderName = request.OrderName;
      newOrder.OrderAdress = request.OrderAdress;
      newOrder.OrderEmail = request.OrderEmail;
      newOrder.OrderPhone = request.OrderPhone;
      newOrder.CustomerId = request.CustomerId;
      newOrder.TotalMoney = request.TourId;
      newOrder.TourId = request.TourId;
      newOrder.TourName = request.TourName;


      _context.Orders.Update(newOrder);
      var res = _context.SaveChanges();
      return Ok(newOrder);
    }
    [HttpDelete("{Id}")]
    public IActionResult Delete(int Id)
    {
      Order newOrder = _context.Orders.Find(Id);
      if (newOrder == null) return null;


      _context.Orders.Remove(newOrder);
      var res = _context.SaveChanges();
      return Ok(res);
    }
    [HttpGet("Paging")]
    public IActionResult Paging(string name, int pageSize, int pageIndex)
    {
      var result = from t1 in _context.Orders
                   
                   //join t3 in _context.Tours on t1.TourId equals t3.TourId
                   select new
                   {
                     t1.OrderId,
                     t1.OrderName,
                     t1.OrderEmail,
                     t1.OrderPhone,
                     t1.OrderAdress,
                     t1.TotalMoney,
                     t1.Status,
                     t1.Note,
                     t1.Code,
                     t1.StartDate,
                     t1.orderDate,
                     t1.PlaceName,
                    t1.CategoryName,
                     t1.TourName

                   };
      if (!string.IsNullOrEmpty(name))
        result = result.Where(x => x.OrderName.Contains(name));

      int total = result.Count();
      int totalPage = (int)Math.Ceiling(total / (double)pageSize);
      result = result.Skip((pageIndex - 1) * pageSize).Take(pageSize);
      PagingModel model = new PagingModel()
      {
        PageIndex = pageIndex,
        PageSize = pageSize,
        TotalPages = totalPage,
        TotalRecords = total,
        HasNextPage = pageIndex < totalPage,
        HasPreviousPage = pageIndex > 1,
        Data = result.ToList()
      };
      return Ok(model);
    }
    [HttpPost("order")]
    public IActionResult OrderTour(Order request)
    {
      var data = new Order()
      {
        orderDate = DateTime.Now,
        Status = "Đang check",
        OrderAdress = request.OrderAdress,
        OrderEmail = request.OrderEmail,
        OrderName = request.OrderName,
        OrderPhone = request.OrderPhone,
        CustomerId = request.CustomerId,
        CategoryName = request.CategoryName,
        PlaceName = request.PlaceName,
        StartDate = request.StartDate,
        TotalMoney = request.TotalMoney,
        TourName = request.TourName,
        Note=request.Note,
        Code = request.Code
      };
      _context.Add(data);
      _context.SaveChanges();
      return Ok(data);

    }
  }

}




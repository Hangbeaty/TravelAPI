using API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustomerController : ControllerBase
  {
    private readonly IConfiguration _configuration;
    private readonly DOAN5Context _context;

    public CustomerController(IConfiguration configuration, DOAN5Context context)
    {
      _configuration = configuration;
      _context = context;
    }
    [HttpPost("Login")]
    public IActionResult Login(Customer request)
    {
      if (string.IsNullOrEmpty(request.CustomerEmail))
      {
        var msg = new
        {
          devMsg = new { fieldName = "UserName", msg = "Tên đăng nhập không để trống" },
          useMsg = "Tên đăng nhập không để trống",
          Code = 400,
        };
        return Ok(msg);
      }
      if (string.IsNullOrEmpty(request.Password))
      {
        var msg = new
        {
          devMsg = new { fieldName = "Password", msg = "Bạn phải nhập Mật khẩu" },
          useMsg = "Mật khẩu không để trống",
          Code = 400,
        };
        return Ok(msg);
      }
      var admin = _context.Customers.SingleOrDefault(x => x.CustomerEmail == request.CustomerEmail && x.Password == request.Password);
      if (admin != null)
      {
        return Ok(admin);
      }
      else
      {
        var msg = new
        {
          useMsg = "Tài khoản hoặc mật khẩu chưa chính xác",
          Code = 999,
        };
        return Ok(msg);
      }

    }
    [HttpGet]
    public IActionResult Index()
    {
      var result = _context.Customers.ToList();
      return Ok(result);
    }
    [HttpPost]
    public IActionResult Post(Customer request)
    {


      _context.Customers.Add(request);
      var res = _context.SaveChanges();

      return StatusCode(201, request);
    }
    [HttpPut]
    public IActionResult Update(Customer request)
    {
      Customer newcustomer = _context.Customers.Find(request.CustomerId);
      if (newcustomer == null) return null;

      newcustomer.CustomerName = request.CustomerName;
      newcustomer.CustomerPhone = request.CustomerPhone;
      newcustomer.CustomerEmail = request.CustomerEmail;
      newcustomer.CustomerAdress = request.CustomerAdress;
      _context.Customers.Update(newcustomer);
      var res = _context.SaveChanges();
      return Ok(newcustomer);
    }
    [HttpDelete("{Id}")]
    public IActionResult Delete(int Id)
    {
      Customer newcustomer = _context.Customers.Find(Id);
      if (newcustomer == null) return null;


      _context.Customers.Remove(newcustomer);
      var res = _context.SaveChanges();
      return Ok(res);
    }
    [HttpGet("Paging")]
    public IActionResult Paging(string name, int pageSize, int pageIndex)
    {
      var result = from t1 in _context.Customers
                   select new
                   {
                     t1.CustomerId,
                     t1.CustomerName,

                     t1.CustomerPhone,
                     t1.CustomerEmail,
                     t1.CustomerAdress
                   };
      if (!string.IsNullOrEmpty(name))
        result = result.Where(x => x.CustomerName.Contains(name));

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
  }
}

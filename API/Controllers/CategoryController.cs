using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.IO;
using System;
using System.Data.SqlClient;
using API.Models;
using System.Linq;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoryController : ControllerBase
  {
    private readonly IConfiguration _configuration;
    //private readonly IWebHostEnvironment _env;
    private readonly DOAN5Context _context;
    public CategoryController(IConfiguration configuration, DOAN5Context context)
    {
      _configuration = configuration;
      _context = context;
    }
    [HttpGet]
    public IActionResult Index()
    {
      var result = _context.Categories.ToList();
      return Ok(result);
    }
    [HttpPost]
    public IActionResult Post(Category request)
    {

      var daa = new Category()
      {
        CategoryName =request.CategoryName,
        Description=request.Description
      };
      _context.Categories.Add(daa);
       _context.SaveChanges();

      return StatusCode(201, daa);
    }
    [HttpPut]
    public IActionResult Update(Category request)
    {
      Category newcategory = _context.Categories.Find(request.CategoryId);
      if (newcategory == null) return null;

      newcategory.CategoryName = request.CategoryName;
      newcategory.Description = request.Description;
      _context.Categories.Update(newcategory);
      var res = _context.SaveChanges();
      return Ok(newcategory);
    }
    [HttpDelete("{Id}")]
    public IActionResult Delete(int Id)
    {
      Category newcategory = _context.Categories.Find(Id);
      if (newcategory == null) return null;


      _context.Categories.Remove(newcategory);
      var res = _context.SaveChanges();
      return Ok(res);
    }
    [HttpGet("Paging")]
    public IActionResult Paging(string name, int pageSize, int pageIndex)
    {
      var result = from t1 in _context.Categories
                   select new
                   {
                     t1.CategoryName,

                     t1.Description,
                     t1.CategoryId
                   };
      if (!string.IsNullOrEmpty(name))
        result = result.Where(x => x.CategoryName.Contains(name));

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


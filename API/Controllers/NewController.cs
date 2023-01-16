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
  public class NewController : ControllerBase
  {
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;
    private readonly DOAN5Context _context;
    public NewController(IConfiguration configuration, IWebHostEnvironment env, DOAN5Context context)
    {
      _configuration = configuration;
      _env = env;
      _context = context;
    }
    [HttpGet]
    public IActionResult Index()
    {
      var result = _context.News.ToList();
      return Ok(result);
    }
    [HttpPost]
    public IActionResult Post(New request)
    {


      _context.News.Add(request);
      var res = _context.SaveChanges();

      return StatusCode(201, request);
    }
    [HttpPut]
    public IActionResult Update(New request)
    {
      New newNew = _context.News.Find(request.NewId);
      if (newNew == null) return null;

      newNew.NewName = request.NewName;
      newNew.Description = request.Description;
      newNew.Image = request.Image;

      _context.News.Update(newNew);
      var res = _context.SaveChanges();
      return Ok(newNew);
    }
    [HttpDelete("{Id}")]
    public IActionResult Delete(int Id)
    {
      New newNew = _context.News.Find(Id);
      if (newNew == null) return null;


      _context.News.Remove(newNew);
      var res = _context.SaveChanges();
      return Ok(res);
    }
    [HttpGet("{Id}")]
    public IActionResult FindsById(int Id)
    {
      var res = _context.News.Find(Id);
      return Ok(res);
    }
    [HttpGet("Paging")]
    public IActionResult Paging(string name, int pageSize, int pageIndex)
    {
      var result = from t1 in _context.News
                   select new
                   {
                     t1.NewId,
                     t1.NewName,
                     t1.Description,
                     t1.Image,
                   };
      if (!string.IsNullOrEmpty(name))
        result = result.Where(x => x.NewName.Contains(name));

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

    [HttpPost("UploadPhotos")]
    public IActionResult UploadPhotos()
    {
      var httpRequest = Request.Form;
      var posted = httpRequest.Files[0];
      string filename = posted.FileName.ToString();
      var physicalPath = _env.ContentRootPath + "/Photos/" + Path.GetFileName(filename);

      using (var stream = new FileStream(physicalPath, FileMode.Create))
      {
        posted.CopyTo(stream);
      }
      return new JsonResult(filename);
    }

  }
}



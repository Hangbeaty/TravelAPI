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
using System.Xml.Linq;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TourController : ControllerBase
  {
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;
    private readonly DOAN5Context _context;
    public TourController(IConfiguration configuration, IWebHostEnvironment env, DOAN5Context context)
    {
      _configuration = configuration;
      _env = env;
      _context
        = context;
    }
    [HttpGet]
    public IActionResult Index()
    {
      var result = _context.Tours.ToList();
      return Ok(result);
    }
    [HttpGet("search")]
    public IActionResult Search(string name)
    {
      var result = from t1 in _context.Tours
                   join t2 in _context.Categories on t1.CategoryId equals t2.CategoryId
                   join t3 in _context.Places on t1.PlaceId equals t3.PlaceId
                   select new
                   {
                     t1.TourId,
                     t1.TourName,
                     t1.Description,
                     t1.Price,
                     t1.PriceDiscount,
                     t1.Image,
                     t1.Time,
                     t2.CategoryId,
                     t2.CategoryName,
                     t3.PlaceId,
                     t3.PlaceName
                   };
      if (!string.IsNullOrEmpty(name))
        result = result.Where(x => x.TourName.Contains(name));
      return Ok(result);
    }


    [HttpPost]
    public IActionResult Post(Tour request)
    {


      _context.Tours.Add(request);
      var res = _context.SaveChanges();

      return StatusCode(201, request);
    }
    [HttpPut]
    public IActionResult Update(Tour request)
    {
      Tour newtour = _context.Tours.Find(request.TourId);
      if (newtour == null) return null;

      newtour.TourName = request.TourName;
      newtour.CategoryId = request.CategoryId;
      newtour.PlaceId = request.PlaceId;
      newtour.Description = request.Description;
      newtour.Price = request.Price;
      newtour.PriceDiscount = request.PriceDiscount;
      newtour.Image = request.Image;
      newtour.PlaceName = request.PlaceName;
      newtour.Time = request.Time;

      _context.Tours.Update(newtour);
      var res = _context.SaveChanges();
      return Ok(newtour);
    }
    [HttpDelete("{Id}")]
    public IActionResult Delete(int Id)
    {
      Tour newtour = _context.Tours.Find(Id);
      if (newtour == null) return null;


      _context.Tours.Remove(newtour);
      var res = _context.SaveChanges();
      return Ok(res);
    }
    //[HttpGet("{Id}")]
    //public IActionResult GetById(int Id)
    //{
    //  var res = _context.Tours.Find(Id);
    //  return Ok(res);
    //}
    [HttpGet("Paging")]
    public IActionResult Paging(string name, int pageSize, int pageIndex)
    {
      var result = from t1 in _context.Tours
                   join t2 in _context.Categories on t1.CategoryId equals t2.CategoryId
                   join t3 in _context.Places on t1.PlaceId equals t3.PlaceId
                   select new
                   {
                     t1.TourId,
                     t1.TourName,
                     t1.Description,
                     t1.Price,
                     t1.PriceDiscount,
                     t1.Image,
                     t1.Time,
                     t2.CategoryId,
                     t2.CategoryName,
                     t3.PlaceId,
                     t3.PlaceName
                   };
      if (!string.IsNullOrEmpty(name))
        result = result.Where(x => x.TourName.Contains(name));
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


    [HttpGet("{Id}")]
    public IActionResult FindById(int Id)
    {
      Tour newtour = _context.Tours.Where(x =>x.TourId == Id).Select(p => new Tour() {

        TourId = p.TourId,
        TourName= p.TourName,
        Price=p.Price,
        Description=p.Description,
        Time=p.Time,
        CategoryName=p.Category.CategoryName,
        PlaceName=p.Place.PlaceName,
        Image=p.Image,
        

      }).FirstOrDefault();

      if (newtour == null) return null;
      return Ok(newtour); 
    }
  }
}




using API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]

  public class PlaceController : ControllerBase
  {

    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;
    private readonly DOAN5Context _context;
    public PlaceController(IConfiguration configuration, IWebHostEnvironment env, DOAN5Context context)
    {
      _configuration = configuration;
      _env = env;
      _context = context;
    }
    [HttpGet]
    public IActionResult Index()
    {
      var result = _context.Places.ToList();
      return Ok(result);
    }
    [HttpPost]
    public IActionResult Post(Place request)
    {


      _context.Places.Add(request);
      var res = _context.SaveChanges();

      return StatusCode(201, request);
    }
    [HttpPut]
    public IActionResult Update(Place request)
    {
      Place newPlace = _context.Places.Find(request.PlaceId);
      if (newPlace == null) return null;

      newPlace.PlaceName = request.PlaceName;
      newPlace.Description = request.Description;
      newPlace.Image = request.Image;

      _context.Places.Update(newPlace);
      var res = _context.SaveChanges();
      return Ok(newPlace);
    }
    [HttpDelete("{Id}")]
    public IActionResult Delete(int Id)
    {
      Place newPlace = _context.Places.Find(Id);
      if (newPlace == null) return null;


      _context.Places.Remove(newPlace);
      var res = _context.SaveChanges();
      return Ok(res);
    }
    [HttpGet("Paging")]
    public IActionResult Paging(string name, int pageSize, int pageIndex)
    {
      var result = from t1 in _context.Places
                   select new
                   {
                     t1.PlaceId,
                     t1.PlaceName,
                     t1.Description,
                     t1.Image,
                   };
      if (!string.IsNullOrEmpty(name))
        result = result.Where(x => x.PlaceName.Contains(name));

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




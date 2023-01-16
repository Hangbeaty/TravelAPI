using API.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System;

namespace API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ContactController : ControllerBase
  {
    private readonly IConfiguration _configuration;
    private readonly DOAN5Context _context;
    public ContactController(IConfiguration configuration, DOAN5Context context)
    {
      _configuration = configuration;

      _context = context;
    }
    [HttpGet]
    public IActionResult Index()
    {
      var result = _context.Contacts.ToList();
      return Ok(result);
    }
    [HttpPost]
    public IActionResult Post(Contact request)
    {


      _context.Contacts.Add(request);
      var res = _context.SaveChanges();

      return StatusCode(201, request);
    }
    [HttpPut]
    public IActionResult Update(Contact request)
    {
      Contact newContact = _context.Contacts.Find(request.Id);
      if (newContact == null) return null;

      newContact.Name = request.Name;
      newContact.Email = request.Email;
      newContact.Phone = request.Phone;
      newContact.Content = request.Content;

      _context.Contacts.Update(newContact);
      var res = _context.SaveChanges();
      return Ok(newContact);
    }
    [HttpDelete("{Id}")]
    public IActionResult Delete(int Id)
    {
      Contact newContact = _context.Contacts.Find(Id);
      if (newContact == null) return null;


      _context.Contacts.Remove(newContact);
      var res = _context.SaveChanges();
      return Ok(res);
    }
    [HttpGet("Paging")]
    public IActionResult Paging(string name, int pageSize, int pageIndex)
    {
      var result = from t1 in _context.Contacts
                   select new
                   {
                     t1.Id,
                     t1.Name,
                     t1.Email,
                     t1.Phone,
                     t1.Content
                   };
      if (!string.IsNullOrEmpty(name))
        result = result.Where(x => x.Name.Contains(name));

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




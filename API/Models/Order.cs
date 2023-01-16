using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
  public partial class Order
  {
    public int OrderId { get; set; }
    public string OrderName { get; set; }
    public string OrderAdress { get; set; }
    public string OrderEmail { get; set; }
    public string OrderPhone { get; set; }
    public int? CustomerId { get; set; }
    public int? TotalMoney { get; set; }
    public int? TourId { get; set; }
    public string TourName { get; set; }
    public string Status { get; set; }
    public DateTime orderDate { get; set; }
    public string CategoryName { get; set; }
    public string PlaceName { get; set; }
    public string StartDate { get; set; }
    public string Code { get; set; }
    public string Note { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual Tour Tour { get; set; }
  }
}

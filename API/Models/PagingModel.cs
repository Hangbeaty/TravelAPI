namespace API.Models
{
  public class PagingModel
  {
    public int TotalRecords { get; set; }//tổng số bản ghi
    public int PageIndex { get; set; } = 1;//số trang
    public int PageSize { get; set; }//số bản ghi trên 1 trang
    public int TotalPages { get; set; }//tổng số trang
    public bool HasNextPage { get; set; }//tiến trang
    public bool HasPreviousPage { get; set; }//lùi trang
    public object Data { get; set; }


  }
}

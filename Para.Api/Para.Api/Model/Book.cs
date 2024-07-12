using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Para.Api;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Author { get; set; }
    public int PageCount { get; set; }
    public int Year { get; set; }

}
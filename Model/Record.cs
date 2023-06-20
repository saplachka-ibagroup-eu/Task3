using System.ComponentModel.DataAnnotations;

namespace Task3.Model;

public class Record
{
    [Key]
    public int Id { get; set; }
    public string Date { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string SurName { get; set; }
    public string City { get; set; }
    public string Country { get; set; }


}

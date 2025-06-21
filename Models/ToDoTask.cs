using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models;

public class TodoTask
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    public bool IsDone { get; set; }
}

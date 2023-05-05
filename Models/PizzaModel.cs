using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PizzaMania.Models;

public class Pizza
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Photo { get; set; }

    [NotMapped]
    public IFormFile? formFile { get; set; }

    public double Price { get; set; }


    public List<Ingredient>? Ingredients { get; set; } = new List<Ingredient>();
}


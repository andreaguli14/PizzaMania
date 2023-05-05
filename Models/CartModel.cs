using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PizzaMania.Models;

public class Cart
{
    [Key]
    public int Id { get; set; }

    public List<CartObjects> CartObjects { get; set; } = new List<CartObjects>();
}


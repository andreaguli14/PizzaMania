using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PizzaMania.Models;

public class CartObjects
{
    [Key]
    public int Id { get; set; }

    public int Quantity { get; set; } 

    public Pizza Pizza { get; set; } = new Pizza();
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace PizzaMania.Models;


    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string Address { get; set; }

        public string? AdditionNotes { get; set; }

        public string? Status { get; set; } = " ";

    public List<CartObjects> CartObjects { get; set; } = new List<CartObjects>();

        
    }



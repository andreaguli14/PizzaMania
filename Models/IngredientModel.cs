using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace PizzaMania.Models;


    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Pizza> Pizzas { get; set; } = new List<Pizza>();
    }



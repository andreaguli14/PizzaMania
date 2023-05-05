using Microsoft.AspNetCore.Identity;

namespace PizzaMania.Models;

public class User: IdentityUser
{
    public Cart cart { get; set; } = new Cart();
}


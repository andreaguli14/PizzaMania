using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PizzaMania.Models;
using PizzaMania.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace PizzaMania.Controllers;

[Authorize]
public class CartController : Controller
{ 

    private UserManager<User> _user;
    private ApplicationDbContext _db;
    public CartController(ApplicationDbContext db, UserManager<User> user)
    {
        _db = db;
        _user = user;
    }

    public IActionResult Index()
    {
      var model = _db.Users.Include(x=>x.cart).ThenInclude(x => x.CartObjects).ThenInclude(x=>x.Pizza).FirstOrDefault(x=>x.Id == _user.GetUserId(User));
          return View(model.cart);
    }


    public IActionResult UpdateQuantity(int id, int quantity)
    {
        var oggetto = _db.CartObjects.FirstOrDefault(x => x.Id == id);
        oggetto.Quantity = quantity;
        _db.SaveChanges();


        return RedirectToAction("Index");

    }


    public IActionResult Checkout(string address, string notes)
    {
        var user = _db.Users.Include(x => x.cart).ThenInclude(x => x.CartObjects).ThenInclude(x => x.Pizza).FirstOrDefault(x => x.Id == _user.GetUserId(User));
        var order = new Order();
        order.AdditionNotes = notes;
        order.Address = address;
        foreach( var i in user.cart.CartObjects)
        {
            order.CartObjects.Add(i);
        }
        user.cart.CartObjects.Clear();

        _db.Orders.Add(order);
        _db.SaveChanges();


        return RedirectToAction("Index");

    }



}


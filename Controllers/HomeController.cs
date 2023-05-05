using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PizzaMania.Models;

namespace PizzaMania.Controllers;

public class HomeController : Controller
{
    

    public HomeController()
    {
       
    }

    public IActionResult Index()
    {

          return View();
    }

 
}


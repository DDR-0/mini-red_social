using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mini_red_social1.Models;

namespace mini_red_social1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    
    }

    public IActionResult Registro()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int? id)
    {
        // El código llega desde UseStatusCodePagesWithReExecute para mostrar
        // el mensaje correcto cuando la ruta solicitada no existe
        var statusCode = id ?? StatusCodes.Status500InternalServerError;
        Response.StatusCode = statusCode;
        ViewData["StatusCode"] = statusCode;
        ViewData["RequestedPath"] = HttpContext.Request.Path;

        return View("~/Views/Home/error.cshtml");
    }
}
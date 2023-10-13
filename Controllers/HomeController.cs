
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class HomeController : ControllerBase{
    public HomeController(){

    }
    [HttpGet("/controllerhello")]
    public string Hello () => "Hello World from Controller";
}
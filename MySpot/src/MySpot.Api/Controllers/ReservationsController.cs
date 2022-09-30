using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public void Get()
    {
    }
    
    [HttpPost]
    public void Post(Reservation reservation)
    {
    }
}
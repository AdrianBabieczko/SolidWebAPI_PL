using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;

namespace MySpot.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private static int _id = 1;
    
    private static readonly List<Reservation> Reservations = new();

    private static readonly List<string> ParkingSpotNames = new()
    {
        "P1", "P2", "P3", "P4", "P5"
    };

    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> Get() => Ok(Reservations);

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> Get(int id)
    {
        var reservation = Reservations.SingleOrDefault(x => x.Id == id);

        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }
    

    [HttpPost]
    public ActionResult Post(Reservation reservation)
    {
        if (ParkingSpotNames.All(x=>x != reservation.ParkingSpotName))
        {
            return BadRequest();
        }

        reservation.Date = DateTime.UtcNow.AddDays(1).Date;
        
        var reservationAlreadyExists = Reservations.Any(x =>
            x.ParkingSpotName == reservation.ParkingSpotName && x.Date.Date == reservation.Date.Date);

        if (reservationAlreadyExists)
        {
            return BadRequest();
        }
        
        reservation.Id = _id;
        _id++;
        Reservations.Add(reservation);

        return CreatedAtAction(nameof(Get), new { id = reservation.Id }, null);
    }

    [HttpPut]
    public ActionResult Put(Reservation reservation)
    {
        return Ok();
    }
}
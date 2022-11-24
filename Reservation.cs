using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using System.Text;
using online_hotel_reservation.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;

namespace online_hotel_reservation.Controllers
{
    [ApiController]
    [Route("")]
    public class ReservationController : ControllerBase
    {
        public ApplicationDbContext _context;
        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Please Write code here to complete the API controller class
        static List<string> strings = new List<string>()
    {
        "value0", "value1", "value2"
    };
        // GET api/values
        [HttpGet]
        [RouteAttribute("getreservation/{ReservationID}")]
        public CustomerReservation getreservation(int ReservationID)
        {
            var data = _context.Reservation.Find(ReservationID);

            return data;
        }

        [HttpGet]
        [RouteAttribute("reservationlist")]
        public IActionResult getreservationlist()
        {
            var data = _context.Reservation;

            return Ok(data);
        }

        [HttpDelete]
        [RouteAttribute("Cancelbooking/{ReservationID}")]
        public IActionResult Cancelbooking(int ReservationID)
        {
            var data = _context.Reservation.Find(ReservationID);
            _context.Reservation.Remove(data);
            _context.SaveChanges();
            List <string> list = new List<string>();
            if (_context.Reservation == null)
                return Ok();
            return Ok();
        }

        [HttpPost]
        [RouteAttribute("InsertBooking")]

        public IActionResult insertreservation([FromBody] CustomerReservation customerReservation)
        {
            //return Ok(customerReservation);
            _context.Reservation.Add(customerReservation);
            var data = _context.Reservation.Find(customerReservation.ReservationID);
            _context.SaveChanges();

            return Ok("Registration successful");
        }


    }
}

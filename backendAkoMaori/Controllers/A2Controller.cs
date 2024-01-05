

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using A2.Data;
using A2.Dtos;
using A2.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.RegularExpressions;
using A2.Helper;
using System.IO;
using System.Text;
using System.Globalization;

namespace A2.Controllers
{
    [Route("webapi")]
    [ApiController]
    public class A2Controller : Controller
    {
        private readonly IA2Repo _repository;

        public A2Controller(IA2Repo repository)
        {
            _repository = repository;
        }

        [HttpPost("Register")]
        public ActionResult<User> Register(User u)
        {
            var UsernamenotTaken = _repository.UsernameTaken(u.UserName);
            var name = u.UserName;
            if (UsernamenotTaken == false)
            { 
                _repository.AddUser(u);
                return CreatedAtAction(nameof(_repository.GetUser),new { name = u.UserName },"User successfully registered.");
            }
            else
            {
                return Ok("UserName " + name + " is not available.");
            }
        }

        [HttpGet("getUser/{name}")]
        public User getUser(string name)
        {
            return _repository.GetUser(name);
        }






        [Authorize(AuthenticationSchemes = "A2Authentication")]
        [Authorize(Policy = "UserOnly")]
        [HttpGet("PurchaseItem/{id}")]
        public ActionResult<PurchaseOutput> PurchaseItem(int id)
        {
            ClaimsIdentity ci = HttpContext.User.Identities.FirstOrDefault();
            Claim c = ci.FindFirst("UserName");

            if (_repository.FindProduct(id))
            {
                PurchaseOutput o = new PurchaseOutput { userName = c.Value, productID = id };

                return Ok(o);
            }
            else
                return BadRequest("Product " + id + " not found");
        }

        [Authorize(AuthenticationSchemes = "A2Authentication")]
        [Authorize(Policy = "OrganizerOnly")]
        [HttpPost("AddEvent")]
        public ActionResult<Event> AddEvent(EventDto e)
        {
            

            Regex rx = new Regex("^[0-9]{8}T[0-9]{6}Z$");
            if (rx.Match(e.Start).Success && rx.Match(e.End).Success)
            {
                Event ev = new Event { Id = e.Id, Description = e.Description, End = e.End, Location = e.Location, Start = e.Start, Summary = e.Summary };
                _repository.InsertEvent(ev);
                return Ok("Success");
            }
            else if (rx.Match(e.Start).Success)
                return BadRequest("The format of End should be yyyyMMddTHHmmssZ.");
            else if (rx.Match(e.End).Success)
                return BadRequest("The format of Start should be yyyyMMddTHHmmssZ.");
            return BadRequest("The format of Start and End should be yyyyMMddTHHmmssZ.");
        }


        [Authorize(AuthenticationSchemes = "A2Authentication")]
        [Authorize(Policy = "OrganizerOrUser")]
        [HttpGet("EventCount")]
        public int EventCount()
        {
            return _repository.GetNumEvents();
        }


        [Authorize(AuthenticationSchemes = "A2Authentication")]
        [Authorize(Policy = "OrganizerOrUser")]
        [HttpGet("Event/{id}")]
        public ActionResult Event(int id)
        {
            Event e = _repository.GetEvent(id);
            if (e == null)
                return BadRequest("Event " + id + " does not exist.");

            Response.Headers.Add("Content-Type", "text/calendar");
            return Ok(e);
        }


    }
}
using System.Collections.Generic;
using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Services;
using Microsoft.AspNetCore.Mvc;

namespace fleeman_with_dot_net.Controllers
{
    [ApiController]
    [Route("api/addons")]
    public class AddOnDetailsController : ControllerBase
    {
        private readonly IAddOnDetailsService service;

        public AddOnDetailsController(IAddOnDetailsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AddOnDetails>> GetAllAddOns()
        {
            var addOns = service.GetAllAddOns();
            return Ok(addOns);
        }

        [HttpGet("{id}")]
        public ActionResult<AddOnDetails> GetAddOnById(int id)
        {
            var addOn = service.GetAddOnById(id);
            if (addOn == null) return NotFound();
            return Ok(addOn);
        }

        [HttpPost]
        public IActionResult AddAddOn(AddOnDetails addOn)
        {
            var createdAddOn = service.AddAddOn(addOn);
            return Ok(createdAddOn);
        }

        [HttpPut("{id}")]
        public ActionResult<AddOnDetails> UpdateAddOn(int id, AddOnDetails addOn)
        {
            if (id != addOn.addOnId)
                return BadRequest();

            var updatedAddOn = service.UpdateAddOn(addOn);
            if (updatedAddOn == null) return NotFound();

            return Ok(updatedAddOn);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddOn(int id)
        {
            var deleted = service.DeleteAddOn(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}

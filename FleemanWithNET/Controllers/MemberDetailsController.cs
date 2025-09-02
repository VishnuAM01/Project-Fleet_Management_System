using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace fleeman_with_dot_net.Controllers
{
    [ApiController]
    [Route("api/members")]
    public class MemberDetailsController : ControllerBase
    {
        private readonly IMemberDetailsService service;

        public MemberDetailsController(IMemberDetailsService service)
        {
           this.service = service;
        }
    
        [HttpGet]
        public ActionResult<IEnumerable<MemberDetails>> GetAllMembers()
        {
            var members = service.GetAllMembers();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public ActionResult<MemberDetails> GetMemberById(int id)
        {
            var member = service.GetMemberById(id);
            if (member == null)
                return NotFound();

            return Ok(member);
        }

        [HttpPost]
        public IActionResult AddMember(MemberDetails member)
        {
            var createdMember = service.AddMember(member);
            return Ok(createdMember);
        }


        [HttpPut("{id}")]
        public ActionResult<MemberDetails> UpdateMember(int id, MemberDetails member)
        {
            if (id != member.Member_Id)
                return BadRequest();

            var updatedMember = service.UpdateMember(member);
            if (updatedMember == null)
                return NotFound();

            return Ok(updatedMember);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMember(int id)
        {
            if (service.DeleteMember(id))
                return NoContent();

            return NotFound();
        }
    }
}

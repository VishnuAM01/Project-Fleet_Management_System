using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Services;
using Microsoft.AspNetCore.Mvc;

namespace fleeman_with_dot_net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // public class StateDetailsController : ControllerBase
    // {
    //private readonly IStateDetailsService service;

    //public StateDetailsController(IStateDetailsService stateService)
    //{
    //    service = stateService;
    //}

    //[HttpGet]
    //public IActionResult GetAllStates()
    //{
    //    return Ok(service.GetAllStates());
    //}

    //[HttpGet("{id}")]
    //public IActionResult GetStateById(int id)
    //{
    //    var state = service.GetStateById(id);
    //    if (state == null)
    //        return NotFound();

    //    return Ok(state);
    //}

    //[HttpPost]
    //public IActionResult AddState(StateDetails state)
    //{
    //    service.AddState(state);
    //    return Ok("State added successfully");
    //}

    //[HttpPut("{id}")]
    //public IActionResult UpdateState(int id, StateDetails state)
    //{
    //    if (id != state.State_Id)
    //        return BadRequest("State ID mismatch");

    //    service.UpdateState(state);
    //    return Ok("State updated successfully");
    //}

    //[HttpDelete("{id}")]
    //public IActionResult DeleteState(int id)
    //{
    //    service.DeleteState(id);
    //    return Ok("State deleted successfully");
    //}



    //}


    public class StateDetailsController : ControllerBase
    {
        IStateDetailsService _stateService;
        public StateDetailsController(IStateDetailsService stateService)
        {
            this._stateService = stateService;
        }

        //GET /api/StateDetails → returns all states
        [HttpGet]
        public ActionResult<IEnumerable<StateDetails>> GetAllStates()
        {
            return Ok(_stateService.GetAllStates());
        }

        //GET /api/StateDetails/{id} → returns a single state or 404
        [HttpGet("{id}")]
        public ActionResult<StateDetails> GetState(int id)
        {
            var state = _stateService.GetState(id);
            if (state != null)
            {
                return Ok(state);
            }
            return NotFound();
        }

        //POST /api/StateDetails → creates a new state and returns 201 Created
        [HttpPost]
        public ActionResult<StateDetails> AddState(StateDetails state)
        {
            _stateService.Add(state);
            return Ok();
        }

        //PUT /api/StateDetails/{id} → updates a state, validates ID match
        [HttpPut("{id}")]
        public IActionResult UpdateState(int id, StateDetails state)
        {
            if (id != state.State_Id)
            {
                return BadRequest("State ID mismatch");
            }
            var result = _stateService.Update(state);
            return Ok(result);
        }

        //DELETE /api/StateDetails/{id} → deletes a state, returns 204 No Content
        [HttpDelete("{id}")]
        public IActionResult DeleteState(int id)
        {
            var state = _stateService.DeleteState(id);
            if (state == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

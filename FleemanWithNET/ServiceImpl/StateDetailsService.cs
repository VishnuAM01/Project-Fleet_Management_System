using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Repositories;
using fleeman_with_dot_net.Services;

namespace fleeman_with_dot_net.Services
{
    public class StateDetailsService : IStateDetailsService
    {
    //    private readonly FleetDBContext context;

    //    public StateDetailsService(FleetDBContext context)
    //    {
    //        this.context = context;
    //    }

    //    public IEnumerable<StateDetails> GetAllStates()
    //    {
    //        return context.state_details.ToList();
    //    }

    //    public StateDetails GetStateById(int id)
    //    {
    //        return context.state_details.FirstOrDefault(s => s.State_Id == id);
    //    }

    //    public void AddState(StateDetails state)
    //    {
    //        context.state_details.Add(state);
    //        context.SaveChanges();
    //    }

    //    public void UpdateState(StateDetails state)
    //    {
    //        context.state_details.Update(state);
    //        context.SaveChanges();
    //    }

    //    public void DeleteState(int id)
    //    {
    //        var state = context.state_details.Find(id);
    //        if (state != null)
    //        {
    //            context.state_details.Remove(state);
    //            context.SaveChanges();
    //        }
    //    }
    //}

    private readonly FleetDBContext context;
    public StateDetailsService(FleetDBContext context)
    {
        this.context = context;
    }
    //Add a new State
    public StateDetails Add(StateDetails state)
    {
        context.state_details.Add(state);
        context.SaveChanges();
        return state;

    }
    //Delete a state using Id
    public StateDetails DeleteState(int Id)
    {
        StateDetails state = context.state_details.Find(Id);
        if (state != null)
        {
            context.state_details.Remove(state);
            context.SaveChangesAsync();
        }
        return state;
    }

    //Get all states
    public IEnumerable<StateDetails> GetAllStates()
    {
        List<StateDetails> states = context.state_details.ToList();
        return states;
    }

    //Get a single state by ID
    public StateDetails? GetState(int Id)
    {
        StateDetails state = context.state_details.FirstOrDefault(s => s.State_Id == Id);
        return state;
    }

    //Update state
    public StateDetails Update(StateDetails state)
    {
        StateDetails existingState = context.state_details.Find(state.State_Id);
        if (existingState != null)
            existingState.State_Name = state.State_Name;
        context.SaveChanges();
        return state;
    }
}
}

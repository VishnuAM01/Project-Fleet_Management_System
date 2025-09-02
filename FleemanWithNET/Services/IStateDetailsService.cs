using fleeman_with_dot_net.Models;
using System.Collections.Generic;

namespace fleeman_with_dot_net.Services
{
    public interface IStateDetailsService
    {
        //IEnumerable<StateDetails> GetAllStates();
        //StateDetails GetStateById(int id);
        //void AddState(StateDetails state);
        //void UpdateState(StateDetails state);
        //void DeleteState(int id);

        StateDetails GetState(int Id);
        IEnumerable<StateDetails> GetAllStates();
        StateDetails Add(StateDetails state);
        StateDetails Update(StateDetails state);
        StateDetails DeleteState(int Id);
    }
}

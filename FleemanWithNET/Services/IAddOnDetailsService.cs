using System.Collections.Generic;
using fleeman_with_dot_net.Models;

namespace fleeman_with_dot_net.Services
{
    public interface IAddOnDetailsService
    {
        IEnumerable<AddOnDetails> GetAllAddOns();
        AddOnDetails GetAddOnById(int id);
        AddOnDetails AddAddOn(AddOnDetails addOn);
        AddOnDetails UpdateAddOn(AddOnDetails addOn);
        bool DeleteAddOn(int id);
    }
}

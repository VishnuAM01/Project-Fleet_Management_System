using System.Collections.Generic;
using System.Linq;
using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Repositories;
using fleeman_with_dot_net.Services;

namespace fleeman_with_dot_net.ServiceImpl
{
    public class AddOnDetailsService : IAddOnDetailsService
    {
        private readonly FleetDBContext context;

        public AddOnDetailsService(FleetDBContext context)
        {
            this.context = context;
        }

        public AddOnDetails AddAddOn(AddOnDetails addOn)
        {
            context.add_on_details.Add(addOn);
            context.SaveChanges();
            return addOn;
        }

        public bool DeleteAddOn(int id)
        {
            var addOn = context.add_on_details.FirstOrDefault(a => a.addOnId == id);
            if (addOn == null) return false;

            context.add_on_details.Remove(addOn);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<AddOnDetails> GetAllAddOns()
        {
            return context.add_on_details.ToList();
        }

        public AddOnDetails GetAddOnById(int id)
        {
            return context.add_on_details.FirstOrDefault(a => a.addOnId == id);
        }

        public AddOnDetails UpdateAddOn(AddOnDetails addOn)
        {
            var existingAddOn = context.add_on_details.FirstOrDefault(a => a.addOnId == addOn.addOnId);
            if (existingAddOn == null) return null;

            existingAddOn.addOnName = addOn.addOnName;
            existingAddOn.addOnPrice = addOn.addOnPrice;

            context.SaveChanges();
            return existingAddOn;
        }
    }
}

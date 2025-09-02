using fleeman_with_dot_net.Models;
using System.Collections.Generic;

namespace fleeman_with_dot_net.Services
{
    public interface IAddOnBookService
    {
        IEnumerable<AddOnBook> GetAllAddOnBooks();
        IEnumerable<AddOnBook> GetAddOnBooksByBookingId(int bookingId);
        AddOnBook AddAddOnBook(AddOnBook addOnBook);
        bool DeleteAddOnBook(int id);
    }
}

using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Repositories;
using fleeman_with_dot_net.Services;
using System.Collections.Generic;
using System.Linq;

namespace fleeman_with_dot_net.ServiceImpl
{
    public class AddOnBookService : IAddOnBookService
    {
        private readonly FleetDBContext _context;

        public AddOnBookService(FleetDBContext context)
        {
            _context = context;
        }

        public IEnumerable<AddOnBook> GetAllAddOnBooks()
        {
            return _context.add_on_book.ToList();
        }

        public IEnumerable<AddOnBook> GetAddOnBooksByBookingId(int bookingId)
        {
            return _context.add_on_book.Where(a => a.BookingId == bookingId).ToList();
        }

        public AddOnBook AddAddOnBook(AddOnBook addOnBook)
        {
            _context.add_on_book.Add(addOnBook);
            _context.SaveChanges();
            return addOnBook;
        }

        public bool DeleteAddOnBook(int id)
        {
            var record = _context.add_on_book.FirstOrDefault(a => a.Id == id);
            if (record == null)
                return false;

            _context.add_on_book.Remove(record);
            _context.SaveChanges();
            return true;
        }
    }
}

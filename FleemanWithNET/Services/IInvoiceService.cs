using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Models;

namespace fleeman_with_dot_net.Services
{
    public interface IInvoiceService
    {
        InvoiceResponseDTO CreateInvoice(InvoiceCreationRequestDTO request);
        InvoiceResponseDTO GetInvoiceById(int invoiceId);
        IEnumerable<InvoiceResponseDTO> GetAllInvoices();
        IEnumerable<InvoiceResponseDTO> GetInvoicesByBookingId(int bookingId);
    }
}

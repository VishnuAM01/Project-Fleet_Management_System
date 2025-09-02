using fleeman_with_dot_net.DTO;

namespace fleeman_with_dot_net.Services
{
    public interface IEmailService
    {
        Task<bool> SendInvoiceEmailAsync(int invoiceId);
    }
}

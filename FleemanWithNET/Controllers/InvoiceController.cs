using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Services;
using Microsoft.AspNetCore.Mvc;

namespace fleeman_with_dot_net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IEmailService _emailService;

        public InvoiceController(IInvoiceService invoiceService, IEmailService emailService)
        {
            _invoiceService = invoiceService;
            _emailService = emailService;
        }

        [HttpPost("create")]
        public ActionResult<InvoiceResponseDTO> CreateInvoice([FromBody] InvoiceCreationRequestDTO request)
        {
            try
            {
                var invoice = _invoiceService.CreateInvoice(request);
                return CreatedAtAction(nameof(GetInvoiceById), new { invoiceId = invoice.InvoiceId }, invoice);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{invoiceId}")]
        public ActionResult<InvoiceResponseDTO> GetInvoiceById(int invoiceId)
        {
            try
            {
                var invoice = _invoiceService.GetInvoiceById(invoiceId);
                return Ok(invoice);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<InvoiceResponseDTO>> GetAllInvoices()
        {
            var invoices = _invoiceService.GetAllInvoices();
            return Ok(invoices);
        }

        [HttpGet("booking/{bookingId}")]
        public ActionResult<IEnumerable<InvoiceResponseDTO>> GetInvoicesByBookingId(int bookingId)
        {
            var invoices = _invoiceService.GetInvoicesByBookingId(bookingId);
            return Ok(invoices);
        }

        [HttpPost("send-invoice-email/{invoiceId}")]
        public async Task<ActionResult> SendInvoiceEmail(int invoiceId)
        {
            try
            {
                var success = await _emailService.SendInvoiceEmailAsync(invoiceId);
                
                if (success)
                {
                    return Ok(new { message = "Invoice email sent successfully", invoiceId = invoiceId });
                }
                else
                {
                    return BadRequest(new { message = "Failed to send invoice email", invoiceId = invoiceId });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, invoiceId = invoiceId });
            }
        }
    }
}

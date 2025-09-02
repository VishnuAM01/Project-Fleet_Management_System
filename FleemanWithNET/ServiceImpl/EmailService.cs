using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Services;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace fleeman_with_dot_net.ServiceImpl
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly IInvoiceService _invoiceService;
        private readonly string _phpApiBaseUrl;

        public EmailService(HttpClient httpClient, IInvoiceService invoiceService, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _invoiceService = invoiceService;
            _phpApiBaseUrl = configuration["PhpApiSettings:BaseUrl"] ?? "http://localhost:80";
        }

        public async Task<bool> SendInvoiceEmailAsync(int invoiceId)
        {
            try
            {
                Console.WriteLine($"[EmailService] Start - Sending invoice email for InvoiceId={invoiceId}");

                // Ensure timeout so we don't hang forever
                //_httpClient.Timeout = TimeSpan.FromSeconds(10);

                // 1. Get invoice data
                Console.WriteLine("[EmailService] Fetching invoice data...");
                var invoice = _invoiceService.GetInvoiceById(invoiceId);
                if (invoice == null)
                {
                    Console.WriteLine("[EmailService] Invoice not found.");
                    return false;
                }
                Console.WriteLine("[EmailService] Invoice fetched successfully.");

                // 2. Create payload
                var emailRequest = new
                {
                    invoiceId = invoice.InvoiceId,
                    invoiceDate = invoice.InvoiceDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    bookingId = invoice.BookingId,
                    actualReturnDate = invoice.ActualReturnDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    dropLocation = invoice.DropLocation,
                    carId = invoice.CarId,
                    carRentalAmount = invoice.CarRentalAmount,
                    addonRentalAmount = invoice.AddonRentalAmount,
                    totalAmount = invoice.TotalAmount,
                    fuelStatus = invoice.FuelStatus,
                    carName = invoice.CarName,
                    memberName = invoice.MemberName,
                    dropLocationName = invoice.DropLocationName
                };

                // 3. Serialize
                var jsonContent = JsonSerializer.Serialize(emailRequest);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                Console.WriteLine($"[EmailService] Payload prepared: {jsonContent}");

                // 4. Send to PHP microservice
                var apiUrl = $"{_phpApiBaseUrl}/api/send-invoice-email";
                Console.WriteLine($"[EmailService] Sending POST to {apiUrl}...");
                var response = await _httpClient.PostAsync(apiUrl, content);

                // 5. Handle response
                var rawResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[EmailService] Raw Response: {rawResponse}");

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<EmailResponse>(rawResponse);
                    Console.WriteLine($"[EmailService] Email send result: {result?.Success}");
                    return result?.Success ?? false;
                }
                else
                {
                    Console.WriteLine($"[EmailService] Request failed. Status: {response.StatusCode}");
                    return false;
                }
            }
            catch (TaskCanceledException tex) when (!tex.CancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("[EmailService] Request timed out.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmailService] Error: {ex}");
                return false;
            }
        }

        private class EmailResponse
        {
            [JsonPropertyName("success")]
            public bool Success { get; set; }

            [JsonPropertyName("message")]
            public string Message { get; set; } = string.Empty;
        }
    }
}

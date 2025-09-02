using fleeman_with_dot_net.Repositories;
using fleeman_with_dot_net.ServiceImpl;
using fleeman_with_dot_net.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace fleeman_with_dot_net
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database connection
            var connectionString = builder.Configuration.GetConnectionString("dbcs");
            builder.Services.AddDbContext<FleetDBContext>(options =>
                options.UseSqlServer(connectionString));

            // Dependency Injection
            builder.Services.AddScoped<IAirportDetailsService, AirportDetailsService>();
            builder.Services.AddScoped<IStateDetailsService, StateDetailsService>();
            builder.Services.AddScoped<ICityDetailsService, CityDetailsService>();
            builder.Services.AddScoped<ILocationDetailsService, LocationDetailsService>();
            builder.Services.AddScoped<ICarDetailsService, CarDetailsService>();        
            builder.Services.AddScoped<IVehicleDetailsService, VehicleDetailsService>();
            builder.Services.AddScoped<IAddOnDetailsService, AddOnDetailsService>();
            builder.Services.AddScoped<IMemberDetailsService, MemberDetailsService>();
            builder.Services.AddScoped<IBookingDetailsService, BookingDetailsService>();
                            builder.Services.AddScoped<IAddOnBookService, AddOnBookService>();
                builder.Services.AddScoped<IUser, UserService>();
                builder.Services.AddScoped<IInvoiceService, InvoiceService>();
                builder.Services.AddScoped<IBookingConfirmationDetailsService, BookingConfirmationDetailsService>();
                builder.Services.AddScoped<IEmailService, EmailService>();
                builder.Services.AddScoped<IBookingStatusService, BookingStatusService>();
                builder.Services.AddHttpClient();


            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("*") // your React app
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            // Get the secret key from configuration
            var jwtKey = builder.Configuration["Jwt:Key"];
            var key = Encoding.ASCII.GetBytes(jwtKey);

            // Add authentication with JWT Bearer token and logging
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true
                };

                // Add detailed debugging events
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Authentication failed:");
                        Console.WriteLine($"  Exception: {context.Exception}");
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("Token validated successfully.");
                        return Task.CompletedTask;
                    },
                    OnChallenge = context =>
                    {
                        Console.WriteLine("JWT Challenge triggered.");
                        Console.WriteLine($"  Error: {context.Error}");
                        Console.WriteLine($"  Description: {context.ErrorDescription}");
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        Console.WriteLine($"Received JWT token: {context.Token}");
                        return Task.CompletedTask;
                    }
                };
            });

            builder.Services.AddAuthorization();

            // Add controllers so attribute routing works
            builder.Services.AddControllers();


            var app = builder.Build();

            // Middleware
            app.UseHttpsRedirection();
            app.UseAuthorization();

            // Map controller routes
            app.MapControllers();

            //app.UseCors("AllowNextJs");

            app.UseCors();

            // Uncomment the next line to run test data insertion
            //await RunTestDataSql(app);

            app.Run();
        }

        // Add this method to run the SQL file
        public static async Task RunTestDataSql(WebApplication app)
        {
            try
            {
                var sqlContent = await File.ReadAllTextAsync("test_data.sql");
                using var scope = app.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<FleetDBContext>();
                await context.Database.ExecuteSqlRawAsync(sqlContent);
                Console.WriteLine("Test data inserted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error running SQL: {ex.Message}");
            }
        }
    }
}

using EmployeeManagement.Core;
using Microsoft.EntityFrameworkCore;
using Serilog;
namespace EmployeeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => // To preserve the default behaviour, capture the original delegate to call later.
            {
                var builtInFactory = options.InvalidModelStateResponseFactory;

                options.InvalidModelStateResponseFactory = context =>
                {
                    var logger = context.HttpContext.RequestServices
                                        .GetRequiredService<ILogger<Program>>();

                    // Perform logging here.
                    logger.LogError($"Input request is invalid. {string.Join("; ", context.ModelState.Values
                                            .SelectMany(x => x.Errors)
                                            .Select(x => x.ErrorMessage))}");
                    return builtInFactory(context);
                };
            });
            builder.Services.AddDbContext<EmployeeDbContext>(opt =>
                     opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            //Enable API swagger for documentation
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //enable httplogging to log request and response
            builder.Services.AddHttpLogging(o => { });
            //Register Global exception handler
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            //Configure serilog Logging 
            var logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(builder.Configuration)
                        .Enrich.FromLogContext()
                        .CreateLogger();
            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            //security configuration
            // Example: Configuring CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    //we can configure our origins
                    builder.WithOrigins("https://localhost")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
            builder.AddJwtAuthentication();
            builder.RegisterServices();

            var app = builder.Build();
            app.UseHttpLogging();
            app.UseExceptionHandler("/");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            // Registers the authentication middleware that uses the default authentication scheme set by AddAuthentication.
            app.UseAuthentication();
            // Registers the authorization middleware to enforce authorization policies on the app's routes.
            app.UseAuthorization();
            // Maps controller routes.
            app.MapControllers().RequireAuthorization();
            app.Run();
        }
    }
}
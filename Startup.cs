using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.IdentityModel.Tokens;
using Practice.Controllers;
using Practice.Models;

using System.Security.Cryptography;
using System.Text;

namespace Practice
{ public class Startup {
  private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {


            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOriginWithCredentials",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole(); 
            });



            services.AddSingleton<OTP>();
            services.AddTransient<EmailSenderController>();
          
            services.AddControllers();
            var key = Encoding.ASCII.GetBytes(GenerateSecretKey(32));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

          
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAnyOriginWithCredentials"); // Apply the CORS policy
           
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            // Rest of your configuration
        }


        public static string GenerateSecretKey(int length)
        {
            byte[] randomBytes = new byte[length];
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }
    }
}

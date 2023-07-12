
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;
using System;
using System.Security.Cryptography;


namespace Practice
{
    public class Startup



    {

        

        public static string GenerateSecretKey(int length)
        {
            byte[] randomBytes = new byte[length];
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // ...

            // Configure JWT authentication
            var key = Encoding.ASCII.GetBytes(GenerateSecretKey(32)); // Replace with your secret key
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

            // ...
        }
        // Generate a 256-bit (32-byte) secret key

      



    }
}







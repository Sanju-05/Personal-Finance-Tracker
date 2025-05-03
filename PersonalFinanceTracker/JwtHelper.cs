using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PersonalFinanceTracker.Models.Entities;

namespace PersonalFinanceTracker.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtHelper(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }

        public string GenerateJwtToken(User user)
        {
            //var jwtSettings = _config.GetSection("JwtSettings");
            var jwtSettings = _config.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(secretKey);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                //expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpiryMinutes"])),
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["AccessTokenExpirationMinutes"])), // Expiry time
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // New method to get UserId from JWT token stored in cookie
        public string GetUserIdFromCookie()
        {
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["jwt"];

            if (string.IsNullOrEmpty(token))
            {
                return null;  // Or handle it as unauthorized
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

            if (jwtToken == null)
            {
                return null;  // Handle invalid token or return a default value
            }

            var userIdClaim = jwtToken?.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            return userIdClaim;
        }
        public static bool IsUserLoggedIn(HttpContext context)
        {
            var token = context.Request.Cookies["jwt"];
            if (string.IsNullOrEmpty(token))
                return false;

            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                return jwtToken != null && jwtToken.Claims.Any(c => c.Type == "UserId");
            }
            catch
            {
                return false;
            }
        }

    }
}

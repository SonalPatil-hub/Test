using EmployeeManagement.Interfaces;
using EmployeeManagement.Models.Common;
using EmployeeManagement.Models.Request.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Services
{
    public class SecurityService : ISecurityService
    {
        // Private field to hold the configuration settings injected through the constructor.
        private readonly IConfiguration _configuration;
        public readonly ISecurityRepository _securityRepository;
        public SecurityService(IConfiguration configuration,ISecurityRepository securityRepository)
        {
                _securityRepository = securityRepository;
            _configuration = configuration;
        }
        public async Task<BaseResponse<string>> Login(LoginRequest request)
        {
            var result = new BaseResponse<string>();

            var user = await _securityRepository.GetUser(request);

            // Checks if the user object is null, which means no matching user was found.
            if (user.Result == null)
            {
                result.Errors.Add(new Error() { ErrorMessage = "No matching user found" });
                // Returns a 401 Unauthorized response with a custom message.
                return result;
            }
            // Calls a method to generate a JWT token for the authenticated user.
            var token = IssueToken(user.Result);
            result.Result = token;
            return result;
            // Returns a 200 OK response, encapsulating the JWT token in an anonymous object.
        }

        private string IssueToken(User user)
        {
            // Creates a new symmetric security key from the JWT key specified in the app configuration.
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            // Sets up the signing credentials using the above security key and specifying the HMAC SHA256 algorithm.
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Defines a set of claims to be included in the token.
            var claims = new List<Claim>
            {
                // Custom claim using the user's ID.
                new Claim("Myapp_User_Id", user.Id.ToString()),
                // Standard claim for user identifier, using username.
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                // Standard claim for user's email.
                new Claim(ClaimTypes.Email, user.Email),
                // Standard JWT claim for subject, using user ID.
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };
            // Adds a role claim for each role associated with the user.
            user.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
            // Creates a new JWT token with specified parameters including issuer, audience, claims, expiration time, and signing credentials.
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token expiration set to 1 hour from the current time.
                signingCredentials: credentials);
            // Serializes the JWT token to a string and returns it.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

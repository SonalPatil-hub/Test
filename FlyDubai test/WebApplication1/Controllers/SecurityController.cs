
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Models.Request.Security;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models.Common;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        
        private readonly ISecurityService _securityService;
        public SecurityController(ISecurityService securityService)
        {
            
            _securityService = securityService;
        }
        /// <summary>
        /// Action method responding to POST requests on "api/Users/Login".Use this to get the token, which can be used for authorization of other APIs
        /// </summary>
        /// <param name="request">Login request</param>
        /// <returns>Return authorization bearer token</returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BaseResponse<string>>> Login([FromBody] LoginRequest request)
        {
            // Checks if the provided model (request) is valid based on data annotations in the Login model.
            if (ModelState.IsValid)
            {
                var response = await _securityService.Login(request);
                return Ok(response);               
               
            }
            // If the model state is not valid, returns a 400 Bad Request.
            return BadRequest("Invalid Request Body");
        }
      
    }
}

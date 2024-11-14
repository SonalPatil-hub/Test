using EmployeeManagement.Interfaces;
using EmployeeManagement.Models.Common;
using EmployeeManagement.Models.Request.Employee;
using EmployeeManagement.Models.Response.Employee;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger,IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }
        /// <summary>
        /// Get employee list with given serach criteria
        /// </summary>
        /// <param name="employeeSearchRequest"></param>
        /// <returns>Employee Response list</returns>

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaginationResponse<EmployeeSearchResponse>>> SearchEmployee(EmployeeSearchRequest employeeSearchRequest)
        {
            //log 
            _logger.LogInformation($"SearchEmployee invoked by user : {employeeSearchRequest.UserId}");
            {
                var searchResult = await _employeeService.SearchEmployee(employeeSearchRequest);
                if (searchResult.Error != null)
                {
                    switch (searchResult.Error.StatusCode)
                    {
                        case System.Net.HttpStatusCode.NotFound:
                            return NotFound();
                    }
                }

                return Ok(searchResult);
            }
        }
    }
}


using EmployeeManagement.Core;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models.Common;
using EmployeeManagement.Models.Request.Employee;
using EmployeeManagement.Models.Response.Employee;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ILogger<EmployeeRepository> _logger;
        private readonly EmployeeDbContext _employeeDbContext;
        public EmployeeRepository(ILogger<EmployeeRepository>logger,EmployeeDbContext employeeDbContext)
        {
            _logger = logger;
            _employeeDbContext = employeeDbContext;
        }
        /// <summary>
        /// Get the data from employee table with given serach criteria
        /// </summary>
        /// <param name="employeeSearchRequest"></param>
        /// <returns></returns>
        public async Task<PaginationResponse<EmployeeSearchResponse>> GetEmployeeData(EmployeeSearchRequest employeeSearchRequest)
        {
            var searchResult = new PaginationResponse<EmployeeSearchResponse>();
            var query = from e in _employeeDbContext.EmployeeDataSet
                             join d in _employeeDbContext.Department
                             on e.EmpDeptId equals d.DeptId into empD
                             from ed in  empD.DefaultIfEmpty()
                             select new { e, ed };
            //Apply filter
            if(!string.IsNullOrWhiteSpace(employeeSearchRequest.FirstName))
            {
                query = query.Where(x => x.e.FirstName.Equals(employeeSearchRequest.FirstName));
            }
            if (!string.IsNullOrWhiteSpace(employeeSearchRequest.LastName))
            {
                query = query.Where(x => x.e.LastName.Equals(employeeSearchRequest.LastName));
            }
            if (!string.IsNullOrWhiteSpace(employeeSearchRequest.EmpDeptName))
            {
                query = query.Where(x => x.ed.DeptName.Equals(employeeSearchRequest.EmpDeptName));
            }
            if (!string.IsNullOrWhiteSpace(employeeSearchRequest.EmployeeId))
            {
                query = query.Where(x => x.e.EmployeeId.Equals(employeeSearchRequest.EmployeeId));
            }
            if (employeeSearchRequest.StartDate != DateTime.MinValue && employeeSearchRequest.EndDate != DateTime.MinValue)
            {
                query = query.Where(x => x.e.Doj <= employeeSearchRequest.EndDate && x.e.Doj >= employeeSearchRequest.StartDate);
            }
            //Apply sorting
            if(!string.IsNullOrWhiteSpace(employeeSearchRequest.OrderByName) && !string.IsNullOrWhiteSpace(employeeSearchRequest.OrderByType))
            {
                switch (employeeSearchRequest.OrderByName.ToLower())
                {
                    case "firstname":
                        if (employeeSearchRequest.OrderByName.Equals("ASC"))
                            query = query.OrderBy(x => x.e.FirstName);
                        else
                            query = query.OrderByDescending(x => x.e.FirstName);
                        break;
                    case "lastname":
                        if (employeeSearchRequest.OrderByName.Equals("ASC"))
                            query = query.OrderBy(x => x.e.LastName);
                        else
                            query = query.OrderByDescending(x => x.e.LastName);
                        break;
                    case "doj":
                        if (employeeSearchRequest.OrderByName.Equals("ASC"))
                            query = query.OrderBy(x => x.e.Doj);
                        else
                            query = query.OrderByDescending(x => x.e.Doj);
                        break;
                    case "employeeid":
                        if (employeeSearchRequest.OrderByName.Equals("ASC"))
                            query = query.OrderBy(x => x.e.EmployeeId);
                        else
                            query = query.OrderByDescending(x => x.e.EmployeeId);
                        break;

                }
                
            }
            else
            {
                query = query.OrderBy(x => x.e.CreatedDate);
            }

            var totalCount = await query.CountAsync();
            if (totalCount > 0)
            {
                //Apply pagination
                query = query.Skip(employeeSearchRequest.Skip).Take(employeeSearchRequest.Take);
                searchResult.Items = await (from r in query
                                     select new EmployeeSearchResponse
                                     {
                                         EmployeeId = r.e.EmployeeId,
                                         EmployeeName = r.e.FirstName + " " + r.e.LastName,
                                         Email = r.e.Email,
                                         EmpDeptName = r.ed.DeptName,
                                         PhoneNumber = r.e.PhoneNumber,
                                         Address = r.e.Address,
                                         ConfirmationDate = r.e.ConfirmationDate,
                                         Doj = r.e.Doj
                                     }).AsNoTracking().ToListAsync();
                searchResult.TotalCount = totalCount;
                searchResult.FilteredCount = searchResult.Items.Count();
                return searchResult;
            }
            _logger.LogError($"Records not found for given search criteria. {employeeSearchRequest}");
            searchResult.Error =new Error {StatusCode = System.Net.HttpStatusCode.NotFound, ErrorMessage = "Records not found for given search criteria." };
            return searchResult;
        }
    }
}

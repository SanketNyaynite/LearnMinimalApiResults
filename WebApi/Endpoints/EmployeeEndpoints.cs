using WebApi.Models;
using WebApi.Results;

namespace WebApi.Endpoints
{
    public static class EmployeeEndpoints
    {
        public static void MapEmployeeEndpoints(this WebApplication app)
        {
            app.MapGet("/", HtmlResult () =>
            {
                string html = "<h2>Welcome to our API<h2> Our API is used to learn ASP.NET Core 9.0 features and improvements.";

                return new HtmlResult(html);
            });

            app.MapGet("/employees", () =>
            {
                var employees = EmployeesRepository.GetEmployees();

                return TypedResults.Ok(employees);
            });

            app.MapPost("/employees", (Employee employee) =>
            {
                if(employee is null || employee.Id < 0)
                {
                    return Microsoft.AspNetCore.Http.Results.ValidationProblem(new Dictionary<string, string[]>
                //var employee = EmployeesRepository.GetEmployeeById(Id);
                //return employee is not null
                //? TypedResults.Ok(employee)
                //: Microsoft.AspNetCore.Http.Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    { "id", new[] { "Employee is not provided or is not valid." } }
                },
                statusCode: 400);
                }


                EmployeesRepository.AddEmployee(employee);
            return TypedResults.Created($"/employees/{employee.Id}", employee);

        }).WithParameterValidation();
        }
    }
}

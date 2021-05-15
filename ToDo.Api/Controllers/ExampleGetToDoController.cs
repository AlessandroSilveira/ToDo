using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Refit;
using ToDo.Domain.Services;

namespace ToDo.Api.Controllers
{
    [ApiController]
    [Route("api/todos")]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class ExampleGetToDoController : ControllerBase
    {
        [Route("GetToDos")]
        [HttpGet]
        public async Task<IActionResult> GetToDos()
        {
            var httpClientHandler = new HttpClientHandler();
            var toDoBaseUrl = "http://localhost:5000";

            httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, certificate, chain, sslPolicyErrors) => true;

            var refitApiClient = RestService.For<IExampleGetToDoService>(
                new HttpClient(httpClientHandler)
                {
                    BaseAddress = new Uri(toDoBaseUrl)
                }
            );
            var response = await refitApiClient.GetAllToDo();
            return Ok(response);
        }
    }
}
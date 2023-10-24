using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Category.Request;
using POS.Application.Dtos.Client.Request;
using POS.Application.Interfaces;
using POS.Application.Services;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientApplication _clientApplication;

        public ClientController(IClientApplication clientApplication)
        {
            _clientApplication = clientApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListClients()
        {
            var response = await _clientApplication.ListClients();
            return Ok(response);
        }

        [HttpGet("{clientId:int}")]
        public async Task<IActionResult> ClientById(int clientId)
        {
            var response = await _clientApplication.ClientById(clientId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterClient([FromBody] ClientRequestDto requestDto)
        {
            var response = await _clientApplication.RegisterClient(requestDto);
            return Ok(response);
        }

        [HttpPut("{clientId:int}")]
        public async Task<IActionResult> EditClient(int clientId, [FromBody] ClientRequestDto requestDto)
        {
            var response = await _clientApplication.EditClient(clientId, requestDto);
            return Ok(response);
        }

        [HttpDelete("{clientId:int}")]
        public async Task<IActionResult> RemoveClient(int clientId)
        {
            var response = await _clientApplication.RemoveClient(clientId);
            return Ok(response);
        }
    }
}

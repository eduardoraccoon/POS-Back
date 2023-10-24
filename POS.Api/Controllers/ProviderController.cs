using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Client.Request;
using POS.Application.Dtos.Provider.Request;
using POS.Application.Interfaces;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderApplication _providerApplication;

        public ProviderController(IProviderApplication providerApplication)
        {
            _providerApplication = providerApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListProviders()
        {
            var response = await _providerApplication.ListProviders();
            return Ok(response);
        }

        [HttpGet("{providerId:int}")]
        public async Task<IActionResult> ProviderById(int providerId)
        {
            var response = await _providerApplication.ProviderById(providerId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterProvider([FromBody] ProviderRequestDto requestDto)
        {
            var response = await _providerApplication.RegisterProvider(requestDto);
            return Ok(response);
        }

        [HttpPut("{providerId:int}")]
        public async Task<IActionResult> EditClient(int providerId, [FromBody] ProviderRequestDto requestDto)
        {
            var response = await _providerApplication.EditProvider(providerId, requestDto);
            return Ok(response);
        }

        [HttpDelete("{providerId:int}")]
        public async Task<IActionResult> RemoveClient(int providerId)
        {
            var response = await _providerApplication.RemoveProvider(providerId);
            return Ok(response);
        }
    }
}

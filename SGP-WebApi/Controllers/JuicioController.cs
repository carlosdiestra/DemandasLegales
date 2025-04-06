using MediatR;
using Microsoft.AspNetCore.Mvc;
using SGP_Application.Contracts;

namespace SGP_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JuicioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JuicioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("resolver")]
        public async Task<IActionResult> Resolver([FromBody] ResolverJuicioRequest request)
        {
            var resultado = await _mediator.Send(request);
            return Ok(resultado);
        }
    }
}

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

        [HttpPost("resolve")]
        public async Task<IActionResult> Resolve([FromBody] ResolveJudgmentRequest request)
        {
            var resultado = await _mediator.Send(new SGP_Application.Contracts.Handlers.ResolveJudgmentFirstHandler.Command(request.ParteDemandante, request.ParteDemandado));
            return Ok(resultado);
        }

        [HttpPost("resolveHistory")]
        public async Task<IActionResult> ResolveHistory([FromBody] ResolveJudgmentRequest request)
        {
            var resultado = await _mediator.Send(new SGP_Application.Contracts.Handlers.
                                                     ResolveJudgmentSecondHandler.Command(request.ParteDemandante, request.ParteDemandado));
            return Ok(resultado);
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistorial()
        {
            var historial = await _mediator.Send(new SGP_Application.FindContracts.Query());
            return Ok(historial);
        }
    }
}

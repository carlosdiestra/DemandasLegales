using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SGP_Application.Contracts;
using SGP_Application.Contracts.Handlers.ResolveJudgmentFirstHandler;
using SGP_Application.Contracts.Handlers.ResolveJudgmentSecondHandler;
using SGP_Application.FindContracts;

namespace SGP_WebApi_Tests
{
    public class JuicioControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public JuicioControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "Parte 1 - Resolver juicio sin historial")]
        public async Task ResolverJuicio_DeberiaRetornarResultadoValido()
        {
            var request = new ResolveJudgmentRequest
            {
                ParteDemandante = "KN",
                ParteDemandado = "VV"
            };

            var response = await _client.PostAsJsonAsync("api/juicio/resolve", request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<ResolveJudgmentResponse>();
            result.Should().NotBeNull();
            result!.Ganador.Should().Be("Demandante");
        }

        [Fact(DisplayName = "Parte 2 - Resolver juicio y guardar historial")]
        public async Task ResolverJuicioConHistorial_DeberiaRetornarResultadoYGuardar()
        {
            var request = new ResolveJudgmentRequest
            {
                ParteDemandante = "K",
                ParteDemandado = "NV"
            };

            var response = await _client.PostAsJsonAsync("api/juicio/resolveHistory", request);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<ResolveJudgmentResponse>();
            result.Should().NotBeNull();
            result!.Ganador.Should().Be("Demandante");
        }

        [Fact(DisplayName = "Parte 3 - Consultar historial de juicios")]
        public async Task ConsultarHistorial_DeberiaRetornarListaHistorial()
        {
            var response = await _client.GetAsync("api/juicio/history");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var historial = await response.Content.ReadFromJsonAsync<List<HistoryJudgmentResponse>>();
            historial.Should().NotBeNull();
            historial!.Count.Should().BeGreaterThan(0);
        }
    }
}
// See https://aka.ms/new-console-template for more information
using SGP_Application.Contracts;
using System.Net.Http.Json;


using var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:7054/"); // Ajustar según host local/API

Console.WriteLine("==========================");
Console.WriteLine(" Bienvenido a Guerras de Poder - Cliente Externo");
Console.WriteLine("==========================\n");

Console.Write("Parte Demandante (ej: KN): ");
var demandante = Console.ReadLine();

Console.Write("Parte Demandado (ej: NNV): ");
var demandado = Console.ReadLine();

var request = new ResolverJuicioRequest
{
    ParteDemandante = demandante!,
    ParteDemandado = demandado!
};

var response = await httpClient.PostAsJsonAsync("api/juicio/resolver", request);

if (!response.IsSuccessStatusCode)
{
    Console.WriteLine("\nError al contactar el servicio.");
    return;
}

var resultado = await response.Content.ReadFromJsonAsync<ResolverJuicioResponse>();

Console.WriteLine("\n==========================");
Console.WriteLine($"Ganador: {resultado!.Ganador}");
Console.WriteLine($"Puntos Demandante: {resultado.PuntosDemandante}");
Console.WriteLine($"Puntos Demandado: {resultado.PuntosDemandado}");
Console.WriteLine("==========================");

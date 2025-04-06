// See https://aka.ms/new-console-template for more information
using SGP_Application.Contracts;
using SGP_Application.FindContracts;
using SGP_Console.Enum;
using SGP_Console.Routes;
using System.Net.Http.Json;

using var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri(ApiRoutes.BaseUrl); // Cambia según puerto de tu API

while (true)
{
    Console.Clear();
    Console.WriteLine("===================================");
    Console.WriteLine("   Bienvenido a Guerras de Poder");
    Console.WriteLine("===================================");
    Console.WriteLine("1. Parte 1: Resolver juicio");
    Console.WriteLine("2. Parte 2: Resolver juicio + guardar historial");
    Console.WriteLine("3. Parte 3: Consultar historial de juicios");
    Console.WriteLine("0. Salir");
    Console.Write("Seleccione una opción: ");

    var opcion = Console.ReadLine();
    MenuOption opcionEnum = Enum.TryParse(opcion, out MenuOption parsed) ? parsed : MenuOption.Salir;

    if (opcionEnum == MenuOption.Salir)
        break;

    if (opcionEnum == MenuOption.Parte1 || opcionEnum == MenuOption.Parte2)
    {
        string demandante;
        do
        {
            Console.Write("Parte Demandante (solo letras K, N, V): ");
            demandante = Console.ReadLine()!;
            if (!EsEntradaValida(demandante))
                Console.WriteLine("Entrada no válida. Solo se permiten K, N y V.");
        } while (!EsEntradaValida(demandante));

        string demandado;
        do
        {
            Console.Write("Parte Demandado (solo letras K, N, V): ");
            demandado = Console.ReadLine()!;
            if (!EsEntradaValida(demandado))
                Console.WriteLine("Entrada no válida. Solo se permiten K, N y V.");
        } while (!EsEntradaValida(demandado));

        var request = new ResolveJudgmentRequest
        {
            ParteDemandante = demandante,
            ParteDemandado = demandado
        };

        string endpoint = opcionEnum == MenuOption.Parte1 ? ApiRoutes.Resolve : ApiRoutes.ResolveHistory;
        var response = await httpClient.PostAsJsonAsync(endpoint, request);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(" Error al contactar el servicio.");
        }
        else
        {
            var resultado = await response.Content.ReadFromJsonAsync<ResolveJudgmentResponse>();

            Console.WriteLine("========= RESULTADO =========");
            Console.WriteLine($"Ganador:            {resultado!.Ganador}");
            Console.WriteLine($"Puntos Demandante:  {resultado.PuntosDemandante}");
            Console.WriteLine($"Puntos Demandado:   {resultado.PuntosDemandado}");
            Console.WriteLine("=============================");
        }
    }
    else if (opcionEnum == MenuOption.Parte3)
    {
        var response = await httpClient.GetAsync(ApiRoutes.History);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine("No se pudo obtener el historial.");
        }
        else
        {
            var historial = await response.Content.ReadFromJsonAsync<List<HistoryJudgmentResponse>>();

            if (historial is not null && historial.Any())
            {
                Console.WriteLine("========== HISTORIAL ==========");
                foreach (var h in historial)
                {
                    Console.WriteLine($"Fecha: {h.Fecha}");
                    Console.WriteLine($"Demandante: {h.ParteDemandante}");
                    Console.WriteLine($"Demandado:  {h.ParteDemandado}");
                    Console.WriteLine($"Ganador:    {h.Ganador}");
                    Console.WriteLine("----------------------------------");
                }
            }
            else
            {
                Console.WriteLine("No hay registros de historial.");
            }
        }
    }
    else
    {
        Console.WriteLine("Opción inválida.");
    }

    Console.WriteLine("Presione una tecla para continuar...");
    Console.ReadKey();

    bool EsEntradaValida(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        var validChars = new[] { 'K', 'N', 'V' };
        return input.ToUpper().All(c => validChars.Contains(c));
    }
}

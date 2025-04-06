using MediatR;
using SGP_Domain.Entities;
using SGP_Domain.Enums;
using SGP_Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP_Application.Contracts
{
    
    public class ResolverJuicioHandler : IRequestHandler<ResolverJuicioRequest, ResolverJuicioResponse>
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ResolverJuicioHandler(AppDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        //Primera Parte
        //public Task<ResolverJuicioResponse> Handle(ResolverJuicioRequest request, CancellationToken cancellationToken)
        //{
        //    int Puntos(string parte)
        //    {
        //        if (parte.Contains('K')) return 5; // Rey anula validadores
        //        return parte.Sum(c => (int)Enum.Parse<Rol>(c.ToString()));
        //    }

        //    var puntosDemandante = Puntos(request.ParteDemandante);
        //    var puntosDemandado = Puntos(request.ParteDemandado);

        //    return Task.FromResult(new ResolverJuicioResponse
        //    {
        //        Ganador = puntosDemandante > puntosDemandado ? "Demandante" : "Demandado",
        //        PuntosDemandante = puntosDemandante,
        //        PuntosDemandado = puntosDemandado
        //    });
        //}

        //Segunda parte
        public async Task<ResolverJuicioResponse> Handle(ResolverJuicioRequest request, CancellationToken cancellationToken)
        {
            int Puntos(string parte)
            {
                if (parte.Contains('K')) return 5;
                return parte.Sum(c => (int)Enum.Parse<Rol>(c.ToString()));
            }

            var puntosDemandante = Puntos(request.ParteDemandante);
            var puntosDemandado = Puntos(request.ParteDemandado);
            var ganador = puntosDemandante > puntosDemandado ? "Demandante" : "Demandado";

            // Guardar historial
            _context.Historiales.Add(new HistorialJuicio
            {
                Id = Guid.NewGuid(),
                ParteDemandante = request.ParteDemandante,
                ParteDemandado = request.ParteDemandado,
                Ganador = ganador,
                Fecha = DateTime.UtcNow
            });

            await _unitOfWork.SaveChangesAsync();

            return new ResolverJuicioResponse
            {
                Ganador = ganador,
                PuntosDemandante = puntosDemandante,
                PuntosDemandado = puntosDemandado
            };
        }
    }
}

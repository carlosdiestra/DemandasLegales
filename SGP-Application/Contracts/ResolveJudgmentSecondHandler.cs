using AutoMapper;
using MediatR;
using SGP_Domain.Entities;
using SGP_Domain.Rules;
using SGP_Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP_Application.Contracts.Handlers.ResolveJudgmentSecondHandler
{
    public record Command(string ParteDemandante, string ParteDemandado) : IRequest<ResolveJudgmentResponse>;
    public class Handler : IRequestHandler<Command, ResolveJudgmentResponse>
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFirmaRules _rules;
        private readonly IMapper _mapper;

        public Handler(AppDbContext context,
                                     IUnitOfWork unitOfWork,
                                     IFirmaRules firmaRules,
                                     IMapper mapper)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _rules = firmaRules;
            _mapper = mapper;
        }


        //Segunda parte
        public async Task<ResolveJudgmentResponse> Handle(Command request, CancellationToken cancellationToken)
        {
            var puntosDemandante = _rules.CalcularPuntos(request.ParteDemandante);
            var puntosDemandado = _rules.CalcularPuntos(request.ParteDemandado);
            var ganador = puntosDemandante > puntosDemandado ? "Demandante" : "Demandado";

            // Guardar historial
            _context.Historiales.Add(new HistorialJuicio
            {
                ParteDemandante = request.ParteDemandante,
                ParteDemandado = request.ParteDemandado,
                Ganador = ganador,
                Fecha = DateTime.UtcNow
            });

            await _unitOfWork.SaveChangesAsync();

            var resultado = new ResolveJudgment
            {
                Ganador = ganador,
                PuntosDemandante = puntosDemandante,
                PuntosDemandado = puntosDemandado
            };

            var response = _mapper.Map<ResolveJudgmentResponse>(resultado);

            return await Task.FromResult(response);
        }
    }
}

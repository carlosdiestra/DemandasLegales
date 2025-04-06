using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SGP_Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP_Application.FindContracts
{
    public record Query : IRequest<List<HistoryJudgmentResponse>>;

    public class Handler : IRequestHandler<Query, List<HistoryJudgmentResponse>>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public Handler(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<HistoryJudgmentResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var historial = await _context.Historiales
                .OrderByDescending(h => h.Fecha)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<HistoryJudgmentResponse>>(historial);
        }
    }
}

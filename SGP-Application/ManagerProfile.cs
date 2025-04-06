using AutoMapper;
using SGP_Application.Contracts;
using SGP_Application.FindContracts;
using SGP_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGP_Application
{
    public class ManagerProfile : Profile
    {
        public ManagerProfile()
        {
            CreateMap<ResolveJudgment, ResolveJudgmentResponse>();
            CreateMap<HistorialJuicio, HistoryJudgmentResponse>();
        }
    }
}

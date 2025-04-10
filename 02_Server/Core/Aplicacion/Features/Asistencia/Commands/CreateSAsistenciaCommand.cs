using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using AutoMapper;
using Dominio.Entities.Asistencia;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Features.Asistencia.Commands
{
    public class CreateSAsistenciaCommand : IRequest<Response<int>>
    {
        public int? Uid { get; set; }
        public long? UserId { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? Status { get; set; }
        public int? Punch { get; set; }
        public string IpBiometrico { get; set; }
        public DateTime? HoraEntrada { get; set; }
        public DateTime? HoraSalida { get; set; }
        public DateTime? Fecha { get; set; }

        public class Handler : IRequestHandler<CreateSAsistenciaCommand, Response<int>>
        {
            private readonly IRepositoryAsync<SAsistencia> _repo;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<SAsistencia> repo, IMapper mapper)
            {
                _repo = repo;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreateSAsistenciaCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<SAsistencia>(request);
                var newItem = await _repo.AddAsync(entity);
                return new Response<int>(newItem.IdbioAsistencia);
            }
        }
    }

}

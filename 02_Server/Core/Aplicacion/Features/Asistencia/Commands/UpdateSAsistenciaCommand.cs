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
    public class UpdateSAsistenciaCommand : IRequest<Response<int>>
    {
        public int IdbioAsistencia { get; set; }
        public int? Uid { get; set; }
        public long? UserId { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? Status { get; set; }
        public int? Punch { get; set; }
        public string IpBiometrico { get; set; }
        public DateTime? HoraEntrada { get; set; }
        public DateTime? HoraSalida { get; set; }
        public DateTime? Fecha { get; set; }

        public class Handler : IRequestHandler<UpdateSAsistenciaCommand, Response<int>>
        {
            private readonly IRepositoryAsync<SAsistencia> _repo;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<SAsistencia> repo, IMapper mapper)
            {
                _repo = repo;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(UpdateSAsistenciaCommand request, CancellationToken cancellationToken)
            {
                var entity = await _repo.GetByIdAsync(request.IdbioAsistencia);
                if (entity == null)
                    throw new KeyNotFoundException("Asistencia no encontrada");

                _mapper.Map(request, entity);
                await _repo.UpdateAsync(entity);
                return new Response<int>(entity.IdbioAsistencia);
            }
        }
    }

}

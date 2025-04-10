using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
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
    public class DeleteSAsistenciaCommand : IRequest<Response<int>>
    {
        public int IdbioAsistencia { get; set; }

        public class Handler : IRequestHandler<DeleteSAsistenciaCommand, Response<int>>
        {
            private readonly IRepositoryAsync<SAsistencia> _repo;

            public Handler(IRepositoryAsync<SAsistencia> repo)
            {
                _repo = repo;
            }

            public async Task<Response<int>> Handle(DeleteSAsistenciaCommand request, CancellationToken cancellationToken)
            {
                var entity = await _repo.GetByIdAsync(request.IdbioAsistencia);
                if (entity == null)
                    throw new KeyNotFoundException("Asistencia no encontrada");

                await _repo.DeleteAsync(entity);
                return new Response<int>(entity.IdbioAsistencia);
            }
        }
    }

}

using Aplicacion.DTOs.Asistencia;
using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using Ardalis.Specification;
using AutoMapper;
using Dominio.Entities.Asistencia;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Features.Asistencia.Queries
{

    public class GetAllSAsistenciaQuery : IRequest<Response<List<SAsistenciaDto>>>
    {
        public class GetAllSAsistenciaQueryHandler : IRequestHandler<GetAllSAsistenciaQuery, Response<List<SAsistenciaDto>>>
        {
            private readonly IRepositoryAsync<SAsistencia> _repositoryAsync;
            private readonly IMapper _mapper;
            public GetAllSAsistenciaQueryHandler(IRepositoryAsync<SAsistencia> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<SAsistenciaDto>>> Handle(GetAllSAsistenciaQuery request, CancellationToken cancellationToken)
            {
                var _SAsistencia = await _repositoryAsync.ListAsync();
                var _SAsistenciaDto = _mapper.Map<List<SAsistenciaDto>>(_SAsistencia);
                return new Response<List<SAsistenciaDto>>(_SAsistenciaDto);
            }
        }

    }

    public class SAsistenciaSpecification : Specification<SAsistenciaDto>
    {
        public SAsistenciaSpecification()
        {
          
        }
    }

}

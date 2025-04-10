using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using AutoMapper;
using Dominio.Entities.Persona;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Features.Persona.Commands
{
    public class CreateRrhPersonaCommand : IRequest<Response<int>>
    {
        public string NombreApellido { get; set; }
        public string Ci { get; set; }
        public string Exp { get; set; }
        public string Celular { get; set; }

        public class Handler : IRequestHandler<CreateRrhPersonaCommand, Response<int>>
        {
            private readonly IRepositoryAsync<RrhPersona> _repo;
            private readonly IMapper _mapper;

            public Handler(IRepositoryAsync<RrhPersona> repo, IMapper mapper)
            {
                _repo = repo;
                _mapper = mapper;
            }

            public async Task<Response<int>> Handle(CreateRrhPersonaCommand request, CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<RrhPersona>(request);
                var created = await _repo.AddAsync(entity);
                return new Response<int>(created.IdrrhPersona);
            }
        }
    }

}

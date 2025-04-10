using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
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
    public class UpdateRrhPersonaCommand : IRequest<Response<int>>
    {
        public int IdrrhPersona { get; set; }
        public string NombreApellido { get; set; }
        public string Ci { get; set; }
        public string Exp { get; set; }
        public string Celular { get; set; }

        public class Handler : IRequestHandler<UpdateRrhPersonaCommand, Response<int>>
        {
            private readonly IRepositoryAsync<RrhPersona> _repo;

            public Handler(IRepositoryAsync<RrhPersona> repo)
            {
                _repo = repo;
            }

            public async Task<Response<int>> Handle(UpdateRrhPersonaCommand request, CancellationToken cancellationToken)
            {
                var entity = await _repo.GetByIdAsync(request.IdrrhPersona);
                if (entity == null)
                    throw new KeyNotFoundException("Persona no encontrada");

                entity.NombreApellido = request.NombreApellido;
                entity.Ci = request.Ci;
                entity.Exp = request.Exp;
                entity.Celular = request.Celular;

                await _repo.UpdateAsync(entity);
                return new Response<int>(entity.IdrrhPersona);
            }
        }
    }

}

﻿using Aplicacion.Interfaces;
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
    public class DeleteRrhPersonaCommand : IRequest<Response<int>>
    {
        public int IdrrhPersona { get; set; }

        public class Handler : IRequestHandler<DeleteRrhPersonaCommand, Response<int>>
        {
            private readonly IRepositoryAsync<RrhPersona> _repo;

            public Handler(IRepositoryAsync<RrhPersona> repo)
            {
                _repo = repo;
            }

            public async Task<Response<int>> Handle(DeleteRrhPersonaCommand request, CancellationToken cancellationToken)
            {
                var entity = await _repo.GetByIdAsync(request.IdrrhPersona);
                if (entity == null)
                    throw new KeyNotFoundException("Persona no encontrada");

                await _repo.DeleteAsync(entity);
                return new Response<int>(entity.IdrrhPersona);
            }
        }
    }

}

using Aplicacion.Interfaces;
using Aplicacion.Wrappers;
using Dominio.Entities.Vistas;
using MediatR;
using Ardalis.Specification;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Dominio.Entities.Persona;

namespace Aplicacion.Features.Asistencia.Queries
{
    public class GetSugerenciasNombresQuery : IRequest<Response<List<string>>>
    {
        public string SearchText { get; set; }
    }

    public class GetSugerenciasNombresQueryHandler
        : IRequestHandler<GetSugerenciasNombresQuery, Response<List<string>>>
    {
        private readonly IRepositoryAsync<RrhPersona> _repository;

        public GetSugerenciasNombresQueryHandler(IRepositoryAsync<RrhPersona> repository)
        {
            _repository = repository;
        }

        public async Task<Response<List<string>>> Handle(
            GetSugerenciasNombresQuery request,
            CancellationToken cancellationToken)
        {
            // IDs que quieres ignorar
            var idsExcluidos = new List<int> { 1, 26 };

            var registros = await _repository.ListAsync(
                new SugerenciasNombresSpecification(request.SearchText, idsExcluidos),
                cancellationToken);

            var sugerencias = registros
                .Select(x => x.NombreApellido)
                .Distinct()
                .Take(5)
                .OrderBy(x => x)
                .ToList();

            return new Response<List<string>>(sugerencias);
        }
    }

    public class SugerenciasNombresSpecification : Specification<RrhPersona>
    {
        public SugerenciasNombresSpecification(string searchText, List<int> idsExcluidos)
        {
            var busqueda = searchText?.Trim().ToLower() ?? "";

            Query
                .Where(x => x.NombreApellido.ToLower().Contains(busqueda))
                .Where(x => !idsExcluidos.Contains(x.IdrrhPersona)) // Exclusión de IDs
                .OrderBy(x => x.NombreApellido)
                .AsNoTracking();
        }
    }

}
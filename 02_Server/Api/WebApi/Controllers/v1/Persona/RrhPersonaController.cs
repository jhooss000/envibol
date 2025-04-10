using Aplicacion.Features.Persona.Commands;
using Aplicacion.Features.Persona.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Webapi.Controllers.v1;

namespace WebApi.Controllers.v1.Persona
{
    [ApiVersion("1.0")]
    [ApiController]
    public class RrhPersona : BaseApiController
    {
        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        => Ok(await Mediator.Send(new GetAllRrhPersonaQuery()));

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateRrhPersonaCommand cmd)
            => Ok(await Mediator.Send(cmd));

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UpdateRrhPersonaCommand cmd)
        {
            if (id != cmd.IdrrhPersona) return BadRequest();
            return Ok(await Mediator.Send(cmd));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
            => Ok(await Mediator.Send(new DeleteRrhPersonaCommand { IdrrhPersona = id }));
    }
}

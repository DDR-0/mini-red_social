using Microsoft.AspNetCore.Mvc;
using mini_red_social1.Data;
using mini_red_social1.Models;

namespace mini_red_social1.Controllers.Api;

[ApiController]
[Route("api/registros-auditoria")]
public class RegistrosAuditoriaApiController : ApiCrudController<Registro_auditoria>
{
    public RegistrosAuditoriaApiController(SocialDBcontext context) : base(context)
    {
    }
}

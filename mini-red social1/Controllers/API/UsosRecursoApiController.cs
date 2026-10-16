using Microsoft.AspNetCore.Mvc;
using mini_red_social1.Data;
using mini_red_social1.Models;

namespace mini_red_social1.Controllers.Api;

[ApiController]
[Route("api/usos-recurso")]
public class UsosRecursoApiController : ApiCrudController<Uso_recurso>
{
    public UsosRecursoApiController(SocialDBcontext context) : base(context)
    {
    }
}

using Microsoft.AspNetCore.Mvc;
using mini_red_social1.Data;
using mini_red_social1.Models;

namespace mini_red_social1.Controllers.Api;

[ApiController]
[Route("api/tipos-interaccion")]
public class TiposInteraccionApiController : ApiCrudController<Tipo_Interaccion>
{
    public TiposInteraccionApiController(SocialDBcontext context) : base(context)
    {
    }
}

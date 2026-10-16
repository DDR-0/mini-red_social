using Microsoft.AspNetCore.Mvc;
using mini_red_social1.Data;
using mini_red_social1.Models;

namespace mini_red_social1.Controllers.Api;

[ApiController]
[Route("api/interacciones")]
public class InteraccionesApiController : ApiCrudController<Interaccion_social>
{
    public InteraccionesApiController(SocialDBcontext context) : base(context)
    {
    }
}

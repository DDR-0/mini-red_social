using Microsoft.AspNetCore.Mvc;
using mini_red_social1.Data;
using mini_red_social1.Models;

namespace mini_red_social1.Controllers.Api;

[ApiController]
[Route("api/usuarios")]
public class UsuariosApiController : ApiCrudController<Usuario>
{
    public UsuariosApiController(SocialDBcontext context) : base(context)
    {
    }
}

using Microsoft.AspNetCore.Mvc;
using mini_red_social1.Data;
using mini_red_social1.Models;

namespace mini_red_social1.Controllers.Api;

[ApiController]
[Route("api/recursos")]
public class RecursosController : ApiCrudController<Recurso>
{
    public RecursosController(SocialDBcontext context) : base(context)
    {
    }
}

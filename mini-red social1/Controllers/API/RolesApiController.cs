using Microsoft.AspNetCore.Mvc;
using mini_red_social1.Data;
using mini_red_social1.Models;

namespace mini_red_social1.Controllers.Api;

[ApiController]
[Route("api/roles")]
public class RolesApiController : ApiCrudController<Roles>
{
    public RolesApiController(SocialDBcontext context) : base(context)
    {
    }
}

using Microsoft.AspNetCore.Mvc;
using mini_red_social1.Data;
using mini_red_social1.Models;

namespace mini_red_social1.Controllers.Api;

[ApiController]
[Route("api/snapshots-diarios")]
public class SnapshotsDiariosApiController : ApiCrudController<Snapshot_diaria>
{
    public SnapshotsDiariosApiController(SocialDBcontext context) : base(context)
    {
    }
}

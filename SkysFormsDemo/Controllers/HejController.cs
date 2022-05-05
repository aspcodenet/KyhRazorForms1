using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SkysFormsDemo.Controllers;


[Route("api/hej")]
public class HejController : ControllerBase
{
    public IActionResult Index()
    {
        var a = 12;
        return Ok(a);
    }
}
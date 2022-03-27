using Microsoft.AspNetCore.Mvc;
using StarBoutique.WebApi.Models;
namespace StarBoutique.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    [HttpGet("all")]
    public IEnumerable<Room> GetAll()
    {
        return new List<Room>();
    }
}

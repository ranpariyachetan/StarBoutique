using System.Net;
using Microsoft.AspNetCore.Mvc;
using StarBoutique.WebApi.Models;
using StarBoutique.WebApi.Services;
namespace StarBoutique.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private IRoomService roomService;
    public RoomController()
    {
        this.roomService = new RoomService();
    }

    [HttpGet("all")]
    public IEnumerable<Room> GetAll()
    {
        return roomService.GetAllRooms();
    }

    [HttpGet("{roomId}")]
    public Room GetRoomById(string roomId)
    {
        return roomService.GetRoomById(roomId);
    }

    [HttpPost("{roomId}")]
    public ActionResult Allocate(string? roomId, AllocationModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = roomService.UpdateRoomStatus(roomId, RoomStatus.Occupied); ;
        if (result)
        {
            return NoContent();
        }
        else
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return new JsonResult("Allocation failed due to internal error.");
        }
    }
}

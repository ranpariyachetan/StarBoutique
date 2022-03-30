using Microsoft.AspNetCore.Mvc;
using StarBoutique.WebApi.Models;
using StarBoutique.WebApi.Services;
using StarBoutique.WebApi.Exceptions;
namespace StarBoutique.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
    private IRoomService roomService;
    public RoomController(IRoomService roomService)
    {
        this.roomService = roomService;
    }

    [HttpGet("all")]
    public IEnumerable<Room> GetAll()
    {
        return roomService.GetAllRooms();
    }

    [HttpGet("{roomId}")]
    public ActionResult GetRoomById(string roomId)
    {
        try
        {
            return new JsonResult(roomService.GetRoomById(roomId));
        }
        catch (RoomNotFoundException)
        {
            return NotFound(new { error = "Room not found." });
        }
    }

    [HttpPost("{roomId}/assign")]
    public ActionResult Assign(string? roomId)
    {
        try
        {
            roomService.UpdateRoomStatus(roomId, RoomStatus.Occupied);
            return NoContent();
        }
        catch (RoomNotFoundException)
        {
            return BadRequest(new { error = "Invalid roomid provided." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}

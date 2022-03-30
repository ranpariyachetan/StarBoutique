using System.Net;
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
    public ActionResult GetRoomById(string roomId)
    {
        try
        {
            return new JsonResult(roomService.GetRoomById(roomId));
        }
        catch(RoomNotFoundExceptiion)
        {
            return NotFound(new {error = "Room not found."});
        }
    }

    [HttpPost("allocate/{roomId}")]
    public ActionResult Allocate(string? roomId, AllocationModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            roomService.UpdateRoomStatus(roomId, RoomStatus.Occupied);
            return NoContent();
        }
        catch(RoomNotFoundExceptiion)
        {
            return BadRequest(new {error = "Invalid roomid provided."});
        }
    }
}

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

    [HttpGet("available")]
    public IEnumerable<Room> GetAvailableRooms()
    {
        return roomService.GetAllRooms(RoomStatus.Available);
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

    [HttpPost("assign")]
    public ActionResult Assign()
    {
        try
        {
            var assignedRoomId = roomService.AssignRoom();

            return new JsonResult(new { roomId = assignedRoomId });
        }
        catch (RoomNotAvailableException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("{roomId}/checkout")]
    public ActionResult CheckOut(string? roomId)
    {
        try
        {
            roomService.UpdateRoomStatus(roomId, RoomStatus.Vacant);

            return new JsonResult(new { roomId = roomId, status = RoomStatus.Vacant.ToString() });
        }
        catch (RoomNotAvailableException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("{roomId}/available")]
    public ActionResult Available(string? roomId)
    {
        try
        {
            roomService.UpdateRoomStatus(roomId, RoomStatus.Available);

            return new JsonResult(new { roomId = roomId, status = RoomStatus.Available.ToString() });
        }
        catch (InvalidStatusUpdateException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch(RoomNotFoundException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch(Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("{roomId}/repair")]
    public ActionResult Repair(string? roomId)
    {
        try
        {
            roomService.UpdateRoomStatus(roomId, RoomStatus.Repair);

            return new JsonResult(new { roomId = roomId, status = RoomStatus.Repair.ToString() });
        }
        catch (InvalidStatusUpdateException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch(RoomNotFoundException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("{roomId}/vacant")]
    public ActionResult Vacant(string? roomId)
    {
        try
        {
            roomService.UpdateRoomStatus(roomId, RoomStatus.Vacant);

            return new JsonResult(new { roomId = roomId, status = RoomStatus.Vacant.ToString() });
        }
        catch (InvalidStatusUpdateException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch(RoomNotFoundException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using StarBoutique.WebApi.Models;
using StarBoutique.WebApi.Services;
using StarBoutique.WebApi.Exceptions;
using System.Net;
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

    [HttpGet("all/{status}")]
    public ActionResult GetRooms(string status)
    {
        if (status.ToLower() == "all")
        {
            return new JsonResult(roomService.GetAllRooms());
        }
        else if (Enum.TryParse(status, true, out RoomStatus result))
        {
            return new JsonResult(roomService.GetAllRooms(result));
        }
        else
        {
            return BadRequest(new { error = "Invalid status value passed." });
        }
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
        return UpdateRoomStatus(roomId, RoomStatus.Vacant);
    }

    [HttpPost("{roomId}/available")]
    public ActionResult Available(string? roomId)
    {
        return UpdateRoomStatus(roomId, RoomStatus.Available);
    }

    [HttpPost("{roomId}/repair")]
    public ActionResult Repair(string? roomId)
    {
        return UpdateRoomStatus(roomId, RoomStatus.Repair);
    }

    [HttpPost("{roomId}/vacant")]
    public ActionResult Vacant(string? roomId)
    {
        return UpdateRoomStatus(roomId, RoomStatus.Vacant);
    }

    private ActionResult UpdateRoomStatus(string? roomId, RoomStatus status)
    {
        try
        {
            roomService.UpdateRoomStatus(roomId, status);

            return new JsonResult(new { roomId = roomId, status = status });
        }
        catch (InvalidStatusUpdateException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (RoomNotFoundException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return new JsonResult(new { error = ex.Message });
        }
    }
}
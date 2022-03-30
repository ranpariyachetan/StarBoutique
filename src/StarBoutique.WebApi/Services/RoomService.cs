using StarBoutique.WebApi.Models;

namespace StarBoutique.WebApi.Services;

public interface IRoomService
{
    IEnumerable<Room> GetAllRooms();
    bool    UpdateRoomStatus(string? roomId, RoomStatus roomStatus);
    Room GetRoomById(string roomId);
}

public class RoomService : IRoomService
{
    public IEnumerable<Room> GetAllRooms()
    {
        return new List<Room>();
    }

    public bool UpdateRoomStatus(string? roomId, RoomStatus status)
    {
        return true;
    }

    public Room GetRoomById(string roomId)
    {
        return new Room {Id = roomId, Status = RoomStatus.Available};
    }
}
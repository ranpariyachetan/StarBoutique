using StarBoutique.WebApi.Models;
using StarBoutique.WebApi.Repositories;
using StarBoutique.WebApi.Exceptions;

namespace StarBoutique.WebApi.Services;

public interface IRoomService
{
    IEnumerable<Room> GetAllRooms();
    void UpdateRoomStatus(string? roomId, RoomStatus roomStatus);
    Room GetRoomById(string roomId);
}

public class RoomService : IRoomService
{
    private IRoomRepository repository;
    public RoomService()
    {
        repository = new RoomRepository();
    }
    public IEnumerable<Room> GetAllRooms()
    {
        return repository.GetAllRooms();
    }

    public void UpdateRoomStatus(string? roomId, RoomStatus status)
    {
        var room  = repository.GetRoomById(roomId);

        if(room != null)
        {
            room.Status = status;
            repository.UpdateRoom(room);
        }
        else
        {
            throw new NotFoundException("Room not found.");
        }
    }

    public Room GetRoomById(string roomId)
    {
        var room = repository.GetRoomById(roomId);

        if(room != null)
        {
            return room;
        }
        else
        {
            throw new NotFoundException("Room not found.");
        }
    }
}
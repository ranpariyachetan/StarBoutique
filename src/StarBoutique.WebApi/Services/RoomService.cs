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
    private Dictionary<RoomStatus, IEnumerable<RoomStatus>> allowedRoomStatus;
    private IRoomRepository repository;
    public RoomService()
    {
        repository = new RoomRepository();
        allowedRoomStatus = new Dictionary<RoomStatus, IEnumerable<RoomStatus>>();
        allowedRoomStatus.Add(RoomStatus.Available, new List<RoomStatus> {RoomStatus.Occupied});
        allowedRoomStatus.Add(RoomStatus.Occupied, new List<RoomStatus> {RoomStatus.Vacant});
        allowedRoomStatus.Add(RoomStatus.Repair, new List<RoomStatus> {RoomStatus.Vacant});
        allowedRoomStatus.Add(RoomStatus.Vacant, new List<RoomStatus> {RoomStatus.Available, RoomStatus.Repair});
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
            var currentStatus = room.Status;
            if(currentStatus == status)
            {
                throw new Exception($"Room is already {currentStatus}.");
            }

            if(allowedRoomStatus[currentStatus].Contains(status))
            {
                room.Status = status;                
                repository.UpdateRoom(room);
            }
            else
            {
                throw new InvalidStatusUpdateException();
            }
        }
        else
        {
            throw new RoomNotFoundExceptiion();
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
            throw new RoomNotFoundExceptiion();
        }
    }
}
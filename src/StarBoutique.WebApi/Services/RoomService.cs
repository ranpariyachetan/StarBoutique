using StarBoutique.WebApi.Models;
using StarBoutique.WebApi.Repositories;
using StarBoutique.WebApi.Exceptions;

namespace StarBoutique.WebApi.Services;

public interface IRoomService
{
    IEnumerable<Room> GetAllRooms();
    IEnumerable<Room> GetAllRooms(RoomStatus status);
    void UpdateRoomStatus(string? roomId, RoomStatus roomStatus);

    string AssignRoom();
    Room GetRoomById(string roomId);
}

public class RoomService : IRoomService
{
    private Dictionary<RoomStatus, IEnumerable<RoomStatus>> allowedRoomStatus;
    private IRoomRepository repository;
    public RoomService(IRoomRepository roomRepository)
    {
        repository = roomRepository;
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
    public IEnumerable<Room> GetAllRooms(RoomStatus status)
    {
        return repository.GetAllRoomsByStatus(status);
    }

    public void UpdateRoomStatus(string? roomId, RoomStatus status)
    {
        var room  = repository.GetRoomById(roomId);

        if(room != null)
        {
            var currentStatus = room.RoomStatus;
            if(currentStatus == status)
            {
                throw new InvalidStatusUpdateException($"Room is already {currentStatus}.");
            }

            if(allowedRoomStatus[currentStatus].Contains(status))
            {
                room.RoomStatus = status;                
                repository.UpdateRoom(room);
            }
            else
            {
                throw new InvalidStatusUpdateException();
            }
        }
        else
        {
            throw new RoomNotFoundException();
        }
    }

    public string AssignRoom()
    {
        var availableRoom = repository.GetNextAvailableRoom();

        if(availableRoom != null)
        {
            availableRoom.RoomStatus = RoomStatus.Occupied;
            repository.UpdateRoom(availableRoom);
            return availableRoom.Id;
        }
        else
        {
            throw new RoomNotAvailableException();
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
            throw new RoomNotFoundException();
        }
    }
}
using StarBoutique.WebApi.Models;

namespace StarBoutique.WebApi.Repositories;

public interface IRoomRepository
{
    IEnumerable<Room> GetAllRoomsByStatus(RoomStatus status);
    Room? GetRoomById(string? roomId);

    void UpdateRoom(Room room);
    Room GetNextAvailableRoom();
}

public class RoomRepository: IRoomRepository
{
    private Dictionary<string, Room> rooms = new Dictionary<string, Room>();
    private Room nextAvailableRoom;
    public RoomRepository()
    {
        rooms.Add("1A", new Room{Id="1A", Status = RoomStatus.Available});
        rooms.Add("1B", new Room{Id="1B", Status = RoomStatus.Available});
        rooms.Add("1C", new Room{Id="1C", Status = RoomStatus.Available});
        rooms.Add("1D", new Room{Id="1D", Status = RoomStatus.Available});
        rooms.Add("1E", new Room{Id="1E", Status = RoomStatus.Available});
        rooms.Add("2E", new Room{Id="2E", Status = RoomStatus.Available});
        rooms.Add("2D", new Room{Id="2D", Status = RoomStatus.Available});
        rooms.Add("2C", new Room{Id="2C", Status = RoomStatus.Available});
        rooms.Add("2B", new Room{Id="2B", Status = RoomStatus.Available});
        rooms.Add("2A", new Room{Id="2A", Status = RoomStatus.Available});
        rooms.Add("3A", new Room{Id="3A", Status = RoomStatus.Available});
        rooms.Add("3B", new Room{Id="3A", Status = RoomStatus.Available});
        rooms.Add("3C", new Room{Id="3B", Status = RoomStatus.Available});
        rooms.Add("3D", new Room{Id="3C", Status = RoomStatus.Available});
        rooms.Add("3E", new Room{Id="3D", Status = RoomStatus.Available});
        rooms.Add("4E", new Room{Id="4E", Status = RoomStatus.Available});
        rooms.Add("4D", new Room{Id="4D", Status = RoomStatus.Available});
        rooms.Add("4C", new Room{Id="4C", Status = RoomStatus.Available});
        rooms.Add("4B", new Room{Id="4B", Status = RoomStatus.Available});
        rooms.Add("4A", new Room{Id="4A", Status = RoomStatus.Available});
        nextAvailableRoom = rooms["1A"];
    }

    public IEnumerable<Room> GetAllRoomsByStatus(RoomStatus status)
    {
        return rooms.Values.Where(room => room.Status == status);
    }

    public Room? GetRoomById(string roomId)
    {
        if(rooms.ContainsKey(roomId))
        {
            return rooms[roomId];
        }
        return null;
    }

    public void UpdateRoom(Room room)
    {
        if(rooms.ContainsKey(room.Id))
        {
            rooms[room.Id] = room;
        }
    }

    public Room GetNextAvailableRoom()
    {
        foreach(var kvPair in rooms)
        {
            if(kvPair.Value.Status == RoomStatus.Available)
            {
                return kvPair.Value;
            }
        }

        return null;
    }
}
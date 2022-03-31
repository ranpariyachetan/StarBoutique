using StarBoutique.WebApi.Models;

namespace StarBoutique.WebApi.Repositories;

public interface IRoomRepository
{
    IEnumerable<Room> GetAllRooms();
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
        rooms.Add("1A", new Room{Id="1A", RoomStatus = RoomStatus.Available});
        rooms.Add("1B", new Room{Id="1B", RoomStatus = RoomStatus.Available});
        rooms.Add("1C", new Room{Id="1C", RoomStatus = RoomStatus.Available});
        rooms.Add("1D", new Room{Id="1D", RoomStatus = RoomStatus.Available});
        rooms.Add("1E", new Room{Id="1E", RoomStatus = RoomStatus.Available});
        rooms.Add("2E", new Room{Id="2E", RoomStatus = RoomStatus.Available});
        rooms.Add("2D", new Room{Id="2D", RoomStatus = RoomStatus.Available});
        rooms.Add("2C", new Room{Id="2C", RoomStatus = RoomStatus.Available});
        rooms.Add("2B", new Room{Id="2B", RoomStatus = RoomStatus.Available});
        rooms.Add("2A", new Room{Id="2A", RoomStatus = RoomStatus.Available});
        rooms.Add("3A", new Room{Id="3A", RoomStatus = RoomStatus.Available});
        rooms.Add("3B", new Room{Id="3A", RoomStatus = RoomStatus.Available});
        rooms.Add("3C", new Room{Id="3B", RoomStatus = RoomStatus.Available});
        rooms.Add("3D", new Room{Id="3C", RoomStatus = RoomStatus.Available});
        rooms.Add("3E", new Room{Id="3D", RoomStatus = RoomStatus.Available});
        rooms.Add("4E", new Room{Id="4E", RoomStatus = RoomStatus.Available});
        rooms.Add("4D", new Room{Id="4D", RoomStatus = RoomStatus.Available});
        rooms.Add("4C", new Room{Id="4C", RoomStatus = RoomStatus.Available});
        rooms.Add("4B", new Room{Id="4B", RoomStatus = RoomStatus.Available});
        rooms.Add("4A", new Room{Id="4A", RoomStatus = RoomStatus.Available});
        nextAvailableRoom = rooms["1A"];
    }

    public IEnumerable<Room> GetAllRooms()
    {
        return rooms.Values;
    }

    public IEnumerable<Room> GetAllRoomsByStatus(RoomStatus status)
    {
        return rooms.Values.Where(room => room.RoomStatus == status);
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
            if(kvPair.Value.RoomStatus == RoomStatus.Available)
            {
                return kvPair.Value;
            }
        }

        return null;
    }
}
using System.Text.Json.Serialization;
namespace StarBoutique.WebApi.Models;

public class Room
{
    public string Id { get; set; }

    [JsonIgnore]
    public RoomStatus RoomStatus { get; set; }

    public string Status { get { return RoomStatus.ToString(); } }
}

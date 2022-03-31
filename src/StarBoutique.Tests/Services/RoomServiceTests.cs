using StarBoutique.WebApi.Services;
using StarBoutique.WebApi.Repositories;
using StarBoutique.WebApi.Models;
using StarBoutique.WebApi.Exceptions;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;

namespace StarBoutique.Tests.Services;

public class RoomServiceTests
{
    private RoomService roomService;
    private Mock<IRoomRepository> mockRoomRepository;

    [SetUp]
    public void SetUp()
    {
        mockRoomRepository = new Mock<IRoomRepository>();

        roomService = new RoomService(mockRoomRepository.Object);
    }

    [Test]
    public void GetAllRoomsSuccess()
    {
        mockRoomRepository.Setup(repository => repository.GetAllRooms()).Returns(new List<Room>());

        var result = roomService.GetAllRooms();

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    [TestCase(RoomStatus.Available)]
    [TestCase(RoomStatus.Occupied)]
    [TestCase(RoomStatus.Vacant)]
    [TestCase(RoomStatus.Repair)]
    public void GetAllRoomsByStatusSuccess(RoomStatus status)
    {
        mockRoomRepository.Setup(repository => repository.GetAllRoomsByStatus(status)).Returns(new List<Room>());

        var result = roomService.GetAllRooms(status);

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void UpdateRoomStatusSuccess()
    {
        var roomId = "Room1";
        var status = RoomStatus.Occupied;

        var room = new Room { Id = roomId, RoomStatus = RoomStatus.Available };

        mockRoomRepository.Setup(repository => repository.GetRoomById(roomId)).Returns(room);
        mockRoomRepository.Setup(repository => repository.UpdateRoom(room));

        roomService.UpdateRoomStatus(roomId, status);
    }

    [Test]
    public void UpdateRoomStatusThrowsExceptionWhenRoomHasTheSameStatus()
    {
        var roomId = "Room1";
        var status = RoomStatus.Available;

        var room = new Room { Id = roomId, RoomStatus = RoomStatus.Available };

        mockRoomRepository.Setup(repository => repository.GetRoomById(roomId)).Returns(room);

        var ex = Assert.Throws<InvalidStatusUpdateException>(() => roomService.UpdateRoomStatus(roomId, status));

        Assert.AreEqual($"Room is already {status}.", ex.Message);
    }

    [Test]
    public void UpdateRoomStatusThrowsExceptionWhenStatusIsNotValid()
    {
        var roomId = "Room1";
        var status = RoomStatus.Available;

        var room = new Room { Id = roomId, RoomStatus = RoomStatus.Repair };

        mockRoomRepository.Setup(repository => repository.GetRoomById(roomId)).Returns(room);

        var ex = Assert.Throws<InvalidStatusUpdateException>(() => roomService.UpdateRoomStatus(roomId, status));

        Assert.AreEqual("Specified status update is not valid.", ex.Message);
    }

    [Test]
    public void UpdateRoomStatusThrowsExceptionWhenRoomNotFound()
    {
        var roomId = "Room1";
        var status = RoomStatus.Available;

        mockRoomRepository.Setup(repository => repository.GetRoomById(roomId)).Returns(null as Room);

        var ex = Assert.Throws<RoomNotFoundException>(() => roomService.UpdateRoomStatus(roomId, status));

        Assert.AreEqual("Room not found.", ex.Message);
    }

    [Test]
    public void AssignRoomSuccess()
    {
        var roomId = "Room1";
        var status = RoomStatus.Available;

        var room = new Room { Id = roomId, RoomStatus = status };

        mockRoomRepository.Setup(repository => repository.GetNextAvailableRoom()).Returns(room);
        mockRoomRepository.Setup(repository => repository.UpdateRoom(room));

        var result = roomService.AssignRoom();
        Assert.AreEqual(roomId, result);
    }

    [Test]
    public void AssignRoomThrowExceptionWhenRoomNotAvailable()
    {
        mockRoomRepository.Setup(repository => repository.GetNextAvailableRoom()).Returns(null as Room);

        var ex = Assert.Throws<RoomNotAvailableException>(() => roomService.AssignRoom());

        Assert.AreEqual("Room not available for assignment.", ex.Message);
    }

    [Test]
    public void GetRoomByIdSuccess()
    {
        var roomId = "Room1";
        var room = new Room {Id = roomId, RoomStatus = RoomStatus.Available};

        mockRoomRepository.Setup(repository => repository.GetRoomById(roomId)).Returns(room);

        var result = roomService.GetRoomById(roomId);

        Assert.That(result, Is.Not.Null);
        Assert.AreEqual(room.Id, result.Id);
        Assert.AreEqual(room.RoomStatus, result.RoomStatus);
    }

    [Test]
    public void GetRoomByIdThrowsExceptionWhenRoomNotFound()
    {
        var roomId = "Room1";
        mockRoomRepository.Setup(repository => repository.GetRoomById(roomId)).Returns(null as Room);

        var ex = Assert.Throws<RoomNotFoundException>(() => roomService.GetRoomById(roomId));

        Assert.AreEqual("Room not found.", ex.Message);
    }
}
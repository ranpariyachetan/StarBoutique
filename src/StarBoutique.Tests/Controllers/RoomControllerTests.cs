using StarBoutique.WebApi.Controllers;
using StarBoutique.WebApi.Models;
using StarBoutique.WebApi.Services;
using StarBoutique.WebApi.Exceptions;
using System.Collections.Generic;
using System;
using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
namespace StarBoutique.Tests.Controllers;
public class RoomControllerTests
{
    private RoomController roomController;
    Mock<IRoomService> mockRoomService;
    [SetUp]
    public void Setup()
    {
        mockRoomService = new Mock<IRoomService>(MockBehavior.Strict);

        roomController = new RoomController(mockRoomService.Object);
    }

    [Test]
    [TestCase("Available")]
    [TestCase("Vacant")]
    [TestCase("Occupied")]
    [TestCase("Repair")]
    public void GetAvailableRoomsSuccessForValidStatusValue(string roomStatus)
    {
        mockRoomService.Setup(service => service.GetAllRooms(It.IsAny<RoomStatus>())).Returns(new List<Room>());
        var result = roomController.GetRooms(roomStatus);

        Assert.That(result, Is.Not.Null);
        Assert.IsInstanceOf(typeof(JsonResult), result);
    }

    [Test]
    public void GetAvailableRoomsSuccessForAllRooms()
    {
        mockRoomService.Setup(service => service.GetAllRooms()).Returns(new List<Room>());
        var result = roomController.GetRooms("all");

        Assert.That(result, Is.Not.Null);
        Assert.IsInstanceOf(typeof(JsonResult), result);
    }

    [Test]
    public void GetAvailableRoomsReturnBadRequestResult()
    {
        mockRoomService.Setup(service => service.GetAllRooms()).Returns(new List<Room>());
        var result = roomController.GetRooms(Guid.NewGuid().ToString());

        Assert.That(result, Is.Not.Null);
        Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);
    }

    [Test]
    public void GetRoomByIdSuccess()
    {
        var roomId = "Room1";

        var room = new Room { Id = roomId, RoomStatus = RoomStatus.Occupied };
        mockRoomService.Setup(service => service.GetRoomById(roomId)).Returns(room);

        var result = roomController.GetRoomById(roomId);
        Assert.That(result, Is.Not.Null);
        Assert.IsInstanceOf(typeof(JsonResult), result);
        var jsonResult = result as JsonResult;
        Assert.IsInstanceOf(typeof(Room), jsonResult.Value);
        var roomResult = jsonResult.Value as Room;
        Assert.AreEqual(roomId, roomResult.Id);
    }

    [Test]
    public void GetRoomByIdReturnsBadRequestIfRoomNotFound()
    {
        var roomId = "Room1";

        mockRoomService.Setup(service => service.GetRoomById(roomId)).Throws<RoomNotFoundException>();

        var result = roomController.GetRoomById(roomId);
        Assert.That(result, Is.Not.Null);
        Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);
    }

    [Test]
    public void AssignSuccess()
    {
        var roomId = "Room1";

        mockRoomService.Setup(service => service.AssignRoom()).Returns(roomId);

        var result = roomController.Assign();
        Assert.That(result, Is.Not.Null);
        Assert.IsInstanceOf(typeof(JsonResult), result);
    }

    [Test]
    public void AssignFailWithErrorRoomNotAvailable()
    {
        mockRoomService.Setup(service => service.AssignRoom()).Throws<RoomNotAvailableException>();

        var result = roomController.Assign();
        Assert.That(result, Is.Not.Null);
        Assert.IsInstanceOf(typeof(BadRequestObjectResult), result);
    }
}
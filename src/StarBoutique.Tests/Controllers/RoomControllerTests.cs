using StarBoutique.WebApi.Controllers;
using StarBoutique.WebApi.Models;
using StarBoutique.WebApi.Services;
using System.Collections.Generic;
using System;
using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
namespace StarBoutique.Tests.Controllers;
public class RoomControllerTests
{
    private RoomController roomController;
    Mock<IRoomService>  mockRoomService;
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
}
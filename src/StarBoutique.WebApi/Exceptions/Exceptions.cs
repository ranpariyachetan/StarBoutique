namespace StarBoutique.WebApi.Exceptions;

public class RoomNotFoundException : Exception
{
    public RoomNotFoundException() : base("Room not found")
    {

    }
}

public class InvalidStatusUpdateException : Exception
{
    public InvalidStatusUpdateException() : base("Specified status update is not valid.")
    {

    }
}
namespace StarBoutique.WebApi.Exceptions;

public class RoomNotFoundExceptiion : Exception
{
    public RoomNotFoundExceptiion() : base("Room not found")
    {

    }
}

public class InvalidStatusUpdateException : Exception
{
    public InvalidStatusUpdateException() : base("Specified status update is not valid.")
    {

    }
}
namespace Reservoom.Models;

public class RoomID
{
    public int FloorNumber { get; }
    public int RoomNumber { get; }
    public RoomID(int floorNumber, int roomNumber)
    {
        FloorNumber = floorNumber;
        RoomNumber = roomNumber;
    }

    public override string ToString()
    {
        return $"{FloorNumber}{RoomNumber}";
    }

    public override bool Equals(object? obj)
    {
        return obj is RoomID roomId &&
               FloorNumber == roomId.FloorNumber &&
               RoomNumber == roomId.RoomNumber;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(FloorNumber, RoomNumber);
    }
}
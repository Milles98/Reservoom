﻿namespace Reservoom.Models;

public class Reservation
{
    public RoomID RoomId { get; }
    public string Username { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public TimeSpan Length => EndTime.Subtract(StartTime);
    public Reservation(RoomID roomId, string username, DateTime startTime, DateTime endTime)
    {
        RoomId = roomId;
        Username = username;
        StartTime = startTime;
        EndTime = endTime;
    }

}
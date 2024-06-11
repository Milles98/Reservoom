using System.Runtime.Serialization;
using Reservoom.Models;

namespace Reservoom.Exceptions;

public class ReservationConflictException : Exception
{
    public Reservation ExistingReservation { get; }
    public Reservation IncomingReservation { get; }
    
    public ReservationConflictException(Reservation incomingReservation, Reservation existingReservation)
    {
        IncomingReservation = incomingReservation;
        ExistingReservation = existingReservation;
    }

    public ReservationConflictException(string? message, Reservation incomingReservation, Reservation existingReservation) : base(message)
    {
        IncomingReservation = incomingReservation;
        ExistingReservation = existingReservation;
    }

    public ReservationConflictException(string? message, Exception? innerException, Reservation incomingReservation, Reservation existingReservation) : base(message, innerException)
    {
        IncomingReservation = incomingReservation;
        ExistingReservation = existingReservation;
    }
}
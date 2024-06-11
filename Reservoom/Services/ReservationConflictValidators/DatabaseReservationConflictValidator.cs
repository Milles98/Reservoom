using Microsoft.EntityFrameworkCore;
using Reservoom.DbContexts;
using Reservoom.DTOs;
using Reservoom.Models;

namespace Reservoom.Services.ReservationConflictValidators;

public class DatabaseReservationConflictValidator : IReservationConflictValidator
{
    private readonly ReservoomDbContextFactory _dbContextFactory;

    public DatabaseReservationConflictValidator(ReservoomDbContextFactory dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<Reservation> GetConflictingReservation(Reservation reservation)
    {
        using (ReservoomDbContext context = _dbContextFactory.CreateDbContext())
        {
            ReservationDTO reservationDto = await context.Reservations
                .Where(r => r.FloorNumber == reservation.RoomId.FloorNumber)
                .Where(r => r.RoomNumber == reservation.RoomId.RoomNumber)
                .Where(r => r.EndTime > reservation.StartTime)
                .Where(r => r.StartTime < reservation.EndTime)
                .FirstOrDefaultAsync() ?? throw new InvalidOperationException();

            return ToReservation(reservationDto);
        }
    }
    
    private static Reservation ToReservation(ReservationDTO r)
    {
        return new Reservation(new RoomID(r.FloorNumber, r.RoomNumber), r.Username, r.StartTime, r.EndTime);
    }
}
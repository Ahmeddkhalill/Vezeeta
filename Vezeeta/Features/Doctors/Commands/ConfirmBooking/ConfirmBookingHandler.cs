namespace Vezeeta.Features.Doctors.Commands.ConfirmBooking;

public class ConfirmBookingHandler(ApplicationDbContext context, IHttpContextAccessor accessor)
    : IRequestHandler<ConfirmBookingRequest, Result>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IHttpContextAccessor _accessor = accessor;

    public async Task<Result> Handle(ConfirmBookingRequest request, CancellationToken cancellationToken)
    {
        var userId = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
            return Result.Failure(BookingErrors.Unauthorized);

        var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == userId, cancellationToken);
        if (doctor is null)
            return Result.Failure(BookingErrors.NotFoundDoctor);

        var booking = await _context.Bookings
            .FirstOrDefaultAsync(b => b.Id == request.BookingId, cancellationToken);

        if (booking is null)
            return Result.Failure(BookingErrors.NotFound);

        if (booking.IsConfirmed)
            return Result.Failure(BookingErrors.AlreadyConfirmed);

        if (booking.DoctorId != doctor.Id)
            return Result.Failure(BookingErrors.NotFoundDoctor);

        booking.IsConfirmed = true;
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
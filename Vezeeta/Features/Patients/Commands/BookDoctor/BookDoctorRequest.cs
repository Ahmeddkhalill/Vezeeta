using Vezeeta.Features.Patients.Models;

namespace Vezeeta.Features.Patients.Commands.BookDoctor;

public record BookDoctorRequest(
    int ScheduleId,
    int TimeSlotId,
    string? CouponCode
) : IRequest<Result<BookDoctorResponse>>;
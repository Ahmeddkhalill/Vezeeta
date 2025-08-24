using Microsoft.EntityFrameworkCore;
using Vezeeta.Features.Patients.Models;

namespace Vezeeta.Features.Patients.Queries.SearchDoctors;

public class GetAllDoctorsHandler(ApplicationDbContext context)
    : IRequestHandler<GetAllDoctorsRequest, Result<IEnumerable<GetAllDoctorsResponse>>>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<IEnumerable<GetAllDoctorsResponse>>> Handle(GetAllDoctorsRequest request, CancellationToken cancellationToken)
    {
        var doctors = await _context.Doctors
            .Include(d => d.User)
            .Include(d => d.Specialization)
            .Include(d => d.DoctorSchedules)
                .ThenInclude(s => s.TimeSlots)
            .Where(d => d.DoctorSchedules.Any(s => s.TimeSlots.Any(ts => !ts.IsBooked)))
            .ToListAsync(cancellationToken);

        if (!doctors.Any())
            return Result.Success(Enumerable.Empty<GetAllDoctorsResponse>());

        var response = doctors.Select(d => new GetAllDoctorsResponse(
            FullName: $"{d.User!.FirstName} {d.User.LastName}",
            Email: d.User.Email!,
            Phone: d.User.PhoneNumber!,
            Image: d.User.Image,
            Specialization: d.Specialization?.Name ?? string.Empty,
            Gender: d.User.Gender.ToString(),
            Schedules: d.DoctorSchedules
                .Where(s => s.TimeSlots.Any(ts => !ts.IsBooked)) // بس اللي فيها مواعيد متاحة
                .Select(s => new DoctorScheduleDto(
                    Id: s.Id,
                    Date: s.Date,
                    DayOfWeek: s.DayOfWeek.ToString(),
                    TimeSlots: s.TimeSlots
                        .Where(ts => !ts.IsBooked) // بس المتاحة
                        .Select(ts => new DoctorTimeSlotDto(
                            Id: ts.Id,
                            StartTime: ts.StartTime.ToString(@"hh\\:mm"),
                            EndTime: ts.EndTime.ToString(@"hh\\:mm"),
                            Price: ts.Price
                        )).ToList()
                )).ToList()
        ));

        return Result.Success(response);
    }
}

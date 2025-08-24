//using Vezeeta.Features.Patients.Models;
//using Vezeeta.Features.Patients.Queries.GetPatientById;

//namespace Vezeeta.Features.Patients.Queries.GetById;

//public class GetPatientByIdHandler(ApplicationDbContext context) : IRequestHandler<GetPatientByIdRequest, PatientDetailsResponse?>
//{
//    private readonly ApplicationDbContext _context = context;

//    public async Task<PatientDetailsResponse?> Handle(GetPatientByIdRequest request, CancellationToken cancellationToken)
//    {
//        // Get patient including the User
//        var patient = await _context.Patients
//            .Include(p => p.User)
//            .FirstOrDefaultAsync(p => p.Id == request.PatientId, cancellationToken);

//        if (patient is null)
//            return null;

//        // Get bookings for this patient
//        var bookings = await _context.Bookings
//            .Include(b => b.Doctor)
//                .ThenInclude(d => d.User)
//            .Include(b => b.DoctorSchedule)
//            .Include(b => b.DoctorTimeSlot)
//            .Where(b => b.PatientId == patient.ApplicationUserId)
//            .Select(b => new BookingResponse(
//                b.Id,
//                b.Doctor!.User.FirstName + " " + b.Doctor.User.LastName,
//                b.Doctor.User.Email!,
//                b.DoctorSchedule.Date,
//                b.DoctorTimeSlot!.StartTime.ToString(@"hh\:mm"),
//                b.DoctorTimeSlot.EndTime.ToString(@"hh\:mm"),
//                b.FinalPrice,
//                b.IsConfirmed,
//                b.CouponCode
//            ))
//            .ToListAsync(cancellationToken);

//        // Return full patient details
//        return new PatientDetailsResponse(
//            patient.User.Image,
//            $"{patient.User.FirstName} {patient.User.LastName}",
//            patient.User.Email ?? string.Empty,
//            patient.User.PhoneNumber ?? string.Empty,
//            patient.User.Gender.ToString(),
//            patient.User.DateOfBirth,
//            bookings
//        );
//    }
//}

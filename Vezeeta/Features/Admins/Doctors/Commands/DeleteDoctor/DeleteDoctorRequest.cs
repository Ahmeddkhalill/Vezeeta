namespace Vezeeta.Features.Admins.Doctors.Commands.DeleteDoctor;

public record DeleteDoctorRequest (int DoctorId) : IRequest<Result>;
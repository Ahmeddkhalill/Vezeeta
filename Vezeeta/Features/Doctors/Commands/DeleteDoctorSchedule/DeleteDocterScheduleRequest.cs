namespace Vezeeta.Features.Doctors.Commands.DeleteDoctorSchedule;

public record DeleteDocterScheduleRequest(int ScheduleId) : IRequest<Result>;

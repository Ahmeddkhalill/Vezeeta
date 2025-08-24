namespace Vezeeta.Errors;

public static class SpecializationErrors
{
  public static readonly Error NotFound = new(
      "Specialization.NotFound",
      "Specialization not found.",
      StatusCodes.Status404NotFound);

}

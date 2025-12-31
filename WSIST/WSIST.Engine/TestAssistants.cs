namespace WSIST.Engine;

public class TestAssistants
{
    public static double? GradeVerifier(DateOnly dueDate, double? grade)
    {
        if (dueDate > DateOnly.FromDateTime(DateTime.Today))
        {
            return null;
        }

        if (grade is not null && (grade < 1 || grade > 6))
        {
            throw new ArgumentOutOfRangeException(nameof(grade), "Grade must be between 1 and 6.");
        }

        return grade;
    }
}

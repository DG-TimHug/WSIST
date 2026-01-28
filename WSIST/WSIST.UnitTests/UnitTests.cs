using NUnit.Framework;
using WSIST.Engine;

namespace WSIST.UnitTests;

public class UnitTests
{
    [Test]
    public async Task TestIfNewTestGetsMade()
    {
        var repo = new FakeTestRepository();
        var manager = new TestManagement(repo);

        await manager.NewTestMaker(
            "Math Test",
            Test.Subjects.Math,
            new DateOnly(2026, 02, 04),
            Test.TestVolume.VeryHigh,
            Test.PersonalUnderstanding.VeryLow,
            4.5
        );

        Assert.That(manager.Tests.Any(t => t.Title == "Math Test"));
    }

    [Test]
    public async Task CheckIfTestWasDeleted()
    {
        var repo = new FakeTestRepository();
        var manager = new TestManagement(repo);

        await manager.NewTestMaker(
            "Temp",
            Test.Subjects.Math,
            DateOnly.FromDateTime(DateTime.Today),
            Test.TestVolume.Low,
            Test.PersonalUnderstanding.Low,
            null
        );

        var id = manager.Tests.First().Id;

        await manager.TestRemover(id);

        Assert.That(manager.Tests.Any(t => t.Id == id), Is.False);
    }

    [Test]
    public void CheckIfGradeIsNotNullIfInThePast()
    {
        DateOnly dueDate = new(2025, 06, 07);
        var grade = 4.6;

        var result = TestAssistants.GradeVerifier(dueDate, grade);

        Assert.That(result, Is.Not.Null);
    }

    [Test]
    public void CheckIfGradeIsNullIfInTheFuture()
    {
        DateOnly dueDate = new(2030, 06, 07);
        var grade = 4.6;

        var result = TestAssistants.GradeVerifier(dueDate, grade);

        Assert.That(result, Is.Null);
    }
}
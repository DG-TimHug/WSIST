using NUnit.Framework;
using WSIST.Engine;

namespace WSIST.UnitTests;

public class UnitTests
{
    [Test]
    public void TestIfNewTestGetsMade()
    {
        //arrange
        var manager = new TestManagement();
        //act
        manager.NewTestMaker(
            "Math Tets",
            Test.Subjects.Math,
            new DateOnly(2026, 02,04),
            Test.TestVolume.VeryHigh,
            Test.PersonalUnderstanding.VeryLow,
            4.5
        );
        
        //assert
        Assert.That(manager.Tests.Any(t => t.Title == "Math Tets"));
    }

    [Test]
    public void CheckIfTestWasDeleted()
    {
        //arrange
        var manager = new TestManagement();
        Guid id = new Guid("6db7ac0d-b947-4344-b19c-e7974d862817");
        //act
        manager.TestRemover(id);
        Assert.That(manager.Tests.Any(t => t.Id == id), Is.False, "The Test with the given ID should no longer exist");
    }
}

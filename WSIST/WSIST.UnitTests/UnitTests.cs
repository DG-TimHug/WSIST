using NUnit.Framework;
using WSIST.Engine;

namespace WSIST.UnitTests;

public sealed class UnitTests
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
        Assert.That(manager.Tests.First().Title, Is.EqualTo("Math Tets"));
    }

    [Test]
    public void CheckIfTestWasDeleted()
    {
        //arrange
        var manager = new TestManagement();
        Guid id = new Guid("09bdb11b-94ba-4f61-b28d-ccb77f51711a");
        //act
        manager.TestRemover(id);
        var result = false;
        foreach (var test in manager.Tests)
        {
            if (test.Id == id)
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }
        Assert.That(result, Is.False, "We Check if There are any Tests that still have that id");
    }
}

using WSIST.Engine;

namespace WSIST.UnitTests;

[TestClass]
public sealed class UnitTests
{
    [TestMethod]
    [Obsolete("Obsolete")]
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
        Assert.Equals("Math Test", manager.Tests.First().Title);
    }
}

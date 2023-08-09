using System.Collections;
using DockManager.Controls.Core.Args;
using FluentAssertions;
using ChangeType = DockManager.Controls.Core.Args.LayoutManagerRegionArgs.ChangeType;

namespace DockManager.Controls.Unit.Tests.Core.Args;

public class LayoutManagerRegionArgsTests
{
    [Theory]
    [ClassData(typeof(LayoutManagerRegionArgsOldDefinitionsData))]
    public void OldDefinitions_ShouldDisplayTheTotalAmountOfDefinitions(string? oldLayoutString, int expectedValue)
    {
        //Arrange
        var args = new LayoutManagerRegionArgs(LayoutManagerRegionArgs.RegionType.Columns)
        {
            OldValue = oldLayoutString
        };
        //Assert
        args.OldDefinitionsCount.Should().Be(expectedValue);
    }
    [Theory]
    [ClassData(typeof(LayoutManagerRegionArgsNewDefinitionsData))]
    public void NewDefinitions_ShouldDisplayTheTotalAmountOfDefinitions(string? newLayoutString, int expectedValue)
    {
        //Arrange
        var args = new LayoutManagerRegionArgs(LayoutManagerRegionArgs.RegionType.Columns)
        {
            NewValue = newLayoutString
        };
        //Assert
        args.NewDefinitionsCount.Should().Be(expectedValue);
    }

    [Theory]
    [ClassData(typeof(LayoutManagerRegionArgsChangeTypeData))]
    public void ChangeType_ShouldReturnCorrectChangeType_WhenPassedInNewAndOldValues(
        string oldLayoutString, 
        string newLayoutString,
        ChangeType expectedResult)
    {
        //Arrange
        var args = new LayoutManagerRegionArgs(LayoutManagerRegionArgs.RegionType.Columns)
        {
            NewValue = newLayoutString,
            OldValue = oldLayoutString
        };

        //Assert
        args.Change.Should().Be(expectedResult);
    }
}

public class LayoutManagerRegionArgsOldDefinitionsData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "*,*,*", 3 };
        yield return new object[] { "50,*", 2 };
        yield return new object[] { null, 1 };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
public class LayoutManagerRegionArgsNewDefinitionsData : LayoutManagerRegionArgsOldDefinitionsData{}

public class LayoutManagerRegionArgsChangeTypeData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "", "*", ChangeType.ValueChanged };
        yield return new object[] { "", "", ChangeType.NoChanges };
        yield return new object[] { null, "", ChangeType.NoChanges };
        yield return new object[] { "*,*", "*", ChangeType.Remove };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
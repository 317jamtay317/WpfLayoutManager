using System.Collections;
using System.Windows;
using DockManager.Controls.Helpers.Builders;
using DockManager.Controls.Unit.Tests.Core.Builders;
using FluentAssertions;

namespace DockManager.Controls.Unit.Tests.Core.Builders;

public class LayoutDefinitionTests
{
    [Theory]
    [ClassData(typeof(LayoutDefinitionEqualsData))]
    public void Equals_ShouldBeTrue_WhenAllItemsInCollectionEqualItemInOtherCollectionAndSameCount(
        string leftString,
        string rightString,
        bool expectedValue)
    {
        // Arrange
        LayoutDefinition left = leftString;
        LayoutDefinition right = rightString;

        // Act

        // Assert
        left.Equals(right).Should().Be(expectedValue);
    }

    [Theory]
    [ClassData(typeof(CheckLayoutLengthSize))]
    public void Create_ShouldParseValuesCorrect_WhenCorrectStringProvided(string layoutString, GridLength expetedLength)
    {
        // Arrange
        // Act
        var definition = LayoutDefinition.Create(layoutString);

        // Assert
        expetedLength.Should().BeEquivalentTo(definition.First());
    }

    [Theory]
    [ClassData(typeof(ValidLayoutParseData))]
    public void Create_ShouldCreateLayoutCorrectly(string layoutString, int count)
    {
        // Arrange
        // Act
        var layoutDefinition = LayoutDefinition.Create(layoutString);
        // Assert
        layoutDefinition.Count.Should().Be(count);
    }

    [Theory]
    [ClassData(typeof(LayoutDefinitionIsValidTestData))]
    public void IsValidString(string layoutString, bool expectedResult)
    {
        // Arrange
        // Act
        var isValid = LayoutDefinition.IsValidString(layoutString);

        // Assert
        isValid.Should().Be(expectedResult);
    }

    [Fact]
    public void ToString_ShouldReturnSameValueCreatedWith()
    {
        //Arrange
        var expectedString = "auto,50,100,*,2*";
        var definition = LayoutDefinition.Create(expectedString);

        //Act
        var actualString = definition.ToString();

        //Assert
        expectedString.Should().BeEquivalentTo(actualString);
    }
    
}

public class LayoutDefinitionIsValidTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "bad string", false };
        yield return new object[] { "*,*,auto", true };
        yield return new object[] { "2*", true };
        yield return new object[] { "50", true };
        yield return new object[] { string.Empty, true };
        yield return new object[] { null, false };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
public class CheckLayoutLengthSize : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "*", new GridLength(1, GridUnitType.Star) };
        yield return new object[] { "2*", new GridLength(2, GridUnitType.Star) };
        yield return new object[] { "auto", new GridLength() };
        yield return new object[] {"50", new GridLength(50) };
        yield return new object[] {string.Empty, new GridLength() };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
public class ValidLayoutParseData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "auto,*" , 2};
        yield return new object[] { "auto,*,*", 3 };
        yield return new object[] { "auto,2*,*", 3 };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class InvalidLayoutParseData : IEnumerable<string>
{
    public IEnumerator<string> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class LayoutDefinitionEqualsData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { "*,*", "*, *", true };
        yield return new object[] { "*,auto", "*,auto", true };
        yield return new object[] { "*,auto", "*,50", false };
        yield return new object[] { null, "", true };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
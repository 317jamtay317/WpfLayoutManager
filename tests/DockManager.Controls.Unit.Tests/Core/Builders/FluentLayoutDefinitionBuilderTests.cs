using System.Windows;
using DockManager.Controls.Helpers.Builders;
using FluentAssertions;

namespace DockManager.Controls.Unit.Tests.Core.Builders;

public class FluentLayoutDefinitionBuilderTests
{
    [Fact]
    public void Build_ShouldReturnCorrectLayoutDefinition_WhenCorrectStringPassed()
    {
        // Arrange
        var builder = new FluentLayoutDefinitionBuilder("*");

        // Act
        var definition = builder.Build();

        // Assert

        definition.Should().BeEquivalentTo(LayoutDefinition.Create("*"));
    }

    [Fact]
    public void Build_ShouldAllowAddingMultipleDefinitionsUsingTheFluentApi()
    {
        // Arrange
        var builder = new FluentLayoutDefinitionBuilder();

        // Act
        builder.AddDefinition("*").AddDefinition("50").AddDefinition("auto");
        var definition = builder.Build();

        // Assert
        definition.Should().BeEquivalentTo(LayoutDefinition.Create("*,50,auto"));
    }

    [Fact]
    public void Build_ShouldAddGridLength()
    {
        //Arrange
        var builder = new FluentLayoutDefinitionBuilder();

        //Act
        builder.AddDefinition(new GridLength(50));
        var definition = builder.Build();

        //Assert
        definition.Should().BeEquivalentTo(LayoutDefinition.Create("50"));
    }
}
using System.Windows;
using System.Windows.Controls;
using DockManager.Controls.Core.Behaviors;
using FluentAssertions;

namespace DockManager.Controls.Unit.Tests.Core.Behaviors;

public class GridBehaviorTests
{
    [StaFact]
    public void Rows_ShouldAddCorrectRows_WhenValidStringPassedIn()
    {
        //Arrange
        var grid = new Grid();
        var behavior = new GridBehavior();
        behavior.Attach(grid);
        var layoutString = "auto,*,auto";

        grid.RowDefinitions.Should().HaveCount(1);
        //Act
        behavior.Rows = layoutString;

        //Assert
        grid.RowDefinitions.Should().HaveCount(3);
        grid.RowDefinitions[0].Height
            .Should()
            .BeEquivalentTo(new RowDefinition() { Height = new () }.Height);
        grid.RowDefinitions[1].Height
            .Should()
            .BeEquivalentTo(new RowDefinition() { Height = new (1 ,GridUnitType.Star) }.Height);
        grid.RowDefinitions[2].Height
            .Should()
            .BeEquivalentTo(new RowDefinition() { Height = new () }.Height);
    }
    
    [StaFact]
    public void Columns_ShouldAddCorrectRows_WhenValidStringPassedIn()
    {
        //Arrange
        var grid = new Grid();
        var behavior = new GridBehavior();
        behavior.Attach(grid);
        var layoutString = "auto,*,auto";

        grid.ColumnDefinitions.Should().HaveCount(1);
        //Act
        behavior.Columns = layoutString;

        //Assert
        grid.ColumnDefinitions.Should().HaveCount(3);
        grid.ColumnDefinitions[0].Width
            .Should()
            .BeEquivalentTo(new ColumnDefinition() { Width = new () }.Width);
        grid.ColumnDefinitions[1].Width
            .Should()
            .BeEquivalentTo(new ColumnDefinition() { Width = new (1 ,GridUnitType.Star) }.Width);
        grid.ColumnDefinitions[2].Width
            .Should()
            .BeEquivalentTo(new ColumnDefinition() { Width = new () }.Width);
    }
}
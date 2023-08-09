using System.Linq;
using System.Text;
using System.Windows;

namespace DockManager.Controls.Helpers.Builders;

internal class FluentLayoutDefinitionBuilder
{
    public FluentLayoutDefinitionBuilder()
    {
    }

    public FluentLayoutDefinitionBuilder(string currentString)
    {
        _stringBuilder.Append(currentString);
    }

    public FluentLayoutDefinitionBuilder(LayoutDefinition definition)
        :this(definition?.ToString()??string.Empty)
    {
    }

    public FluentLayoutDefinitionBuilder AddDefinition(string length)
    {
        Append(length);
        return this;
    }

    public FluentLayoutDefinitionBuilder AddDefinition(GridLength length)
    {
        Append(length.ToString());
        return this;
    }

    public FluentLayoutDefinitionBuilder AddAutoDefinition()
    {
        Append(string.Empty);
        return this;
    }

    private void Append(string definition)
    {
        if (_stringBuilder.Length > 0 && _stringBuilder.ToString().Last() != ',')
        {
            _stringBuilder.Append($",{definition}");
        }
        else
        {
            _stringBuilder.Append(definition);
        }
    }

    public LayoutDefinition Build()
    {
        var layoutString = _stringBuilder.ToString();

        return LayoutDefinition.Create(layoutString);
    }

    private StringBuilder _stringBuilder = new();
}
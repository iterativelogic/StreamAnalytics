using StreamAnalytics.System.Constants;

namespace StreamAnalytics.System.Models.Tags.Values
{
  public class TagValue
  {
    private object _value;
    private TagValueDataType _dataType;

    public TagValue(bool value)
    {
      _dataType = TagValueDataType.Boolean;
      _value = value;
    }

    public TagValue(int value)
    {
      _dataType = TagValueDataType.Numerical;
      _value = value;
    }

    private TagValue(string value)
    {
      _dataType = TagValueDataType.String;
      _value = value;
    }     
        
    public Guid DataTypeId => _dataType switch {
      TagValueDataType.Boolean => DataTypes.Boolean,
      TagValueDataType.Numerical => DataTypes.Boolean,
      TagValueDataType.String => DataTypes.Boolean,
      _ => Guid.Empty
    };

    public string StringValue => ToString();

    public T GetValue<T>() => (T)_value;

    public override string ToString()
    {
      return _value.ToString();
    }

    private static TagValue CreateStringValue(string value)
    {
      if (bool.TryParse(value, out bool boolValue))
        return new TagValue(boolValue);
      else if (int.TryParse(value, out int intValue))
        return new TagValue(intValue);
      else
        return new TagValue(value);
    }

    public static implicit operator TagValue(bool value) => new TagValue(value);
    public static implicit operator TagValue(int value) => new TagValue(value);
    public static implicit operator TagValue(string value) => CreateStringValue(value);
  }
}

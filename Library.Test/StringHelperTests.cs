namespace Library.Test;

public class StringHelperTests
{
    [Fact]
    public void JsonTest()
    {
        var json = "{\"key\": \"value\"}";
        var invalid = "invalid string";

        Assert.True(StringHelper.IsJson(json));
        Assert.False(StringHelper.IsJson(invalid));
    }

    [Fact]
    public void XmlTest()
    {
        var xml = "<root><key>value</key></root>";
        var invalid = "invalid string";

        Assert.True(StringHelper.IsXml(xml));
        Assert.False(StringHelper.IsXml(invalid));
    }
}

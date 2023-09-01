namespace TestStringHelper;

public class TestStringEnumerableExtensions {
    [SetUp]
    public void Setup() { }

    [Test]
    public void TestCommonPrefix() {
        Assert.That(new[] { "SomeValue", "SomeValue1", "SomeValue2" }.CommonPrefix(), Is.EqualTo("SomeValue"));
        Assert.That(new[] { "SomeValue", "SomValue1", "SoValue2" }.CommonPrefix(), Is.EqualTo("So"));
        Assert.That(new[] { "SomeValue", "XSomeValue1", "YSomeValue2" }.CommonPrefix(), Is.EqualTo(""));
        Assert.That(Array.Empty<string>().CommonPrefix(), Is.EqualTo(""));
    }
    
    [Test]
    public void TestCommonSuffix() {
        Assert.That(new[] { "SomeValue", "1SomeValue", "2SomeValue" }.CommonSuffix(), Is.EqualTo("SomeValue"));
        Assert.That(new[] { "SomeValue", "1SomValue", "2SoValue" }.CommonSuffix(), Is.EqualTo("Value"));
        Assert.That(new[] { "Hello World!", "Hello Universe!" }.CommonSuffix(), Is.EqualTo("!"));
        Assert.That(Array.Empty<string>().CommonSuffix(), Is.EqualTo(""));
    }

    [Test]
    public void TestToStream()
    {
        var buffer = new byte[1024];
        var seq = new[] {"Hello", "World"};
        var stream = seq.ToStream();
        Assert.That(stream, Is.Not.Null);
        var bytesRead = stream.Read(buffer, 0, buffer.Length);
        Assert.That(bytesRead, Is.EqualTo(11));
        Assert.That(Encoding.UTF8.GetString(buffer, 0, bytesRead), Is.EqualTo("Hello\nWorld"));
        stream = seq.ToStream();
        bytesRead = stream.Read(buffer, 0, 4);
        Assert.That(bytesRead, Is.EqualTo(4));
        Assert.That(Encoding.UTF8.GetString(buffer, 0, bytesRead), Is.EqualTo("Hell"));
        bytesRead = stream.Read(buffer, 0, 4);
        Assert.That(bytesRead, Is.EqualTo(4));
        Assert.That(Encoding.UTF8.GetString(buffer, 0, bytesRead), Is.EqualTo("o\nWo"));
        bytesRead = stream.Read(buffer, 0, 4);
        Assert.That(bytesRead, Is.EqualTo(3));
        Assert.That(Encoding.UTF8.GetString(buffer, 0, bytesRead), Is.EqualTo("rld"));
    }
}
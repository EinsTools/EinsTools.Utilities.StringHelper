namespace TestStringHelper;

public class TestStringExtensions {
    [SetUp]
    public void Setup() { }

    [Test]
    public void TestDefaultIfNullOrEmpty() {
        Assert.That("".DefaultIfNullOrEmpty("default"), Is.EqualTo("default"));
        Assert.That(" ".DefaultIfNullOrEmpty("default"), Is.EqualTo(" "));
        Assert.That("test".DefaultIfNullOrEmpty("default"), Is.EqualTo("test"));
        Assert.That(((string?)null).DefaultIfNullOrEmpty("default"), Is.EqualTo("default"));
    }

    [Test]
    public void TestDefaultIfNullOrWhiteSpace() {
        Assert.That("".DefaultIfNullOrWhiteSpace("default"), Is.EqualTo("default"));
        Assert.That(" ".DefaultIfNullOrWhiteSpace("default"), Is.EqualTo("default"));
        Assert.That("test".DefaultIfNullOrWhiteSpace("default"), Is.EqualTo("test"));
        Assert.That(((string?)null).DefaultIfNullOrWhiteSpace("default"), Is.EqualTo("default"));
        Assert.That("".DefaultIfNullOrWhiteSpace(), Is.EqualTo(""));
        Assert.That(" ".DefaultIfNullOrWhiteSpace(), Is.EqualTo(""));
        Assert.That("test".DefaultIfNullOrWhiteSpace(), Is.EqualTo("test"));
        Assert.That(((string?)null).DefaultIfNullOrWhiteSpace(), Is.EqualTo(""));
    }

    [Test]
    public void TestDefaultIfNull() {
        Assert.That("".DefaultIfNull("default"), Is.EqualTo(""));
        Assert.That(" ".DefaultIfNull("default"), Is.EqualTo(" "));
        Assert.That("test".DefaultIfNull("default"), Is.EqualTo("test"));
        Assert.That(((string?)null).DefaultIfNull("default"), Is.EqualTo("default"));
        Assert.That("".DefaultIfNull(), Is.EqualTo(""));
        Assert.That(" ".DefaultIfNull(), Is.EqualTo(" "));
        Assert.That("test".DefaultIfNull(), Is.EqualTo("test"));
        Assert.That(((string?)null).DefaultIfNull(), Is.EqualTo(""));
    }

    private record TestRecord(string? Key1, string? Key2);

    [Test]
    public void TestDeserialize1() {
        var json = @"
{
    ""Key1"": ""Value1"",
    ""Key2"": ""Value2""
}
";
        var obj = json.Deserialize<TestRecord>();
        Assert.That(obj.Key1, Is.EqualTo("Value1"));
        Assert.That(obj.Key2, Is.EqualTo("Value2"));
    }

    [Test]
    public void TestDeserialize2() {
        var json = @"
{
    ""Key2"": ""Value2"",
    ""Key3"": ""Value3""
}";
        var obj = json.Deserialize<TestRecord>();
        Assert.That(obj.Key1, Is.Null);
        Assert.That(obj.Key2, Is.EqualTo("Value2"));
    }
    
    [Test]
    public void TestDeserialize3() {
        var json = @"
{
    ""key1"": ""Value1"",
    ""key2"": ""Value2""
}";
        var obj = json.Deserialize<TestRecord>(new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
        Assert.That(obj.Key1, Is.EqualTo("Value1"));
        Assert.That(obj.Key2, Is.EqualTo("Value2"));
    }
    
    [Test]
    public void TestDeserializeCaseInsensitive1() {
        var json = @"
{
    ""key1"": ""Value1"",
    ""key2"": ""Value2""
}";
        var obj = json.DeserializeCaseInsensitive<TestRecord>();
        Assert.That(obj.Key1, Is.EqualTo("Value1"));
        Assert.That(obj.Key2, Is.EqualTo("Value2"));
    }
    
    [Test]
    public void TestDeserializeCaseInsensitive2() {
        var json = @"
{
    ""Key1"": ""Value1"",
    ""Key2"": ""Value2""
}";
        var obj = json.DeserializeCaseInsensitive<TestRecord>();
        Assert.That(obj.Key1, Is.EqualTo("Value1"));
        Assert.That(obj.Key2, Is.EqualTo("Value2"));
    }
    
    

    [Test]
    public void TestSerialize() {
        var data = new TestRecord("Value1", "Value2");
        var json = data.Serialize();
        var dataFromJson = json.Deserialize<TestRecord>();
        Assert.That(dataFromJson.Key1, Is.EqualTo("Value1"));
        Assert.That(dataFromJson.Key2, Is.EqualTo("Value2"));
    }
    
    [Test]
    public void TestToBase64() {
        Assert.That("".ToBase64(), Is.EqualTo(""));
        Assert.That("test".ToBase64(), Is.EqualTo("dGVzdA=="));
        Assert.That("test".ToBase64(Encoding.ASCII), Is.EqualTo("dGVzdA=="));
        Assert.That("test".ToBase64(Encoding.UTF8), Is.EqualTo("dGVzdA=="));
        Assert.That("test".ToBase64(Encoding.Unicode), Is.EqualTo("dABlAHMAdAA="));
    }
    
    [Test]
    public void TestFromBase64() {
        Assert.That("".FromBase64(), Is.EqualTo(""));
        Assert.That("dGVzdA==".FromBase64(), Is.EqualTo("test"));
        Assert.That("dGVzdA==".FromBase64(Encoding.ASCII), Is.EqualTo("test"));
        Assert.That("dGVzdA==".FromBase64(Encoding.UTF8), Is.EqualTo("test"));
        Assert.That("dABlAHMAdAA=".FromBase64(Encoding.Unicode), Is.EqualTo("test"));
    }
    
    [Test]
    public void TestRemovePrefix() {
        Assert.That("".RemovePrefix(""), Is.EqualTo(""));
        Assert.That("".RemovePrefix("test"), Is.EqualTo(""));
        Assert.That("test".RemovePrefix(""), Is.EqualTo("test"));
        Assert.That("test".RemovePrefix("test"), Is.EqualTo(""));
        Assert.That("test".RemovePrefix("tes"), Is.EqualTo("t"));
        Assert.That("test".RemovePrefix("tesx"), Is.EqualTo("test"));
    }
    
    [Test]
    public void TestRemoveSuffix() {
        Assert.That("".RemoveSuffix(""), Is.EqualTo(""));
        Assert.That("".RemoveSuffix("test"), Is.EqualTo(""));
        Assert.That("test".RemoveSuffix(""), Is.EqualTo("test"));
        Assert.That("test".RemoveSuffix("test"), Is.EqualTo(""));
        Assert.That("test".RemoveSuffix("est"), Is.EqualTo("t"));
        Assert.That("test".RemoveSuffix("xest"), Is.EqualTo("test"));
    }
    
    [Test]
    public void TestRemoveRegex() {
        Assert.That("".RemoveRegex(new Regex("\\d+")), Is.EqualTo(""));
        Assert.That("test".RemoveRegex(new Regex("\\d+")), Is.EqualTo("test"));
        Assert.That("test123".RemoveRegex(new Regex("\\d+")), Is.EqualTo("test"));
        Assert.That("test123a".RemoveRegex(new Regex("\\d+")), Is.EqualTo("testa"));
        Assert.That("test123a456".RemoveRegex(new Regex("\\d+")), Is.EqualTo("testa"));
        Assert.That("1t2e3s4t5".RemoveRegex(new Regex("\\d+")), Is.EqualTo("test"));
    }

    [Test]
    public void TestRemoveRegex1()
    {
        Assert.That("".RemoveRegex("\\d+"), Is.EqualTo(""));
        Assert.That("test".RemoveRegex("\\d+"), Is.EqualTo("test"));
        Assert.That("test123".RemoveRegex("\\d+"), Is.EqualTo("test"));
        Assert.That("test123a".RemoveRegex("\\d+"), Is.EqualTo("testa"));
        Assert.That("test123a456".RemoveRegex("\\d+"), Is.EqualTo("testa"));
        Assert.That("1t2e3s4t5".RemoveRegex("\\d+"), Is.EqualTo("test"));   
    }
    
    [Test]
    public void TestEndsWithNoCase() {
        Assert.That("".EndsWithNoCase(""), Is.True);
        Assert.That("".EndsWithNoCase("test"), Is.False);
        Assert.That("test".EndsWithNoCase(""), Is.True);
        Assert.That("test".EndsWithNoCase("test"), Is.True);
        Assert.That("test".EndsWithNoCase("Test"), Is.True);
        Assert.That("test".EndsWithNoCase("Tes"), Is.False);
        Assert.That("test".EndsWithNoCase("xest"), Is.False);
    }
    
    [Test]
    public void TestStartsWithNoCase() {
        Assert.That("".StartsWithNoCase(""), Is.True);
        Assert.That("".StartsWithNoCase("test"), Is.False);
        Assert.That("test".StartsWithNoCase(""), Is.True);
        Assert.That("test".StartsWithNoCase("test"), Is.True);
        Assert.That("test".StartsWithNoCase("Test"), Is.True);
        Assert.That("test".StartsWithNoCase("tes"), Is.True);
        Assert.That("test".StartsWithNoCase("xest"), Is.False);
    }
    
    [Test]
    public void TestWithDirectorySeparator() {
        Assert.That("".WithDirectorySeparator(), Is.EqualTo(Path.DirectorySeparatorChar.ToString()));
        Assert.That("test".WithDirectorySeparator(), Is.EqualTo("test" + Path.DirectorySeparatorChar));
        Assert.That($"test{Path.DirectorySeparatorChar}".WithDirectorySeparator(), Is.EqualTo("test" + Path.DirectorySeparatorChar));
        Assert.That($"test{Path.AltDirectorySeparatorChar}".WithDirectorySeparator(), Is.EqualTo("test" + Path.AltDirectorySeparatorChar));
        Assert.That("test".WithDirectorySeparator(), Is.EqualTo("test" + Path.DirectorySeparatorChar));
    }
    
    [Test]
    public void TestEndsWithDirectorySeparator() {
        Assert.That("".EndsWithDirectorySeparator(), Is.False);
        Assert.That("test".EndsWithDirectorySeparator(), Is.False);
        Assert.That($"test{Path.DirectorySeparatorChar}".EndsWithDirectorySeparator(), Is.True);
        Assert.That($"test{Path.AltDirectorySeparatorChar}".EndsWithDirectorySeparator(), Is.True);
    }
    
    [Test]
    public void TestWithoutDirectorySeparator() {
        Assert.That("".WithoutDirectorySeparator(), Is.EqualTo(""));
        Assert.That("test".WithoutDirectorySeparator(), Is.EqualTo("test"));
        Assert.That($"test{Path.DirectorySeparatorChar}".WithoutDirectorySeparator(), Is.EqualTo("test"));
        Assert.That($"test{Path.AltDirectorySeparatorChar}".WithoutDirectorySeparator(), Is.EqualTo("test"));
    }

    [Test]
    public void TestSanitizeFileName() {
        var ch = Path.GetInvalidFileNameChars()[0];
        Assert.That($"test{ch}test.txt".SanitizeFileName(), Is.EqualTo("test_test.txt"));
        Assert.That($"test{ch}test.txt".SanitizeFileName('_'), Is.EqualTo("test_test.txt"));
        Assert.That($"test{ch}test.txt".SanitizeFileName('-'), Is.EqualTo("test-test.txt"));
        Assert.That($"test{ch}test.txt".SanitizeFileName(' '), Is.EqualTo("test test.txt"));
        Assert.That($"test.txt".SanitizeFileName(' '), Is.EqualTo("test.txt"));
        Assert.That("Hello/World!".SanitizeFileName(), Is.EqualTo("Hello_World!"));
    }
    
    [Test]
    public void TestContainsNoCase() {
        Assert.That("".ContainsNoCase(""), Is.True);
        Assert.That("".ContainsNoCase("test"), Is.False);
        Assert.That("test".ContainsNoCase(""), Is.True);
        Assert.That("test".ContainsNoCase("test"), Is.True);
        Assert.That("test".ContainsNoCase("Test"), Is.True);
        Assert.That("test".ContainsNoCase("tes"), Is.True);
        Assert.That("test".ContainsNoCase("xest"), Is.False);
    }

    [Test]
    public void TestContainsAny1() {
        Assert.That("".ContainsAny(), Is.False);
        Assert.That("".ContainsAny("test"), Is.False);
        Assert.That("test".ContainsAny(), Is.False);
        Assert.That("test".ContainsAny("test"), Is.True);
        Assert.That("Hello, World!".ContainsAny("Universe"), Is.False);
        Assert.That("Hello, World!".ContainsAny("Universe", "Hello"), Is.True);
        Assert.That("Hello, World!".ContainsAny("Universe", "Hello", "World"), Is.True);
        Assert.That("Hello, World!".ContainsAny("Universe", "World", "Test"), Is.True);
    }

    [Test]
    public void TestContainsAny2() {
        Assert.That("".ContainsAny(StringComparison.OrdinalIgnoreCase), Is.False);
        Assert.That("".ContainsAny(StringComparison.OrdinalIgnoreCase, "test"), Is.False);
        Assert.That("test".ContainsAny(StringComparison.OrdinalIgnoreCase), Is.False);
        Assert.That("test".ContainsAny(StringComparison.OrdinalIgnoreCase, "test"), Is.True);
        Assert.That("test".ContainsAny(StringComparison.OrdinalIgnoreCase, "Test"), Is.True);
        Assert.That("Hello, World!".ContainsAny(StringComparison.OrdinalIgnoreCase, "Universe"), Is.False);
        Assert.That("Hello, World!".ContainsAny(StringComparison.OrdinalIgnoreCase, "Universe", "Hello"), Is.True);
        Assert.That("Hello, World!".ContainsAny(StringComparison.OrdinalIgnoreCase, "Universe", "hello", "world"), Is.True);
    }

    [Test]
    public void TestContainsAny3() {
        Assert.That("".ContainsAny(Array.Empty<string>(), StringComparison.OrdinalIgnoreCase), Is.False);
        Assert.That("".ContainsAny(new [] { "test" }, StringComparison.OrdinalIgnoreCase), Is.False);
        Assert.That("test".ContainsAny(Array.Empty<string>(), StringComparison.OrdinalIgnoreCase), Is.False);
        Assert.That("test".ContainsAny(new [] { "test" }, StringComparison.OrdinalIgnoreCase), Is.True);
        Assert.That("test".ContainsAny(new [] { "Test" }, StringComparison.OrdinalIgnoreCase), Is.True);
        Assert.That("Hello, World!".ContainsAny(new [] { "Universe" }, StringComparison.OrdinalIgnoreCase), Is.False);
        Assert.That("Hello, World!".ContainsAny(new [] { "Universe", "Hello" }, StringComparison.OrdinalIgnoreCase), Is.True);
        Assert.That("Hello, World!".ContainsAny(new [] { "Universe", "hello", "world" }, StringComparison.OrdinalIgnoreCase), Is.True);
    }
    
    [Test]
    public void TestContainsAnyNoCase() {
        Assert.That("".ContainsAnyNoCase(), Is.False);
        Assert.That("".ContainsAnyNoCase("test"), Is.False);
        Assert.That("test".ContainsAnyNoCase(), Is.False);
        Assert.That("test".ContainsAnyNoCase("test"), Is.True);
        Assert.That("Hello, World!".ContainsAnyNoCase("Universe"), Is.False);
        Assert.That("Hello, World!".ContainsAnyNoCase("Universe", "Hello"), Is.True);
        Assert.That("Hello, World!".ContainsAnyNoCase("Universe", "Hello", "World"), Is.True);
        Assert.That("Hello, World!".ContainsAnyNoCase("Universe", "world", "test"), Is.True);
    }

    [Test]
    public void TestTryParseInt()
    {
        Assert.That("".TryParseInt(out var _), Is.False);
        Assert.That("test".TryParseInt(out var _), Is.False);
        Assert.That("123".TryParseInt(out var i), Is.True);
        Assert.That(i, Is.EqualTo(123));
        Assert.That("123.456".TryParseInt(out var _), Is.False);
        Assert.That("12345678901234567890".TryParseInt(out var _), Is.False);
    }

    [Test]
    public void TryParseLong()
    {
        Assert.That("".TryParseLong(out var _), Is.False);
        Assert.That("test".TryParseLong(out var _), Is.False);
        Assert.That("123".TryParseLong(out var i), Is.True);
        Assert.That(i, Is.EqualTo(123));
        Assert.That("123.456".TryParseLong(out var _), Is.False);
        Assert.That(long.MaxValue.ToString().TryParseLong(out i), Is.True);
        Assert.That(i, Is.EqualTo(long.MaxValue));
        Assert.That("12345678901234567890".TryParseInt(out var _), Is.False);
    }
    
    [Test]
    public void TestTryParseBoolean()
    {
        Assert.That("".TryParseBoolean(out var _), Is.False);
        Assert.That("test".TryParseBoolean(out var _), Is.False);
        Assert.That("true".TryParseBoolean(out var b), Is.True);
        Assert.That(b, Is.True);
        Assert.That("false".TryParseBoolean(out b), Is.True);
        Assert.That(b, Is.False);
        Assert.That("True".TryParseBoolean(out b), Is.True);
        Assert.That(b, Is.True);
    }
    
    [Test]
    public void TestTryParseGuid()
    {
        Assert.That("".TryParseGuid(out var _), Is.False);
        Assert.That("test".TryParseGuid(out var _), Is.False);
        Assert.That("123".TryParseGuid(out var _), Is.False);
        Assert.That("123456789012345678901234567890123456".TryParseGuid(out var _), Is.False);
        Assert.That("12345678901234567890123456789012".TryParseGuid(out var g), Is.True);
        Assert.That(g, Is.EqualTo(Guid.Parse("12345678-9012-3456-7890-123456789012")));
        Assert.That("12345678-9012-3456-7890-123456789012".TryParseGuid(out g), Is.True);
        Assert.That(g, Is.EqualTo(Guid.Parse("12345678-9012-3456-7890-123456789012")));
    }

    [Test]
    public void TestBreak()
    {
        Assert.That("".Break('x'), Is.EqualTo(("", "")));
        Assert.That("test".Break('x'), Is.EqualTo(("test", "")));
        Assert.That("testx".Break('x'), Is.EqualTo(("test", "")));
        Assert.That("xtest".Break('x'), Is.EqualTo(("", "test")));
        Assert.That("xtestx".Break('x'), Is.EqualTo(("", "testx")));
        Assert.That("testxtest".Break('x'), Is.EqualTo(("test", "test")));
    }

    [Test]
    public void TestExpandTabs()
    {
        Assert.That("Dies ist ein Test".ExpandTabs(4), Is.EqualTo("Dies ist ein Test"));
        Assert.That("Dies\tist\tein\tTest".ExpandTabs(4), Is.EqualTo("Dies    ist ein Test"));
    }

    [Test]
    public void TestCaseFold()
    {
        Assert.That("Dies ist ein Test".CaseFold(), Is.EqualTo("dies ist ein test"));
        Assert.That("Straße".CaseFold(), Is.EqualTo("strasse"));
        Assert.That("Straße".CaseFold(false), Is.EqualTo("straße"));
    }
    
    [Test]
    public static void TestEditDistance() {
        Assert.That("".EditDistance(""), Is.EqualTo(0));
        Assert.That("".EditDistance("test"), Is.EqualTo(4));
        Assert.That("test".EditDistance(""), Is.EqualTo(4));
        Assert.That("test".EditDistance("test"), Is.EqualTo(0));
        Assert.That("test".EditDistance("test1"), Is.EqualTo(1));
        Assert.That("test".EditDistance("test12"), Is.EqualTo(2));
        Assert.That("test".EditDistance("tes"), Is.EqualTo(1));
        Assert.That("Hello World!".EditDistance("Hello Universe!"), Is.EqualTo(7) );
    }
    
    [Test]
    public static void TestReverse() {
        Assert.That("".Reverse(), Is.EqualTo(""));
        Assert.That("test".Reverse(), Is.EqualTo("tset"));
        Assert.That("test1".Reverse(), Is.EqualTo("1tset"));
        Assert.That("test12".Reverse(), Is.EqualTo("21tset"));
        Assert.That("tes".Reverse(), Is.EqualTo("set"));
    }

    [Test]
    public static void TestCollapseWhiteSpace()
    {
        Assert.That("".CollapseWhitespace(), Is.EqualTo(""));
        Assert.That("test".CollapseWhitespace(), Is.EqualTo("test"));
        Assert.That("test 1".CollapseWhitespace(), Is.EqualTo("test 1"));
        Assert.That("test  1".CollapseWhitespace(), Is.EqualTo("test 1"));
        Assert.That("test   1".CollapseWhitespace(), Is.EqualTo("test 1"));
        Assert.That("test    1".CollapseWhitespace(), Is.EqualTo("test 1"));
    }

    [Test]
    public static void TestParseFileSize()
    {
        Assert.That("1".ParseFileSize(), Is.EqualTo(1L));
        Assert.That("1KB".ParseFileSize(), Is.EqualTo(1024L));
        Assert.That("1MB".ParseFileSize(), Is.EqualTo(1024L * 1024L));
        Assert.That("1GB".ParseFileSize(), Is.EqualTo(1024L * 1024L * 1024L));
        Assert.That("1TB".ParseFileSize(), Is.EqualTo(1024L * 1024L * 1024L * 1024L));
        Assert.That("1PB".ParseFileSize(), Is.EqualTo(1024L * 1024L * 1024L * 1024L * 1024L));
        Assert.That("77".ParseFileSize(), Is.EqualTo(77L));
        Assert.That("77KB".ParseFileSize(), Is.EqualTo(77L * 1024L));
        Assert.That("77MB".ParseFileSize(), Is.EqualTo(77L * 1024L * 1024L));
        Assert.That("77GB".ParseFileSize(), Is.EqualTo(77L * 1024L * 1024L * 1024L));
        Assert.That("77TB".ParseFileSize(), Is.EqualTo(77L * 1024L * 1024L * 1024L * 1024L));
        Assert.That("77PB".ParseFileSize(), Is.EqualTo(77L * 1024L * 1024L * 1024L * 1024L * 1024L));
        Assert.That(" 1 ".ParseFileSize(), Is.EqualTo(1L));
        Assert.That(" 1 KB ".ParseFileSize(), Is.EqualTo(1024L));
        Assert.That(" 1 MB ".ParseFileSize(), Is.EqualTo(1024L * 1024L));
        Assert.That(" 1 GB ".ParseFileSize(), Is.EqualTo(1024L * 1024L * 1024L));
        Assert.That(" 1 TB ".ParseFileSize(), Is.EqualTo(1024L * 1024L * 1024L * 1024L));
        Assert.That(" 1 PB ".ParseFileSize(), Is.EqualTo(1024L * 1024L * 1024L * 1024L * 1024L));
    }
}
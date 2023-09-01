namespace TestStringHelper;

public class TestStringBuilderExtensions {
    [SetUp]
    public void Setup() { }
    
    [Test]
    public static void TestAppendExpandEnvironment() {
        var sb = new StringBuilder();
        var guid = Guid.NewGuid().ToString();
        var value = Guid.NewGuid().ToString();
        Environment.SetEnvironmentVariable(guid, value);
        sb.AppendExpandEnvironment($"Hello, %{guid}%!");
        Assert.That(sb.ToString(), Is.EqualTo($"Hello, {value}!"));
        sb.Clear();
        sb.AppendExpandEnvironment($"Hello, %{guid}%!", new Dictionary<string, string?>(), false);
        Assert.That(sb.ToString(), Is.EqualTo($"Hello, %{guid}%!"));
        sb.Clear();
        var valueNew = Guid.NewGuid().ToString();
        sb.AppendExpandEnvironment($"Hello, %{guid}%!", new Dictionary<string, string?> {{guid, valueNew}});
        Assert.That(sb.ToString(), Is.EqualTo($"Hello, {valueNew}!"));
    }
    
    [Test]
    public static void TestAppendLineExpandEnvironment() {
        var sb = new StringBuilder();
        var guid = Guid.NewGuid().ToString();
        var value = Guid.NewGuid().ToString();
        Environment.SetEnvironmentVariable(guid, value);
        sb.AppendLineExpandEnvironment($"Hello, %{guid}%!");
        Assert.That(sb.ToString(), Is.EqualTo($"Hello, {value}!\n").Or.EqualTo($"Hello, {value}!\r\n"));
        sb.Clear();
        sb.AppendLineExpandEnvironment($"Hello, %{guid}%!", new Dictionary<string, string?>(), false);
        Assert.That(sb.ToString(), Is.EqualTo($"Hello, %{guid}%!\n").Or.EqualTo($"Hello, %{guid}%!\r\n"));
        sb.Clear();
        var valueNew = Guid.NewGuid().ToString();
        sb.AppendLineExpandEnvironment($"Hello, %{guid}%!", new Dictionary<string, string?> {{guid, valueNew}});
        Assert.That(sb.ToString(), Is.EqualTo($"Hello, {valueNew}!\n").Or.EqualTo($"Hello, {valueNew}!\r\n"));
    }
    
    [Test]
    public static void TestRepeat() {
        const string str = "Hello";
        Assert.That(str.Repeat(0), Is.EqualTo(""));
        Assert.That(str.Repeat(1), Is.EqualTo(str));
        Assert.That(str.Repeat(2), Is.EqualTo(str + str));
        Assert.That(str.Repeat(3), Is.EqualTo(str + str + str));
        // Assert that a negative count throws an exception
        Assert.Throws<ArgumentOutOfRangeException>(() => str.Repeat(-1));
    }

    [Test]
    public static void TestIsAllAlphanumeric() {
        Assert.That("Hello".IsAllAlphanumeric(), Is.True);
        Assert.That("Hello123".IsAllAlphanumeric(), Is.True);
        Assert.That("Hello123!".IsAllAlphanumeric(), Is.False);
        Assert.That("Hello123 ".IsAllAlphanumeric(), Is.False);
        Assert.That("Hello123\n".IsAllAlphanumeric(), Is.False);
        Assert.That("123Hallo8910".IsAllAlphanumeric(), Is.True);
    }

    [Test]
    public static void TestIsAllAlphanumericOrUnderscore() {
        Assert.That("Hello".IsAllAlphanumericOrUnderscore(), Is.True);
        Assert.That("Hello123".IsAllAlphanumericOrUnderscore(), Is.True);
        Assert.That("Hello123!".IsAllAlphanumericOrUnderscore(), Is.False);
        Assert.That("Hello123 ".IsAllAlphanumericOrUnderscore(), Is.False);
        Assert.That("Hello123\n".IsAllAlphanumericOrUnderscore(), Is.False);
        Assert.That("Hello123_".IsAllAlphanumericOrUnderscore(), Is.True);
    }

    [Test]
    public static void TestIsAllNumeric() {
        Assert.That("123".IsAllNumeric(), Is.True);
        Assert.That("123.456".IsAllNumeric(), Is.False);
    }

    [Test]
    public static void TestEnsureLeft() {
        Assert.That("Hello".EnsureLeft("Hello"), Is.EqualTo("Hello"));
        Assert.That(" World!".EnsureLeft("Hello"), Is.EqualTo("Hello World!"));
        Assert.That("Hello World!".EnsureLeft("Hello"), Is.EqualTo("Hello World!"));
        Assert.That("Hola World!".EnsureLeft("Hello"), Is.EqualTo("HelloHola World!"));
    }

    [Test]
    public static void TestEnsureRight() {
        Assert.That("Hello".EnsureRight("Hello"), Is.EqualTo("Hello"));
        Assert.That("Hello ".EnsureRight("World!"), Is.EqualTo("Hello World!"));
        Assert.That("Hello World!".EnsureRight("World!"), Is.EqualTo("Hello World!"));
        Assert.That("Hello World! ".EnsureRight("World!"), Is.EqualTo("Hello World! World!"));
    }
    
    [Test]
    public static void TestHtmlEncode() {
        Assert.That("Hello World!".HtmlEncode(), Is.EqualTo("Hello World!"));
        Assert.That("<Hello World!>".HtmlEncode(), Is.EqualTo("&lt;Hello World!&gt;"));
        Assert.That("Hello & World!".HtmlEncode(), Is.EqualTo("Hello &amp; World!"));
    }
    
    [Test]
    public static void TestHtmlDecode() {
        Assert.That("Hello World!".HtmlDecode(), Is.EqualTo("Hello World!"));
        Assert.That("&lt;Hello World!&gt;".HtmlDecode(), Is.EqualTo("<Hello World!>"));
        Assert.That("Hello &amp; World!".HtmlDecode(), Is.EqualTo("Hello & World!"));
    }
    
    [Test]
    public static void TestSurroundWith() {
        Assert.That("Hello World!".SurroundWith("(", ")"), Is.EqualTo("(Hello World!)"));
        Assert.That("Hello World!".SurroundWith("|"), Is.EqualTo("|Hello World!|"));
    }
    
    [Test]
    public static void TestSanitizeFileName() {
        Assert.That("Hello World!".SanitizeFileName(), Is.EqualTo("Hello World!"));
        Assert.That("Hello/World!".SanitizeFileName(), Is.EqualTo("Hello_World!"));
        Assert.That("Hello\\World!".SanitizeFileName(), Is.EqualTo("Hello_World!"));
        Assert.That("Hello:World!".SanitizeFileName(), Is.EqualTo("Hello_World!"));
        Assert.That("Hello*World!".SanitizeFileName(), Is.EqualTo("Hello_World!"));
        Assert.That("Hello?World!".SanitizeFileName(), Is.EqualTo("Hello_World!"));
        Assert.That("Hello\"World!".SanitizeFileName(), Is.EqualTo("Hello_World!"));
        Assert.That("Hello<World!".SanitizeFileName(), Is.EqualTo("Hello_World!"));
        Assert.That("Hello>World!".SanitizeFileName(), Is.EqualTo("Hello_World!"));
        Assert.That("Hello|World!".SanitizeFileName(), Is.EqualTo("Hello_World!"));
    }
}
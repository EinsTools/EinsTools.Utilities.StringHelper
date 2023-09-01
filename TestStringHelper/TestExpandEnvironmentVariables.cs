namespace TestStringHelper;

public class TestExpandEnvironmentVariables {
    [SetUp]
    public void Setup() { }

    [Test]
    public void TestSimple() {
        var str = "Hello, %USERNAME%!";
        var dict = new Dictionary<string, string?> {
            ["USERNAME"] = "World"
        };
        var result = str.ExpandEnvironmentVariables(dict, false);
        Assert.That(result, Is.EqualTo("Hello, World!"));
    }
    
    [Test]
    public void TestSystem() {
        var guid = Guid.NewGuid().ToString();
        Environment.SetEnvironmentVariable(guid, "World");
        var str = $"Hello, %{guid}%!";
        var dict = new Dictionary<string, string?> {
            ["USERNAME"] = "World"
        };
        var result = str.ExpandEnvironmentVariables(dict);
        Assert.That(result, Is.EqualTo("Hello, World!"));
    }
    
    [Test]
    public void TestSystem2() {
        var guid = Guid.NewGuid().ToString();
        Environment.SetEnvironmentVariable(guid, "World");
        var str = $"Hello, %{guid}%!";
        var dict = new Dictionary<string, string?> {
            ["USERNAME"] = "World"
        };
        var result = str.ExpandEnvironmentVariables(dict, false);
        Assert.That(result, Is.EqualTo($"Hello, %{guid}%!"));
    }
    
    [Test]
    public void TestSystem3() {
        var guid = Guid.NewGuid().ToString();
        Environment.SetEnvironmentVariable(guid, "XYZ");
        var str = $"Hello, %{guid}%!";
        var dict = new Dictionary<string, string?> {
            [guid] = "World"
        };
        var result = str.ExpandEnvironmentVariables(dict, true);
        Assert.That(result, Is.EqualTo($"Hello, World!"));
    }
    
    [Test]
    public void TestNonExistentValue() {
        var guid = Guid.NewGuid().ToString();
        var str = $"Hello, %{guid}%!";
        var dict = new Dictionary<string, string?> {
            
        };
        var result = str.ExpandEnvironmentVariables(dict, false);
        Assert.That(result, Is.EqualTo($"Hello, %{guid}%!"));
    }
    
    [Test]
    public void TestUnclosed() {
        var str = "Hello, %USERNAME!";
        var dict = new Dictionary<string, string?> {
            ["USERNAME"] = "World"
        };
        var result = str.ExpandEnvironmentVariables(dict, false);
        Assert.That(result, Is.EqualTo($"Hello, %USERNAME!"));
    }
    
    [Test]
    public void TestUnclosedAtTheBeginning() {
        var str = "%USERNAME!";
        var dict = new Dictionary<string, string?> {
            ["USERNAME"] = "World"
        };
        var result = str.ExpandEnvironmentVariables(dict, false);
        Assert.That(result, Is.EqualTo($"%USERNAME!"));
    }
    
    [Test]
    public void TestUnclosedAtTheEnd() {
        var str = "Hello, %";
        var dict = new Dictionary<string, string?> {
            ["USERNAME"] = "World"
        };
        var result = str.ExpandEnvironmentVariables(dict, false);
        Assert.That(result, Is.EqualTo($"Hello, %"));
    }
    
    [Test]
    public void TestEmpty() {
        var str = "";
        var dict = new Dictionary<string, string?> {
            ["USERNAME"] = "World"
        };
        var result = str.ExpandEnvironmentVariables(dict, false);
        Assert.That(result, Is.EqualTo($""));
    }
    
    [Test]
    public void TestSimple1() {
        var variables = new Dictionary<string, string?> { { "MYTMP", "C:\\Users\\JohnDoe\\AppData\\Local\\Temp" } };
        var result = "%MYTMP%".ExpandEnvironmentVariables(variables); 
        Assert.That(result, Is.EqualTo("C:\\Users\\JohnDoe\\AppData\\Local\\Temp")); 
    }
    
    [Test]
    public void TestNonExistingVariable() {
        var variables = new Dictionary<string, string?> { { "MYTMP", "C:\\Users\\JohnDoe\\AppData\\Local\\Temp" } };
        var result = "%MYTMP2%".ExpandEnvironmentVariables(variables); 
        Assert.That(result, Is.EqualTo("%MYTMP2%")); 
    }
}
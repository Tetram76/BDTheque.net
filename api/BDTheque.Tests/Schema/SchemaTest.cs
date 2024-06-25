namespace BDTheque.Tests.Schema;

public class SchemaTest
{
    [Fact]
    public async Task SchemaChangeTest()
    {
        ISchema schema = await TestServices.Executor.GetSchemaAsync(default);
        schema.ToString().Should().MatchSnapshot();
    }
}

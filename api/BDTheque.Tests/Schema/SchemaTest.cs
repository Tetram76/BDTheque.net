namespace BDTheque.Tests.Schema;

using BDTheque.Tests.Helpers;

public class SchemaTest
{
    [Fact]
    public async Task SchemaChangeTest()
    {
        ISchema schema = await TestServices.Executor.GetSchemaAsync(default);
        schema.ToString().Should().MatchSnapshot();
    }
}

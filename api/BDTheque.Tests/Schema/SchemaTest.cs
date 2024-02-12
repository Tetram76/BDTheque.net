namespace BDTheque.Tests.Schema;

using BDTheque.Tests.Helpers;
using HotChocolate.Types;

public class SchemaTest
{
    [Fact]
    public async Task SchemaChangeTest()
    {
        ISchema schema = await TestServices.Executor.GetSchemaAsync(default);
        schema.ToString().MatchSnapshot();
    }
}

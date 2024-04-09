namespace Library.Test;

public class RepositoryManagerTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public RepositoryManagerTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task RegisterRetrieveTest()
    {
        var manager = new RepositoryManager(_fixture.Db);

        await manager.Register("item1", "{\"key\": \"value\"}", 1);
        var retrievedItem = await manager.Retrieve("item1");

        Assert.True(retrievedItem == "{\"key\": \"value\"}");
    }

    [Fact]
    public async Task GetTypeTest()
    {
        var manager = new RepositoryManager(_fixture.Db);

        await manager.Register("item2", "{\"key\": \"value\"}", 1);
        var type = await manager.GetType("item2");

        Assert.True(type == 1);
    }

    [Fact]
    public async Task DeregisterTest()
    {
        var manager = new RepositoryManager(_fixture.Db);

        await manager.Register("item3", "{\"key\": \"value\"}", 1);
        await manager.Deregister("item3");

        await Assert.ThrowsAsync<KeyNotFoundException>(async () => await manager.Retrieve("item1"));
    }

    [Fact]
    public async Task RegisterDublicateTest()
    {
        var manager = new RepositoryManager(_fixture.Db);

        await manager.Register("item4", "{\"key\": \"value\"}", 1);

        var exception = await Assert.ThrowsAsync<ArgumentException>(async () => await manager.Register("item4", "{\"key\": \"value\"}", 1));
        Assert.Contains("Item already exists in the db.", exception.Message);
    }
}
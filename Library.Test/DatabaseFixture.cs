namespace Library.Test;

public class DatabaseFixture : IDisposable
{
    public AppDbContext Db { get; set; }

    public DatabaseFixture()
    {
        Db = new AppDbContext();
        Db.Database.EnsureCreated();
    }

    public void Dispose()
    {
        Db.Database.EnsureDeleted();
        Db.Dispose();
    }
}

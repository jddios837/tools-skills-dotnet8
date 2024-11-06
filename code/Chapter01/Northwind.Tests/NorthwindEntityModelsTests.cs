using Northwind.EntityModels;

namespace Northwind.Tests;

public class NorthwindEntityModelsTests
{
    [Fact]
    public void Can_Connect_Is_True()
    {
        using (NorthwindContext db = new()) // arrange
        {
            bool canConnect = db.Database.EnsureCreated(); // act
            Assert.True(canConnect);
        }
    }
}
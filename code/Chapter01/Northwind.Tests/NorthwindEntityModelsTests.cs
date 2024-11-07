using Northwind.EntityModels;

namespace Northwind.Tests;

public class NorthwindEntityModelsTests
{
    public NorthwindEntityModelsTests()
    {
        Environment.SetEnvironmentVariable("DB_USER_ID", "sa");
        Environment.SetEnvironmentVariable("DB_PASSWORD", "Tetra714217");
    }
    
    [Fact]
    public void Can_Connect_Is_True()
    {
        using (NorthwindContext db = new()) // arrange
        {
            bool canConnect = db.Database.CanConnect(); // act
            Assert.True(canConnect);
        }
    }

    [Fact]
    public void ProviderIsSqlServer()
    {
        using (NorthwindContext db = new())
        {
            string? provider = db.Database.ProviderName;
            Assert.Equal("Microsoft.EntityFrameworkCore.SqlServer", provider);
        }
    }

    [Fact]
    public void ProductIdIsChai()
    {
        using (NorthwindContext db = new())
        {
            Product? product = db?.Products?.Single(p => p.ProductId == 1);
            Assert.Equal("Chai", product?.ProductName);
        }
    }
}
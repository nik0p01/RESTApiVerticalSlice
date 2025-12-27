using NetArchTest.Rules;

using Xunit;

namespace RESTApiVerticalSlice.Tests;

public class ProductArchitectureTests
{
    [Fact]
    public void Create_Should_Not_Depend_On_Other_Product_Features()
    {
        var result = Types
            .InNamespace("RESTApiVerticalSlice.Features.Products.Create")
            .ShouldNot()
            .HaveDependencyOnAny(
                "RESTApiVerticalSlice.Features.Products.Delete",
                "RESTApiVerticalSlice.Features.Products.Update",
                "RESTApiVerticalSlice.Features.Products.GetAll",
                "RESTApiVerticalSlice.Features.Products.GetById")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}

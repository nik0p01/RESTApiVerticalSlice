using RESTApiVerticalSlice.Features.Products.Data;
using RESTApiVerticalSlice.Features.Products.Models;

namespace RESTApiVerticalSlice.Features.Products.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;

    public ProductService(IProductRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<Product>> GetAllAsync() => _repo.GetAllAsync();

    public Task<Product?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);

    public async Task<Product> CreateAsync(ProductDto dto)
    {
        // business rules can be applied here
        var product = new Product(Guid.NewGuid(), dto.Name, dto.Price);
        return await _repo.CreateAsync(product);
    }

    public Task<bool> UpdateAsync(Guid id, ProductDto dto)
    {
        // business rules, validation, events, etc.
        var product = new Product(id, dto.Name, dto.Price);
        return _repo.UpdateAsync(product);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        // e.g. check references before delete
        return _repo.DeleteAsync(id);
    }
}

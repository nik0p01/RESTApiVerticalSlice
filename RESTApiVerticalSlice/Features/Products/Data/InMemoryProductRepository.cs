using RESTApiVerticalSlice.Features.Products.Models;

namespace RESTApiVerticalSlice.Features.Products.Data;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _items =
    [
        new Product(Guid.Parse("11111111-1111-1111-1111-111111111111"), "Sample A", 10.0m),
        new Product(Guid.Parse("22222222-2222-2222-2222-222222222222"), "Sample B", 20.0m),
    ];

    public Task<Product> CreateAsync(Product product)
    {
        _items.Add(product);
        return Task.FromResult(product);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var existing = _items.FirstOrDefault(x => x.Id == id);
        if (existing is null)
            return Task.FromResult(false);
        _items.Remove(existing);
        return Task.FromResult(true);
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Product>>(_items);
    }

    public Task<Product?> GetByIdAsync(Guid id)
    {
        var existing = _items.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(existing);
    }

    public Task<bool> UpdateAsync(Product product)
    {
        var idx = _items.FindIndex(x => x.Id == product.Id);
        if (idx == -1)
            return Task.FromResult(false);
        _items[idx] = product;
        return Task.FromResult(true);
    }
}

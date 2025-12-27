using RESTApiVerticalSlice.Storage.Models;

namespace RESTApiVerticalSlice.Storage;

public class InMemoryProductStorage
{
    private readonly List<ProductEntity> _items = new()
    {
        new ProductEntity(Guid.Parse("11111111-1111-1111-1111-111111111111"), "Sample A", 10.0m),
        new ProductEntity(Guid.Parse("22222222-2222-2222-2222-222222222222"), "Sample B", 20.0m),
    };

    public Task<ProductEntity> CreateAsync(ProductEntity product)
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

    public Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<ProductEntity>>(_items);
    }

    public Task<ProductEntity?> GetByIdAsync(Guid id)
    {
        var existing = _items.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(existing);
    }

    public Task<bool> UpdateAsync(ProductEntity product)
    {
        var idx = _items.FindIndex(x => x.Id == product.Id);
        if (idx == -1)
            return Task.FromResult(false);
        _items[idx] = product;
        return Task.FromResult(true);
    }
}

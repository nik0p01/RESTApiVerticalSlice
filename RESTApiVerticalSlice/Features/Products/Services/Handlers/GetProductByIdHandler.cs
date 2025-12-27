using MediatR;
using RESTApiVerticalSlice.Features.Products.Data;
using RESTApiVerticalSlice.Features.Products.Models;

namespace RESTApiVerticalSlice.Features.Products.Services.Handlers;

public sealed class GetProductByIdQuery : IRequest<ProductResponseDto?>
{
    public Guid Id { get; }
    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResponseDto?>
{
    private readonly IProductRepository _repository;
    public GetProductByIdHandler(IProductRepository repo)
    {
        _repository = repo;
    }

    public async Task<ProductResponseDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id);
        return product is null ? null : new ProductResponseDto(product.Id, product.Name, product.Price);
    }
}

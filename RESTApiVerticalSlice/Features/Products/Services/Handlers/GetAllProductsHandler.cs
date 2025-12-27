using MediatR;
using RESTApiVerticalSlice.Features.Products.Data;
using RESTApiVerticalSlice.Features.Products.Models;

namespace RESTApiVerticalSlice.Features.Products.Services.Handlers;

public sealed class GetAllProductsQuery : IRequest<IEnumerable<Product>> { }

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _repository;

    public GetAllProductsHandler(IProductRepository repo)
    {
        _repository = repo;
    }

    public Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken) => _repository.GetAllAsync();
}

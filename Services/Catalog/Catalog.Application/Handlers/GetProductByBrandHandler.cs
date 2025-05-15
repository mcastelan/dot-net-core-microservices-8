﻿

using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductByBrandHandler : IRequestHandler<GetProductByBrandQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByBrandHandler(IProductRepository productRepository)
    {
        this._productRepository = productRepository;
    }
    public async Task<IList<ProductResponse>> Handle(GetProductByBrandQuery request, CancellationToken cancellationToken)
    {
        var productList = await _productRepository.GetProductsByBrand(request.BrandName);
        var productResponseList = ProductMapper.Mapper.Map<IList<Product>, IList<ProductResponse>>(productList.ToList());
        return productResponseList;
    }
}

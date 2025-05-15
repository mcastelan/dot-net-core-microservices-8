using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsHandler(IProductRepository productRepository)
    {
        this._productRepository = productRepository;
    }
    public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        //var productList = await _productRepository.GetProducts();
        //var productResponseList = ProductMapper.Mapper.Map<IList<Product>, IList<ProductResponse>>(productList.ToList());      
        //return productResponseList;
        var productList = await _productRepository.GetProducts(request.CatalogSpecParams);
        var productResposeList = ProductMapper.Mapper.Map<Pagination<ProductResponse>>(productList);
        return productResposeList;
    }
}

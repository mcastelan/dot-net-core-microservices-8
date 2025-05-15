using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers;

public class GetAllProductTypesHandler : IRequestHandler<GetAllProductTypesQuery, IList<ProductTypeResponse>>
{
    private readonly ITypesRepository _typesRepository;

    public GetAllProductTypesHandler( ITypesRepository typesRepository)
    {
        this._typesRepository = typesRepository;
    }
    public async Task<IList<ProductTypeResponse>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
    {
        var productTypesList = await _typesRepository.GetAllProductTypes();
        var productTypeResponseList = ProductMapper.Mapper.Map<IList<ProductType>, IList<ProductTypeResponse>>(productTypesList.ToList());
        return productTypeResponseList;
    }
}

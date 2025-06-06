﻿using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public class GetAllProductTypesQuery : IRequest<IList<ProductTypeResponse>>
{
}

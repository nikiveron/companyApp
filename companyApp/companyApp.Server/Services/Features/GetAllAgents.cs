using AutoMapper;
using companyApp.Server.Filters;
using companyApp.Server.Filters.Pagination;
using companyApp.Server.Models.DTOs;
using companyApp.Server.Services.Interfaces;
using FluentValidation;
using FluentValidator;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace companyApp.Server.Services.Features;

public class GetAllAgents
{
    // Input 
    public class Query(string route, InfoFilter infoFilter, PaginationFilter paginationFilter) : IRequest<PagedResponse<List<ReadAgentDTO>>>
    {
        public string Route { get; set; } = route;
        public InfoFilter InformFilter { get; set; } = infoFilter;
        public PaginationFilter ValidFilter { get; set; } = paginationFilter;
    }
    //Handler
    public class Handler(IAgentRepository AgentRepository, IUriService uriService, IMapper mapper) : IRequestHandler<Query, PagedResponse<List<ReadAgentDTO>>>
    {
        public async Task<PagedResponse<List<ReadAgentDTO>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var route = request.Route;
            var pageFilter = new PaginationFilter(request.ValidFilter.PageNumber, request.ValidFilter.PageSize);
            var informFilter = new InfoFilter(request.InformFilter.Inn, request.InformFilter.PhoneNumber, request.InformFilter.Email, request.InformFilter    .OgrnFrom, request.InformFilter.OgrnTo, request.InformFilter.Priority);
            var getResult = await AgentRepository.Get(informFilter, pageFilter, cancellationToken);
            var pagedData = getResult.Item2;
            var totalRecords = getResult.Item1;
            var pagedResponse = PaginationHelper.CreatePagedReponse<ReadAgentDTO>(pagedData, pageFilter, totalRecords, uriService, route);
            return pagedResponse;
        }
    }
}

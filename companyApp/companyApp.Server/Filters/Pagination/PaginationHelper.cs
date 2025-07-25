﻿using companyApp.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace companyApp.Server.Filters.Pagination;

public class PaginationHelper
{
    public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, PaginationFilter validFilter, int totalRecords, IUriService uriService, string route)
    {
        var response = new PagedResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
        var totalPages = totalRecords / (double)validFilter.PageSize;
        int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
        response.NextPage =
            validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
            ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize), route)
            : null;
        response.PreviousPage =
            validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
            ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize), route)
            : null;
        response.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);
        response.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);
        response.TotalPages = roundedTotalPages;
        response.TotalRecords = totalRecords;
        return response;
    }
    public async static Task<List<T>> ApplyPagination<T>(IQueryable<T> queryBase, PaginationFilter validFilter, CancellationToken cancellationToken)
    {
        var pageList = await queryBase
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)  
            .Take(validFilter.PageSize)
            .ToListAsync(cancellationToken);
        return pageList;
    }
}

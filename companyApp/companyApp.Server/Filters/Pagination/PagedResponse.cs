﻿namespace companyApp.Server.Filters.Pagination;

public class PagedResponse<T>
{
    public T Data { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Uri FirstPage { get; set; }
    public Uri LastPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public Uri NextPage { get; set; }
    public Uri PreviousPage { get; set; }

    public PagedResponse() { }

    public PagedResponse(T data, int pageNumber, int pageSize)
    {
        this.Data = data;
        this.PageNumber = pageNumber;
        this.PageSize = pageSize;
    }
}

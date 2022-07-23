namespace CodeZero.Domain.Common.Interfaces;

/// <summary>
/// Paged list interface
/// </summary>
public interface IPagedList<T>
{
    //int PageIndex { get; }
    //int PageSize { get; }
    int PageNumber { get; }
    int TotalCount { get; }
    int TotalPages { get; }
    bool HasPreviousPage { get; }
    bool HasNextPage { get; }
    List<T> Items { get; }
}
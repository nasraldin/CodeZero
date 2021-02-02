using CodeZero.Entities;
using System.Collections.Generic;
using System.ComponentModel;

namespace CodeZero.Domain.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class ListExtensions
    {
        public static PaginatedList<T> ToPaginatedList<T>(this List<T> list, int pageIndex, int pageSize, int total)
        {
            return new PaginatedList<T>(list, pageIndex, pageSize, total);
        }
    }
}

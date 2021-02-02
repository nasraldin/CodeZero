// -----------------------------------------------------------------------
//  <copyright file="IPagedList.cs" company="Profily">
//      Copyright (c) 2017 Profily Corporation.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <date>10/21/2017 2:14 AM</date>
// -----------------------------------------------------------------------

using System.Collections.Generic;

namespace Profily.Core.Interfaces
{
    /// <summary>
    ///     Paged list interface
    /// </summary>
    public interface IPagedList<T> : IList<T>
    {
        int PageIndex { get; }
        int PageSize { get; }
        int TotalCount { get; }
        int TotalPages { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
    }
}
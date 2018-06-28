//  <copyright file="EfDynamicFiltersUnitOfWorkFilterExecuter.cs" project="CodeZero.EntityFramework" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Data.Entity;
using CodeZero.Domain.Uow;
using CodeZero.Extensions;
using CodeZero.Reflection;
using EntityFramework.DynamicFilters;

namespace CodeZero.EntityFramework.Uow
{
    public class EfDynamicFiltersUnitOfWorkFilterExecuter : IEfUnitOfWorkFilterExecuter
    {
        public void ApplyDisableFilter(IUnitOfWork unitOfWork, string filterName)
        {
            foreach (var activeDbContext in unitOfWork.As<EfUnitOfWork>().GetAllActiveDbContexts())
            {
                activeDbContext.DisableFilter(filterName);
            }
        }

        public void ApplyEnableFilter(IUnitOfWork unitOfWork, string filterName)
        {
            foreach (var activeDbContext in unitOfWork.As<EfUnitOfWork>().GetAllActiveDbContexts())
            {
                activeDbContext.EnableFilter(filterName);
            }
        }

        public void ApplyFilterParameterValue(IUnitOfWork unitOfWork, string filterName, string parameterName, object value)
        {
            foreach (var activeDbContext in unitOfWork.As<EfUnitOfWork>().GetAllActiveDbContexts())
            {
                if (TypeHelper.IsFunc<object>(value))
                {
                    activeDbContext.SetFilterScopedParameterValue(filterName, parameterName, (Func<object>)value);
                }
                else
                {
                    activeDbContext.SetFilterScopedParameterValue(filterName, parameterName, value);
                }
            }
        }

        public void ApplyCurrentFilters(IUnitOfWork unitOfWork, DbContext dbContext)
        {
            foreach (var filter in unitOfWork.Filters)
            {
                if (filter.IsEnabled)
                {
                    dbContext.EnableFilter(filter.FilterName);
                }
                else
                {
                    dbContext.DisableFilter(filter.FilterName);
                }

                foreach (var filterParameter in filter.FilterParameters)
                {
                    if (TypeHelper.IsFunc<object>(filterParameter.Value))
                    {
                        dbContext.SetFilterScopedParameterValue(filter.FilterName, filterParameter.Key, (Func<object>)filterParameter.Value);
                    }
                    else
                    {
                        dbContext.SetFilterScopedParameterValue(filter.FilterName, filterParameter.Key, filterParameter.Value);
                    }
                }
            }
        }
    }
}

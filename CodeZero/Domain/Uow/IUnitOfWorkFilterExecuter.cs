//  <copyright file="IUnitOfWorkFilterExecuter.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
namespace CodeZero.Domain.Uow
{
    public interface IUnitOfWorkFilterExecuter
    {
        void ApplyDisableFilter(IUnitOfWork unitOfWork, string filterName);
        void ApplyEnableFilter(IUnitOfWork unitOfWork, string filterName);
        void ApplyFilterParameterValue(IUnitOfWork unitOfWork, string filterName, string parameterName, object value);
    }
}

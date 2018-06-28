//  <copyright file="CodeZeroODataController.cs" project="CodeZero.AspNetCore.OData" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using CodeZero.Domain.Uow;
using Microsoft.AspNet.OData;

namespace CodeZero.AspNetCore.OData.Controllers
{
    public abstract class CodeZeroODataController : ODataController
    {
        public IUnitOfWorkManager UnitOfWorkManager { get; set; }
    }
}

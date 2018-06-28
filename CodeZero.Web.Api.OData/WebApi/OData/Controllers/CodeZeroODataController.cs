//  <copyright file="CodeZeroODataController.cs" project="CodeZero.Web.Api.OData" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.OData;
using CodeZero.Domain.Uow;

namespace CodeZero.WebApi.OData.Controllers
{
    public abstract class CodeZeroODataController : ODataController
    {
        public IUnitOfWorkManager UnitOfWorkManager { get; set; }

        protected IUnitOfWorkCompleteHandle UnitOfWorkCompleteHandler { get; private set; }

        protected bool IsDisposed { get; set; }

        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            UnitOfWorkCompleteHandler = UnitOfWorkManager.Begin();
            return base.ExecuteAsync(controllerContext, cancellationToken);
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                UnitOfWorkCompleteHandler.Complete();
                UnitOfWorkCompleteHandler.Dispose();
            }

            IsDisposed = true;

            base.Dispose(disposing);
        }
    }
}
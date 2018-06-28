//  <copyright file="EntityHistoryInterceptor.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using Castle.DynamicProxy;
using System.Linq;

namespace CodeZero.EntityHistory
{
    internal class EntityHistoryInterceptor : IInterceptor
    {
        public IEntityChangeSetReasonProvider ReasonProvider { get; set; }

        public EntityHistoryInterceptor()
        {
            ReasonProvider = NullEntityChangeSetReasonProvider.Instance;
        }

        public void Intercept(IInvocation invocation)
        {
            var methodInfo = invocation.MethodInvocationTarget;
            var useCaseAttribute = methodInfo.GetCustomAttributes(true).OfType<UseCaseAttribute>().FirstOrDefault()
                  ?? methodInfo.DeclaringType.GetCustomAttributes(true).OfType<UseCaseAttribute>().FirstOrDefault();

            if (useCaseAttribute?.Description == null)
            {
                invocation.Proceed();
                return;
            }

            using (ReasonProvider.Use(useCaseAttribute.Description))
            {
                invocation.Proceed();
            }
        }
    }
}

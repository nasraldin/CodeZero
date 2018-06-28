//  <copyright file="AspectAttribute.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Reflection;

namespace CodeZero.Aspects
{
    //THIS NAMESPACE IS WORK-IN-PROGRESS

    internal abstract class AspectAttribute : Attribute
    {
        public Type InterceptorType { get; set; }

        protected AspectAttribute(Type interceptorType)
        {
            InterceptorType = interceptorType;
        }
    }

    internal interface ICodeZeroInterceptionContext
    {
        object Target { get; }

        MethodInfo Method { get; }

        object[] Arguments { get; }

        object ReturnValue { get; }

        bool Handled { get; set; }
    }

    internal interface ICodeZeroBeforeExecutionInterceptionContext : ICodeZeroInterceptionContext
    {

    }


    internal interface ICodeZeroAfterExecutionInterceptionContext : ICodeZeroInterceptionContext
    {
        Exception Exception { get; }
    }

    internal interface ICodeZeroInterceptor<TAspect>
    {
        TAspect Aspect { get; set; }

        void BeforeExecution(ICodeZeroBeforeExecutionInterceptionContext context);

        void AfterExecution(ICodeZeroAfterExecutionInterceptionContext context);
    }

    internal abstract class CodeZeroInterceptorBase<TAspect> : ICodeZeroInterceptor<TAspect>
    {
        public TAspect Aspect { get; set; }

        public virtual void BeforeExecution(ICodeZeroBeforeExecutionInterceptionContext context)
        {
        }

        public virtual void AfterExecution(ICodeZeroAfterExecutionInterceptionContext context)
        {
        }
    }

    internal class Test_Aspects
    {
        internal class MyAspectAttribute : AspectAttribute
        {
            public int TestValue { get; set; }

            public MyAspectAttribute()
                : base(typeof(MyInterceptor))
            {
            }
        }

        internal class MyInterceptor : CodeZeroInterceptorBase<MyAspectAttribute>
        {
            public override void BeforeExecution(ICodeZeroBeforeExecutionInterceptionContext context)
            {
                Aspect.TestValue++;
            }

            public override void AfterExecution(ICodeZeroAfterExecutionInterceptionContext context)
            {
                Aspect.TestValue++;
            }
        }

        public class MyService
        {
            [MyAspect(TestValue = 41)] //Usage!
            public void DoIt()
            {

            }
        }

        public class MyClient
        {
            private readonly MyService _service;

            public MyClient(MyService service)
            {
                _service = service;
            }

            public void Test()
            {
                _service.DoIt();
            }
        }
    }
}

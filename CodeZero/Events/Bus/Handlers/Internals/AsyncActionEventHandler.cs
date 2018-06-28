//  <copyright file="AsyncActionEventHandler.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;
using System.Threading.Tasks;
using CodeZero.Dependency;

namespace CodeZero.Events.Bus.Handlers.Internals
{
    /// <summary>
    /// This event handler is an adapter to be able to use an action as <see cref="IAsyncEventHandler{TEventData}"/> implementation.
    /// </summary>
    /// <typeparam name="TEventData">Event type</typeparam>
    internal class AsyncActionEventHandler<TEventData> :
        IAsyncEventHandler<TEventData>,
        ITransientDependency
    {
        /// <summary>
        /// Function to handle the event.
        /// </summary>
        public Func<TEventData, Task> Action { get; private set; }

        /// <summary>
        /// Creates a new instance of <see cref="AsyncActionEventHandler{TEventData}"/>.
        /// </summary>
        /// <param name="handler">Action to handle the event</param>
        public AsyncActionEventHandler(Func<TEventData, Task> handler)
        {
            Action = handler;
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="eventData"></param>
        public async Task HandleEventAsync(TEventData eventData)
        {
            await Action(eventData);
        }
    }
}
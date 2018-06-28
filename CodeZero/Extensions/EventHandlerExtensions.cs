//  <copyright file="EventHandlerExtensions.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="EventHandler"/>.
    /// </summary>
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// Raises given event safely with given arguments.
        /// </summary>
        /// <param name="eventHandler">The event handler</param>
        /// <param name="sender">Source of the event</param>
        public static void InvokeSafely(this EventHandler eventHandler, object sender)
        {
            eventHandler.InvokeSafely(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Raises given event safely with given arguments.
        /// </summary>
        /// <param name="eventHandler">The event handler</param>
        /// <param name="sender">Source of the event</param>
        /// <param name="e">Event argument</param>
        public static void InvokeSafely(this EventHandler eventHandler, object sender, EventArgs e)
        {
            if (eventHandler == null)
            {
                return;
            }

            eventHandler(sender, e);
        }

        /// <summary>
        /// Raises given event safely with given arguments.
        /// </summary>
        /// <typeparam name="TEventArgs">Type of the <see cref="EventArgs"/></typeparam>
        /// <param name="eventHandler">The event handler</param>
        /// <param name="sender">Source of the event</param>
        /// <param name="e">Event argument</param>
        public static void InvokeSafely<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (eventHandler == null)
            {
                return;
            }

            eventHandler(sender, e);
        }
    }
}
//  <copyright file="IEventData.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Events.Bus
{
    /// <summary>
    /// Defines interface for all Event data classes.
    /// </summary>
    public interface IEventData
    {
        /// <summary>
        /// The time when the event occured.
        /// </summary>
        DateTime EventTime { get; set; }

        /// <summary>
        /// The object which triggers the event (optional).
        /// </summary>
        object EventSource { get; set; }
    }
}

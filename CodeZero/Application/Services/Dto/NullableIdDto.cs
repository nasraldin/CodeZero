//  <copyright file="NullableIdDto.cs" project="CodeZero" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System;

namespace CodeZero.Application.Services.Dto
{
    /// <summary>
    /// This DTO can be directly used (or inherited)
    /// to pass an nullable Id value to an application service method.
    /// </summary>
    /// <typeparam name="TId">Type of the Id</typeparam>
    [Serializable]
    public class NullableIdDto<TId>
        where TId : struct
    {
        public TId? Id { get; set; }

        public NullableIdDto()
        {

        }

        public NullableIdDto(TId? id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// A shortcut of <see cref="NullableIdDto{TId}"/> for <see cref="int"/>.
    /// </summary>
    [Serializable]
    public class NullableIdDto : NullableIdDto<int>
    {
        public NullableIdDto()
        {

        }

        public NullableIdDto(int? id)
            : base(id)
        {

        }
    }
}
//  <copyright file="EditionFeatureSetting.cs" project="CodeZero.Identity.Common" solution="CodeZero">
//      Copyright (c) 2018 CodeZero Framework.  All rights reserved.
//  </copyright>
//  <author>Nasr Aldin M.</author>
//  <email>nasr2ldin@gmail.com</email>
//  <website>https://nasraldin.com/codezero</website>
//  <github>https://nasraldin.github.io/CodeZero</github>
//  <date>01/01/2018 01:00 AM</date>
using System.ComponentModel.DataAnnotations.Schema;
using CodeZero.Application.Editions;

namespace CodeZero.Application.Features
{
    /// <summary>
    /// Feature setting for an <see cref="Edition"/>.
    /// </summary>
    public class EditionFeatureSetting : FeatureSetting
    {
        /// <summary>
        /// Gets or sets the edition.
        /// </summary>
        /// <value>
        /// The edition.
        /// </value>
        [ForeignKey("EditionId")]
        public virtual Edition Edition { get; set; }

        /// <summary>
        /// Gets or sets the edition Id.
        /// </summary>
        /// <value>
        /// The edition Id.
        /// </value>
        public virtual int EditionId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditionFeatureSetting"/> class.
        /// </summary>
        public EditionFeatureSetting()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditionFeatureSetting"/> class.
        /// </summary>
        /// <param name="editionId">The edition Id.</param>
        /// <param name="name">Feature name.</param>
        /// <param name="value">Feature value.</param>
        public EditionFeatureSetting(int editionId, string name, string value)
            :base(name, value)
        {
            EditionId = editionId;
        }
    }
}
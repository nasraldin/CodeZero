using CodeZero.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace CodeZero.Entities
{
    public class Log : Entity<long>
    {
        /// <summary>
        /// Gets or sets the Logged date.
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        [StringLength(15)]
        public string Level { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [StringLength(6000)]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the message template.
        /// </summary>
        public string MessageTemplate { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        [StringLength(6000)]
        public string Exception { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        public string Properties { get; set; }

        /// <summary>
        /// Gets or sets the exception data.
        /// </summary>
        [StringLength(1000)]
        public string ExceptionData { get; set; }

        /// <summary>
        /// Gets or sets the exception type.
        /// </summary>
        [StringLength(255)]
        public string ExceptionType { get; set; }

        /// <summary>
        /// Gets or sets the application.
        /// </summary>
        [StringLength(60)]
        public string Application { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        [StringLength(50)]
        public string Host { get; set; }

        /// <summary>Gets or sets the IP Address.</summary>
        [StringLength(128)]
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the browser.
        /// </summary>
        [StringLength(100)]
        public string Browser { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        [StringLength(2083)]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        //[Required]
        [StringLength(255)]
        public string Logger { get; set; }

        /// <summary>
        /// Gets or sets the thread.
        /// </summary>
        //[Required]
        [StringLength(255)]
        public string Thread { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        [StringLength(255)]
        public string User { get; set; }
    }
}
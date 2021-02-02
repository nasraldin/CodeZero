using System;

namespace CodeZero.Domain
{
    /// <summary>
    /// Used to make an entity new.
    /// </summary>
    public interface INew
    {
        /// <summary>
        /// Used to mark an entity is new. 
        /// </summary>
        bool IsNew { get; set; }

        /// <summary>
        /// New start date time of this entity.
        /// </summary>
        DateTime IsNewStartDateTimeUtc { get; set; }

        /// <summary>
        /// New end date time of this of this entity.
        /// </summary>
        DateTime IsNewEndDateTimeUtc { get; set; }
    }
}

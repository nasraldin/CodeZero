namespace CodeZero.Domain.Entities
{
    /// <summary>
    /// Used to display sequence/order of entities.
    /// </summary>
    public interface IDisplaySequence
    {
        /// <summary>
        /// Used to mark an entity sequence. 
        /// </summary>
        int? DisplaySequence { get; set; }
    }
}

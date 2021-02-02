namespace Profily.Core.Interfaces
{
    /// <summary>
    /// Represents a Meta Tages
    /// </summary>
    public interface IHasMetaTages
    {
        /// <summary>
        /// Gets or sets the meta title
        /// </summary>
        string MetaTitle { get; set; }

        /// <summary>
        /// Gets or sets the meta keywords
        /// </summary>
        string MetaKeywords { get; set; }

        /// <summary>
        /// Gets or sets the meta description
        /// </summary>
        string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the meta author
        /// </summary>
        string MetaAuthor { get; set; }

        /// <summary>
        /// Gets or sets the meta copyright
        /// </summary>
        string MetaCopyright { get; set; }
    }
}
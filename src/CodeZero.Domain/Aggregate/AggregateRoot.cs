using CodeZero.Domain.Entities;

namespace CodeZero.Domain
{
    [Serializable]
    public abstract class AggregateRoot : BasicAggregateRoot, IHasConcurrencyStamp
    {
        public virtual string ConcurrencyStamp { get; set; }

        protected AggregateRoot()
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
        }
    }
}
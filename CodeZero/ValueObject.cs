using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeZero
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            var valueObject = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        public static bool operator ==(ValueObject a, ValueObject b)
        {
#pragma warning disable IDE0041 // Use 'is null' check
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
#pragma warning restore IDE0041 // Use 'is null' check
                return true;

#pragma warning disable IDE0041 // Use 'is null' check
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
#pragma warning restore IDE0041 // Use 'is null' check
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }
    }
}
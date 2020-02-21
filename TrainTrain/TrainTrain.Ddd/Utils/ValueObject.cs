using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainTrain.Ddd.Utils
{
    public abstract class ValueObject
    {
        private readonly Lazy<int> _lazyHashCode;

        protected ValueObject()
        {
            _lazyHashCode = new Lazy<int>(() => HashCode.GenerateStructural(GetPropertyValues()));
        }
        
        protected abstract IEnumerable<object> GetPropertyValues();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            var other = (ValueObject) obj;

            return
                GetPropertyValues()
                    .SequenceEqual(
                        other.GetPropertyValues(),
                        StructuralEqualityComparer.Instance);
        }

        public override int GetHashCode() => _lazyHashCode.Value;

        public static bool operator ==(ValueObject a, ValueObject b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b)
        {
            return !(a == b);
        }
    }
}
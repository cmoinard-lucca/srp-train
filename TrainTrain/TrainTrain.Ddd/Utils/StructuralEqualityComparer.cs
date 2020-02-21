using System.Collections.Generic;

namespace TrainTrain.Ddd.Utils
{
    public class StructuralEqualityComparer : StructuralEqualityComparerBase<object>
    {
        public static StructuralEqualityComparer Instance { get; } = new StructuralEqualityComparer();
        
        protected override IEnumerable<object> GetEqualityValues(object obj)
        {
            yield return obj;
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainTrain.Ddd.Utils
{
    public static class HashCode
    {
        public static int Generate(IEnumerable<object> values, Func<object, int> getHashCode) =>
            values
                .Aggregate(
                    1,
                    (current, c) =>
                    {
                        unchecked
                        {
                            return current * 397 ^ (c != null ? getHashCode(c) : 0);
                        }
                    });
        
        public static int GenerateStructural(IEnumerable<object> values) =>
            Generate(values, StructuralEqualityComparer.Instance.GetHashCode);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using TrainTrain.Ddd.Utils;

namespace TrainTrain.Ddd.UnitésDeMesure
{
    public class Volume : ValueObject, IComparable<Volume>
    {
        private readonly double _value;

        private Volume(double value)
        {
            _value = value;
        }

        protected override IEnumerable<object> GetPropertyValues()
        {
            yield return _value;
        }


        public static Volume Zéro { get; } = new Volume(0);
        public static Volume M3(double value) => new Volume(value);

        public int CompareTo(Volume other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return _value.CompareTo(other._value);
        }

        public override string ToString() => $"{_value:0.###}m³";
        
        public static Volume Min(Volume v1, Volume v2) =>
            new Volume(Math.Min(v1._value, v2._value));

        public static Volume operator + (Volume operand1, Volume operand2) =>
            new Volume(operand1._value + operand2._value);
        
        public static Volume operator - (Volume operand1, Volume operand2) =>
            new Volume(operand1._value - operand2._value);

        public static bool operator >  (Volume operand1, Volume operand2) => 
            operand1.CompareTo(operand2) == 1;

        public static bool operator <  (Volume operand1, Volume operand2) => 
            operand1.CompareTo(operand2) == -1;

        public static bool operator >=  (Volume operand1, Volume operand2) => 
            operand1.CompareTo(operand2) >= 0;

        public static bool operator <=  (Volume operand1, Volume operand2) => 
            operand1.CompareTo(operand2) <= 0;
        
        public static explicit operator double(Volume v) => v._value;
    }
    
    public static class VolumeExtensions
    {
        public static Volume Sum<T>(this IEnumerable<T> source, Func<T, Volume> getVolume) =>
            source
                .Aggregate(
                    Volume.Zéro,
                    (acc, o) => acc + getVolume(o));
    }
}
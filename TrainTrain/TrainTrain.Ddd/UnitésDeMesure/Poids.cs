using System;
using System.Collections.Generic;
using System.Linq;
using TrainTrain.Ddd.Utils;

namespace TrainTrain.Ddd.UnitésDeMesure
{
    public class Poids : ValueObject, IComparable<Poids>
    {
        private readonly double _value;

        private Poids(double value)
        {
            _value = value;
        }

        protected override IEnumerable<object> GetPropertyValues()
        {
            yield return _value;
        }
        
        public static Poids Zéro { get; } = new Poids(0);
        public static Poids Kg(double value) => new Poids(value);
        public static Poids Tonnes(double value) => new Poids(value * 1000);

        public int CompareTo(Poids other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return _value.CompareTo(other._value);
        }

        public override string ToString() => $"{_value:0.###}kg";

        public static Poids operator + (Poids operand1, Poids operand2) =>
            new Poids(operand1._value + operand2._value);
        
        public static Poids operator * (Poids operand1, double operand2) =>
            new Poids(operand1._value * operand2);
        
        public static bool operator >  (Poids operand1, Poids operand2) => 
            operand1.CompareTo(operand2) == 1;

        public static bool operator <  (Poids operand1, Poids operand2) => 
            operand1.CompareTo(operand2) == -1;

        public static bool operator >=  (Poids operand1, Poids operand2) => 
            operand1.CompareTo(operand2) >= 0;

        public static bool operator <=  (Poids operand1, Poids operand2) => 
            operand1.CompareTo(operand2) <= 0;
        
        public static explicit operator double(Poids p) => p._value;
    }
    
    public static class PoidsExtensions
    {
        public static Poids Sum<T>(this IEnumerable<T> source, Func<T, Poids> getPoids) =>
            source
                .Aggregate(
                    Poids.Zéro, 
                    (acc, o) => acc + getPoids(o));
    }
}
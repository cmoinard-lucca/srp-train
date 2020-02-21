using System;
using TrainTrain.Ddd.UnitésDeMesure;

namespace TrainTrain.Ddd
{
    public class Locomotive
    {
        public Locomotive(Poids capacitéDeTraction)
        {
            if (capacitéDeTraction <= Poids.Zéro)
                throw new InvalidOperationException();
            
            CapacitéDeTraction = capacitéDeTraction;
        }
    
        public Poids CapacitéDeTraction { get; }
    }
}
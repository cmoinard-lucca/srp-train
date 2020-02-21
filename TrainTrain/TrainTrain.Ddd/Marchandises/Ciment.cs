using TrainTrain.Ddd.UnitésDeMesure;

namespace TrainTrain.Ddd.Marchandises
{
    public class Ciment : Marchandise
    {
        public Ciment() : base(Poids.Kg(2500))
        {
        }
    }
}
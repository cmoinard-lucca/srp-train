using TrainTrain.Ddd.UnitésDeMesure;

namespace TrainTrain.Ddd.Marchandises
{
    public class Blé : Céréale
    {
        public Blé() : base(Poids.Kg(600))
        {
        }
    }
}
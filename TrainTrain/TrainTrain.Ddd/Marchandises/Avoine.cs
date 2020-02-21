using TrainTrain.Ddd.UnitésDeMesure;

namespace TrainTrain.Ddd.Marchandises
{
    public class Avoine : Céréale
    {
        public Avoine() : base(Poids.Kg(500))
        {
        }
    }
}
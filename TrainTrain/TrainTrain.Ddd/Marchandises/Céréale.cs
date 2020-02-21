using TrainTrain.Ddd.UnitésDeMesure;

namespace TrainTrain.Ddd.Marchandises
{
    public abstract class Céréale : Marchandise
    {
        protected Céréale(Poids poidsParM3)
            : base(poidsParM3)
        {
        }
    }
}
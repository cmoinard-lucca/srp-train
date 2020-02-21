using TrainTrain.Ddd.UnitésDeMesure;

namespace TrainTrain.Ddd.Marchandises
{
    public abstract class Marchandise
    {
        protected Marchandise(Poids poidsParM3)
        {
            PoidsParM3 = poidsParM3;
        }

        public Poids PoidsParM3 { get; }
    }
}
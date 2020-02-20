namespace TrainTrain.Anemic
{
    public abstract class Wagon
    {
        public double PoidsÀVide { get; set; }
        public double VolumeMaximum { get; set; }
        public double VolumeUtilisé { get; set; }
    }
    
    public class Wagon<T> : Wagon
        where T : Marchandise2
    {
    }
}
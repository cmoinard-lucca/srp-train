namespace TrainTrain.Anemic
{
    public abstract class Marchandise2
    {
        public double PoidsEnKgParM3 { get; set; }
    }

    public abstract class Céréale : Marchandise2
    {
    }

    public class Avoine : Céréale
    {
        public Avoine()
        {
            PoidsEnKgParM3 = 500;
        }
    }

    public class Ciment : Marchandise2
    {
        public Ciment()
        {
            PoidsEnKgParM3 = 2500;
        }
    }
    
    public enum Marchandise
    {
        Avoine,
        Ciment
    }
}
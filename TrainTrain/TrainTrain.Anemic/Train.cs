using System.Collections.Generic;

namespace TrainTrain.Anemic
{
    public class Train
    {
        public Locomotive Locomotive { get; set; }
        public List<Wagon> Wagons { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using TrainTrain.Ddd.Marchandises;
using TrainTrain.Ddd.UnitésDeMesure;

namespace TrainTrain.Ddd
{
    public class Train
    {
        private readonly Locomotive _locomotive;
        private readonly List<Wagon> _wagons = new List<Wagon>();

        public Train(Locomotive locomotive)
        {
            _locomotive = locomotive;
        }

        public Volume VolumeTotalUtilisé<T>() where T : Marchandise =>
            _wagons
                .OfType<Wagon<T>>()
                .Aggregate(
                    Volume.Zéro,
                    (acc, w) => acc + w.VolumeUtilisé);

        public void Assigner<T>(Wagon<T> wagon)
            where T : Marchandise
        {
            var wagons =
                _wagons
                    .OfType<Wagon<T>>()
                    .ToList();

            var poidsTotal =
                wagons.Aggregate(
                    Poids.Zéro,
                    (acc, w) => acc + w.PoidsTotal);
            
            if (_locomotive.CapacitéDeTraction < poidsTotal + wagon.PoidsÀVide)
                throw new InvalidOperationException("Le poids total excède la capacité de traction");
            
            _wagons.Add(wagon);
        }

        public void Charger<T>(T marchandise, Volume volume) 
            where T : Marchandise
        {
            if (!_wagons.Any())
                throw new InvalidOperationException("Pas de wagons");

            var wagons =
                _wagons
                    .OfType<Wagon<T>>()
                    .ToList();

            if (!wagons.Any())
                throw new InvalidOperationException($"Pas de wagons de type {typeof(T)}");

            var volumeDisponible = wagons.Sum(w => w.VolumeRestant);
            if (volumeDisponible < volume)
                throw new InvalidOperationException($"Pas assez de place dans les wagons de type {typeof(T)}");

            var poidsTotal = _wagons.Sum(w => w.PoidsTotal);
            var poidsÀAjouter = marchandise.PoidsParM3 * (double) volume;
            if (_locomotive.CapacitéDeTraction < poidsTotal + poidsÀAjouter)
                throw new InvalidOperationException($"Pas assez de place dans les wagons de type {typeof(T)}");

            var volumeRestantÀRépartir = volume;
            var i = 0;
            while (Volume.Zéro < volumeRestantÀRépartir)
            {
                var wagon = wagons[i++];
                
                var volumeRépartissable = Volume.Min(wagon.VolumeRestant, volumeRestantÀRépartir);
                wagon.Charger(marchandise, volumeRépartissable);
                
                volumeRestantÀRépartir -= volumeRépartissable;
            }
        }
    }
}
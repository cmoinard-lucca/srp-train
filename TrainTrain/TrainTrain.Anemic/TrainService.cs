using System;
using System.Linq;

namespace TrainTrain.Anemic
{
    public class TrainService
    {
        public void ChargerMarchandise<T>(Train train, double volume) where  T : Marchandise2
        {
            if (Math.Abs(volume) < 0.000001)
            {
                return;
            }

            var wagons =
                train.Wagons
                    .OfType<Wagon<T>>()
                    .ToList();
            if (!wagons.Any())
            {
                throw new InvalidOperationException($"Pas de wagon de {typeof(T)}");
            }

            var volumeTotalDisponible =
                wagons.Sum(wagon => wagon.VolumeMaximum - wagon.VolumeUtilisé);

            if (volumeTotalDisponible < volume)
            {
                throw new InvalidOperationException($"Pas assez de wagons de {typeof(T)}");
            }

            var marchandise = Activator.CreateInstance<T>();
            var poidsTotal =
                train.Wagons.Sum(w => w.PoidsÀVide) +
                (train.Wagons.Sum(w => w.VolumeUtilisé) + volume ) * marchandise.PoidsEnKgParM3 / 1000;

            if (train.Locomotive.CapacitéDeTraction < poidsTotal)
            {
                throw new InvalidOperationException($"La locomotive n'est pas capable de tracter autant de {typeof(T)}");
            }

            var volumeRestantÀRépartir = volume;
            var i = 0;
            while (0 < volumeRestantÀRépartir)
            {
                var wagon = wagons[i++];
                
                var volumeRépartissable = Math.Min(wagon.VolumeMaximum - wagon.VolumeUtilisé, volumeRestantÀRépartir);
                wagon.VolumeUtilisé += volumeRépartissable;

                volumeRestantÀRépartir -= volumeRépartissable;
            }
        }
    }
}
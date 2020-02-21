using System;
using System.Collections.Generic;
using System.Linq;
using TrainTrain.Ddd.Marchandises;
using TrainTrain.Ddd.UnitésDeMesure;

namespace TrainTrain.Ddd
{
    public abstract class Wagon
    {
        protected Wagon(Poids poidsÀVide, Volume volumeMax)
        {
            if (poidsÀVide <= Poids.Zéro)
                throw new InvalidOperationException("Le poids doit être strictement positif");
            
            if (volumeMax <= Volume.Zéro)
                throw new InvalidOperationException("Le volume max doit être strictement positif");
            
            PoidsÀVide = poidsÀVide;
            VolumeMax = volumeMax;
            
            VolumeUtilisé = Volume.Zéro;
        }

        protected Volume VolumeMax { get; }

        public Poids PoidsÀVide { get; }

        public Volume VolumeUtilisé { get; protected set; }

        public Poids PoidsTotal => PoidsÀVide + PoidsTotalDesMarchandises;

        protected abstract Poids PoidsTotalDesMarchandises { get; }
    }

    public class Wagon<T> : Wagon
        where T : Marchandise
    {
        private readonly List<(T Marchandise, Volume Volume)> _chargements =
            new List<(T Marchandise, Volume Volume)>();
        
        public Wagon(Poids poidsÀVide, Volume volumeMax) 
            : base(poidsÀVide, volumeMax)
        {
        }

        public Volume VolumeRestant => VolumeMax - VolumeUtilisé;
        
        protected override Poids PoidsTotalDesMarchandises =>
            _chargements
                .Select(c => c.Marchandise.PoidsParM3 * (double)VolumeUtilisé)
                .Aggregate(
                    Poids.Zéro,
                    (acc, p) => acc + p);
        

        public void Charger(T marchandise, Volume volume)
        {
            if (VolumeRestant < volume)
            {
                throw new InvalidOperationException("Le volume ne peut excéder le volume restant");
            }

            _chargements.Add((marchandise, volume));
            VolumeUtilisé += volume;
        }
    }
}
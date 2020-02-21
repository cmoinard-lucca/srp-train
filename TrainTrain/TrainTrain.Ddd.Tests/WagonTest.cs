using System;
using TrainTrain.Ddd.Marchandises;
using TrainTrain.Ddd.UnitésDeMesure;
using Xunit;

namespace TrainTrain.Ddd.Tests
{
    public class WagonTest
    {
        [Fact]
        public void UnWagonAvecUnPoidsÀVideÀZero_DoitLeverUneException()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new Wagon<Céréale>(Poids.Zéro, Volume.M3(10)));
        }
        
        [Fact]
        public void UnWagonAvecUnVolumeMaxÀZero_DoitLeverUneException()
        {
            Assert.Throws<InvalidOperationException>(() =>
                new Wagon<Céréale>(Poids.Tonnes(20), Volume.Zéro));
        }
        
        [Fact]
        public void UnChargementAvecUnVolumeSupérieurAuVolumeMax_DoitLeverUneException()
        {
            var wagon = new Wagon<Céréale>(Poids.Tonnes(20), Volume.M3(100));
            
            Assert.Throws<InvalidOperationException>(() =>
                wagon.Charger(new Avoine(), Volume.M3(200)));
        }
        
        [Fact]
        public void UnChargementAvecUnVolumeSupérieurAuVolumeRestant_DoitLeverUneException()
        {
            var wagon = new Wagon<Céréale>(Poids.Tonnes(20), Volume.M3(100));

            wagon.Charger(new Avoine(), Volume.M3(50));
            
            Assert.Throws<InvalidOperationException>(() =>
                wagon.Charger(new Avoine(), Volume.M3(80)));
        }
        
        [Fact]
        public void UnChargementAvecUnVolumeInférieurAuVolumeRestant_DoitAjouterCeVolume()
        {
            var wagon = new Wagon<Céréale>(Poids.Tonnes(20), Volume.M3(100));

            wagon.Charger(new Avoine(), Volume.M3(50));
            
            Assert.Equal(Volume.M3(50), wagon.VolumeUtilisé);
        }
    }
}
using System;
using TrainTrain.Ddd.Marchandises;
using TrainTrain.Ddd.UnitésDeMesure;
using Xunit;

namespace TrainTrain.Ddd.Tests
{
    public class TrainTest
    {
        [Fact]
        public void AssignerUnWagonQuiDépasseLaCapacitéDeTraction_DoitLeverUneException()
        {
            var train = new Train(new Locomotive(Poids.Kg(10)));

            Assert.Throws<InvalidOperationException>(() =>
                train.Assigner(
                    new Wagon<Avoine>(Poids.Tonnes(1), Volume.M3(10))));
        }

        [Fact]
        public void DoitLeverUneException_SiPasDeWagonDeMarchandise()
        {
            var train = new Train(new Locomotive(Poids.Kg(10)));

            Assert.Throws<InvalidOperationException>(() =>
                train.Charger(new Avoine(), Volume.M3(10)));
        }

        [Fact]
        public void DoitLeverUneException_SiPasDeWagonDeLaMarchandiseDemandée()
        {
            var train = new Train(new Locomotive(Poids.Tonnes(1000)));
            train.Assigner(new Wagon<Avoine>(Poids.Tonnes(20), Volume.M3(20)));
            
            Assert.Throws<InvalidOperationException>(() =>
                train.Charger(new Ciment(), Volume.M3(10)));
        }

        [Fact]
        public void DoitLeverUneException_SiPlusDePlaceDisponibleDansLesWagonsDuTypeDemandé()
        {
            var train = new Train(new Locomotive(Poids.Tonnes(1000)));
            train.Assigner(new Wagon<Avoine>(Poids.Tonnes(20), Volume.M3(20)));
            
            Assert.Throws<InvalidOperationException>(() =>
                train.Charger(new Avoine(), Volume.M3(100)));
        }

        [Fact]
        public void DoitLeverUneException_SiOnPeutChargerLeVolume_MaisQueLePoidsTotalDépasseLaCapacitéDeTraction()
        {
            var train = new Train(new Locomotive(Poids.Tonnes(1000)));
            train.Assigner(new Wagon<Avoine>(Poids.Tonnes(20), Volume.M3(20000)));
            
            Assert.Throws<InvalidOperationException>(() =>
                train.Charger(new Avoine(), Volume.M3(10000)));
        }
        
        [Fact]
        public void DoitPouvoirChargerUneMarchandise_SIlResteDeLaPlaceDansUnWagon()
        {
            var train = new Train(new Locomotive(Poids.Tonnes(1000)));
            train.Assigner(new Wagon<Avoine>(Poids.Tonnes(20), Volume.M3(20)));
        
            train.Charger(new Avoine(), Volume.M3(10));
            var actual = train.VolumeTotalUtilisé<Avoine>();
            
            Assert.Equal(Volume.M3(10), actual);
        }
        
        [Fact]
        public void DoitPouvoirChargerUneMarchandise_SIlResteDeLaPlaceDansPlusieursWagons()
        {
            var train = new Train(new Locomotive(Poids.Tonnes(1000)));
            train.Assigner(new Wagon<Avoine>(Poids.Tonnes(20), Volume.M3(20)));
            train.Assigner(new Wagon<Avoine>(Poids.Tonnes(20), Volume.M3(20)));
            train.Charger(new Avoine(), Volume.M3(10));
            
            train.Charger(new Avoine(), Volume.M3(20));
            var actual = train.VolumeTotalUtilisé<Avoine>();
            
            Assert.Equal(Volume.M3(30), actual);
        }
    }
}
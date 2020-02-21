using System;
using System.Collections.Generic;
using System.Linq;
using TrainTrain.Anemic.Marchandises;
using Xunit;

namespace TrainTrain.Anemic.Tests
{
    public class TrainServiceTest
    {
        private readonly TrainService _trainService = new TrainService();
        
        [Fact]
        public void DoitLeverUneException_SiPasDeWagonDeMarchandise()
        {
            var train =
                new Train
                {
                    Locomotive = new Locomotive {CapacitéDeTraction = 100},
                    Wagons = new List<Wagon>
                    {
                        new Wagon<Céréale>
                        {
                            PoidsÀVide = 20,
                            VolumeMaximum = 100,
                            VolumeUtilisé = 20
                        },
                    }
                };

            Assert.Throws<InvalidOperationException>(
                () => _trainService.ChargerMarchandise(new Ciment(), train, 10));
        }
        
        [Fact]
        public void NeRienCharger_NeChangePasLaContenanceDesWagons()
        {
            var train = CréerTrain();

            _trainService.ChargerMarchandise(new Avoine(), train, 0);

            var wagonAvoine = train.Wagons.OfType<Wagon<Avoine>>().First();
            Assert.Equal(20, wagonAvoine.VolumeUtilisé);
        }
        
        [Fact]
        public void ChargerMoinsQueLaCapacitéMaximale_ChangeLaContenanceDUnWagon()
        {
            var train = CréerTrain();

            _trainService.ChargerMarchandise(new Avoine(), train, 10);

            var wagonAvoine = train.Wagons.OfType<Wagon<Avoine>>().First();
            Assert.Equal(30, wagonAvoine.VolumeUtilisé);
        }
        
        [Fact]
        public void ChargerPlusQueLaCapacitéMaximale_ChangeLaContenanceDePlusieursWagons()
        {
            var train =
                new Train
                {
                    Locomotive = new Locomotive { CapacitéDeTraction = 10000 },
                    Wagons = new List<Wagon>
                    {
                        new Wagon<Avoine>
                        {
                            PoidsÀVide = 20,
                            VolumeMaximum = 100,
                            VolumeUtilisé = 20
                        },
                        new Wagon<Avoine>
                        {
                            PoidsÀVide = 20,
                            VolumeMaximum = 100,
                            VolumeUtilisé = 20
                        }
                    }
                };

            _trainService.ChargerMarchandise(new Avoine(), train, 100);

            var wagon1 = train.Wagons.OfType<Wagon<Avoine>>().First();
            Assert.Equal(100, wagon1.VolumeUtilisé);

            var wagon2 = train.Wagons.OfType<Wagon<Avoine>>().Skip(1).First();
            Assert.Equal(40, wagon2.VolumeUtilisé);
        }
        
        [Fact]
        public void ChargerPlusQueLaCapacitéMaximaleDeTousLesWagons_DoitLeverUneException()
        {
            var train =
                new Train
                {
                    Locomotive = new Locomotive { CapacitéDeTraction = 100 },
                    Wagons = new List<Wagon>
                    {
                        new Wagon<Avoine>
                        {
                            PoidsÀVide = 20,
                            VolumeMaximum = 100,
                            VolumeUtilisé = 20
                        },
                        new Wagon<Avoine> 
                        {
                            PoidsÀVide = 20,
                            VolumeMaximum = 100,
                            VolumeUtilisé = 20
                        }
                    }
                };

            Assert.Throws<InvalidOperationException>(() =>
                _trainService.ChargerMarchandise(new Avoine(), train, 1000));
        }

        
        [Fact]
        public void ChargerPlusQueLaCapacitéDeTraction_DoitLeverUneException()
        {
            var train =
                new Train
                {
                    Locomotive = new Locomotive { CapacitéDeTraction = 70 },
                    Wagons = new List<Wagon>
                    {
                        new Wagon<Avoine>
                        {
                            PoidsÀVide = 20,
                            VolumeMaximum = 100,
                            VolumeUtilisé = 20
                        },
                        new Wagon<Avoine> 
                        {
                            PoidsÀVide = 20,
                            VolumeMaximum = 100,
                            VolumeUtilisé = 20
                        }
                    }
                };

            Assert.Throws<InvalidOperationException>(() =>
                _trainService.ChargerMarchandise(new Avoine(), train, 160));
        }

        private static Train CréerTrain() =>
            new Train
            {
                Locomotive = new Locomotive { CapacitéDeTraction = 100 },
                Wagons = new List<Wagon>
                {
                    new Wagon<Avoine>
                    {
                        PoidsÀVide = 20,
                        VolumeMaximum = 100,
                        VolumeUtilisé = 20
                    },
                    new Wagon<Ciment> 
                    {
                        PoidsÀVide = 20,
                        VolumeMaximum = 50,
                        VolumeUtilisé = 20
                    }
                }
            };
    }
}
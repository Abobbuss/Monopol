using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopol.Warehouse;
using System;
using System.Collections.Generic;

namespace MonopolTestProject1
{
    [TestClass]
    public class PalletTests
    {
        [TestMethod]
        public void VolumeCalculation_EmptyPallet_ReturnsZero()
        {
            //
            var pallet = new Pallet();

            //
            double actualVolume = pallet.Volume;

            //
            Assert.AreEqual(0, actualVolume);
        }

        [TestMethod]
        public void VolumeCalculation_PalletWithBoxes_ReturnsCorrectVolume()
        {
            //
            var box1 = new Box { Width = 2, Height = 2, Depth = 2 };
            var box2 = new Box { Width = 3, Height = 3, Depth = 3 }; 
            var box3 = new Box { Width = 4, Height = 4, Depth = 4 }; 

            var pallet = new Pallet();
            pallet.Boxes.AddRange(new List<Box> { box1, box2, box3 });

            //
            double expectedVolume = 8 + 27 + 64;
            double actualVolume = pallet.Volume;

            //
            Assert.AreEqual(expectedVolume, actualVolume);
        
        
        
        }

        [TestMethod]
        public void ExpiryDateCalculation_PalletWithBoxes_ReturnsMinBoxExpiryDate()
        {
            //
            var box1 = new Box { ProductionDate = new DateTime(2024, 4, 20), Width = 2, Height = 2, Depth = 2 };
            var box2 = new Box { ProductionDate = new DateTime(2024, 4, 22), Width = 3, Height = 3, Depth = 3 };
            var box3 = new Box { ProductionDate = new DateTime(2024, 4, 24), Width = 4, Height = 4, Depth = 4 };

            var pallet = new Pallet();
            pallet.Boxes.AddRange(new List<Box> { box1, box2, box3 });

            //
            DateTime expectedExpiryDate = new DateTime(2024, 7, 29); 
            DateTime actualExpiryDate = pallet.ExpiryDate;

            //
            Assert.AreEqual(expectedExpiryDate, actualExpiryDate);
        }

        [TestMethod]
        public void Weight_NoBoxes_Returns30()
        {
            //
            var pallet = new Pallet();

            //
            var weight = pallet.Weight;

            //
            Assert.AreEqual(30, weight);
        }

        [TestMethod]
        public void Weight_SingleBox_ReturnsBoxWeightPlus30()
        {
            //
            var box = new Box { Weight = 20 };
            var pallet = new Pallet();
            pallet.Boxes.Add(box);

            //
            var weight = pallet.Weight;

            //
            Assert.AreEqual(50, weight);
        }

        [TestMethod]
        public void Weight_MultipleBoxes_ReturnsTotalBoxWeightPlus30()
        {
            //
            var box1 = new Box { Weight = 20 };
            var box2 = new Box { Weight = 15 };
            var pallet = new Pallet();
            pallet.Boxes.Add(box1);
            pallet.Boxes.Add(box2);

            //
            var weight = pallet.Weight;

            //
            Assert.AreEqual(65, weight);
        }
    }
}

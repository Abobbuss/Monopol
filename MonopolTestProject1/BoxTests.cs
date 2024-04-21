using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopol.Warehouse;
using System;

namespace MonopolTestProject1
{
    [TestClass]
    public class BoxTests
    {
        [TestMethod]
        public void VolumeCalculation_CorrectDimensions_ReturnsExpectedVolume()
        {
            //
            double expectedVolume = 100;
            double width = 5;
            double height = 5;
            double depth = 4;
            double weight = 10;

            var box = new Box
            {
                Width = width,
                Height = height,
                Depth = depth,
                Weight = weight
            };

            //
            double actualVolume = box.Volume;

            //
            Assert.AreEqual(expectedVolume, actualVolume);
        }

        [TestMethod]
        public void ExpiryDateCalculation_CorrectProductionDate_ReturnsExpectedExpiryDate()
        {
            // 
            DateTime productionDate = new DateTime(2024, 4, 20);
            DateTime expectedExpiryDate = new DateTime(2024, 7, 29);

            // 
            var box = new Box { ProductionDate = productionDate };
            DateTime actualExpiryDate = box.ExpiryDate;

            // 
            Assert.AreEqual(expectedExpiryDate, actualExpiryDate);
        }
    }
}

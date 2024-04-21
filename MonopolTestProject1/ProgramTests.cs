using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopol;
using Monopol.Warehouse;
using System;
using System.Collections.Generic;
using System.IO;

namespace MonopolTestProject1
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void GroupAndSortPallets_CorrectSorting_ReturnsSortedPallets()
        {
            //
            var box1 = new Box { ProductionDate = new DateTime(2024, 4, 1), Width = 1, Height = 1, Depth = 1, Weight = 5};
            var box2 = new Box { ProductionDate = new DateTime(2024, 4, 5), Width = 2, Height = 2, Depth = 2, Weight = 7};
            var box3 = new Box { ProductionDate = new DateTime(2024, 4, 10), Width = 3, Height = 3, Depth = 3, Weight = 9};
            var box4 = new Box { ProductionDate = new DateTime(2024, 4, 10), Width = 3, Height = 3, Depth = 3, Weight = 10};


            var pallet1 = new Pallet();
            pallet1.Boxes.Add(box1);
            pallet1.Boxes.Add(box2);

            var pallet2 = new Pallet();
            pallet2.Boxes.Add(box3);

            var pallet3 = new Pallet();
            pallet3.Boxes.Add(box4);

            var pallets = new List<Pallet> { pallet1, pallet2, pallet3 };

            //
            var sortedPallets = Program.GroupAndSortPallets(pallets);


            //
            Assert.IsTrue(sortedPallets.IndexOf(pallet1) < sortedPallets.IndexOf(pallet2));
            Assert.IsTrue(sortedPallets.IndexOf(pallet2) < sortedPallets.IndexOf(pallet3));
        }

        [TestMethod]
        public void GetPalletsWithLongestExpiry_ReturnsCorrectPallets()
        {
            //
            var box1 = new Box { ProductionDate = new DateTime(2024, 4, 1), Width = 1, Height = 1, Depth = 1, Weight = 5 };
            var box2 = new Box { ProductionDate = new DateTime(2024, 4, 5), Width = 2, Height = 2, Depth = 2, Weight = 7 };
            var box3 = new Box { ProductionDate = new DateTime(2024, 4, 10), Width = 3, Height = 3, Depth = 3, Weight = 9 };
            var box4 = new Box { ProductionDate = new DateTime(2024, 4, 10), Width = 3, Height = 3, Depth = 4, Weight = 10 };

            var pallet1 = new Pallet();
            pallet1.Boxes = new List<Box> { box1, box2 };

            var pallet2 = new Pallet();
            pallet2.Boxes = new List<Box> { box3 };

            var pallet3 = new Pallet();
            pallet3.Boxes = new List<Box> { box4 };

            var pallets = new List<Pallet> { pallet1, pallet2, pallet3 };

            //
            var selectedPallets = Program.GetPalletsWithLongestExpiry(pallets, 3);

            //
            Assert.AreEqual(pallet2, selectedPallets[0]);
            Assert.AreEqual(pallet3, selectedPallets[1]);
            Assert.AreEqual(pallet1, selectedPallets[2]);
        }
    }
}

using Monopol.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monopol
{
    public class Program
    {
        static void Main(string[] args)
        {
            const string CommandGenerateBoxesAndPallets = "1";
            const string CommandShowAllPallets = "2";
            const string CommandShowGroupAndSortPallets = "3";
            const string CommandShowSortedPalletsWithLongestExpiry = "4";
            const string CommandExit = "5";

            bool isWork = true;

            List<Box> boxes = new List<Box>();
            List<Pallet> pallets = new List<Pallet>();

            while (isWork)
            {
                Console.WriteLine($"\n\nЧто делаем?" +
                    $"\nВведите {CommandGenerateBoxesAndPallets} для генерация коробок и паллетов" +
                    $"\nВведите {CommandShowAllPallets} для вывода информации по паллетам" +
                    $"\nВведите {CommandShowGroupAndSortPallets} для сортировки паллетов по сроку годности и весу" +
                    $"\nВведите {CommandShowSortedPalletsWithLongestExpiry} для вывода 3 палетов, содержащих коробки с наиб. сроком годности, отсортированные по объему" +
                    $"\nВведите {CommandExit} для завершения работы");

                string userMessage = Console.ReadLine();

                switch (userMessage)
                {
                    case CommandGenerateBoxesAndPallets:
                        GenerateBoxesAndPallets(ref boxes, ref pallets);
                        break;
                    case CommandShowAllPallets:
                        PrintPalletsInfo(pallets);
                        break;
                    case CommandShowGroupAndSortPallets:
                        PrintGroupAndSortPallets(pallets);
                        break;
                    case CommandShowSortedPalletsWithLongestExpiry:
                        PrintPalletsWithLongestExpiry(pallets);
                        break;
                    case CommandExit:
                        isWork = false;
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
        }

        public static (List<Box>, List<Pallet>) GenerateBoxesAndPallets(ref List<Box> boxes, ref List<Pallet> pallets)
        {
            Console.WriteLine("Введите количество коробок, которое необходимо создать");
            int countBoxes;

            while (!int.TryParse(Console.ReadLine(), out countBoxes) || countBoxes <= 0)
            {
                Console.WriteLine("Пожалуйста, введите корректное значение (целое положительное число):");
            }

            boxes = GenerateRandomBoxes(countBoxes);

            Console.WriteLine("Введите количество паллетов, которое необходимо создать");
            int countPallets;

            while (!int.TryParse(Console.ReadLine(), out countPallets) || countPallets <= 0)
            {
                Console.WriteLine("Пожалуйста, введите корректное значение (целое положительное число):");
            }

            pallets = GenerateRandomPallets(countPallets, boxes);

            return (boxes, pallets);
        }

        public static List<Box> GenerateRandomBoxes(int count)
        {
            List<Box> boxes = new List<Box>();
            Random random = new Random();

            int randomValue = 10;
            int radomValueDate = 100;

            for (int i = 0; i < count; i++)
            {
                Box box = new Box
                {
                    ID = i + 1,
                    Width = random.NextDouble() * randomValue,
                    Height = random.NextDouble() * randomValue,
                    Depth = random.NextDouble() * randomValue,
                    Weight = random.NextDouble() * randomValue,
                    ProductionDate = DateTime.Now.AddDays(-random.Next(1, radomValueDate)),
                };
                boxes.Add(box);
            }

            return boxes;
        }

        public static List<Pallet> GenerateRandomPallets(int count, List<Box> boxes)
        {
            List<Pallet> pallets = new List<Pallet>();
            Random random = new Random();

            int randovValue = 100;

            for (int i = 0; i < count; i++)
            {
                Pallet pallet = new Pallet
                {
                    ID = i + 1,
                    Width = random.NextDouble() * randovValue,
                    Height = random.NextDouble() * randovValue,
                    Depth = random.NextDouble() * randovValue,
                };
                pallets.Add(pallet);
            }

            foreach (var box in boxes)
            {
                int randomIndex = random.Next(0, pallets.Count);
                pallets[randomIndex].Boxes.Add(box);
            }

            return pallets;
        }

        public static void PrintPalletsInfo(List<Pallet> pallets)
        {
            Console.WriteLine("Информация по палетам:");

            foreach (var pallet in pallets)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Паллета ID: {pallet.ID}, Срок годности: {pallet.ExpiryDateShortString}, Вес: {pallet.Weight} , Объем: {pallet.Volume}");
                Console.ResetColor();

                Console.WriteLine("Коробки на паллете:");

                foreach (var box in pallet.Boxes)
                {
                    PrintBoxesInfo(box);
                }

                Console.WriteLine();
            }
        }

        public static void PrintBoxesInfo(Box box) =>
            Console.WriteLine($"Коробка ID: {box.ID}, Срок годности: {box.ExpiryDateShortString}, Вес: {box.Weight} , Объем: {box.Volume}");

        public static void PrintGroupAndSortPallets(List<Pallet> pallets)
        {
            List<Pallet> sortedPallets = GroupAndSortPallets(pallets);

            PrintPalletsInfo(sortedPallets);
        }

        public static void PrintPalletsWithLongestExpiry(List<Pallet> pallets)
        {
            int defaultCountBoxes = 3;
            List<Pallet> sortedPallets = GetPalletsWithLongestExpiry(pallets, defaultCountBoxes);

            PrintPalletsInfo(sortedPallets);
        }

        public static List<Pallet> GroupAndSortPallets(List<Pallet> pallets)
        {
            List<Pallet> sortedPallets = pallets.OrderBy(p => p.ExpiryDate)
                                                 .ThenBy(p => p.Weight)
                                                 .ToList();

            return sortedPallets;
        }

        public static List<Pallet> GetPalletsWithLongestExpiry(List<Pallet> pallets, int count)
        {
            var sortedPalletsWithLongestExpiry = pallets.OrderByDescending(p => p.ExpiryDate)
                                                            .ThenBy(p => p.Volume)
                                                            .Take(count)
                                                            .ToList();
            return sortedPalletsWithLongestExpiry;
        }
    }
}
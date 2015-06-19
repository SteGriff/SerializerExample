using System;
using System.Collections.Generic;
using System.IO;

namespace SerializerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "C:\\temp\\game.sav";

            Console.WriteLine("Serializer demo");
            ConsoleKey choice = ConsoleKey.NoName;
            
            while(choice != ConsoleKey.R && choice != ConsoleKey.W)
            {
                Console.WriteLine("[R]ead or [W]rite? ");
                choice = Console.ReadKey().Key;
                Console.WriteLine();

                if (choice == ConsoleKey.R)
                {
                    ReadData(fileName);
                }
                else if (choice == ConsoleKey.W)
                {
                    WriteFakeData(fileName);
                }
                else
                {
                    Console.WriteLine("Press R or W");
                }
            }

            Console.ReadLine();
        }

        private static void ReadData(string fileName)
        {
            FileStream fileStream = File.Open(fileName, FileMode.Open);
            var fileReader = new StreamReader(fileStream);
            byte[] fileBinary = Convert.FromBase64String(fileReader.ReadToEnd());

            GameObject game;
            Serializer.BinaryToObject<GameObject>(fileBinary, out game);

            Console.WriteLine("{0}'s game \nDate {1} \nScore: {2}\n\nBuildings:", game.PlayerName, game.WorldDate.ToShortDateString(), game.Score);
            foreach (var b in game.Buildings)
            {
                Console.WriteLine("{0}:{1} ({2})", b.ID, b.Name, b.Type.ToString());
            }

            Console.WriteLine("Done");
        }

        private static void WriteFakeData(string fileName)
        {
            Console.WriteLine("Setup");

            //Set up some fake data to save
            var game = new GameObject()
            {

                PlayerName = "SteGriff",
                Score = 9522,
                WorldDate = DateTime.Now,

                Buildings = new List<SerializerExample.GameObject.Building>()
                {
                    new SerializerExample.GameObject.Building()
                    {
                        ID = 1,
                        Name = "Houses",
                        Type = GameObject.Building.BuildingType.Residential
                    },
                    new SerializerExample.GameObject.Building(){
                        ID = 2,
                        Name = "Factory",
                        Type = GameObject.Building.BuildingType.Industrial
                    }
                }

            };

            Console.WriteLine("Write");

            //Set up byte array for the data
            byte[] gameData;

            //Serialize it
            Serializer.ObjectToBinary(game, out gameData);

            //(Optional) marshall the binary into a base64 string
            string gameData64 = Convert.ToBase64String(gameData);

            //You can then save/transmit this data
            var fileStream = File.Open(fileName, FileMode.OpenOrCreate);
            var fileWriter = new StreamWriter(fileStream);
            fileWriter.Write(gameData64);
            fileWriter.Close();

            Console.WriteLine("Done");
        }
    }
}

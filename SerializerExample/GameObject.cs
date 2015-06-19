using System;
using System.Collections.Generic;

namespace SerializerExample
{
    [Serializable]
    public class GameObject
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public List<Building> Buildings { get; set; }
        public DateTime WorldDate { get; set; }

        [Serializable]
        public class Building
        {
            public string Name { get; set; }
            public long ID { get; set; }
            public BuildingType Type { get; set; }

            public enum BuildingType
            {
                Residential,
                Commercial,
                Industrial
            }
        }
    }
}

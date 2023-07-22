using System;
using System.Collections.Generic;
using System.Linq;

namespace _4._Snowwhite
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Dwarf> dwarfs = new List<Dwarf>();
            Dictionary<string, List<int>> hats =
                new Dictionary<string, List<int>>();
            string[] input = Console.ReadLine().Split(" <:> ");
            while (input[0] != "Once upon a time")
            {
                string dwarfName = input[0];
                string hatColor = input[1];
                int physic = int.Parse(input[2]);
                Dwarf dwarf = dwarfs.FirstOrDefault(x => x.Name == dwarfName && x.Color == hatColor);
                if (dwarf == null)
                {
                    dwarf = new Dwarf(dwarfName, hatColor, physic);
                    dwarfs.Add(dwarf);
                    if (!hats.ContainsKey(hatColor))
                    {
                        hats.Add(hatColor, new List<int>());
                    }
                    hats[hatColor].Add(physic);
                }
                else
                {
                    if (dwarf.Physic < physic)
                    {
                        dwarf.Physic = physic;
                    }
                    if (!hats[hatColor].Contains(physic))
                    {
                        hats[hatColor].Add(physic);
                    }
                }
                input = Console.ReadLine().Split(" <:> ");
            }
            foreach (Dwarf item in dwarfs.OrderByDescending(x => x.Physic).ThenByDescending(x => hats[x.Color].Count))
            {
                Console.WriteLine($"({item.Color}) {item.Name} <-> {item.Physic}");
            }
        }
    }

    public class Dwarf
    {
        public Dwarf(string name, string color, int physic)
        {
            Name = name;
            Color = color;
            Physic = physic;
        }
        public string Name { get; private set; }
        public string Color { get; set; }
        public int Physic { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace _5._Dragon_Army
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<Dragon>> dragons =
                new Dictionary<string, List<Dragon>>();
            int dragonsCount = int.Parse(Console.ReadLine());
            for (int i = 1; i <= dragonsCount; i++)
            {
                string[] dragonDetails = Console.ReadLine().Split();
                string type = dragonDetails[0];
                string name = dragonDetails[1];
                int damage = 45;
                int health = 250;
                int armor = 10;
                if (dragonDetails[2] != "null")
                {
                    damage = int.Parse(dragonDetails[2]);
                }
                if (dragonDetails[3]!="null")
                {
                    health = int.Parse(dragonDetails[3]);
                }
                if (dragonDetails[4]!="null")
                {
                    armor = int.Parse(dragonDetails[4]);
                }
                Dragon dragon = new Dragon(name, damage, health, armor);
                if (!dragons.ContainsKey(type))
                {
                    dragons[type] = new List<Dragon>();
                }
                Dragon existingDragon = dragons[type].FirstOrDefault(g => g.Name == name);
                if (existingDragon!=null)
                {
                    existingDragon.Damage = damage;
                    existingDragon.Health = health;
                    existingDragon.Armor = armor;
                }
                else
                {
                    dragons[type].Add(dragon);
                }
            }
            foreach (KeyValuePair<string, List<Dragon>> item in dragons)
            {
                double averageDamage = item.Value.Average(g => g.Damage);
                double averageHealth = item.Value.Average(x => x.Health);
                double averageArmor = item.Value.Average(f => f.Armor);
                Console.WriteLine($"{item.Key}::({averageDamage:f2}/{averageHealth:f2}/{averageArmor:f2})");
                foreach (Dragon dragonsToPrint in item.Value.OrderBy(g=>g.Name))
                {
                    Console.WriteLine($"-{dragonsToPrint.Name} -> damage: {dragonsToPrint.Damage}, health: {dragonsToPrint.Health}, armor: {dragonsToPrint.Armor}");
                }
            }
        }
    }
    public class Dragon
    {
        public Dragon(string name, int damage, int health, int armor)
        {
            this.Name = name;
            this.Damage = damage;
            this.Health = health;
            this.Armor = armor;
        }
        public string Name { get; private set; }
        public int Damage { get; set; }
        public int Health { get; set; }
        public int Armor { get; set; }
    }
}

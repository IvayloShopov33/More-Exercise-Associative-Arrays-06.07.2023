using System;
using System.Collections.Generic;
using System.Linq;

namespace _3._MOBA_Challenger
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();
            string input = Console.ReadLine();
            while (input!="Season end")
            {
                if (input.Contains("->"))
                {
                    string[] playerDetails = input.Split(" -> ");
                    string name = playerDetails[0];
                    string position = playerDetails[1];
                    int skill = int.Parse(playerDetails[2]);
                    if (!players.Any(g=>g.Name==name))
                    {
                        Player player = new Player(name, new Dictionary<string, int>());
                        player.PositionAndSkill.Add(position, skill);
                        players.Add(player);
                    }
                    else
                    {
                        Player selectedPlayer = players.First(x => x.Name == name);
                        if (!selectedPlayer.PositionAndSkill.ContainsKey(position))
                        {
                            selectedPlayer.PositionAndSkill.Add(position, skill);
                        }
                        else
                        {
                            if (selectedPlayer.PositionAndSkill[position]<skill)
                            {
                                selectedPlayer.PositionAndSkill[position] = skill;
                            }
                        }
                    }
                }
                else if (input.Contains("vs"))
                {
                    string[] fighters = input.Split(" vs ");
                    string firstPlayer = fighters[0];
                    string secondPlayer = fighters[1];
                    if (players.Any(x=>x.Name==firstPlayer) && players.Any(x=>x.Name==secondPlayer))
                    {
                        Player firstFighter = players.First(x => x.Name == firstPlayer);
                        Player secondFighter = players.First(x => x.Name == secondPlayer);
                        int firstPlayerWins = 0;
                        int secondPlayerWins = 0;
                        if (firstFighter.PositionAndSkill.Count>=secondFighter.PositionAndSkill.Count)
                        {
                            foreach (var item in firstFighter.PositionAndSkill)
                            {
                                if (secondFighter.PositionAndSkill.ContainsKey(item.Key))
                                {
                                    if (firstFighter.PositionAndSkill[item.Key]> secondFighter.PositionAndSkill[item.Key])
                                    {
                                        firstPlayerWins++;
                                    }
                                    else if (firstFighter.PositionAndSkill[item.Key] < secondFighter.PositionAndSkill[item.Key])
                                    {
                                        secondPlayerWins++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (var item in secondFighter.PositionAndSkill)
                            {
                                if (firstFighter.PositionAndSkill.ContainsKey(item.Key))
                                {
                                    if (firstFighter.PositionAndSkill[item.Key] > secondFighter.PositionAndSkill[item.Key])
                                    {
                                        firstPlayerWins++;
                                    }
                                    else if (firstFighter.PositionAndSkill[item.Key] < secondFighter.PositionAndSkill[item.Key])
                                    {
                                        secondPlayerWins++;
                                    }
                                }
                            }
                        }

                        if (firstPlayerWins>secondPlayerWins)
                        {
                            players.Remove(secondFighter);
                        }
                        else if (firstPlayerWins<secondPlayerWins)
                        {
                            players.Remove(firstFighter);

                        }
                    }
                }
                input = Console.ReadLine();
            }

            foreach (var player in players.OrderByDescending(x=>x.PositionAndSkill.Values.Sum()).ThenBy(x=>x.Name))
            {
                Console.WriteLine($"{player.Name}: {player.PositionAndSkill.Values.Sum()} skill");
                foreach (var playerDetails in player.PositionAndSkill.OrderByDescending(x=>x.Value).ThenBy(g=>g.Key))
                {
                    Console.WriteLine($"- {playerDetails.Key} <::> {playerDetails.Value}");
                }
            }
        }
    }
    public class Player
    {
        public Player(string name, Dictionary<string, int> positionAndSkill)
        {
            Name = name;
            PositionAndSkill = positionAndSkill;
        }
        public string Name { get; private set; }
        public Dictionary<string, int> PositionAndSkill { get; set; }
    }
}

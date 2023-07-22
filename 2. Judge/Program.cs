using System;
using System.Collections.Generic;
using System.Linq;

namespace _2._Judge
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Contest> contests = new List<Contest>();
            Dictionary<string, int> individualStandings =
                new Dictionary<string, int>();
            string[] input = Console.ReadLine().Split(" -> ");
            while (input[0] != "no more time")
            {
                string username = input[0];
                string contestName = input[1];
                int points = int.Parse(input[2]);
                Contest contest = contests.FirstOrDefault(x => x.Name == contestName);
                if (contest==null)
                {
                     contest = new Contest(contestName, new Dictionary<string, int>());
                    contest.UsernamesWithPoints.Add(username, points);
                    contests.Add(contest);
                }
                else
                {
                    Contest selectedContest = contests.First(x => x.Name == contestName);
                    if (selectedContest.UsernamesWithPoints.ContainsKey(username))
                    {
                        if (selectedContest.UsernamesWithPoints[username] < points)
                        {
                            selectedContest.UsernamesWithPoints[username] = points;
                        }
                    }
                    else
                    {
                        selectedContest.UsernamesWithPoints.Add(username, points);
                    }
                }
                if (!contest.UsernamesWithPoints.ContainsKey(username))
                    contest.UsernamesWithPoints.Add(username, points);
                else
                    contest.UsernamesWithPoints[username] = Math.Max(contest.UsernamesWithPoints[username], points);

                    input = Console.ReadLine().Split(" -> ");
            }
            foreach (Contest contest in contests)
            {
                int index = 1;
                Console.WriteLine($"{contest.Name}: {contest.UsernamesWithPoints.Count} participants");
                foreach (var item in contest.UsernamesWithPoints.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
                {
                    Console.WriteLine($"{index}. {item.Key} <::> {item.Value}");
                    index++;
                }
                foreach (var item in contest.UsernamesWithPoints)
                {
                    if (individualStandings.ContainsKey(item.Key))
                    {
                        individualStandings[item.Key] += item.Value;
                    }
                    else
                    {
                        individualStandings[item.Key] = item.Value;
                    }
                }
            }
            int secondIndex = 1;
            Console.WriteLine("Individual standings:");
            foreach (var item in individualStandings.OrderByDescending(x => x.Value).ThenBy(g => g.Key))
            {
                Console.WriteLine($"{secondIndex}. {item.Key} -> {item.Value}");
                secondIndex++;
            }
        }
    }

    public class Contest
    {
        public Contest(string name, Dictionary<string, int> usernameWithPoints)
        {
            Name = name;
            UsernamesWithPoints = usernameWithPoints;
        }
        public string Name { get; private set; }
        public Dictionary<string, int> UsernamesWithPoints { get; set; }
    }
}
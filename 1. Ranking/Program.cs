using System;
using System.Collections.Generic;
using System.Linq;

namespace _1._Ranking
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> contests =
                new Dictionary<string, string>();
            List<Student> students = new List<Student>();
            string[] contestsWithPasswords = Console.ReadLine().Split(':');
            while (contestsWithPasswords[0]!="end of contests")
            {
                contests.Add(contestsWithPasswords[0], contestsWithPasswords[1]);
                contestsWithPasswords = Console.ReadLine().Split(':');
            }
            string[] usernamesWithPoints = Console.ReadLine().Split("=>");
            while (usernamesWithPoints[0]!="end of submissions")
            {
                string contest = usernamesWithPoints[0];
                string password = usernamesWithPoints[1];
                if (contests.ContainsKey(contest) && contests[contest]==password)
                {
                    string username = usernamesWithPoints[2];
                    int points = int.Parse(usernamesWithPoints[3]);
                    if (!students.Any(g => g.Name == username))
                    {
                        Dictionary<string, int> studentDetails =
                            new Dictionary<string, int>();
                        studentDetails.Add(contest, points);
                        Student student = new Student(username, studentDetails);
                        students.Add(student);
                    }
                    else
                    {
                        Student selectedStudent = students.First(x => x.Name == username);
                        if (!selectedStudent.Courses.ContainsKey(contest))
                        {
                            selectedStudent.Courses.Add(contest, points);
                        }
                        else if (students.Any(g => g.Name == username) && students.Any(x => x.Courses.ContainsKey(contest)))
                        {
                            int selectedPoints = selectedStudent.Courses[contest];
                            if (selectedPoints < points)
                            {
                                selectedStudent.Courses[contest] = points;
                            }
                        }
                    }
                }
                usernamesWithPoints = Console.ReadLine().Split("=>");
            }
            string bestStudent = string.Empty;
            int maxPoints = 0;
            foreach (Student student in students)
            {
                int studentMaxPoints = student.Courses.Values.Sum();
                if (maxPoints<studentMaxPoints)
                {
                    maxPoints = studentMaxPoints;
                    bestStudent = student.Name;
                }
            }
                Console.WriteLine($"Best candidate is {bestStudent} with total {maxPoints} points.");
            Console.WriteLine("Ranking:");
            foreach (Student student in students.OrderBy(x => x.Name))
            {
                Console.WriteLine($"{student.Name}");
                foreach (var course in student.Courses.OrderByDescending(x=>x.Value))
                {
                    Console.WriteLine($"#  {course.Key} -> {course.Value}");
                }
            }
        }
    }
    public class Student
    {
        public Student(string name, Dictionary<string, int> courses)
        {
            Name = name;
            Courses = courses;
        }
       public string Name { get; private set; }
        public Dictionary<string, int> Courses { get; set; }
    }
}

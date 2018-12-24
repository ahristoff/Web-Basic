
namespace _1_School_Competition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            var scores = new Dictionary<string, int>();
            var categories = new Dictionary<string, SortedSet<string>>();

            while (true)
            {
                var line = Console.ReadLine();

                if (line == "END")
                {
                    break;
                }

                var parts = line.Split();
                var name = parts[0];
                var category = parts[1];
                var score = int.Parse(parts[2]);

                if (!scores.ContainsKey(name))
                {
                    scores[name] = 0;
                }

                if (!categories.ContainsKey(name))
                {
                    categories[name] = new SortedSet<string>();
                }

                scores[name] += score;
                categories[name].Add(category);
            }

            var orderdStudents = scores
                .OrderByDescending(k => k.Value)
                .ThenBy(k => k.Key);

            foreach (var x in orderdStudents)
            {
                var name = x.Key;
                var score = x.Value;
                var studentCategories = categories[name];

                var categotyText = $"[{string.Join(", ", studentCategories)}]";
                Console.WriteLine($"{name}: {score} {categotyText}");
            }
        }
    }
}

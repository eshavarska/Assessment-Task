using System;
using System.Collections.Generic;
using System.Text;

namespace Assestment_task
{
    class Report
    {
        public string Country { get; set; }
        public double AverageScore { get; set; }
        public double MedianScore { get; set; }
        public int MaxScore { get; set; } = 0;
        public string MaxScorePerson { get; set; }
        public int MinScore { get; set; } = 0;
        public string MinScorePerson { get; set; }
        public int RecordCount { get; set; } = 0;
        public int TotalScore { get; set; } = 0;

        public List<int> Scores;

        public Report(string country, int score, string firstName, string lastName)
        {
            Country = country;
            MaxScore = score;
            MaxScorePerson = firstName + " " + lastName;
            MinScore = score;
            MinScorePerson = firstName + " " + lastName;
            TotalScore = score;
            Scores = new List<int>() { score };
        }

    }
}

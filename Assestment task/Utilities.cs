using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Assestment_task
{
    class Utilities
    {
        //Function to decide whether median should be calculated for an even or odd amount of numbers
        public static double CalculateMedian(List<int> list)
        {
          if (list.Count%2 == 0)
            {
                int k = list.Count / 2;
                int l = k + 1;
                return CalculateMedianEven(list, k, l);
            }
            else
            {
                int k = (list.Count / 2) + 1;
                return CalculateMedianOdd(list, k);

            }
        }

        //Function to calculate median for even list of numbers. Calculates the two middle elements separately and finds the average.
        private static double CalculateMedianEven(List<int> list, int k, int l)
        {
            double a = CalculateMedianOdd(list, k);
            double b = CalculateMedianOdd(list, l);

            return (a + b) / 2;
        }

        //Function to calculate median for odd list of numbers. Uses the Quick Select Algorithm, which does not require a sorted array and has an average complexity of O(n).
        private static int CalculateMedianOdd(List<int> list, int k)
        {
            int pivot = list[0];

            if (list.Count == 1)
            {
                return pivot;
            }
            else
            {
                List<int> left = new List<int>();
                List<int> equal = new List<int>();
                List<int> right = new List<int>();

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] < pivot)
                    {
                        left.Add(list[i]);
                    }
                    else if(list[i] == pivot)
                    {
                        equal.Add(list[i]);
                    }
                    else
                    {
                        right.Add(list[i]);
                    }

                }

                if (left.Count >= k)
                {
                    pivot = CalculateMedianOdd(left, k);
                }
                else if (left.Count < k && (left.Count + equal.Count >= k))
                {
                    return pivot;
                }
                else
                {
                    pivot = CalculateMedianOdd(right, k - left.Count - equal.Count);
                }

                return pivot;
            }
        }

        public static List<Report> CalculateResults(List<Person> data)
        {
            List<Report> results = new List<Report>();

            for (int i = 0; i < data.Count; i++)
            {
                // Check if country has already been added to the results list
                int index = results.FindIndex(report => report.Country == data[i].Country);
                //If yes, compare min and max score and update the data. Increase record count and add the score to the scores list and to the total number (for calculating average).
                if (index >= 0)
                {
                    if (results[index].MaxScore < data[i].Score)
                    {
                        results[index].MaxScore = data[i].Score;
                        results[index].MaxScorePerson = data[i].FirstName + " " + data[i].LastName;
                    }
                    if (results[index].MinScore > data[i].Score)
                    {
                        results[index].MinScore = data[i].Score;
                        results[index].MinScorePerson = data[i].FirstName + " " + data[i].LastName;
                    }
                    results[index].TotalScore += data[i].Score;
                    results[index].RecordCount++;
                    results[index].Scores.Add(data[i].Score);

                }
                //If no, create a new record for the country and add to the results list. Increase record count.
                else
                {
                    Report current = new Report(data[i].Country, data[i].Score, data[i].FirstName, data[i].LastName);
                    current.RecordCount++;
                    results.Add(current);

                }
            }


            return results;
        }

        //Create new .csv file and add headings
        public static void CreateCSV(string country, string averageScore, string medianScore, string maxScore, string maxScorePerson, string minScore, string minScorePerson, string recordCount, string filePath)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, false))
                {
                    file.WriteLine(country + ";" + averageScore + ";" + medianScore + ";" + maxScore + ";" + maxScorePerson + ";" + minScore + ";" + minScorePerson + ";" + recordCount);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error: ", ex);
            }
        }

        //Add new record to the .csv file
        public static void AddRecord(string country, string averageScore, string medianScore, string maxScore, string maxScorePerson, string minScore, string minScorePerson, string recordCount, string filePath)
        {
            try
            {
                using(System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
                {
                    file.WriteLine(country + ";" + averageScore + ";" + medianScore + ";" + maxScore + ";" + maxScorePerson + ";" + minScore + ";" + minScorePerson + ";" + recordCount);
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error: ", ex);
            }
        }
  
    }


    

}

using System;
using System.Collections.Generic;
using System.Text;

namespace Assestment_task
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int Score { get; set; }

        public Person(string firstName, string lastName, string country, string city, string score)
        {
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            City = city;

            try
            {
                Score = Int32.Parse(score);
            }
            catch (FormatException)
            {
                Console.WriteLine("There is an error in the .csv file scores data by " + FirstName + " " + LastName);
                System.Environment.Exit(1);
            }
        }
    }
}

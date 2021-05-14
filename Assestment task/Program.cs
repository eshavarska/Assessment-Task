using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using Microsoft.VisualBasic.FileIO;
using EmailService;
using AegisImplicitMail;

namespace Assestment_task
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath, senderAddress, password, receiverAddress;
            List<Person> data = new List<Person>();

            Console.Write("Please enter the file path: ");
            filePath = Console.ReadLine();

            Console.Write("Please enter the sender email address: ");
            senderAddress = Console.ReadLine();

            Console.Write("Please enter the password for the account: ");
            password = Console.ReadLine();

            Console.Write("Please enter the receiver email address: ");
            receiverAddress = Console.ReadLine();

            Console.WriteLine();

            //Input the data from the .csv file into a list
            try
            {
                using (TextFieldParser csvParser = new TextFieldParser(filePath))
                {
                    csvParser.SetDelimiters(new string[] { ";" });

                    // Skip the row with the column names
                    csvParser.ReadLine();

                    while (!csvParser.EndOfData)
                    {
                        // Read current line fields, pointer moves to the next line.
                        string[] fields = csvParser.ReadFields();
                        //Add fields as a new object of type Person to the list
                        data.Add(new Person(fields[0], fields[1], fields[2], fields[3], fields[4]));
                    }
                }
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.Write("File \"" + filePath + "\" cannot be found");
                System.Environment.Exit(1);
            }

            //Find min and max score, min and max score person, record count
            List<Report> results = Utilities.CalculateResults(data);



            //Calculating median and average
            for (int i = 0; i < results.Count; i++)
            {
                results[i].AverageScore = Math.Round((double)results[i].TotalScore / (double)results[i].RecordCount, 2);
                results[i].MedianScore = Utilities.CalculateMedian(results[i].Scores);
            }

            //Sort results list in-place by Average Score in a descending order
            results.Sort((x, y) => y.AverageScore.CompareTo(x.AverageScore));



            //Create the ReportByCountry.csv file and add the headings
            Utilities.CreateCSV("Country", "Average score", "Median score", "Max score", "Max score person", "Min score", "Min score person", "Record count", "ReportByCountry.csv");

            //Loop through the data and add to the ReportByCountry.csv file
            for (int i = 0; i < results.Count; i++)
            {
                Utilities.AddRecord(results[i].Country, results[i].AverageScore.ToString(), results[i].MedianScore.ToString(), results[i].MaxScore.ToString(),
                    results[i].MaxScorePerson, results[i].MinScore.ToString(), results[i].MinScorePerson, results[i].RecordCount.ToString(), "ReportByCountry.csv");
            }


            //Send ReportByCountry.csv via email
            try
            {
               var mailArgs = new MailArguments
                {
                    MailFrom = senderAddress,
                    Password = password,
                    MailTo = receiverAddress,
                    Subject = "Report By Country",
                    Message = "Report by country is attached.",
                    Port = 465,
                    SmtpHost = "smtp.abv.bg"
                };

                List<MimeAttachment> listAttachments = new List<MimeAttachment>
                {
                 new MimeAttachment("ReportByCountry.csv") //Adding .csv file to the list
                };


                //Send e-mail and display message whether it was successful
                Mail.SendEMail(mailArgs, listAttachments, true, null);
                //Console.WriteLine(Mail.SendEMail(mailArgs, listAttachments, true, null).Item2);

            }
            catch (System.FormatException)
            {
                Console.WriteLine("One or both email addresses are not in the form required for an e-mail address (example@mail.com).");
            }
            

            
            Console.WriteLine("Sending e-mail... ");
            Console.WriteLine();
        }
    }
}

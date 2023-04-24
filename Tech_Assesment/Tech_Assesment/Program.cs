using Newtonsoft.Json;
using System.Runtime.ExceptionServices;
using System.Text.Json.Serialization;
using Tech_Assesment.Models;

namespace Tech_Assesment
{
    class Program
    {
static async Task Main(string[] args)
        {
            string url = "https://reclique.github.io/web-dev-testing/1_accounting_game/questions.json";
            //an http client to send the get request
            HttpClient httpClient = new HttpClient();

            try
            {
                var httpResponseMessage = await httpClient.GetAsync(url);
                //read the string from the response's content.
                string jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

                //Deserialize the json response into a C# arrays of their own types.
                dynamic myQuestions = JsonConvert.DeserializeObject<Question[]>(jsonResponse);

                bool correct = false;
                string basis = "";

                int i = 0;                
                foreach (dynamic question in myQuestions)
                {
                    i = 0;
                    basis = "";

                    Console.Write(question.title);
                    Console.Write(": ");
                    Console.WriteLine(question.description, "\n");

                    do
                    {
                        try
                        {
                            Console.WriteLine("cash or accrual?");
                            basis = Console.ReadLine().ToLower();
                            Console.WriteLine();
                        }
                        catch
                        {
                            Console.WriteLine("Please enter a valid account basis");
                        }
                    } while (basis != "cash" && basis != "accrual");
                    bool questionComplete = false;

                    int maxRows = 0;

                    if (basis == "cash")
                        maxRows = question.correct_answers[0].entries.Length;
                    else
                        maxRows = question.correct_answers[1].entries.Length;
                    

                    List<string> date = new List<string>();
                    List<string> type = new List<string>();
                    List<int> cr = new List<int>();
                    List<int> dr = new List<int>();                                               

                    for (int j = 0; j < maxRows; j++)
                    {
                        string nextDate = "";
                        string nextType = "";
                        int nextCr = -1;
                        int nextDr = -1;

                        Console.WriteLine("Row: " + (j+1) + "\n");

                        Console.Write("When (m/d): ");
                        do
                        {
                            try
                            {
                                nextDate = Console.ReadLine().ToLower();
                            }
                            catch
                            {
                                Console.WriteLine("Please enter a valid date");
                            }
                        }
                        while (nextDate == "");
                        date.Add(nextDate);
                        Console.WriteLine();

                        Console.Write("Type: ");
                        do
                        {
                            try
                            {
                                nextType = (Console.ReadLine().ToLower());
                            }
                            catch
                            {
                                Console.WriteLine("Please enter a valid type");
                            }
                        }
                        while (nextType == "");
                        type.Add(nextType);
                        Console.WriteLine();

                        Console.Write("cr: ");                        
                        do
                        {
                            try
                            {
                                nextCr = (int.Parse(Console.ReadLine()));
                            }
                            catch 
                            {
                                Console.WriteLine("Please enter a valid number");
                            }
                        }
                        while (nextCr == -1);
                        cr.Add(nextCr);
                        Console.WriteLine();

                        Console.Write("dr: ");
                        do
                        {
                            try
                            {
                                nextDr = (int.Parse(Console.ReadLine()));
                            }
                            catch
                            {
                                Console.WriteLine("Please enter a valid number");
                            }
                        }
                        while (nextDr == -1);
                        dr.Add(nextDr);
                        Console.WriteLine();                        
                    }

                    questionComplete = true;

                    do
                    {
                        foreach (dynamic entry in question.correct_answers)
                        {
                            int entries = entry.entries.Length;
                            int l = 0;
                            if (date[l] == entry.entries[l].when
                                & date[l + 1] == entry.entries[l + 1].when
                                & type[l] == entry.entries[l].type
                                & type[l + 1] == entry.entries[l + 1].type
                                & cr[l] == entry.entries[l].Cr
                                & cr[l + 1] == entry.entries[l + 1].Cr
                                & dr[l] == entry.entries[l].Dr
                                & dr[l + 1] == entry.entries[l + 1].Dr)
                                correct = true;
                            l++;
                        }
                        questionComplete = false;

                        if (correct == true)
                            Console.WriteLine("Correct!\n");
                        else
                            Console.WriteLine("Incorrect\n");
                        
                        correct = false;
                    }
                    while (questionComplete == true);
                }                
            }
            catch (Exception e)
            {
                //print the exception message
                Console.WriteLine(e.Message);
            }
        }
    }
}
using System;

namespace DORAScoring
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Please provide exactly 4 arguments: Deployment frequency, Lead time, Change fail percentage, Time to restore.");
                Console.WriteLine("Here are the possible options for each argument:");
                DisplayPossibleInputArgs();
                return;
            }

            string deployFrequency = args[0];
            string leadTime = args[1];
            string changeFailPercentage = args[2];
            string timeToRestore = args[3];


            List<string> invalidInputs = new List<string>();
            if (!IsValidDeployFrequency(deployFrequency))
            {
                invalidInputs.Add("Deployment Frequency: "+args[0]);
            }

            if (!IsValidLeadTime(leadTime))
            {
                invalidInputs.Add("Lead Time: "+args[1]);
            }

            if (!IsValidChangeFailPercentage(changeFailPercentage))
            {
                invalidInputs.Add("Change Fail Percentage: "+args[2]);
            }

            if (!IsValidTimeToRestore(timeToRestore))
            {
                invalidInputs.Add("Time to Restore: "+args[3]);
            }

            if (invalidInputs.Count > 0)
            {
                Console.WriteLine("The following inputs are invalid: " + string.Join(", ", invalidInputs));
                DisplayPossibleInputArgs();
                return;
            }

            string performanceCategory = GetPerformanceCategory(deployFrequency, leadTime, changeFailPercentage, timeToRestore);

            Console.WriteLine("Performance Category: " + performanceCategory);
        }

        static bool IsValidDeployFrequency(string deployFrequency)
        {
            List<string> validOptions = new List<string>
            {
                "Fewer than once per six months",
                "Between once per month and once every six months",
                "Between once per week and once per month",
                "Between once per day and once per week",
                "Between once per hour and once per day",
                "On demand (multiple deploys per day)"
            };

            return validOptions.Exists(option => option.Equals(deployFrequency, StringComparison.OrdinalIgnoreCase));
        }
        static bool IsValidLeadTime(string leadTime)
        {
            List<string> validOptions = new List<string>
            {
                "More than six months",
                "One to six months",
                "One week to one month",
                "One day to one week",
                "Less than one day",
                "Less than one hour"
            };

            return validOptions.Exists(option => option.Equals(leadTime, StringComparison.OrdinalIgnoreCase));
        }
        static bool IsValidChangeFailPercentage(string changeFailPercentage)
        {
            List<string> validOptions = new List<string>
            {
                "0–15%",
                "16–30%",
                "31–45%",
                "46–60%",
                "61–75%",
                "76–100%"
            };

            return validOptions.Exists(option => option.Equals(changeFailPercentage, StringComparison.OrdinalIgnoreCase));
        }
        static bool IsValidTimeToRestore(string timeToRestore)
        {
            List<string> validOptions = new List<string>
            {
                "More than six months",
                "One to six months",
                "One week to one month",
                "One day to one week",
                "Less than one day",
                "Less than one hour"
            };

            return validOptions.Exists(option => option.Equals(timeToRestore, StringComparison.OrdinalIgnoreCase));
        }



        static string GetPerformanceCategory(string deployFrequency, string leadTime, string changeFailPercentage, string timeToRestore)
        {
            int lowScore = CalculateScore(deployFrequency, leadTime, changeFailPercentage, timeToRestore, "Low");
            int mediumScore = CalculateScore(deployFrequency, leadTime, changeFailPercentage, timeToRestore, "Medium");
            int highScore = CalculateScore(deployFrequency, leadTime, changeFailPercentage, timeToRestore, "High");
            int eliteScore = CalculateScore(deployFrequency, leadTime, changeFailPercentage, timeToRestore, "Elite");

            // Determine which two categories the person falls between
            if (lowScore > 0 && mediumScore > 0)
            {
                return "Between Low and Medium";
            }
            else if (mediumScore > 0 && highScore > 0)
            {
                return "Between Medium and High";
            }
            else if (highScore > 0 && eliteScore > 0)
            {
                return "Between High and Elite";
            }

            // If the scores are clearly within one category
            if (eliteScore > 0)
            {
                return "Elite";
            }
            else if (highScore > 0)
            {
                return "High";
            }
            else if (mediumScore > 0)
            {
                return "Medium";
            }
            else if (lowScore > 0)
            {
                return "Low";
            }

            return "Inputs do not match any performance category.";
        }

        static void DisplayPossibleInputArgs()
        {
            Console.WriteLine("Here are the possible options for each argument:");
            Console.WriteLine("1. Deployment frequency:");
            Console.WriteLine("   - Fewer than once per six months");
            Console.WriteLine("   - Between once per month and once every six months");
            Console.WriteLine("   - Between once per week and once per month");
            Console.WriteLine("   - Between once per day and once per week");
            Console.WriteLine("   - Between once per hour and once per day");
            Console.WriteLine("   - On demand (multiple deploys per day)");

            Console.WriteLine("2. Lead time:");
            Console.WriteLine("   - More than six months");
            Console.WriteLine("   - One to six months");
            Console.WriteLine("   - One week to one month");
            Console.WriteLine("   - One day to one week");
            Console.WriteLine("   - Less than one day");
            Console.WriteLine("   - Less than one hour");

            Console.WriteLine("3. Change fail percentage:");
            Console.WriteLine("   - 0–15%");
            Console.WriteLine("   - 16–30%");
            Console.WriteLine("   - 31–45%");
            Console.WriteLine("   - 46–60%");
            Console.WriteLine("   - 61–75%");
            Console.WriteLine("   - 76–100%");

            Console.WriteLine("4. Time to restore:");
            Console.WriteLine("   - More than six months");
            Console.WriteLine("   - One to six months");
            Console.WriteLine("   - One week to one month");
            Console.WriteLine("   - One day to one week");
            Console.WriteLine("   - Less than one day");
            Console.WriteLine("   - Less than one hour");
        }

        static int CalculateScore(string deployFrequency, string leadTime, string changeFailPercentage, string timeToRestore, string category)
        {
            int score = 0;

            // Define the criteria for each category
            var criteria = new Dictionary<string, Dictionary<string, List<string>>>
            {
                {
                    "Low", new Dictionary<string, List<string>>
                    {
                        {"deployFrequency", new List<string> {"Between once per month and once every six months"}},
                        {"leadTime", new List<string> {"One to six months"}},
                        {"changeFailPercentage", new List<string> {"46–60%"}},
                        {"timeToRestore", new List<string> {"One week to one month"}}
                    }
                },
                {
                    "Medium", new Dictionary<string, List<string>>
                    {
                        {"deployFrequency", new List<string> {"Between once per week and once per month"}},
                        {"leadTime", new List<string> {"One week to one month"}},
                        {"changeFailPercentage", new List<string> {"31–45%"}},
                        {"timeToRestore", new List<string> {"Less than one day"}}
                    }
                },
                {
                    "High", new Dictionary<string, List<string>>
                    {
                        {"deployFrequency", new List<string> {"Between once per day and once per week"}},
                        {"leadTime", new List<string> {"One day to one week"}},
                        {"changeFailPercentage", new List<string> {"0–15%"}},
                        {"timeToRestore", new List<string> {"Less than one day"}}
                    }
                },
                {
                    "Elite", new Dictionary<string, List<string>>
                    {
                        {"deployFrequency", new List<string> {"On demand (multiple deploys per day)"}},
                        {"leadTime", new List<string> {"Less than one day"}},
                        {"changeFailPercentage", new List<string> {"0–15%"}},
                        {"timeToRestore", new List<string> {"Less than one hour"}}
                    }
                }
            };

            // Check how closely the inputs match the criteria for the given category
            if (criteria[category]["deployFrequency"].Contains(deployFrequency))
            {
                score++;
            }
            if (criteria[category]["leadTime"].Contains(leadTime))
            {
                score++;
            }
            if (criteria[category]["changeFailPercentage"].Contains(changeFailPercentage))
            {
                score++;
            }
            if (criteria[category]["timeToRestore"].Contains(timeToRestore))
            {
                score++;
            }

            return score;
        }
    }
}

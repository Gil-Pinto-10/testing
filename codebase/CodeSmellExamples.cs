// CodeSmellExamples.cs
namespace MyTestWebService.Vulnerabilities
{
    public static class CodeSmellExamples
    {
        // Method with too many parameters (code smell)
        private static string ProcessUserData(string id, string name, string email, string address, string city, string postalCode, string country, string phone) // S107: Methods should not have too many parameters
        {
            // Imagine complex processing here
            return $"Processed data for {name} in {city} via modular call.";
        }

        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("/unused-var-modular", () => {
                int someValue = 42;
                int anotherValue = 100; // S1481: Remove the unused local variable 'anotherValue'.
                string unusedString;    // S2931 / S1481

                return $"The modular value is {someValue}. Something was unused.";
            });

            app.MapGet("/user-processing-modular", () => {
                return ProcessUserData("2", "Modular User", "modular@example.com", "456 Main St", "ModularVille", "67890", "ModularLand", "555-0200");
            });

            app.MapGet("/complex-logic-modular/{value:int}", (int value) =>
            {
                string result = "Default Modular";
                if (value > 0) // +1
                {
                    if (value < 10) // +2
                    {
                        result = "Small positive modular";
                        for (int i = 0; i < value; i++) // +3
                        {
                            if (i % 2 == 0) // +4
                            {
                                Console.WriteLine($"Modular Even: {i}");
                            }
                        }
                    }
                    else if (value < 100) // +2
                    {
                        result = "Medium positive modular";
                    }
                    else
                    {
                        result = "Large positive modular";
                    }
                }
                else
                {
                    result = "Not positive modular";
                }
                return result; // S3776: Cognitive Complexity
            });
        }
    }
}// CodeSmellExamples.cs
namespace MyTestWebService.Vulnerabilities
{
    public static class CodeSmellExamples
    {
        // Method with too many parameters (code smell)
        private static string ProcessUserData(string id, string name, string email, string address, string city, string postalCode, string country, string phone) // S107: Methods should not have too many parameters
        {
            // Imagine complex processing here
            return $"Processed data for {name} in {city} via modular call.";
        }

        public static void MapEndpoints(WebApplication app)
        {
            app.MapGet("/unused-var-modular", () => {
                int someValue = 42;
                int anotherValue = 100; // S1481: Remove the unused local variable 'anotherValue'.
                string unusedString;    // S2931 / S1481

                return $"The modular value is {someValue}. Something was unused.";
            });

            app.MapGet("/user-processing-modular", () => {
                return ProcessUserData("2", "Modular User", "modular@example.com", "456 Main St", "ModularVille", "67890", "ModularLand", "555-0200");
            });

            app.MapGet("/complex-logic-modular/{value:int}", (int value) =>
            {
                string result = "Default Modular";
                if (value > 0) // +1
                {
                    if (value < 10) // +2
                    {
                        result = "Small positive modular";
                        for (int i = 0; i < value; i++) // +3
                        {
                            if (i % 2 == 0) // +4
                            {
                                Console.WriteLine($"Modular Even: {i}");
                            }
                        }
                    }
                    else if (value < 100) // +2
                    {
                        result = "Medium positive modular";
                    }
                    else
                    {
                        result = "Large positive modular";
                    }
                }
                else
                {
                    result = "Not positive modular";
                }
                return result; // S3776: Cognitive Complexity
            });
        }
    }
}

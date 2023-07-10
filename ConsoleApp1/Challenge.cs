using System.Text.Json;
using System.Text.RegularExpressions;

class Challenge
{
    static void Main()
    {
        
        try {
            Console.Write("Enter path of assets.txt file: ");
            string? filePath = Console.ReadLine();
            string jsonContent = File.ReadAllText(filePath);
            JsonDocument document = JsonDocument.Parse(jsonContent);
            JsonElement assets = document.RootElement.GetProperty("assets");

            string? input = System.String.Empty;// string for user input
            while (!input.Equals("x"))
            {
                try
                {
                    Console.Write("Enter search criteria (x to close): ");
                    input = Console.ReadLine();
                    if (input.StartsWith("x") || input.Equals(""))
                    {
                        break;
                    }

                    int numAssets = 0;// tracks the number of assets identified by the search
                    string[] inputArgs = input.Split(' ');
                    Dictionary<string, string> arguments = new Dictionary<string, string>();
                    string[] tokens = input.Split("--");

                    for (int i = 1; i < tokens.Length; i++)
                    {
                        string token = tokens[i].Trim();
                        int space = token.IndexOf(' ');
                        if (space >= 0)
                        {
                            string key = token.Substring(0, space);
                            string value = token.Substring(space + 1);
                            arguments[key] = value;
                        }
                    }

                    Console.WriteLine("-----------------------Results-----------------------");
                    foreach (JsonElement asset in assets.EnumerateArray())
                    {
                        bool foundAsset = false;
                        foreach (KeyValuePair<string, string> argument in arguments)
                        {
                            JsonElement element = asset.GetProperty(argument.Key);

                            if (argument.Key.Equals("status"))
                            {
                                string temp = "";
                                switch (argument.Value)
                                {
                                    case "normal":
                                        temp = "1";
                                        break;
                                    case "warning":
                                        temp = "2";
                                        break;
                                    case "critical":
                                        temp = "3";
                                        break;
                                    default:
                                        temp = argument.Value;
                                        break;
                                }
                                if (WildcardMatch(element.ToString(), temp))
                                {
                                    foundAsset = true;
                                }
                                else
                                {
                                    foundAsset = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (WildcardMatch(element.ToString(), argument.Value))
                                {
                                    foundAsset = true;
                                }
                                else
                                {
                                    foundAsset = false;
                                    break;
                                }
                            }
                        }
                        if (foundAsset)
                        {
                            numAssets++;
                            Console.WriteLine(asset);
                            Console.WriteLine("-----------------------------------------------------");
                        }
                    }
                    Console.WriteLine("Num Assets: " + numAssets + "\n");

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message + "\n");
                }// end of nested try-catch
            }// end of while loop
        }
        catch(Exception e)
        {
            Console.WriteLine("Error: " + e.Message + "\n");
        }// end of try-catch

        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
    }// end of main

    // WildcardMatch - uses regex to match assets that fit a particular pattern ex. --name newt* 
    static bool WildcardMatch(string input, string pattern)
    {
        string regexPattern = "^" + Regex.Escape(pattern).Replace(@"\*", ".*").Replace(@"\?", ".") + "$";
        return Regex.IsMatch(input, regexPattern, RegexOptions.IgnoreCase);
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace JoshConsoleExperienceTracker
{
    class Program
    {
        

        static void Main(string[] args)
        {
            // Setting a default window size for easier reading.
            Console.SetWindowSize(100, 30);

            // List collection for the text files that will be read in.
            List<Character> listOfCharacters = new List<Character>();

            // Instance of program class used for calling other necessary functions.
            Program programInstance = new Program();

            // Used for reading in a file line-by-line.
            string line;

            // If the directory doesn't exist, creates it. Otherwise, it does nothing.
            System.IO.Directory.CreateDirectory(@"C:/Users/Public/Documents/CharacterData");

            // If the file doesn't exist in the path created, create the file, and add a sample line.
            if (!File.Exists("C:/Users/Public/Documents/CharacterData/JoshCharacterDataInput.txt"))
            {
                Console.WriteLine("JoshCharacterDataInput.txt not found in the proper directory. \nCreating a sample file so application can continue." +
                    "\nRefer to menu options to modify data as necessary. \n\nIt is also worth noting that if there was not a CharacterData directory," +
                    "\none has already been created in the public documents folder, so as to not place the files\nhard-to-reach folders.\n\n");
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:/Users/Public/Documents/CharacterData/JoshCharacterDataInput.txt"))
                {
                    file.WriteLine("Sample Line 0 0");
                }
            }

            // Read in and create instances of character data read in for each line.
            using (StreamReader sr = new StreamReader("C:/Users/Public/Documents/CharacterData/JoshCharacterDataInput.txt"))
            {
                try
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] words = line.Split(' ');
                        listOfCharacters.Add(new Character(words[0], words[1], Convert.ToInt32(words[2]), Convert.ToDouble(words[3])));
                    }// End While-Loop.
                }
                catch(IndexOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("What the above error message indicates is that there is an error reading in the text file. \n" +
                        "Please check the text file to make sure that there are 4 'words' separated by spaces, as the program\n" +
                        "only takes in 4 variables for data entry. Specifically, it uses two words, a number, and another number.");
                    Environment.Exit(1);
                }
            }

            //Initial data display
            programInstance.DisplayData(listOfCharacters);


            //Main menu of the program.
            while(true)
            {
                Console.WriteLine("\n\n-----------------------\n    0.) Exit Application\n    1.) Display Character Data\n    2.) Add EXP" +
                    "\n    3.) Change Character Values\n    4.) Add Character\n    5.) Remove Character\n    " +
                    "6.) Reset EXP for all characters" +
                    "\n-----------------------\n\nDo what? (Enter Number)");
                // Checks menu response to make sure it is a valid entry.
                if (Int32.TryParse(Console.ReadLine(), out int startingMenuResponse))
                {
                    // Calls appropriate function based on what the user entered.
                    switch (startingMenuResponse)
                    {
                        case 0:
                            Console.WriteLine("Exiting program...");
                            Environment.Exit(0);
                            break;
                        case 1:
                            programInstance.DisplayData(listOfCharacters);
                            break;
                        case 2:
                            programInstance.AddExperience(listOfCharacters);
                            break;
                        case 3:
                            programInstance.ChangeCharacter(listOfCharacters);
                            break;
                        case 4:
                            programInstance.AddCharacter(listOfCharacters);
                            break;
                        case 5:
                            programInstance.RemoveCharacter(listOfCharacters);
                            break;
                        case 6:
                            programInstance.ResetExperience(listOfCharacters);
                            break;
                        default:
                            Console.WriteLine("ERROR: Invalid entry. Please enter a valid number option.\nPress any key to move on.");
                            Console.ReadKey();
                            break;
                    }// End Switch Block.
                }
                else
                {
                    Console.WriteLine("ERROR: Invalid response. Please enter the number corresponding to the action you wish to do." +
                        "\nPress any key to continue.");
                    Console.ReadKey();
                }// End If-Else block.
            }


        }

        void DisplayData(List<Character> currentList)
        {
            //Used for tracking total experience from character data.
            double experiencePool = 0;

            // Creates an initial header for labelling variables.
            Console.WriteLine("\n------------------------\n\t  Name\t\t\t Level\t\tEXP\n" +
            "------------------------");

            foreach (Character person in currentList)
            {
                person.Display();
                experiencePool += person.GetExperience();
            }

            Console.WriteLine($"\nCharacter display end.\n\nTotal experience pool: {experiencePool}\n\nPress any key to go to the menu.");
            Console.ReadKey();
        }

        //addEXP
        void AddExperience(List<Character> currentList)
        {
            
            while(true)
            {
                double experiencePool = 0;
                //display characters and their levels
                Console.WriteLine("\n\n\n------------------------\n    0.) Go Back");

                int count = 1;
                foreach (Character person in currentList)
                {
                    Console.WriteLine($"    {count}.) " + person.GetFullName() + "\t\tLevel: " + person.GetLevel()
                        + $"\tEXP Contributed: " + person.GetExperience());
                    count++;
                    experiencePool += person.GetExperience();
                }
                Console.WriteLine($"------------------------\n\tTotal EXP: {experiencePool}\n------------------------");

                Console.WriteLine("Add experience using who? (Enter number)");

                if (Int32.TryParse(Console.ReadLine(), out int menuNumberChoice))
                {
                    // Try-catch block for catching an exception if the user enters a number that is not associated with a character currently.
                    try
                    {
                        if (menuNumberChoice == 0)
                        {
                            return;
                        }
                        else
                        {
                            // Calling the function with the menu number choice, minus 1, since collections start at position 0, not 1.
                            // Note that, from looking at the menu, option 1 will always be the "lowest" collection entry, which corresponds
                            // to the number 0 entry.
                            currentList[menuNumberChoice - 1].AddExperience();
                            SaveData(currentList);
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("ERROR: Please enter a valid number of the menu options.\nPress any key to move on.");
                        Console.ReadKey();
                    }// End try-catch block.
                }
                else
                {
                    Console.WriteLine("ERROR: Invalid entry. Please enter a valid number option.\nPress any key to continue.");
                    Console.ReadKey();
                }//End if-else block.
            }
            
            
            //add exp using who's level?
            //kill enemy, capture enemy, job complete, or other?
            //if kill, enemy level?
            //if cap, enemy level?
            //if job complete, how much exp?
            //if other action, how much exp?
        }

        //change character values
        void ChangeCharacter(List<Character> currentList)
        {
            int menuNumberChoice = -1;

            while (menuNumberChoice != 0)
            {
                Console.WriteLine("\n\n----------------------");
                int count = 1;

                Console.WriteLine("    0.) Go Back");
                foreach (Character person in currentList)
                {
                    Console.WriteLine("    " + count + ".) " + person.GetFullName());
                    count++;
                }
                Console.WriteLine("----------------------\n\nChange values of who? (Enter number of option above): ");

                if (Int32.TryParse(Console.ReadLine(), out menuNumberChoice))
                {
                    if (menuNumberChoice == 0)
                    {
                        return;
                    }
                    else
                    {
                        try
                        {
                            currentList[menuNumberChoice - 1].ChangeValues();
                            SaveData(currentList);
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine("Error: Invalid menu entry. Please enter a valid number corresponding to your choice." +
                                "\nPress any key to move on.");
                            Console.ReadKey();
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Error: Invalid menu entry. Please enter a valid number corresponding to your choice.\nPress any key to move on.");
                    Console.ReadKey();
                }//End if-else block.
            }// End while loop.
        }

        //add character
        void AddCharacter(List<Character> currentList)
        {
            string firstName = "";
            string lastName = "";
            int characterLevel = 0;

            // Get first name - type '-' to cancel.
            while (true)
            {
                Console.WriteLine("\n\nAdding a new character." +
                    "\n\nWhat is the first name of this new character?\n(Alternatively, type '-' to cancel.)");
                firstName = Console.ReadLine();

                if (firstName.Equals("-"))
                {
                    Console.WriteLine("Cancelling action. Press any key to go back to the menu.");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    break;
                }
            }// End while loop.

            // Get last name - type '-' for no last name.
            while (true)
            {
                Console.WriteLine($"\n\nWhat is the last name of {firstName}?\n(Alternatively, type '-' if there is no last name.");

                lastName = Console.ReadLine();

                if (lastName.Equals("-"))
                {
                    lastName = "NoLastName";
                }
                break;
            }// End while loop.

            // Get level - can't be less than 1.
            while (true)
            {
                Console.WriteLine($"\n\nWhat is the level of {firstName} {lastName}?\nPlease enter a whole number, 1 or greater.");
                if (Int32.TryParse(Console.ReadLine(), out characterLevel))
                {
                    if (characterLevel < 1)
                    {
                        Console.WriteLine("Error: level of character cannot be less than 1.\nPlease enter a number that is 1 or greater." +
                            "\nPress any key to continue.");
                        Console.ReadKey();
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Error: Please enter a valid, whole number.\nPress any key to move on.");
                    Console.ReadKey();
                }
            }// End while loop.

            currentList.Add(new Character(firstName, lastName, characterLevel, 0.0));
            SaveData(currentList);

            // Exiting function
            Console.WriteLine("Character has been added to the data.\nPress any key to display new data, and go back to menu.");
            Console.ReadKey();
            DisplayData(currentList);
        }

        //remove character
        void RemoveCharacter(List<Character> currentList)
        {
            while (true)
            {
                Console.WriteLine("\n\n----------------------");
                int count = 1;

                Console.WriteLine("    0.) Go Back");
                foreach (Character person in currentList)
                {
                    Console.WriteLine("    " + count + ".) " + person.GetFullName());
                    count++;
                }

                Console.WriteLine("----------------------\n\nRemove who? (Enter number)");

                if (Int32.TryParse(Console.ReadLine(), out int removeWhoResponse))
                {
                    if (removeWhoResponse == 0)
                    {
                        return;
                    }
                    else
                    {
                        try
                        {
                            Console.WriteLine($"We're going to remove \n     " + currentList[removeWhoResponse - 1].GetFullName() + "\nAre you sure?" +
                            "\nEnter, in all caps, 'YES' (with all caps, too) if you are sure.");

                            string confirmRemoval = Console.ReadLine();

                            if (confirmRemoval.Equals("YES"))
                            {
                                currentList.RemoveAt(removeWhoResponse - 1);
                                SaveData(currentList);
                                Console.WriteLine("Removed the character. Press any key to go back to the menu.");
                                Console.ReadKey();
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Cancelling action. Press any key to move on.");
                                Console.ReadKey();
                            }
                        }
                        catch (ArgumentOutOfRangeException e)
                        {
                            Console.WriteLine(e.Message);
                            Console.WriteLine("\nSee above error. Please enter a number that corresponds to the options you have." +
                                "\nPress any key to move on.");
                            Console.ReadKey();
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Error: Invalid entry. Please enter a number corresponding to the menu selection." +
                        "\nPress any key to move on.");
                    Console.ReadKey();
                }// End if-else block.

                
            }//End while loop.
        }

        //reset exp
        void ResetExperience(List<Character> currentList)
        {
            Console.WriteLine("\n\nThis will set the set the experience contribution for every character back to 0.\n" +
                "Effectively, this means that the total EXP pool will go back to 0, for the purposes of preparing for the next battle.\n" +
                "Are you absolutely sure you're ready to do this? You cannot undo this action.\nEnter 'YES' (in all caps) if you are prepared.");

            string confirmReset = Console.ReadLine();

            if(confirmReset.Equals("YES"))
            {
                foreach(Character person in currentList)
                {
                    person.SetExperience(0);
                }
                SaveData(currentList);
                Console.WriteLine("Character EXP has been reset.\nPress any key to move on.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Cancelling...\nPress any key to move on.");
                Console.ReadKey();
            }
        }

        //saveData
        void SaveData(List<Character> currentList)
        {
            double totalExperience = 0.0;

            // Saving an input copy, for use upon booting the program again.
            Console.WriteLine("Saving data to a new input file output...");
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:/Users/Public/Documents/CharacterData/JoshCharacterDataInput.txt"))
                {
                    foreach (Character person in currentList)
                    {
                        file.WriteLine(person.GetFullName() + " " + person.GetLevel() + " " + person.GetExperience());
                        totalExperience += person.GetExperience();
                    }
                    
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error: File has not been saved. The given data path may not exist\nin the current run of the application." +
                    "\nPress any key to move on.");
                Console.ReadKey();
            }// End try-catch
            Console.WriteLine("File saved.");

            //Saving an output copy, for making it simple for a user to read it.
            Console.WriteLine("Saving data to a text file output...");
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:/Users/Public/Documents/CharacterData/JoshCharacterDataOutput.txt"))
                {
                    file.WriteLine("\t\t\t\tOUTPUT COPY");
                    file.WriteLine("*****************************************************************************");
                    file.WriteLine("\t  Name\t\t\tLevel    Experience");
                    file.WriteLine("*****************************************************************************");


                    foreach (Character person in currentList)
                    {
                        var temp = String.Format("    {0,-20}   {1,9}    {2,5}",
                            person.GetFullName(), person.GetLevel(), person.GetExperience());
                        file.WriteLine($"{temp}");
                    }
                    file.WriteLine("");
                    file.WriteLine("");
                    file.WriteLine("\tTotal Experience: " + totalExperience);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Error: Output file has not been saved. The given path in the source code may not exist in\n" +
                    "the current run of the application.\nPress any key to move on.");
                Console.ReadKey();
            }// End try-catch
            Console.WriteLine("File saved.");
        }
    }
}

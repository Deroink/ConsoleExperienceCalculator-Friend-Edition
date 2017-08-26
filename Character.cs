using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoshConsoleExperienceTracker
{
    class Character
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        int CharacterLevel { get; set; }
        double ExperienceContribution { get; set; }


        //These variable names are going to give me cancer, but I don't know what else to name them.
        //Why couldn't he have just used a formula, ughhhhhh.

        //Tier for enemies 7 levels under, or lower
        public double TierOne = 5;
        //Tier for enemies 4-3 levels under the player level
        public double TierTwo = 15;
        //Tier for enemies 2-1 levels under the player level
        public double TierThree = 25;
        //Tier for enemies with no level difference
        public double TierFour = 75;
        //Tier for enemies with 1-3 levels higher than the player level
        public double TierFive = 100;
        //Tier for enemies with 4-6 levels more than the player level
        public double TierSix = 150;
        //Tier for enemies with 7 or more levels higher than the player
        public double TierSeven = 250;

        public Character(string newFirst, string newLast, int newLevel, double newEXP)
        {
            FirstName = newFirst;
            LastName = newLast;
            CharacterLevel = newLevel;
            ExperienceContribution = newEXP;
        }

        public void Display()
        {
            Console.WriteLine("    {0,-8}{1,11}    {2,9}        {3,6}",
                FirstName, LastName, CharacterLevel.ToString(), ExperienceContribution.ToString());
        }

        public void AddExperience()
        {
            while(true)
            {
                Console.WriteLine($"\n\nAdding experience points using the shown character:\n\n\t {FirstName} {LastName}\n");
                Console.WriteLine("What action did the party do for EXP gain?\n---------------------------\n" +
                    "    0.) Go Back\n    1.) Kill Enemy\n    2.) Capture Enemy\n    3.) Complete Job/Quest\n    4.) Other" +
                    "\n    5.) Change tier EXP gains");

                if (Int32.TryParse(Console.ReadLine(), out int menuResposne))
                {
                    switch(menuResposne)
                    {
                        case 0:
                            return;
                        case 1:
                            Console.WriteLine("Enemy level? (Enter a whole number)");
                            if(Int32.TryParse(Console.ReadLine(), out int enemyLevel))
                            {
                                int levelDifference = enemyLevel - CharacterLevel;
                                double experienceGained = FindExperienceTier(levelDifference);

                                ExperienceContribution += experienceGained;

                                Console.WriteLine($"The party gained {experienceGained} experience points.\nNow at {ExperienceContribution} EXP total." +
                                    $"\nPress any key to move on.");
                                Console.ReadKey();
                                return;
                            }
                            else
                            {
                                Console.WriteLine("ERROR: Your response is not a valid option.\nPlease enter a number corresponding to the menu choice." +
                                     $"\nPress any key to move on.");
                                Console.ReadKey();
                            }
                            break;
                        case 2:
                            Console.WriteLine("Enemy level? (Enter a whole number)");
                            if (Int32.TryParse(Console.ReadLine(), out int levelOfEnemy))
                            {
                                int levelDifference = levelOfEnemy - CharacterLevel;
                                double experienceGained = FindExperienceTier(levelDifference) + 50;

                                ExperienceContribution += experienceGained;

                                Console.WriteLine($"The party gained {experienceGained} experience points.\nNow at {ExperienceContribution} EXP total." +
                                    $"\nPress any key to move on.");
                                Console.ReadKey();
                                return;
                            }
                            else
                            {
                                Console.WriteLine("ERROR: Your response is not a valid option.\nPlease enter a number corresponding to the menu choice." +
                                     $"\nPress any key to move on.");
                                Console.ReadKey();
                            }
                            break;
                        case 3:
                            Console.WriteLine("How much experience did the party get for completing this job? (Enter a number)");
                            if(Double.TryParse(Console.ReadLine(), out double jobExperience))
                            {
                                ExperienceContribution += jobExperience;
                                Console.WriteLine($"The party gained {jobExperience} experience points.\nNow at {ExperienceContribution} EXP total." +
                                    $"\nPress any key to move on.");
                                Console.ReadKey();
                                return;
                            }
                            else
                            {
                                Console.WriteLine("ERROR: Your response is not a valid option.\nPlease enter a number corresponding to the menu choice." +
                                     $"\nPress any key to move on.");
                                Console.ReadKey();
                            }
                            break;
                        case 4:
                            Console.WriteLine("How much experience did the party get for this action? (Enter a number)");
                            if (Double.TryParse(Console.ReadLine(), out double otherExperience))
                            {
                                ExperienceContribution += otherExperience;
                                Console.WriteLine($"The party gained {otherExperience} experience points.\nNow at {ExperienceContribution} EXP total." +
                                    $"\nPress any key to move on.");
                                Console.ReadKey();
                                return;
                            }
                            else
                            {
                                Console.WriteLine("ERROR: Your response is not a valid option.\nPlease enter a number corresponding to the menu choice." +
                                     $"\nPress any key to move on.");
                                Console.ReadKey();
                            }
                            break;
                        case 5:
                            Console.WriteLine("    0.) Go Back\n    1.) Tier One (Enemy is seven levels under)\n    " +
                                "2.) Tier Two (Enemy is 3 or 4 levels under the player level)\n    3.) Tier Three (Enemy is 1 or 2 levels under)\n    " +
                                "4.) Tier Four (No level difference)\n    5.) Tier Five (Enemy is 1-3 levels higher)\n    " +
                                "6.) Tier Six (Enemy is 4-6 levels higher)\n    7.) Tier Seven (Enemy is 7 or more levels higher)" +
                                "\n\nChange which tier? (Enter number)");

                            if(Int32.TryParse(Console.ReadLine(), out int changeWhichTier))
                            {
                                switch(changeWhichTier)
                                {
                                    case 0:
                                        Console.WriteLine("Cancelling...");
                                        break;
                                    case 1:
                                        Console.WriteLine($"Tier One's current EXP payout is {TierOne}.What is the new EXP amount?\n(Enter a number." +
                                            $" Alternatively, type a non-number to cancel.)");
                                        if(Double.TryParse(Console.ReadLine(), out double newTierOneEXP))
                                        {
                                            Console.WriteLine($"Changing Tier One's EXP payout to {newTierOneEXP}.\nEnter 'y' to confirm these changes.");
                                            string confirmChanges = Console.ReadLine();

                                            if(confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                            {
                                                TierOne = newTierOneEXP;

                                                Console.WriteLine($"Tier One's EXP payout is now {TierOne}.\nPress any key to move on.");
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                Console.WriteLine("Cancelling action.\nPress any key to move on.");
                                                Console.ReadKey();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Non-number detected. Cancelling action.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                        break;
                                    case 2:
                                        Console.WriteLine($"Tier Two's current EXP payout is {TierTwo}. What is the new EXP amount?\n(Enter a number." +
                                            $" Alternatively, type a non-number to cancel.)");
                                        if (Double.TryParse(Console.ReadLine(), out double newTierTwoEXP))
                                        {
                                            Console.WriteLine($"Changing Tier Two's EXP payout to {newTierTwoEXP}.\nEnter 'y' to confirm these changes.");
                                            string confirmChanges = Console.ReadLine();

                                            if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                            {
                                                TierTwo = newTierTwoEXP;

                                                Console.WriteLine($"Tier Two's EXP payout is now {TierTwo}.\nPress any key to move on.");
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                Console.WriteLine("Cancelling action.\nPress any key to move on.");
                                                Console.ReadKey();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Non-number detected. Cancelling action.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                        break;
                                    case 3:
                                        Console.WriteLine($"Tier Three's current EXP payout is {TierThree}. What is the new EXP amount?\n(Enter a number." +
                                            $" Alternatively, type a non-number to cancel.)");
                                        if (Double.TryParse(Console.ReadLine(), out double newTierThreeEXP))
                                        {
                                            Console.WriteLine($"Changing Tier Three's EXP payout to {newTierThreeEXP}.\nEnter 'y' to confirm these changes.");
                                            string confirmChanges = Console.ReadLine();

                                            if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                            {
                                                TierThree = newTierThreeEXP;

                                                Console.WriteLine($"Tier Three's EXP payout is now {TierThree}.\nPress any key to move on.");
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                Console.WriteLine("Cancelling action.\nPress any key to move on.");
                                                Console.ReadKey();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Non-number detected. Cancelling action.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                        break;
                                    case 4:
                                        Console.WriteLine($"Tier Four's current EXP payout is {TierFour}. What is the new EXP amount?\n(Enter a number." +
                                            $" Alternatively, type a non-number to cancel.)");
                                        if (Double.TryParse(Console.ReadLine(), out double newTierFourEXP))
                                        {
                                            Console.WriteLine($"Changing Tier Four's EXP payout to {newTierFourEXP}.\nEnter 'y' to confirm these changes.");
                                            string confirmChanges = Console.ReadLine();

                                            if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                            {
                                                TierFour = newTierFourEXP;

                                                Console.WriteLine($"Tier Four's EXP payout is now {TierFour}.\nPress any key to move on.");
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                Console.WriteLine("Cancelling action.\nPress any key to move on.");
                                                Console.ReadKey();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Non-number detected. Cancelling action.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                        break;
                                    case 5:
                                        Console.WriteLine($"Tier Five's current EXP payout is {TierFive}. What is the new EXP amount?\n(Enter a number." +
                                            $" Alternatively, type a non-number to cancel.)");
                                        if (Double.TryParse(Console.ReadLine(), out double newTierFiveEXP))
                                        {
                                            Console.WriteLine($"Changing Tier Five's EXP payout to {newTierFiveEXP}.\nEnter 'y' to confirm these changes.");
                                            string confirmChanges = Console.ReadLine();

                                            if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                            {
                                                TierFive = newTierFiveEXP;

                                                Console.WriteLine($"Tier Five's EXP payout is now {TierFive}.\nPress any key to move on.");
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                Console.WriteLine("Cancelling action.\nPress any key to move on.");
                                                Console.ReadKey();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Non-number detected. Cancelling action.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                        break;
                                    case 6:
                                        Console.WriteLine($"Tier Six's current EXP payout is {TierSix}. What is the new EXP amount?\n(Enter a number." +
                                            $" Alternatively, type a non-number to cancel.)");
                                        if (Double.TryParse(Console.ReadLine(), out double newTierSixEXP))
                                        {
                                            Console.WriteLine($"Changing Tier Six's EXP payout to {newTierSixEXP}.\nEnter 'y' to confirm these changes.");
                                            string confirmChanges = Console.ReadLine();

                                            if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                            {
                                                TierSix = newTierSixEXP;

                                                Console.WriteLine($"Tier Six's EXP payout is now {TierSix}.\nPress any key to move on.");
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                Console.WriteLine("Cancelling action.\nPress any key to move on.");
                                                Console.ReadKey();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Non-number detected. Cancelling action.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                        break;
                                    case 7:
                                        Console.WriteLine($"Tier Seven's current EXP payout is {TierSeven}. What is the new EXP amount?\n(Enter a number." +
                                            $" Alternatively, type a non-number to cancel.)");
                                        if (Double.TryParse(Console.ReadLine(), out double newTierSevenEXP))
                                        {
                                            Console.WriteLine($"Changing Tier Seven's EXP payout to {newTierSevenEXP}.\nEnter 'y' to confirm these changes.");
                                            string confirmChanges = Console.ReadLine();

                                            if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                            {
                                                TierSeven = newTierSevenEXP;

                                                Console.WriteLine($"Tier Seven's EXP payout is now {TierSeven}.\nPress any key to move on.");
                                                Console.ReadKey();
                                            }
                                            else
                                            {
                                                Console.WriteLine("Cancelling action.\nPress any key to move on.");
                                                Console.ReadKey();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Non-number detected. Cancelling action.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                        break;
                                    default:
                                        Console.WriteLine($"ERROR: {changeWhichTier} is not a valid option.\nPlease enter a number corresponding to a menu action." +
                                            $"\nPress any key to move on.");
                                        Console.ReadKey();
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("ERROR: Your response is not a valid option.\nPlease enter a number corresponding to the menu choice." +
                                     $"\nPress any key to move on.");
                                Console.ReadKey();
                            }
                            
                            break;
                        default:
                            Console.WriteLine($"ERROR: {menuResposne} is not a valid option.\nPlease enter a number corresponding to the menu choice." +
                                $"\nPress any key to move on.");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("ERROR: Your response is not a valid option.\nPlease enter a number corresponding to the menu choice." +
                        $"\nPress any key to move on.");
                    Console.ReadKey();
                }
            }
            
        }
        
        double FindExperienceTier(int levelDifference)
        {
            if(levelDifference <= -5)
            {
                return TierOne;
            }
            else if(levelDifference == -4 || levelDifference == -3)
            {
                return TierTwo;
            }
            else if(levelDifference == -2 || levelDifference == -1)
            {
                return TierThree;
            }
            else if(levelDifference == 0)
            {
                return TierFour;
            }
            else if(levelDifference >= 1 && levelDifference <= 3)
            {
                return TierFive;
            }
            else if(levelDifference >= 4 && levelDifference <= 6)
            {
                return TierSix;
            }
            else if(levelDifference >= 7)
            {
                return TierSeven;
            }
            else
            {
                return 0;
            }
        }

        public void ChangeValues()
        {
            Console.WriteLine($"Looking to change values for the character:\n\t{FirstName} {LastName}\n");
            Console.WriteLine("\n\n----------------------\n    0.) Go Back\n    1.) Experience\n    2.) Level\n" +
                "    3.) Name\n----------------------\n\n" +
                "Change what? (enter #)");
            if (Int32.TryParse(Console.ReadLine(), out int changeWhatResponse))
            {
                string newFirstName = "";
                string newLastName = "";

                switch(changeWhatResponse)
                {
                    case 0:
                        break;
                    case 1:
                        // Change EXP amount for this character.
                        Console.WriteLine($"    What is the new experience amount for {FirstName}? \n    (Type a number, or any negative number to cancel)" +
                            $"\n    (Please note that decimal numbers are supported)");
                        if ((Double.TryParse(Console.ReadLine(), out double newEXP)))
                        {
                            if (newEXP <= -1)
                            {
                                Console.WriteLine("Action cancelled.\nPress any key to move on.");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"    Changing the EXP for {FirstName} from {ExperienceContribution} EXP to {newEXP} EXP." +
                                    $"\n    Type 'y' to confirm these changes.");

                                string confirmChanges = Console.ReadLine();

                                if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                {
                                    ExperienceContribution = newEXP;
                                    Console.WriteLine($"    {FirstName} now has an EXP amount of {ExperienceContribution}.\n    Press any key to move on.");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("Cancelling changes.\nPress any key to move on.");
                                    Console.ReadKey();
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("ERROR: Please enter a valid whole number.\nPress any key to move on.");
                            Console.ReadKey();
                        }
                        break;
                    case 2:
                        // Change level for this character.
                        Console.WriteLine($"    What is the new level for {FirstName}? \n    (Type a whole number, or any negative number to cancel)");
                        if (Int32.TryParse(Console.ReadLine(), out int newLevel))
                        {
                            if (newLevel <= -1)
                            {
                                Console.WriteLine("Action cancelled.\nPress any key to move on.");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"    Changing the level for {FirstName} from {CharacterLevel} to {newLevel}." +
                                    $"\n    Type 'y' to confirm these changes.");

                                string confirmChanges = Console.ReadLine();

                                if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                {
                                    CharacterLevel = newLevel;
                                    Console.WriteLine($"    {FirstName} is now level {CharacterLevel}.\n    Press any key to move on.");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    Console.WriteLine("Cancelling changes.\nPress any key to move on.");
                                    Console.ReadKey();
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Please enter a valid whole number.\nPress any key to move on.");
                            Console.ReadKey();
                        }
                        break;
                    case 3:
                        //Change first name, last name, or both, of this character.
                        Console.WriteLine("\n    0.) Cancel\n    1.) First Name\n    2.) Last Name\n    " +
                            "3.) Both First and Last\nChange what for the name?\nEnter Corresponding Number:");
                        int changeWhichName;

                        if (Int32.TryParse(Console.ReadLine(), out changeWhichName))
                        {
                            switch (changeWhichName)
                            {
                                case 0:
                                    break;
                                case 1:
                                    Console.WriteLine($"    What is the new first name for {FirstName}?\n    (Enter a word, or type 0 to cancel.)");
                                    newFirstName = Console.ReadLine();
                                    if (newFirstName.Equals("0"))
                                    {
                                        Console.WriteLine("Action cancelled.\nPress any key to continue.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"    Changing {FirstName} to {newFirstName}. \n    Enter 'y' to confirm these changes.");
                                        string confirmChanges = Console.ReadLine();

                                        if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                        {
                                            FirstName = newFirstName;
                                            Console.WriteLine($"    This character's first name is now {FirstName}.\n    Press any key to move on.");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Cancelling changes.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                    }
                                    break;
                                case 2:
                                    Console.WriteLine($"    What is the new last name for {FirstName}?\n    (Enter a word, or type 0 to cancel.)");
                                    newLastName = Console.ReadLine();
                                    if (newLastName.Equals("0"))
                                    {
                                        Console.WriteLine("Action cancelled.\nPress any key to continue.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"    Changing {LastName} to {newLastName}. \n    Enter 'y' to confirm these changes.");
                                        string confirmChanges = Console.ReadLine();

                                        if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                        {
                                            LastName = newLastName;
                                            Console.WriteLine($"    This character's last name is now {LastName}.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Cancelling changes.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                    }
                                    break;
                                case 3:
                                    Console.WriteLine("\n\n    New first name? \n    (Alternatively, type 0 to cancel)");
                                    newFirstName = Console.ReadLine();

                                    if (newFirstName.Equals("0"))
                                    {
                                        Console.WriteLine("Action cancelled.\nPress any key to continue.");
                                        Console.ReadKey();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\n\n    New last name?");
                                        newLastName = Console.ReadLine();

                                        Console.WriteLine($"    Changing {FirstName} {LastName} to {newFirstName} {newLastName}.\n    Type 'y' to confirm these changes.");
                                        string confirmChanges = Console.ReadLine();

                                        if (confirmChanges.Equals("y") || confirmChanges.Equals("Y"))
                                        {
                                            FirstName = newFirstName;
                                            LastName = newLastName;
                                            Console.WriteLine($"    This character's name is now {FirstName} {LastName}.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Cancelling changes.\nPress any key to move on.");
                                            Console.ReadKey();
                                        }
                                    }
                                    break;
                                default:
                                    Console.WriteLine($"There is no option for the number {changeWhichName}.\nPress any key to move on.");
                                    Console.ReadKey();
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Please enter a valid whole number.\nPress any key to move on.");
                            Console.ReadKey();
                        }
                        break;
                }
            }
            else
            {
                Console.WriteLine("ERROR: Invalid response. Please enter a number according to the action you wish to perform." +
                    "\nPress any key to go on.");
                Console.ReadKey();
            }

        }


        public void SetExperience(double newEXP)
        {
            ExperienceContribution = newEXP;
        }

        public string GetFullName()
        {
            string fullName = FirstName + " " + LastName;
            return fullName;
        }

        public int GetLevel()
        {
            return CharacterLevel;
        }

        public double GetExperience()
        {
            return ExperienceContribution;
        }

    }
}

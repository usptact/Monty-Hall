using System;

namespace Infer.NET_MontyHall
{
    class Program
    {
        private const int MIN_DOORS = 2;
        private const int MAX_DOORS = 10;
        private const int DEFAULT_DOORS = 3;
        
        static void Main(string[] args)
        {
            ShowWelcomeMessage();
            
            int numberOfDoors = GetNumberOfDoorsFromUser();
            if (numberOfDoors == -1) return; // User quit
            
            int prizeDoor = GetPrizeDoorFromUser(numberOfDoors);
            if (prizeDoor == -1) return; // User quit
            
            PlayGame(numberOfDoors, prizeDoor);
            
            Console.WriteLine("Thank you for playing!");
        }
        
        private static void ShowWelcomeMessage()
        {
            Console.WriteLine("Welcome to the Monty Hall Problem Simulator!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        
        private static int GetNumberOfDoorsFromUser()
        {
            Console.Clear();
            Console.WriteLine("Monty Hall Problem Simulator");
            Console.WriteLine("============================");
            Console.WriteLine();
            Console.Write($"Enter number of doors ({MIN_DOORS}-{MAX_DOORS}) or Q to quit: ");
            
            string input = Console.ReadLine();
            
            if (IsQuitCommand(input))
            {
                Console.WriteLine("Goodbye!");
                return -1;
            }
            
            if (int.TryParse(input, out int doors) && doors >= MIN_DOORS && doors <= MAX_DOORS)
            {
                return doors;
            }
            
            Console.WriteLine($"Invalid input, using default of {DEFAULT_DOORS} doors.");
            return DEFAULT_DOORS;
        }
        
        private static int GetPrizeDoorFromUser(int numberOfDoors)
        {
            Console.Clear();
            Console.WriteLine($"Prize Placement ({numberOfDoors} doors):");
            Console.Write($"Enter door number (1-{numberOfDoors}) for prize, R for random, or Q to quit: ");
            
            string input = Console.ReadLine();
            
            if (IsQuitCommand(input))
            {
                Console.WriteLine("Goodbye!");
                return -1;
            }
            
            if (IsRandomCommand(input))
            {
                return 0; // Random
            }
            
            if (int.TryParse(input, out int doorChoice) && doorChoice >= 1 && doorChoice <= numberOfDoors)
            {
                return doorChoice;
            }
            
            Console.WriteLine("Invalid choice, using random placement.");
            return 0;
        }
        
        private static void PlayGame(int numberOfDoors, int prizeDoor)
        {
            Monty monty = new Monty(numberOfDoors, prizeDoor);
            bool running = true;
            
            while (running)
            {
                monty.GetDoors().DrawDoors();
                Console.WriteLine(monty.GetGameStatus());
                Console.WriteLine();
                
                if (monty.IsInitialPickPhase())
                {
                    running = HandleInitialPickPhase(monty);
                }
                else if (monty.IsFinalChoicePhase())
                {
                    running = HandleFinalChoicePhase(monty);
                }
            }
        }
        
        private static bool HandleInitialPickPhase(Monty monty)
        {
            Console.WriteLine("Pick your initial door:");
            Console.Write($"Enter door number (1-{monty.GetDoors().GetNumberOfDoors()}) or P for prize location, Q to quit: ");
            
            string input = Console.ReadLine();
            
            if (IsQuitCommand(input))
            {
                return false;
            }
            
            if (IsPrizeCommand(input))
            {
                ShowPrizeLocation(monty);
                return true;
            }
            
            if (int.TryParse(input, out int doorNum) && doorNum >= 1 && doorNum <= monty.GetDoors().GetNumberOfDoors())
            {
                return ProcessInitialDoorPick(monty, doorNum);
            }
            
            ShowInvalidCommandMessage();
            return true;
        }
        
        private static bool HandleFinalChoicePhase(Monty monty)
        {
            ShowFinalChoiceMenu(monty);
            Console.Write("Enter command: ");
            
            string input = Console.ReadLine()?.ToUpper();
            
            switch (input)
            {
                case "S":
                    return ProcessSwitchChoice(monty);
                case "K":
                    return ProcessKeepChoice(monty);
                case "P":
                    ShowPrizeLocation(monty);
                    return true;
                case "Q":
                    return false;
                default:
                    ShowInvalidCommandMessage();
                    return true;
            }
        }
        
        private static bool ProcessInitialDoorPick(Monty monty, int doorNum)
        {
            if (monty.MakeInitialPick(doorNum))
            {
                monty.GetDoors().SetUserSelection(doorNum);
                Console.WriteLine($"You picked door {doorNum}!");
                WaitForKeyPress();
                
                monty.OpenEmptyDoors();
            }
            return true;
        }
        
        private static bool ProcessSwitchChoice(Monty monty)
        {
            bool won = monty.MakeFinalChoice(true);
            Console.WriteLine($"You switched to door {monty.GetRemainingDoor()}!");
            
            ShowFinalResult(monty, won);
            return false;
        }
        
        private static bool ProcessKeepChoice(Monty monty)
        {
            bool won = monty.MakeFinalChoice(false);
            Console.WriteLine($"You kept door {monty.GetUserPick()}!");
            
            ShowFinalResult(monty, won);
            return false;
        }
        
        private static void ShowFinalChoiceMenu(Monty monty)
        {
            Console.WriteLine("Final Choice:");
            Console.WriteLine($"S: Switch to door {monty.GetRemainingDoor()}");
            Console.WriteLine($"K: Keep door {monty.GetUserPick()}");
            Console.WriteLine("P: Show prize location (cheat!)");
            Console.WriteLine("Q: Quit");
            Console.WriteLine();
        }
        
        private static void ShowFinalResult(Monty monty, bool won)
        {
            monty.GetDoors().DrawDoors();
            Console.WriteLine(won ? "Congratulations! You won!" : "Sorry, you lost.");
            Console.WriteLine($"The prize was behind door {monty.GetPrizeDoor()}.");
            WaitForKeyPress();
        }
        
        private static void ShowPrizeLocation(Monty monty)
        {
            Console.WriteLine($"The prize is behind door {monty.GetPrizeDoor()}!");
            WaitForKeyPress();
        }
        
        private static void ShowInvalidCommandMessage()
        {
            Console.WriteLine("Invalid command. Press any key to continue...");
            Console.ReadKey();
        }
        
        private static void WaitForKeyPress()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        
        private static bool IsQuitCommand(string input)
        {
            return input?.ToUpper() == "Q";
        }
        
        private static bool IsPrizeCommand(string input)
        {
            return input?.ToUpper() == "P";
        }
        
        private static bool IsRandomCommand(string input)
        {
            return input?.ToUpper() == "R" || input == "1";
        }
    }
}

using System;

namespace Infer.NET_MontyHall
{
    public class Doors
    {
        private const int DOOR_HEIGHT = 8;
        private const int DOOR_WIDTH = 11;
        private const int DOOR_SPACING = 3;
        private const int MIN_DOORS = 2;
        private const int MAX_DOORS = 10;
        private const int WARNING_THRESHOLD = 8;
        
        private bool[] doorStates; // true = open, false = closed
        private int prizeDoor; // Which door contains the prize (1-based)
        private double[] probabilities; // Probability each door contains the prize
        private int numberOfDoors;
        private int userSelection; // Which door the user has selected (0 = none)
        
        public Doors(int numberOfDoors = 3, int prizeDoorNumber = 0)
        {
            if (numberOfDoors < MIN_DOORS)
                throw new ArgumentException($"Number of doors must be at least {MIN_DOORS}");
            
            // High-level application limit for user interface (console display)
            if (numberOfDoors > MAX_DOORS)
                throw new ArgumentException($"Number of doors cannot exceed {MAX_DOORS} (application limit)");
            
            this.numberOfDoors = numberOfDoors;
            doorStates = new bool[numberOfDoors];
            probabilities = new double[numberOfDoors];
            userSelection = 0; // No selection initially
            
            // Initialize all doors as closed
            for (int i = 0; i < numberOfDoors; i++)
            {
                doorStates[i] = false;
                probabilities[i] = 1.0 / numberOfDoors; // Equal initial probabilities
            }
            
            if (prizeDoorNumber >= 1 && prizeDoorNumber <= numberOfDoors)
            {
                prizeDoor = prizeDoorNumber;
            }
            else
            {
                // Random prize placement if not specified or invalid
                Random random = new Random();
                prizeDoor = random.Next(1, numberOfDoors + 1);
            }
        }
        
        public void SetDoorState(int doorNumber, bool isOpen)
        {
            ValidateDoorNumber(doorNumber);
            
            int doorIndex = doorNumber - 1;
            bool wasOpen = doorStates[doorIndex];
            doorStates[doorIndex] = isOpen;
            
            // Update probabilities using Bayes' theorem when opening a door
            if (!wasOpen && isOpen)
            {
                UpdateProbabilitiesAfterOpening(doorIndex);
            }
        }
        
        public bool GetDoorState(int doorNumber)
        {
            ValidateDoorNumber(doorNumber);
            return doorStates[doorNumber - 1];
        }
        
        public void DrawDoors()
        {
            Console.Clear();
            DrawHeader();
            DrawDoorHeaders();
            DrawDoorBodies();
            DrawProbabilities();
            DrawLegend();
        }
        
        private void DrawHeader()
        {
            Console.WriteLine($"Monty Hall Problem - {numberOfDoors} Doors");
            Console.WriteLine(new string('=', 25 + numberOfDoors.ToString().Length));
            
            if (numberOfDoors > WARNING_THRESHOLD)
            {
                Console.WriteLine($"⚠️  Warning: Display may be wide with {numberOfDoors} doors");
            }
            Console.WriteLine();
        }
        
        private void DrawDoorHeaders()
        {
            string header = "";
            for (int i = 1; i <= numberOfDoors; i++)
            {
                header += $"   Door {i}   ";
            }
            Console.WriteLine(header);
            
            string separator = "";
            for (int i = 0; i < numberOfDoors; i++)
            {
                separator += "   =======   ";
            }
            Console.WriteLine(separator);
            Console.WriteLine();
        }
        
        private void DrawDoorBodies()
        {
            for (int row = 0; row < DOOR_HEIGHT; row++)
            {
                string line = "";
                
                for (int door = 0; door < numberOfDoors; door++)
                {
                    int doorNumber = door + 1;
                    line += GetDoorRow(door, doorNumber, row);
                    
                    if (door < numberOfDoors - 1)
                    {
                        line += new string(' ', DOOR_SPACING);
                    }
                }
                
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }
        
        private string GetDoorRow(int doorIndex, int doorNumber, int row)
        {
            if (doorStates[doorIndex]) // Door is open
            {
                return DrawOpenDoor(row, doorNumber);
            }
            else if (doorNumber == userSelection) // User's pick (closed)
            {
                return DrawUserPickDoor(row, doorNumber);
            }
            else // Door is closed (not user's pick)
            {
                return DrawClosedDoor(row);
            }
        }
        
        private void DrawProbabilities()
        {
            string probabilityLine = "";
            for (int i = 0; i < numberOfDoors; i++)
            {
                string probText = $"{probabilities[i]:P1}";
                string paddedProb = CenterText(probText, DOOR_WIDTH);
                probabilityLine += paddedProb;
                
                if (i < numberOfDoors - 1)
                {
                    probabilityLine += new string(' ', DOOR_SPACING);
                }
            }
            Console.WriteLine(probabilityLine);
            Console.WriteLine();
        }
        
        private string CenterText(string text, int width)
        {
            int padding = (width - text.Length) / 2;
            return new string(' ', padding) + text + new string(' ', width - text.Length - padding);
        }
        
        private void DrawLegend()
        {
            Console.WriteLine("Legend: [█] = Closed Door, [ ] = Opened Door, [X] = Your Pick, [$] = Prize!");
            Console.WriteLine();
        }
        
        private string DrawClosedDoor(int row)
        {
            switch (row)
            {
                case 0: return "   ┌─────┐";
                case 1: return "   │ ███ │";
                case 2: return "   │ ███ │";
                case 3: return "   │ ███ │";
                case 4: return "   │ ███ │";
                case 5: return "   │ ███ │";
                case 6: return "   │ ███ │";
                case 7: return "   └─────┘";
                default: return "";
            }
        }
        
        private string DrawOpenDoor(int row, int doorNumber)
        {
            switch (row)
            {
                case 0: return "   ┌─┬─┬─┐"; // Dashed top
                case 1: 
                    if (doorNumber == prizeDoor)
                        return "   │─ $ ─│";
                    else
                        return "   │─   ─│";
                case 2: 
                    if (doorNumber == prizeDoor)
                        return "   │─ $ ─│";
                    else
                        return "   │─   ─│";
                case 3: 
                    if (doorNumber == prizeDoor)
                        return "   │─ $ ─│";
                    else
                        return "   │─   ─│";
                case 4: 
                    if (doorNumber == prizeDoor)
                        return "   │─ $ ─│";
                    else
                        return "   │─   ─│";
                case 5: 
                    if (doorNumber == prizeDoor)
                        return "   │─ $ ─│";
                    else
                        return "   │─   ─│";
                case 6: 
                    if (doorNumber == prizeDoor)
                        return "   │─ $ ─│";
                    else
                        return "   │─   ─│";
                case 7: return "   └─┴─┴─┘"; // Dashed bottom
                default: return "";
            }
        }
        
        private string DrawUserPickDoor(int row, int doorNumber)
        {
            switch (row)
            {
                case 0: return "   ╔═════╗"; // Double-line top
                case 1: return "   ║ ███ ║"; // Double-line sides with filled content
                case 2: return "   ║ ███ ║";
                case 3: return "   ║ ███ ║";
                case 4: return "   ║ ███ ║";
                case 5: return "   ║ ███ ║";
                case 6: return "   ║ ███ ║";
                case 7: return "   ╚═════╝"; // Double-line bottom
                default: return "";
            }
        }
        
        public void ResetAllDoors()
        {
            for (int i = 0; i < numberOfDoors; i++)
            {
                doorStates[i] = false;
            }
        }
        
        public void OpenAllDoors()
        {
            for (int i = 0; i < numberOfDoors; i++)
            {
                doorStates[i] = true;
            }
        }
        
        public int GetPrizeDoor()
        {
            return prizeDoor;
        }
        
        public void SetPrizeDoor(int doorNumber)
        {
            if (doorNumber >= 1 && doorNumber <= numberOfDoors)
            {
                prizeDoor = doorNumber;
            }
            else
            {
                throw new ArgumentException($"Door number must be between 1 and {numberOfDoors}");
            }
        }
        
        public bool HasWon(int doorNumber)
        {
            return doorNumber == prizeDoor && doorStates[doorNumber - 1];
        }
        
        public double GetProbability(int doorNumber)
        {
            if (doorNumber >= 1 && doorNumber <= numberOfDoors)
            {
                return probabilities[doorNumber - 1];
            }
            else
            {
                throw new ArgumentException($"Door number must be between 1 and {numberOfDoors}");
            }
        }
        
        public double[] GetAllProbabilities()
        {
            double[] copy = new double[numberOfDoors];
            Array.Copy(probabilities, copy, numberOfDoors);
            return copy;
        }
        
        private void UpdateProbabilitiesAfterOpening(int openedDoorIndex)
        {
            // Check if the opened door contains the prize
            if (openedDoorIndex == prizeDoor - 1)
            {
                // Door contains prize - set its probability to 1, others to 0
                probabilities = Bayes.UpdateProbabilitiesWithPrize(probabilities, openedDoorIndex);
            }
            else
            {
                // Door is empty - update probabilities using Bayes' theorem
                probabilities = Bayes.UpdateProbabilities(probabilities, openedDoorIndex);
            }
        }
        
        public void ResetProbabilities()
        {
            for (int i = 0; i < numberOfDoors; i++)
            {
                probabilities[i] = 1.0 / numberOfDoors;
            }
        }
        
        public int GetNumberOfDoors()
        {
            return numberOfDoors;
        }
        
        public void SetUserSelection(int doorNumber)
        {
            if (doorNumber >= 0 && doorNumber <= numberOfDoors)
            {
                userSelection = doorNumber;
            }
            else
            {
                throw new ArgumentException($"Door number must be between 0 and {numberOfDoors}");
            }
        }
        
        public int GetUserSelection()
        {
            return userSelection;
        }
        
        public void ClearUserSelection()
        {
            userSelection = 0;
        }
        
        public void SetProbability(int doorNumber, double probability)
        {
            ValidateDoorNumber(doorNumber);
            probabilities[doorNumber - 1] = probability;
        }
        
        private void ValidateDoorNumber(int doorNumber)
        {
            if (doorNumber < 1 || doorNumber > numberOfDoors)
            {
                throw new ArgumentException($"Door number must be between 1 and {numberOfDoors}");
            }
        }
    }
}

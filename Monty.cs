using System;
using System.Collections.Generic;

namespace Infer.NET_MontyHall
{
    public class Monty
    {
        private Doors doors;
        private int userPick;
        private int remainingDoor;
        private bool gamePhase; // false = initial pick, true = final choice
        private Random random;
        
        public Monty(int numberOfDoors = 3, int prizeDoorNumber = 0)
        {
            doors = new Doors(numberOfDoors, prizeDoorNumber);
            userPick = 0;
            remainingDoor = 0;
            gamePhase = false; // Start with initial pick phase
            random = new Random();
        }
        
        public Doors GetDoors()
        {
            return doors;
        }
        
        public bool IsInitialPickPhase()
        {
            return !gamePhase;
        }
        
        public bool IsFinalChoicePhase()
        {
            return gamePhase;
        }
        
        public bool MakeInitialPick(int doorNumber)
        {
            if (IsInitialPickPhase() && doorNumber >= 1 && doorNumber <= doors.GetNumberOfDoors())
            {
                userPick = doorNumber;
                
                // Update probabilities after user pick (no doors opened yet, so probabilities remain equal)
                // In the classic Monty Hall, probabilities don't change until doors are opened
                
                return true;
            }
            return false;
        }
        
        public void OpenEmptyDoors()
        {
            if (!IsInitialPickPhase())
                return;
            
            int numberOfDoors = doors.GetNumberOfDoors();
            int prizeDoor = doors.GetPrizeDoor();
            
            // Determine which door to keep closed
            if (userPick == prizeDoor)
            {
                // User picked the prize door, randomly pick one empty door to keep closed
                var emptyDoors = new List<int>();
                for (int i = 1; i <= numberOfDoors; i++)
                {
                    if (i != prizeDoor)
                    {
                        emptyDoors.Add(i);
                    }
                }
                remainingDoor = emptyDoors[random.Next(emptyDoors.Count)];
            }
            else
            {
                // User didn't pick the prize door, keep the prize door closed
                remainingDoor = prizeDoor;
            }
            
            // Open all doors except user pick and remaining door
            for (int i = 1; i <= numberOfDoors; i++)
            {
                if (i != userPick && i != remainingDoor)
                {
                    doors.SetDoorState(i, true); // This will trigger Bayesian updates
                }
            }
            
            // Apply correct Monty Hall Bayesian update
            UpdateProbabilitiesAfterMontyOpens();
            
            // Move to final choice phase
            gamePhase = true;
        }
        
        public bool MakeFinalChoice(bool switchDoor)
        {
            if (!IsFinalChoicePhase())
                return false;
            
            int finalDoor;
            if (switchDoor)
            {
                finalDoor = remainingDoor;
            }
            else
            {
                finalDoor = userPick;
            }
            
            // Open the final door
            doors.SetDoorState(finalDoor, true);
            
            return doors.HasWon(finalDoor);
        }
        
        public int GetUserPick()
        {
            return userPick;
        }
        
        public int GetRemainingDoor()
        {
            return remainingDoor;
        }
        
        public int GetPrizeDoor()
        {
            return doors.GetPrizeDoor();
        }
        
        public bool HasUserWon()
        {
            if (IsFinalChoicePhase())
            {
                return doors.HasWon(userPick) || doors.HasWon(remainingDoor);
            }
            return false;
        }
        
        public void ResetGame()
        {
            doors.ResetAllDoors();
            doors.ResetProbabilities();
            userPick = 0;
            remainingDoor = 0;
            gamePhase = false;
        }
        
        private void UpdateProbabilitiesAfterMontyOpens()
        {
            int numberOfDoors = doors.GetNumberOfDoors();
            int prizeDoor = doors.GetPrizeDoor();
            
            // Get current probabilities
            double[] probabilities = doors.GetAllProbabilities();
            
            // Reset all probabilities
            for (int i = 0; i < numberOfDoors; i++)
            {
                probabilities[i] = 0.0;
            }
            
            // Set all opened doors to 0 probability (they're already set above)
            for (int i = 1; i <= numberOfDoors; i++)
            {
                if (doors.GetDoorState(i)) // If door is open
                {
                    probabilities[i - 1] = 0.0;
                }
            }
            
            // Calculate probabilities for the two remaining closed doors
            double userDoorProbability = 1.0 / numberOfDoors; // User's door keeps original probability
            double remainingDoorProbability = 1.0 - userDoorProbability; // Remaining door gets the rest
            
            // Set the probabilities for the two remaining doors
            probabilities[userPick - 1] = userDoorProbability;
            probabilities[remainingDoor - 1] = remainingDoorProbability;
            
            // Update the doors with new probabilities
            for (int i = 0; i < numberOfDoors; i++)
            {
                doors.SetProbability(i + 1, probabilities[i]);
            }
        }
        
        public string GetGameStatus()
        {
            if (IsInitialPickPhase())
            {
                return "Phase 1: Make your initial pick";
            }
            else
            {
                return $"Phase 2: Switch to door {remainingDoor} or stay with door {userPick}?";
            }
        }
    }
}

using System;

namespace Infer.NET_MontyHall
{
    public class Bayes
    {
        /// <summary>
        /// Updates probabilities using Bayes' theorem when a door is opened and found empty
        /// </summary>
        /// <param name="probabilities">Current probabilities for each door</param>
        /// <param name="openedDoor">Index of the door that was opened (0-based)</param>
        /// <returns>Updated probabilities</returns>
        public static double[] UpdateProbabilities(double[] probabilities, int openedDoor)
        {
            if (probabilities.Length < 2)
                throw new ArgumentException("Must have at least 2 doors");
            
            if (openedDoor < 0 || openedDoor >= probabilities.Length)
                throw new ArgumentException("Invalid door index");
            
            // Create a copy to avoid modifying the original array
            double[] updatedProbabilities = new double[probabilities.Length];
            Array.Copy(probabilities, updatedProbabilities, probabilities.Length);
            
            // The opened door has probability 0 (it's empty)
            updatedProbabilities[openedDoor] = 0.0;
            
            // Calculate the total probability of remaining doors
            double remainingProbability = 0.0;
            for (int i = 0; i < probabilities.Length; i++)
            {
                if (i != openedDoor)
                {
                    remainingProbability += probabilities[i];
                }
            }
            
            // If remaining probability is 0, we have a problem (shouldn't happen in normal game)
            if (remainingProbability == 0.0)
            {
                throw new InvalidOperationException("No remaining probability after opening door");
            }
            
            // Redistribute probabilities proportionally among remaining doors
            for (int i = 0; i < probabilities.Length; i++)
            {
                if (i != openedDoor)
                {
                    // Scale up the probability proportionally
                    updatedProbabilities[i] = probabilities[i] / remainingProbability;
                }
            }
            
            return updatedProbabilities;
        }
        
        /// <summary>
        /// Updates probabilities when a door is opened and found to contain the prize
        /// </summary>
        /// <param name="probabilities">Current probabilities for each door</param>
        /// <param name="prizeDoor">Index of the door containing the prize (0-based)</param>
        /// <returns>Updated probabilities</returns>
        public static double[] UpdateProbabilitiesWithPrize(double[] probabilities, int prizeDoor)
        {
            if (probabilities.Length < 2)
                throw new ArgumentException("Must have at least 2 doors");
            
            if (prizeDoor < 0 || prizeDoor >= probabilities.Length)
                throw new ArgumentException("Invalid door index");
            
            // Create a copy to avoid modifying the original array
            double[] updatedProbabilities = new double[probabilities.Length];
            
            // The prize door has probability 1, all others have probability 0
            for (int i = 0; i < probabilities.Length; i++)
            {
                updatedProbabilities[i] = (i == prizeDoor) ? 1.0 : 0.0;
            }
            
            return updatedProbabilities;
        }
        
        /// <summary>
        /// Calculates the posterior probability using Bayes' theorem
        /// P(A|B) = P(B|A) * P(A) / P(B)
        /// </summary>
        /// <param name="priorProbability">Prior probability P(A)</param>
        /// <param name="likelihood">Likelihood P(B|A)</param>
        /// <param name="evidence">Evidence P(B)</param>
        /// <returns>Posterior probability P(A|B)</returns>
        public static double CalculatePosterior(double priorProbability, double likelihood, double evidence)
        {
            if (evidence == 0.0)
                throw new ArgumentException("Evidence probability cannot be zero");
            
            return (likelihood * priorProbability) / evidence;
        }
        
        /// <summary>
        /// Normalizes probabilities to ensure they sum to 1
        /// </summary>
        /// <param name="probabilities">Array of probabilities to normalize</param>
        /// <returns>Normalized probabilities</returns>
        public static double[] NormalizeProbabilities(double[] probabilities)
        {
            double sum = 0.0;
            foreach (double prob in probabilities)
            {
                sum += prob;
            }
            
            if (sum == 0.0)
                throw new ArgumentException("Sum of probabilities cannot be zero");
            
            double[] normalized = new double[probabilities.Length];
            for (int i = 0; i < probabilities.Length; i++)
            {
                normalized[i] = probabilities[i] / sum;
            }
            
            return normalized;
        }
        
        /// <summary>
        /// Validates that probabilities sum to 1 (within tolerance)
        /// </summary>
        /// <param name="probabilities">Array of probabilities to validate</param>
        /// <param name="tolerance">Tolerance for floating point comparison</param>
        /// <returns>True if probabilities sum to 1 within tolerance</returns>
        public static bool ValidateProbabilities(double[] probabilities, double tolerance = 1e-10)
        {
            double sum = 0.0;
            foreach (double prob in probabilities)
            {
                sum += prob;
            }
            
            return Math.Abs(sum - 1.0) < tolerance;
        }
    }
}

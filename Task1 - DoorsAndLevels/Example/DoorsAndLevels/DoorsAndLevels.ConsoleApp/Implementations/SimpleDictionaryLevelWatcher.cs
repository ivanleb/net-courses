using DoorsAndLevels.Core.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace DoorsAndLevels.ConsoleApp.Implementations
{
    public class SimpleDictionaryLevelWatcher : ILevelWatcher
    {
        private Dictionary<int, List<int>> levelDoorsHistory = new Dictionary<int, List<int>>();

        private int currentLevel = 0;

        public int GetCurrentLevel()
        {
            return currentLevel;
        }

        public int[] GetDoorsOnCurrentLevel()
        {
            return this.levelDoorsHistory[currentLevel].ToArray();
        }

        public void GoNextLevel(int doorNumber)
        {
            var currentLevelDoorsNumbers = this.levelDoorsHistory[this.currentLevel];
            var nextLevelDoorsNumbers = this.CalcNextDoorsNames(currentLevelDoorsNumbers, doorNumber);

            currentLevel++;

            levelDoorsHistory[this.currentLevel] = nextLevelDoorsNumbers;
        }

        public void GoPreviousLevel()
        {
            currentLevel--;
        }

        public void SetDoorsOnStart(int[] doorsNumbers)
        {
            this.levelDoorsHistory[this.currentLevel] = doorsNumbers.ToList();
        }

        private List<int> CalcNextDoorsNames(List<int> doorsNumbers, int selectedDoorNumber)
        {
            var retVal = new List<int>();

            foreach (var doorNumber in doorsNumbers)
            {
                retVal.Add(doorNumber * selectedDoorNumber);
            }

            return retVal;
        }
    }
}

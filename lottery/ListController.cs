using System;
using System.Collections.Generic;
using System.IO;

namespace lottery
{
    class ListController
    {
        List<string> namesList;
        Random rnd;

        public ListController(string fullpath)
        {
            populateNames(fullpath);

            rnd = new Random();
        }

        private void populateNames(string fullpath)
        {
            StreamReader file = new StreamReader(fullpath);

            namesList = new List<string>();

            string line;
            while((line = file.ReadLine()) != null)
            {
                namesList.Add(line);
            }
        }

        public int GetRemainingCount()
        {
            return namesList.Count;
        }

        public int GetRandom()
        {
            return rnd.Next(namesList.Count);
        }

        public string GetName(int index)
        {
            return namesList[index];
        }

        public void RemoveIndex(int index)
        {
            namesList.RemoveAt(index);
        }
    }
}

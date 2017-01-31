using System;
using System.IO;

namespace lottery
{
    class Lottery
    {
        ListController namesList;
        ListController giftsList;

        const int PICK_WINNER_DURATION = 5;
        const float PICK_DURATION_CHANGE = 0.05f;

        public Lottery()
        {
            populateLists();
            pickWinners();
        }

        private void populateLists()
        {
            string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string namesPath = dir + @"\names.txt";
            string giftsPath = dir + @"\gifts.txt";

            namesList = new ListController(namesPath);
            giftsList = new ListController(giftsPath);
        }

        private void pickWinners()
        {
            while (namesList.GetRemainingCount() > 0 && giftsList.GetRemainingCount() > 0)
            {
                Console.WriteLine("Remaining gifts: " + giftsList.GetRemainingCount());

                int giftIndex = giftsList.GetRandom();
                Console.WriteLine("Gift:   " + giftsList.GetName(giftIndex));
                Console.Write("Winner: ");

                long pickAtTicks = DateTime.Now.Ticks + (TimeSpan.TicksPerSecond * PICK_WINNER_DURATION);

                long lastTs = DateTime.Now.Ticks + (long)(TimeSpan.TicksPerSecond * PICK_DURATION_CHANGE);
                int nameIndex = namesList.GetRandom();
                string nameStr = namesList.GetName(nameIndex);
                int nameStrLength = nameStr.Length;
                Console.Write(nameStr);

                while(DateTime.Now.Ticks < pickAtTicks)
                {
                    if(DateTime.Now.Ticks > lastTs)
                    {
                        for (int i = 0; i < nameStrLength; i++)
                            Console.Write("\b");

                        lastTs = DateTime.Now.Ticks + (long)(TimeSpan.TicksPerSecond * PICK_DURATION_CHANGE);
                        nameIndex = namesList.GetRandom();
                        nameStr = namesList.GetName(nameIndex);
                        nameStrLength = nameStr.Length;
                        Console.Write(nameStr);
                    }
                }

                namesList.RemoveIndex(nameIndex);
                giftsList.RemoveIndex(giftIndex);

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}

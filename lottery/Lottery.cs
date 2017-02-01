using System;
using System.IO;
using System.Threading;

namespace lottery
{
    class Lottery
    {
        ListController namesList;
        ListController giftsList;
        string dir;
        string resultsPath;

        const int PICK_WINNER_DURATION = 5;
        const float PICK_DURATION_CHANGE = 0.05f;

        public Lottery()
        {
            populateLists();
            pickWinners();
        }

        private void populateLists()
        {
            // initialize directory folder
            dir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string namesPath = dir + @"\names.txt";
            string giftsPath = dir + @"\gifts.txt";
            resultsPath = dir + @"\result.txt";

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

                while (!Console.KeyAvailable)
                    Thread.Sleep(250);
                Console.ReadKey(true);

                Console.Write("Winner: ");

                long pickAtTicks = DateTime.Now.Ticks + (TimeSpan.TicksPerSecond * PICK_WINNER_DURATION);

                long lastTs = DateTime.Now.Ticks + (long)(TimeSpan.TicksPerSecond * PICK_DURATION_CHANGE);
                int nameIndex = namesList.GetRandom();
                string nameStr = namesList.GetName(nameIndex);
                int nameStrLength = nameStr.Length + 3;
                Console.Write(nameStr + "   ");

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

                Console.WriteLine();
                Console.WriteLine("1. Received");
                Console.WriteLine("2. Not Received");
                Console.WriteLine("3. Absent");
                
                bool pickedChoice = false;
                while(!pickedChoice)
                {
                    Console.Write("Enter choice: ");
                    int choice;
                    if(Int32.TryParse(Console.ReadLine(), out choice))
                    {
                        switch (choice)
                        {
                            case 1:
                                grantGift(nameIndex, giftIndex);
                                pickedChoice = true;
                                break;
                            case 2:
                                pickedChoice = true;
                                break;
                            case 3:
                                namesList.RemoveIndex(nameIndex);
                                pickedChoice = true;
                                break;
                            default:
                                Console.WriteLine("Uknown command:" + choice);
                                break;
                        }
                    }
                }
                

                Console.WriteLine();
                Console.WriteLine();
            }

            Console.WriteLine("Congratulations!");
        }

        private void grantGift(int nameIndex, int giftIndex)
        {
            string nameStr = namesList.GetName(nameIndex);
            string giftStr = giftsList.GetName(giftIndex);

            // write result to file
            using (StreamWriter file = new StreamWriter(resultsPath, true))
            {
                file.WriteLine(nameStr + " --> " + giftStr);
            }

            // remove from list
            namesList.RemoveIndex(nameIndex);
            giftsList.RemoveIndex(giftIndex);
        }
    }
}

using System;
using System.Threading;

class Demonstration
{
    static void Main()
    {
        new RaceCondition().runTest();
    }
}

public class RaceCondition
{
    int randInt;
    Random random = new Random();


    void printer()
    {
        long i = 100000000;
        while (i != 0)
        {
            if (randInt % 5 == 0) 
            {
                if (randInt % 5 != 0)
                    Console.WriteLine(randInt);
            }
            i--;
        }
    }

    void modifier()
    {
        long i = 100000000;
        while (i != 0)
        {
            randInt = random.Next(1000);
            i--;
        }
    }

    public void runTest()
    {

        Thread printerThread = new Thread(new ThreadStart(printer));
        Thread modifierThread = new Thread(new ThreadStart(modifier));

        printerThread.Start();
        modifierThread.Start();

        printerThread.Join();
        modifierThread.Join();
    }
}
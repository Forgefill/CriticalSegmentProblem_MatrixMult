using System;
using System.Threading;

class Demonstration
{
    static void Main()
    {
        new RaceConditionFixed().runTest();
    }
}

public class RaceConditionFixed
{
    int randInt;
    Random random = new Random();
    Mutex mutex = new Mutex();


    void printer()
    {
        long i = 1000000;
        while (i != 0)
        {
            mutex.WaitOne();
            if (randInt % 5 == 0)
            {
                if (randInt % 5 != 0)
                    Console.WriteLine(randInt);
            }
            mutex.ReleaseMutex();

            i--;

        }
    }

    void modifier()
    {
        long i = 1000000;
        while (i != 0)
        {
            mutex.WaitOne();
            randInt = random.Next(1000);
            mutex.ReleaseMutex();

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
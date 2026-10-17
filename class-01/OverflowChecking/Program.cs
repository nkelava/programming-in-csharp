using System;

namespace OverflowChecking
{
    class Program
    {
        static void Main(string[] args)
        {
            int intNumber = 0;
            long longNumber = int.MaxValue;
            // Uncomment the line below and comment out the line above to cause an overflow
            // Revert that change to prevent the overflow
            //long longNumber = long.MaxValue;

            AssignLongToInt(intNumber, longNumber);

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void AssignLongToInt(int intNumber, long longNumber)
        {
            try
            {
                intNumber = checked((int)longNumber);
                Console.WriteLine($"Int: {intNumber}\nLong: {longNumber}\n\nNo overflow.");
            }
            catch (System.OverflowException ex)
            {
                Console.WriteLine($"Int: {intNumber}\nLong: {longNumber}\n\n{ex.ToString()}");
            }
        }
    }
}

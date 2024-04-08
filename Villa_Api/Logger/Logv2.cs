namespace Villa_Api.Logger
{
    public class Logv2 : ILogging
    {
        public void Log(string message, string type)
        {

            if (type == "error")
            {
                Console.BackgroundColor= ConsoleColor.Red;
                Console.WriteLine("ERROR - " + message);
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                if (type=="warning")
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine(message);
                    Console.BackgroundColor = ConsoleColor.Black;

                }
                else
                {
                    Console.WriteLine(message);
                }
                
            }
        }
    }
}

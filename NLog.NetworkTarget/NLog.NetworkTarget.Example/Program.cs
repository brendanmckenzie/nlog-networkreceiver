namespace NLog.NetworkTarget.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();

            logger.Error("It works!");
        }
    }
}

using System;

namespace WordsAutocomplete
{
    class Program
    {
        static void Main()
        {
            var dataSource = new DataSource();
            var dataDestination = new DataDestination();
            var programScenario = new ProgramScenario(dataSource, dataDestination);
            try
            {
                programScenario.Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
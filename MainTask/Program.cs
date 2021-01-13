using System.Collections.Generic;

namespace MainTask
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            for (int i = 1; i <= 100; i++)
            {
                numbers.Add(i);
            }

            ProgrammersSurvey programmersSurvey = new ProgrammersSurvey();
            programmersSurvey.Run(numbers);
        }
    }
}
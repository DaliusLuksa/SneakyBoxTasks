using System.Collections.Generic;

namespace MainTask
{
    class Program
    {
        static void Main(string[] args)
        {
            // made a list to just imitate the task data
            // the list could be of any type
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
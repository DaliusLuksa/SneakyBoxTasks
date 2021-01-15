using System;

namespace MainTask
{
    class ProgrammersSurvey : TemplateMethod<string>
    {
        /// <summary>
        /// Method for the main logic of the task
        /// </summary>
        /// <typeparam name="T">Type of the value param</typeparam>
        /// <param name="value">Any type of number</param>
        /// <returns>String line with value number and a Sneaky, Box or SneakyBox text</returns>
        public override string ProcessValue<T>(T value)
        {
            dynamic numberValue = value;
            if (numberValue % 3 == 0 && numberValue % 5 == 0)
            {
                return $"{numberValue} SneakyBox";
            }
            else if (numberValue % 3 == 0)
            {
                return $"{numberValue} Sneaky";
            }
            else if (numberValue % 5 == 0)
            {
                return $"{numberValue} Box";
            }
            else
            {
                return value.ToString();
            }
        }

        /// <summary>
        /// Method for printing result line in console window
        /// </summary>
        /// <typeparam name="T">Type of the line param</typeparam>
        /// <param name="line">Result line</param>
        public override void SendResult<T>(T line)
        {
            Console.WriteLine(line);
        }
    }
}
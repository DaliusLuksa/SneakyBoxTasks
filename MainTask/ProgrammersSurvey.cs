using System;

namespace MainTask
{
    class ProgrammersSurvey : TemplateMethod<string>
    {
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

        public override void SendResult<T>(T line)
        {
            Console.WriteLine(line);
        }
    }
}
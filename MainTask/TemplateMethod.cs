using System.Collections.Generic;

namespace MainTask
{
    abstract class TemplateMethod<U>
    {
        public abstract U ProcessValue<T>(T value);
        public abstract void SendResult<T>(T line);
        public void Run<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var infoLine = ProcessValue(list[i]);
                SendResult(infoLine);
            }
        }
    }
}
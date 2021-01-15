using System.Collections.Generic;

namespace MainTask
{
    // Decided to add "U" type for the whole class to use it
    // for ProcessValue return type
    // so that other classes that implements it could return any type
    // also added "T" type for all method's variables
    // so that other classes that implements this class could also
    // change their method params as they see fit
    // tried to make this as much dynamic for the future as much as I could
    // with my current knowledge
    abstract class TemplateMethod<U>
    {
        /// <summary>
        /// Method for doing some sort of actions with the given value
        /// </summary>
        /// <typeparam name="T">Type of the value param</typeparam>
        /// <param name="value">Any type of value</param>
        /// <returns>Any type of the result</returns>
        public abstract U ProcessValue<T>(T value);

        // Decided to leave this method as void
        // because all the actions like console write, SMS sending, file writing, etc.
        // doesn't really require return type
        // unless we want to check if for example the SMS was sent successfully
        // but in this case the SendResult is called for every element in the list
        // so it wouldn't work here correctly
        /// <summary>
        /// Method for doing some sort of action with the given result
        /// </summary>
        /// <typeparam name="T">Type of the line param</typeparam>
        /// <param name="line">Any type of the given result</param>
        public abstract void SendResult<T>(T line);

        // couldn't come up with a place to put this for cycle
        // so decided to place it here because it makes most sense (for me)
        // Template Method
        // first method called should always be ProccessValue
        // and after that SendResult with the result of the ProccessValue passed to it
        // possible to change both of the methods (their logic) but not the order of execution
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
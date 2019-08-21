using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//MAKE SURE TO CHANGE THE namespace to the one of yours (if not Assignment2). Look at your
//other files (program.cs) to see which namespace you have.
namespace PartyPlanner_JohnathanBeal.Class
{

    /// <summary>
    /// This is a class with shared functions that are used to check the user's input
    /// and make sure that it belongs to the class that the calling function needs.
    /// </summary>
    /// <remarks></remarks>
    /// 
    public class Input
    {
        public static int? ParseIntegerInput (string wpfInput, out string message)
        {
            // Reads from the console until a correct integer is received
            bool goodNumber = false;
            int convertedValue = 0;
            message = "";
                goodNumber = int.TryParse(wpfInput, out convertedValue );

            if (!goodNumber)
            {
                string readMessage;
                var unknownType = ReadDoubleFromWpfTextbox(wpfInput, out readMessage);
                
                if (NullCheckUtility.IsNotNull(unknownType))
                {
                    if (unknownType.GetType() == 10.00.GetType())
                    {
                        readMessage = "Please enter an integer and not a decimal";
                        message = readMessage;
                        return null;
                    }
                    else
                    {
                        message = "";
                        return Convert.ToInt32(unknownType);
                    }
                    
                }
                else if (unknownType == null)
                {
                    message = readMessage;
                    return null;
                }
                else
                {
                    message = "";
                    return convertedValue;
                }
                
            }
            return convertedValue;
        }

        public static double? ReadDoubleFromWpfTextbox(string textInput, out string message)
        {
            // Reads from the console until a correct decimal is received
            double input = default(double);
            if (double.TryParse(textInput, out input))
            {
                message = "";
                return input;
            }
            else
            {
                message = "Please reenter the value as a number or decimal number";
                return null;
            }
        }
    }
}

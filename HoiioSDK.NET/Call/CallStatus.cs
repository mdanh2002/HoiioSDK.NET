using System;
using System.Collections.Generic;

using System.Text;

namespace HoiioSDK.NET
{
    /// <summary>
    /// The different available status for a Hoiio call
    /// </summary>
    public enum CallStatusTypes
    {
        ANSWERED, 
        UNANSWERED,
        FAILED, 
        BUSY, 
        ONGOING,

        /// <summary>
        /// The Call Status string could not be resolved into any predefined codes
        /// </summary>
        UNDEFINED
    }
    
    /// <summary>
    /// Helper class to parse a Hoiio call status
    /// </summary>
    public class CallStatusHelper
    {
        /// <summary>
        /// Convert a string into a enum representing a status of a Hoiio call
        /// </summary>
        public static CallStatusTypes CallStatusFromString(string statusStr)
        {
            switch (statusStr)
            {
                case "answered":
                    return CallStatusTypes.ANSWERED;
                case "unanswered":
                    return CallStatusTypes.UNANSWERED;
                case "failed":
                    return CallStatusTypes.FAILED;
                case "busy":
                    return CallStatusTypes.BUSY;
                case "ongoing":
                    return CallStatusTypes.ONGOING; 
                default:
                    return CallStatusTypes.UNDEFINED;
            }
        }
    }
}

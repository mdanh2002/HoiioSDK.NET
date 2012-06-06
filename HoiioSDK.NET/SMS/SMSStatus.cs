using System;
using System.Collections.Generic;

using System.Text;

namespace HoiioSDK.NET
{
    /// <summary>
    /// The different available status for a Hoiio SMS
    /// </summary>
    public enum SMSStatusTypes
    {
        QUEUED,
        DELIVERED,
        FAILED,
        ERROR,

        /// <summary>
        /// The SMS Status string could not be resolved into any predefined codes
        /// </summary>
        UNDEFINED
    }
    
    /// <summary>
    /// Helper class to parse a Hoiio SMS status 
    /// </summary>
    public class SMSStatusHelper
    {
        /// <summary>
        /// Convert a string into a enum representing a status of a Hoiio SMS
        /// </summary>
        public static SMSStatusTypes SMSStatusFromString(string statusStr)
        {
            switch (statusStr)
            {
                case "queued":
                    return SMSStatusTypes.QUEUED;
                case "delivered":
                    return SMSStatusTypes.DELIVERED;
                case "failed":
                    return SMSStatusTypes.FAILED;
                case "error":
                    return SMSStatusTypes.ERROR;
                default:
                    return SMSStatusTypes.UNDEFINED;
            }
        }
    }
}

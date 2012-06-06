using System;
using System.Collections.Generic;

using System.Text;

namespace HoiioSDK.NET
{
    /// <summary>
    /// The different available status for a Hoiio IVR call
    /// </summary>
    public enum IVRStatusTypes
    {
        RINGING,
        ONGOING,
        ENDED,

        /// <summary>
        /// The IVR Status string could not be resolved into any predefined codes
        /// </summary>
        UNDEFINED
    }

    /// <summary>
    /// Helper class to parse a Hoiio IVR call status
    /// </summary>
    public class IVRStatusHelper
    {
        /// <summary>
        /// Convert a string into a enum representing a status of a Hoiio IVR call
        /// </summary>
        public static IVRStatusTypes IVRStatusFromString(string statusStr)
        {
            switch (statusStr)
            {
                case "ringing":
                    return IVRStatusTypes.RINGING;
                case "ongoing":
                    return IVRStatusTypes.ONGOING;
                case "ended":
                    return IVRStatusTypes.ENDED;
                default:
                    return IVRStatusTypes.UNDEFINED;
            }
        }
    }
}

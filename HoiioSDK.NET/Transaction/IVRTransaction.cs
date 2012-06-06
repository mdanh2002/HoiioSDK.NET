using System;
using System.Collections.Generic;

using System.Text;

namespace HoiioSDK.NET
{
    /// <summary>
    /// Identify a Hoiio IVR transaction
    /// </summary>
    public class IVRTransaction : HoiioTransaction
    {
        protected string _session;
        /// <summary>
        /// Current session ID.
        /// </summary>
        public string session
        {
            get
            {
                return _session;
            }
        }

        public IVRTransaction()
        {

        }

        public IVRTransaction(Dictionary<string, object> res)
            : base(res)
        {
            if (res.ContainsKey("session"))
                _session = (string)res["session"];
        }

    }
}

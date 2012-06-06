using System;
using System.Collections.Generic;

using System.Text;
using Newtonsoft.Json.Linq;

namespace HoiioSDK.NET
{
    /// <summary>
    /// The Hoiio API response received after a call rate request
    /// </summary>
    public class CallRate : HoiioResponse
    {
        private string _currency;
        /// <summary>Currency code that this Call will be billed.</summary>
        public string currency
        {
            get
            {
                return _currency;
            }
        }

        private double _rate;
        /// <summary>
        /// Cost per minute (in currency indicated in currency code).
        /// </summary>
        public double rate
        {
            get
            {
                return _rate;
            }
        }

        private double _talkTime;
        /// <summary>
        /// Maximum talk time allowed for this call
        /// </summary>
        public double talkTime
        {
            get
            {
                return _talkTime;
            }
        }

        public CallRate (Dictionary<string, object> res) : base(res)
        {
            if (_success)
            {
                _currency = (string)res["currency"];
                _rate = Convert.ToDouble(res["rate"]);
                _talkTime = Convert.ToDouble(res["talktime"]);
            }
        }
    }
}

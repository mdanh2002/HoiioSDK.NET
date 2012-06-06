using System;
using System.Collections.Generic;

using System.Text;
using Newtonsoft.Json.Linq;

namespace HoiioSDK.NET
{
    /// <summary>
    /// The Hoiio API response received after a SMS rate request
    /// </summary>
    public class SMSRate : HoiioResponse
    {
        private string _currency;
        /// <summary>Currency code that this SMS will be billed.</summary>
        public string currency
        {
            get
            {
                return _currency;
            }
        }

        private int _splitCount;
        /// <summary>
        /// How many SMS is required to send this message.
        /// </summary>
        public int splitCount
        {
            get
            {
                return _splitCount;
            }
        }

        private double _totalCost;
        /// <summary>
        /// Total cost required for sending this message.
        /// </summary>
        public double totalCost
        {
            get
            {
                return _totalCost;
            }
        }

        private double _rate;
        /// <summary>
        /// Cost per SMS (in currency indicated in currency code)
        /// </summary>
        public double rate
        {
            get
            {
                return _rate;
            }
        }

        private bool _isUnicode;
        /// <summary>
        /// Whether this message contains any unicode characters.
        /// </summary>
        public bool isUnicode
        {
            get
            {
                return _isUnicode;
            }
        }

        public SMSRate (Dictionary<string, object> res) : base(res)
        {
            if (_success)
            {
                _currency = (string)res["currency"];
                _rate = Convert.ToDouble(res["rate"]);
                _splitCount = Convert.ToInt32(res["split_count"]);
                _totalCost = Convert.ToDouble(res["total_cost"]);
                _isUnicode = Convert.ToBoolean(res["is_unicode"]);
            }
        }
    }
}

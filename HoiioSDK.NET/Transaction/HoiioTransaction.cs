using System;
using System.Collections.Generic;

using System.Text;
using System.Diagnostics;

namespace HoiioSDK.NET
{
    /// <summary>
    /// Identify a Hoiio transaction, which can be Call, IVR or SMS
    /// </summary>
    public class HoiioTransaction : HoiioResponse
    {
        protected DateTime _date;
        /// <summary>
        /// Date/time of this transaction (in GMT+8).
        /// </summary>
        public DateTime date
        {
            get
            {
                return _date;
            }
        }

        protected string _currency;
        /// <summary>
        /// Currency code that this transaction will be billed.
        /// </summary>
        public string currency
        {
            get
            {
                return _currency;
            }
        }

        protected double _rate;
        /// <summary>
        /// Cost per minute for call or per message for SMS (in currency indicated in currency code).
        /// </summary>
        public double rate
        {
            get
            {
                return _rate;
            }
        }

        protected double _debit;
        /// <summary>
        /// Total cost deducted from your account for making this transaction
        /// </summary>
        public double debit
        {
            get
            {
                return _debit;
            }
        }

        public HoiioTransaction()
        {
        }

        public HoiioTransaction(Dictionary<string, object> res) : base(res)
        {
        }
    }
}

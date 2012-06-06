using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace HoiioSDK.NET
{
    /// <summary>
    /// Identify a Hoiio SMS transaction
    /// </summary>
    public class SMSTransaction : HoiioTransaction
    {
        private SMSStatusTypes _smsStatus;
        /// <summary>
        /// The current status of this SMS. Look at SMSStatus class for possible values.
        /// </summary>       
        public SMSStatusTypes smsStatus
        {
            get
            {
                return _smsStatus;
            }
        }


        private string _dest;
        /// <summary>
        /// The destination number of this SMS.
        /// </summary>
        public string dest
        {
            get
            {
                return _dest;
            }
        }

        private int _splitCount;
        /// <summary>
        /// How many SMS were required to send this message.
        /// </summary>
        public int splitCount
        {
            get
            {
                return _splitCount;
            }
        }

        /// <summary>
        /// Static method to parse a JSON response from server into into a SMSTransaction object
        /// </summary>
        /// <param name="res"></param>
        public SMSTransaction(Dictionary<string, object> res)
            : base(res)
        {
            if (_success)
            {
                _debit = Convert.ToDouble(res["debit"]);
                _smsStatus = SMSStatusHelper.SMSStatusFromString((string)res["sms_status"]);
                _date = StringUtil.stringToDate((string)res["date"]);
                _currency = (string)res["currency"];
                _dest = (string)res["dest"];
                _rate = Convert.ToDouble(res["rate"]);
                _splitCount = Convert.ToInt32(res["split_count"]);
            }
        }

        /// <summary>
        /// Create a new SMS transaction object
        /// </summary>
        public SMSTransaction(string txnRef, SMSStatusTypes  smsStatus, DateTime  date, string  dest, string  currency,
                                    double  rate, int splitCount, double  debit, string  tag = "")
        {
            _txnRef = txnRef;
            _date = date;
            _dest = dest;
            _tag = tag;
            _currency = currency;
            _rate = rate;
            _debit = debit;
            _splitCount = splitCount;
            _smsStatus = smsStatus;
        }
    }
}

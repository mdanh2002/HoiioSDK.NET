using System;
using System.Collections.Generic;

using System.Text;
using Newtonsoft.Json.Linq;

namespace HoiioSDK.NET
{
    /// <summary>
    /// Identify a Hoiio Call transaction
    /// </summary>
    public class CallTransaction : HoiioTransaction
    {
        private CallStatusTypes _fromStatus;
        /// <summary>
        /// The dial status of the source number. Look at CallStatus class for possible values.
        /// </summary>       
        public CallStatusTypes fromStatus
        {
            get
            {
                return _fromStatus;
            }
        }

        private CallStatusTypes _toStatus;
        /// <summary>
        /// The dial status of the destination number. Look at CallStatus class for possible values.
        /// </summary>
        public CallStatusTypes toStatus
        {
            get
            {
                return _toStatus;
            }
        }

        private string _from;
        /// <summary>
        /// The source number of this call.
        /// </summary>
        public string from
        {
            get
            {
                return _from;
            }
        }

        private string _to;
        /// <summary>
        /// The destination number of this call.
        /// </summary>
        public string to
        {
            get
            {
                return _to;
            }
        }

        private int _duration;
        /// <summary>
        /// Duration of this call in minutes
        /// </summary>
        public int duration
        {
            get
            {
                return _duration;
            }
        }

        /// <summary>
        /// Static method to parse a server response from server into into a CallTransaction object
        /// </summary>
        /// <param name="res"></param>
        public CallTransaction(Dictionary<string, object> res)
            : base(res)
        {
            if (_success)
            {
                if (res.ContainsKey("duration")) _duration = Convert.ToInt32(res["duration"]);
                if (res.ContainsKey("debit")) _debit = Convert.ToDouble(res["debit"]);

                _fromStatus = CallStatusHelper.CallStatusFromString((string)res["call_status_dest1"]);
                _toStatus = CallStatusHelper.CallStatusFromString((string)res["call_status_dest2"]);
                _date = StringUtil.stringToDate((string)res["date"]);
                _from = (string)res["dest1"];
                _currency = (string)res["currency"];
                _to = (string)res["dest2"];
                _rate = Convert.ToDouble(res["rate"]);
            }
        }
    }
}

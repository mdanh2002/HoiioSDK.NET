using System;
using System.Collections.Generic;

using System.Text;

namespace HoiioSDK.NET
{
    /// <summary>
    /// Contains information about the IVR session, as notified by Hoiio API.
    /// </summary>
    public class IVRNotification : IVRTransaction
    {
        private IVRStatusTypes _callState;
        /// <summary>
        /// The current state of the IVR.
        /// </summary>
        public IVRStatusTypes callState
        {
            get
            {
                return _callState;
            }
        }

        public CallStatusTypes _dialStatus;
        /// <summary>
        /// The dial status of the number after using a Dial block.
        /// </summary>
        public CallStatusTypes dialStatus
        {
            get
            {
                return _dialStatus;
            }
        }

        private int _duration;
        /// <summary>
        /// Call duration in minutes
        /// </summary>
        public int duration
        {
            get
            {
                return _duration;
            }
        }

        private string _dest;
        /// <summary>
        /// The destination number when using a Dial block.
        /// </summary>
        public string dest
        {
            get
            {
                return _dest;
            }
        }

        private string _digits;
        /// <summary>
        /// The keypad input from the user after using the Gather block.
        /// </summary>
        public string digits
        {
            get
            {
                return _digits;
            }
        }

        public string _recordURL;
        /// <summary>
        /// The URL of the recording after using the Record block.
        /// </summary>
        public string recordURL
        {
            get
            {
                return _recordURL;
            }
        }

        public CallStatusTypes _transferStatus;
        /// <summary>
        /// The transfer status of the number after using a Transfer block.
        /// </summary>
        public CallStatusTypes transferStatus
        {
            get
            {
                return _transferStatus;
            }
        }

        private string _from;
        /// <summary>
        /// The incoming Caller ID to your Hoiio Number.
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
        /// The Hoiio Number that was dialed for incoming call.
        /// </summary>
        public string to
        {
            get
            {
                return _to;
            }
        }

        public IVRNotification(IVRStatusTypes callState, string session, string txnRef,
                                    CallStatusTypes dialStatus = CallStatusTypes.FAILED, string digits = "", string recordURL = "",
                                    CallStatusTypes transferStatus = CallStatusTypes.FAILED,
                                    string from = "", string to = "", string dest = "",
                                    DateTime date = new System.DateTime(), string currency = "", double rate = 0, int duration = 0, double debit = 0, string tag = "")
        {
            _callState = callState;
            _session = session;
            _txnRef = txnRef;
            _tag = tag;

            _dialStatus = dialStatus;
            _digits = digits;
            _recordURL = recordURL;
            _transferStatus = transferStatus;
            _from = from;
            _to = to;
            _dest = dest;

            _date = date;
            _duration = duration;
            _currency = currency;
            _rate = rate;
            _debit = debit;
        }
    }
}

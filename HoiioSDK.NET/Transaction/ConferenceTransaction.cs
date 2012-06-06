using System;
using System.Collections.Generic;

using System.Text;

namespace HoiioSDK.NET
{
    /// <summary>
    /// Identify a Hoiio conference transaction
    /// </summary>
    public class ConferenceTransaction : HoiioResponse
    {
        /// <summary>
        /// The array of the transaction IDs for each party in the conference.
        /// </summary>
        public string[] txnRefs
        {
            get
            {
                if (_rawResponse.ContainsKey("txn_refs"))
                {
                    string txn_refs = (string)_rawResponse["txn_refs"];
                    return txn_refs.Split(new char[] { ',' }, StringSplitOptions.None);
                }
                else
                    return null;
            }
        }

        public ConferenceTransaction(Dictionary<string, object> res)
            : base(res)
        {
        }
    }
}

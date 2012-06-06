using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Newtonsoft.Json;

namespace HoiioSDK.NET
{
    /// <summary>
    /// The different Hoiio transaction type available
    /// </summary>
    public enum HoiioTransactionType
    {
        CallTransaction,
        SMSTransaction,
        IVRTransaction
    }

    /// <summary>
    /// Represent a list of transaction history. It can contain an array of
    /// CallTransaction of SMSTransaction objects.
    /// </summary>
    public class TransactionHistory
    {
        private HoiioResponse _rawResponse;
        /// <summary>
        /// The raw response from the original get history request
        /// </summary>
        public HoiioResponse rawResponse
        {
            get
            {
                return _rawResponse;
            }
        }

        private DateTime _resultFrom;
        /// <summary>
        /// Start date for the query
        /// </summary>
        public DateTime resultFrom
        {
            get
            {
                return _resultFrom;
            }
        }

        private DateTime _resultTo;
        /// <summary>
        /// End date for the query
        /// </summary>
        public DateTime resultTo
        {
            get
            {
                return _resultTo;
            }
        }

        private int _page = 1;
        /// <summary>
        /// Current page. First page starts from 1.
        /// </summary>
        public int page
        {
            get
            {
                return _page;
            }
        }

        private int _totalEntries = 0;
        /// <summary>
        /// Total number of entries in the query result
        /// </summary>
        public int totalEntries
        {
            get
            {
                return _totalEntries;
            }
        }

        private int _entriesCount = 0;
        /// <summary>
        /// Number of entries in this page
        /// </summary>
        public int entriesCount
        {
            get
            {
                return _entriesCount;
            }
        }

        private List<HoiioTransaction> _transactions;
        /// <summary>
        /// Get the array of SMSTransaction or CallTransaction objects.
        /// </summary>
        public List<HoiioTransaction> transactions
        {
            get
            {
                return _transactions;
            }
        }

        /// <summary>
        /// Create a new TransactionHistory object
        /// </summary>
        public TransactionHistory(int totalEntries, int entriesCount, List<HoiioTransaction> transactions,
                                    DateTime resultFrom, DateTime resultTo, int page = 1)
        {
            _resultFrom = resultFrom;
            _resultTo = resultTo;
            _page = page;

            _totalEntries = totalEntries;
            _entriesCount = entriesCount;
            _transactions = transactions;
        }

        /// <summary>
        ///  Static method to parse a server response from server into into a list of CallTransaction or SMSTransaction objects
        /// </summary>
        /// <param name="callTransactionsRes"></param>
        public TransactionHistory(Dictionary<string, object> res, HoiioTransactionType type, DateTime resultFrom, DateTime resultTo, int page)
        {
            if (_transactions== null)
            {
                _transactions = new List<HoiioTransaction>();
            }

            _rawResponse = new HoiioResponse(res);

            if (_rawResponse.success)
            {
                _resultFrom = resultFrom;
                _resultTo = resultTo;

                _totalEntries = Convert.ToInt32(res["total_entries_count"]);
                _entriesCount = Convert.ToInt32(res["entries_count"]);
                _page = page;

                Newtonsoft.Json.Linq.JArray entries = (Newtonsoft.Json.Linq.JArray)res["entries"];
                foreach (Newtonsoft.Json.Linq.JToken entry in entries)
                {
                    Dictionary<String, Object> entryDict = JsonConvert.DeserializeObject<Dictionary<String, Object>>(entry.ToString());

                    HoiioTransaction tx;

                    if (type == HoiioTransactionType.CallTransaction)
                    {
                        tx = new CallTransaction(entryDict);
                        _transactions.Add(tx);
                    }
                    else
                    {
                        tx = new SMSTransaction(entryDict);
                        _transactions.Add(tx);
                    }
                }
            }
        }
    }
}

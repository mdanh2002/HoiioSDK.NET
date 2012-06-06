using System;
using System.Collections.Generic;

using System.Text;

namespace HoiioSDK.NET
{
    /// <summary>
    /// Information about the active Hoiio account
    /// </summary>
    public class AccountInfo : HoiioResponse
    {
        private string _uid;
        /// <summary>
        /// The user id of your account.
        /// </summary>
        public string uid
        {
            get
            {
                return _uid;
            }
        }

        private string _name;
        /// <summary>
        /// The full name of your account.
        /// </summary>
        public string name
        {
            get
            {
                return _name;
            }           
        }

        private string _mobile_number;
        /// <summary>
        /// The registered number of your account. Phone numbers start with a "+" and country code (E.164 format), e.g. +6511111111.
        /// </summary>
        public string mobile_number
        {
            get
            {
                return _mobile_number;
            }
        }

        private string _email;
        /// <summary>
        /// The email address of your account.
        /// </summary>
        public string email
        {
            get
            {
                return _email;
            }
        }

        private string _country;
        /// <summary>
        /// The country of your account in ISO 3166-1 alpha-2 format (e.g. SG).
        /// </summary>
        public string country
        {
            get
            {
                return _country;
            }
        }

        private string _prefix;
        /// <summary>
        /// The country code prefix of your account (e.g. +65)
        /// </summary>
        public string prefix
        {
            get
            {
                return _prefix;
            }
        }

        private string _currency;
        /// <summary>
        /// Currency used for this account. Refer to Currency Code for the list of currency code.
        /// </summary>
        public string currency
        {
            get
            {
                return _currency;
            }
        }

        public AccountInfo(Dictionary<string, object> res)
            : base(res)
        {
            if (_success)
            {
                _currency = (string)res["currency"];
                _prefix = (string)res["prefix"];
                _uid = (string)res["uid"];
                _name = (string)res["name"];
                _mobile_number = (string)res["mobile_number"];
                _email = (string)res["email"];
                _country = (string)res["country"];
            }
        }
    }
}

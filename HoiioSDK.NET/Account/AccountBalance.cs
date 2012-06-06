using System;
using System.Collections.Generic;

using System.Text;

namespace HoiioSDK.NET
{
    /// <summary>
    /// The balance of the current Hoiio account
    /// </summary>
    public class AccountBalance : HoiioResponse
    {
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

        private double _balance;
        /// <summary>
        /// The total available credit balance for your account. This is the sum of your Hoiio Points and Hoiio Bonus Points.
        /// </summary>
        public double balance
        {
            get
            {
                return _balance;
            }            
        }

        private double _points;
        /// <summary>
        /// Your Hoiio Points credit balance. Hoiio Points can be transferred to another account.
        /// </summary>
        public double points
        {
            get
            {
                return _points;
            }            
        }

        private double _bonus;
        /// <summary>
        /// Your Hoiio Bonus Points credit balance. Hoiio Bonus Points cannot be transferred. If you have remaining Hoiio Bonus Points, it will be used for your transactions first before Hoiio Points.
        /// </summary>
        public double bonus
        {
            get
            {
                return _bonus;
            }
        }

        public AccountBalance(Dictionary<string, object> res)
            : base(res)
        {
            if (_success)
            {
                _currency = (string)res["currency"];
                _balance = Convert.ToDouble(res["balance"]);
                _points = Convert.ToDouble(res["points"]);
                _bonus = Convert.ToDouble(res["bonus"]);
            }
        }
    }
}

using System;
using System.Collections.Generic;

using System.Text;

namespace HoiioSDK.NET
{
    /// <summary>
    /// Perform account related task such as getting account info and balance
    /// </summary>
    public class AccountService : HttpService
    {
        private static string URL_ACCOUNT_INFO = "user/get_info";
        private static string URL_ACCOUNT_BAL = "user/get_balance";

        public static Dictionary<string, Object> getAccountInfo(string appId, string accessToken)
        {
            Dictionary<string, string> map = initParam(appId, accessToken);
            return doHttpPost(URL_ACCOUNT_INFO, map);
        }

        public static Dictionary<string, Object> getAccountBalance(string appId, string accessToken)
        {
            Dictionary<string, string> map = initParam(appId, accessToken);
            return doHttpPost(URL_ACCOUNT_BAL, map);
        }
    }
}

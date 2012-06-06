using System;
using System.Collections.Generic;

using System.Text;

namespace HoiioSDK.NET
{
    /// <summary>
    ///
    /// SMSService class provides acccess to SMS-related Hoiio API.
    /// Currently, includes:
    ///  - Send SMS
    ///  - Get SMS rates
    ///  - Get SMS history
    ///  - Get SMS status
    ///  - Parse SMS notifications
    /// </summary>
    public class SMSService : HttpService
    {
        private static string PARAM_DEST = "dest";
        private static string PARAM_SENDER_NAME = "sender_name";
        private static string PARAM_MSG = "msg";
        private static string PARAM_TAG = "tag";
        private static string PARAM_NOTIFY_URL = "notify_url";
        private static string PARAM_FROM = "from";
        private static string PARAM_TO = "to";
        private static string PARAM_PAGE = "page";
        private static string PARAM_TXN_REF = "txn_ref";

        private static string URL_SMS_SEND = "sms/send";
        private static string URL_SMS_GET_RATE = "sms/get_rate";
        private static string URL_SMS_GET_HISTORY = "sms/get_history";
        private static string URL_SMS_QUERY_STATUS = "sms/query_status";

        public static Dictionary<string, Object> send(string appId, string accessToken, string dest,
                string senderName, string msg, string tag, string notifyUrl)
        {
            Dictionary<string, string> map = initParam(appId, accessToken);
            map.Add(PARAM_DEST, dest);
            map.Add(PARAM_MSG, msg);
            if (senderName != null && senderName != "") map.Add(PARAM_SENDER_NAME, senderName);
            map.Add(PARAM_TAG, tag);
            map.Add(PARAM_NOTIFY_URL, notifyUrl);

            return doHttpPost(URL_SMS_SEND, map);
        }

        public static Dictionary<string, Object> getRate(string appId, string accessToken,
                string dest, string msg)
        {
            Dictionary<string, string> map = initParam(appId, accessToken);
            map.Add(PARAM_DEST, dest);
            map.Add(PARAM_MSG, msg);

            return doHttpPost(URL_SMS_GET_RATE, map);
        }

        public static Dictionary<string, Object> queryStatus(string appId, string accessToken,
                string txnRef)
        {
            Dictionary<string, string> map = initParam(appId, accessToken);
            map.Add(PARAM_TXN_REF, txnRef);
            return doHttpPost(URL_SMS_QUERY_STATUS, map);
        }

        public static Dictionary<string, Object> getHistory(string appId, string accessToken,
                DateTime from, DateTime to, int page)
        {
            Dictionary<string, string> map = initParam(appId, accessToken);
            map.Add(PARAM_FROM, StringUtil.dateToString(from));
            map.Add(PARAM_TO, StringUtil.dateToString(to));
            map.Add(PARAM_PAGE, page < 0 ? null : page.ToString());

            return doHttpPost(URL_SMS_GET_HISTORY, map);
        }
    }
}

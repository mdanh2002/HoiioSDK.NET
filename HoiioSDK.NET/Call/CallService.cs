using System;
using System.Collections.Generic;

using System.Text;

namespace HoiioSDK.NET
{
    /// <summary>
    /// CallService class provides acccess to voice-related Hoiio API.
    /// Currently, includes:
    ///  - Make a 2-way callback
    ///  - Make conference calls
    ///  - Get call rates
    ///  - Get call history
    ///  - Get call status
    ///  - Hangup a call
    ///  - Parse call notifications
    /// </summary>
    public class CallService : HttpService
    {
        private static string PARAM_DEST1 = "dest1";
        private static string PARAM_DEST2 = "dest2";
        private static string PARAM_CALLER_ID = "caller_id";
        private static string PARAM_TAG = "tag";
        private static string PARAM_NOTIFY_URL = "notify_url";
        private static string PARAM_FROM = "from";
        private static string PARAM_TO = "to";
        private static string PARAM_PAGE = "page";
        private static string PARAM_TXN_REF = "txn_ref";
        private static string PARAM_DEST = "dest";
        private static string PARAM_ROOM = "room";

        private static string URL_VOICE_CALL = "voice/call";
        private static string URL_VOICE_GET_RATE = "voice/get_rate";
        private static string URL_VOICE_GET_HISTORY = "voice/get_history";
        private static string URL_VOICE_QUERY_STATUS = "voice/query_status";
        private static string URL_VOICE_CONFERENCE = "voice/conference";
        private static string URL_VOICE_HANGUP = "voice/hangup";

        public static Dictionary<String, Object> makeCall(String appId, string accessToken, string dest1, string dest2,
                String callerId, string tag, string notifyUrl)
        {
            Dictionary<String, string> map = initParam(appId, accessToken);

            map.Add(PARAM_DEST2, dest2);
            map.Add(PARAM_DEST1, dest1);
            map.Add(PARAM_CALLER_ID, callerId);
            map.Add(PARAM_TAG, tag);
            map.Add(PARAM_NOTIFY_URL, notifyUrl);

            return doHttpPost(URL_VOICE_CALL, map);
        }

        public static Dictionary<String, Object> getRate(String appId, string accessToken,
                String dest1, string dest2)
        {
            Dictionary<String, string> map = initParam(appId, accessToken);

            map.Add(PARAM_DEST2, dest2);
            map.Add(PARAM_DEST1, dest1);

            return doHttpPost(URL_VOICE_GET_RATE, map);

        }

        public static Dictionary<String, Object> queryStatus(String appId, string accessToken,
                String txnRef)
        {
            Dictionary<String, string> map = initParam(appId, accessToken);

            map.Add(PARAM_TXN_REF, txnRef);

            return doHttpPost(URL_VOICE_QUERY_STATUS, map);
        }

        public static Dictionary<String, Object> getHistory(String appId, string accessToken,
                DateTime from, DateTime to, int page)
        {

            Dictionary<String, string> map = initParam(appId, accessToken);

           map.Add(PARAM_FROM, StringUtil.dateToString(from));
            map.Add(PARAM_TO, StringUtil.dateToString(to));
            map.Add(PARAM_PAGE, page < 0 ? null : page.ToString());

            return doHttpPost(URL_VOICE_GET_HISTORY, map);
        }

        public static Dictionary<String, Object> createConference(String appId, string accessToken,
                String dest, string room, string callerId, string tag,
                String notifyUrl)
        {
            Dictionary<String, string> map = initParam(appId, accessToken);

            map.Add(PARAM_DEST, dest);
            map.Add(PARAM_ROOM, room);
            map.Add(PARAM_CALLER_ID, callerId);
            map.Add(PARAM_TAG, tag);
            map.Add(PARAM_NOTIFY_URL, notifyUrl);

            return doHttpPost(URL_VOICE_CONFERENCE, map);
        }

        public static Dictionary<String, Object> hangup(String appId, string accessToken,
                String txnRef)
        {
            Dictionary<String, string> map = initParam(appId, accessToken);

            map.Add(PARAM_TXN_REF, txnRef);

            return doHttpPost(URL_VOICE_HANGUP, map);
        }
    }
}

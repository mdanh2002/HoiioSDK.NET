using System;
using System.Collections.Generic;

using System.Text;

namespace HoiioSDK.NET
{
    /// <summary>
    /// IVRService class provides acccess to IVR-related Hoiio API.
    /// Currently, includes:
    /// - Answer
    /// - Dial
    /// - Play
    /// - Gather
    /// - Record
    /// - Transfer
    /// - Hangup
    /// </summary>
    public class IVRService : HttpService
    {
        private static string PARAM_DEST = "dest";
        private static string PARAM_MSG = "msg";
        private static string PARAM_CALLER_ID = "caller_id";
        private static string PARAM_TAG = "tag";
        private static string PARAM_NOTIFY_URL = "notify_url";
        private static string PARAM_SESSION = "session";
        private static string PARAM_MAX_DIGITS = "max_digits";
        private static string PARAM_TIMEOUT = "timeout";
        private static string PARAM_ATTEMPTS = "attempts";
        private static string PARAM_MAX_DURATION = "max_duration";

        private static string URL_IVR_DIAL = "ivr/start/dial";
        private static string URL_IVR_PLAY = "ivr/middle/play";
        private static string URL_IVR_GATHER = "ivr/middle/gather";
        private static string URL_IVR_RECORD = "ivr/middle/record";
        private static string URL_IVR_TRANSFER = "ivr/end/transfer";
        private static string URL_IVR_HANGUP = "ivr/end/hangup";

        public static Dictionary<string, Object> dial(string appId, string accessToken, string dest,
                string msg, string callerID, string tag, string notifyUrl)
        {
            Dictionary<string, string> map = initParam(appId, accessToken);
            map.Add(PARAM_DEST, dest);
            map.Add(PARAM_MSG, msg);
            map.Add(PARAM_CALLER_ID, callerID);
            map.Add(PARAM_TAG, tag);
            map.Add(PARAM_NOTIFY_URL, notifyUrl);

            return doHttpPost(URL_IVR_DIAL, map);
        }

        public static Dictionary<string, Object> play(string appId, string accessToken, string session,
                string msg, string tag, string notifyUrl)
        {
            Dictionary<string, string> map = initParam(appId, accessToken);

            map.Add(PARAM_SESSION, session);
            map.Add(PARAM_MSG, msg);
            map.Add(PARAM_TAG, tag);
            map.Add(PARAM_NOTIFY_URL, notifyUrl);

            return doHttpPost(URL_IVR_PLAY, map);
        }

        public static Dictionary<string, Object> gather(string appId, string accessToken, string session,
                string msg, int maxDigits, int timeout, int attempts, string tag,
                string notifyUrl)
        {
            Dictionary<string, string> map = initParam(appId, accessToken);

            map.Add(PARAM_SESSION, session);
            map.Add(PARAM_NOTIFY_URL, notifyUrl);
            map.Add(PARAM_MSG, msg);
            map.Add(PARAM_TAG, tag);
            if (maxDigits > 0) map.Add(PARAM_MAX_DIGITS, maxDigits.ToString());
            if (timeout > 0) map.Add(PARAM_TIMEOUT, timeout.ToString());
            if (attempts > 0) map.Add(PARAM_ATTEMPTS, attempts.ToString());

            return doHttpPost(URL_IVR_GATHER, map);
        }

        public static Dictionary<string, Object> record(string appId, string accessToken, string session,
                string msg, string maxDuration, string tag, string notifyUrl)
        {
            Dictionary<string, string> map = initParam(appId, accessToken);

            map.Add(PARAM_SESSION, session);
            map.Add(PARAM_NOTIFY_URL, notifyUrl);
            map.Add(PARAM_MSG, msg);
            map.Add(PARAM_TAG, tag);
            if (maxDuration != null) map.Add(PARAM_MAX_DURATION, maxDuration);

            return doHttpPost(URL_IVR_RECORD, map);
        }

        public static Dictionary<string, Object> transfer(string appId, string accessToken, string session,
                string msg, string dest, string callerID, string tag,
                string notifyUrl)
        {
            Dictionary<string, string> map = initParam(appId, accessToken);

            map.Add(PARAM_SESSION, session);
            map.Add(PARAM_NOTIFY_URL, notifyUrl);
            map.Add(PARAM_MSG, msg);
            map.Add(PARAM_TAG, tag);
            map.Add(PARAM_DEST, dest);
            map.Add(PARAM_CALLER_ID, callerID);

            return doHttpPost(URL_IVR_TRANSFER, map);
        }

        public static Dictionary<string, Object> hangup(string appId, string accessToken, string session,
                string msg, string tag, string notifyUrl)
        {
            Dictionary<string, string> map = initParam(appId, accessToken);

            map.Add(PARAM_SESSION, session);
            map.Add(PARAM_NOTIFY_URL, notifyUrl);
            map.Add(PARAM_MSG, msg);
            map.Add(PARAM_TAG, tag);

            return doHttpPost(URL_IVR_HANGUP, map);
        }

    }
}

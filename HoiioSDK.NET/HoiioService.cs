using System;
using System.Collections.Generic;

using System.Text;
using Newtonsoft.Json;
using System.Web;
using System.Collections.Specialized;

namespace HoiioSDK.NET
{
    /// <summary>
    /// Wrapper classes for Hoiio API (Call, SMS, IVR, Account, etc). 
    /// End users should use this class to perform most API calls.
    /// </summary>
    public class HoiioService
    {
        /// <summary>
        /// Create a new Hoiio service object with your developer's credentials.
        /// </summary>
        /// <param name="appId">The Hoiio API application ID</param>
        /// <param name="accessToken">The Hoiio API application access token</param>
        public HoiioService(String appId, String accessToken)
        {
            this.appId = appId;
            this.accessToken = accessToken;
        }

        #region Private Declarations
        /** Developer Credentials **/
        private String appId;
        private String accessToken;
        #endregion

        #region Account APIs
        /// <summary>
        /// This API retrieves the general information of your account.
        /// </summary>
        /// <returns></returns>
        public AccountInfo accountGetInfo()
        {
            return new AccountInfo(AccountService.getAccountInfo(appId, accessToken));
        }

        /// <summary>
        /// This API retrieves the Hoiio account balance.
        /// </summary>
        public AccountBalance accountGetBalance()
        {
            return new AccountBalance(AccountService.getAccountBalance(appId, accessToken));
        }
        #endregion

        #region Call APIs
        /// <summary>
        /// This API will dial out to 2 destination number and connect them together in a phone conversation.
        /// </summary>
        /// <param name="dest1">(optional) The first number to call in E.164 format (e.g. +6511111111). This cannot be the same as dest2 parameter. If omitted, the call will be made to the number registered in your developer account</param>
        /// <param name="dest2">The second number to call in E.164 format (e.g. +6511111111). This cannot be the same as dest1 parameter.	</param>
        /// <param name="callerId">(optional) Caller ID that dest2 will see on their incoming call. Can be either developer's registered number/hoiio number/"private"</param>
        /// <param name="tag">(optional) This is a text string containing your own reference ID for this transaction</param>
        /// <param name="notifyUrl"(optional) A fully-qualified HTTP/S callback URL on your web server to be notified when the call ends></param>
        /// <returns></returns>
        public HoiioResponse callMakeCall(String dest1, String dest2, String callerId,
                String tag, String notifyUrl)
        {
            return new HoiioResponse(CallService.makeCall(appId, accessToken, dest1, dest2, callerId, tag, notifyUrl));
        }

        /// <summary>
        /// This API retrieves the billable rate that will be charged for calls made.
        /// </summary>
        /// <param name="dest1">The first number to call in E.164 format (e.g. +6511111111).</param>
        /// <param name="dest2">The second number to call in E.164 format (e.g. +6511111111).</param>
        /// <returns></returns>
        public CallRate callGetRate(String dest1, String dest2)
        {
            return new CallRate(CallService.getRate(appId, accessToken, dest1, dest2));
        }

        /// <summary>
        /// This API allows you to query the current status of a call made previously.
        /// </summary>
        /// <param name="txnRef">The unique reference ID for the required transaction.</param>
        /// <returns></returns>
        public CallTransaction callQueryStatus(String txnRef)
        {
            Dictionary<string, object> res = CallService.queryStatus(appId, accessToken, txnRef);
            return new CallTransaction(res);
        }

        /// <summary>
        /// This API will retrieve the history of calls made by this application.
        /// </summary>
        /// <param name="from">(optional) Retrieve call history made by this app starting from this date/time in "YYYY-MM-DD HH:MM:SS" (GMT+8) format. E.g. "2010-01-01 00:00:00". If omitted, call history will be retrieved from the earliest transaction.</param>
        /// <param name="to">(optional) Retrieve call history made by this app before this date/time in "YYYY-MM-DD HH:MM:SS" (GMT+8) format. E.g. "2010-01-01 00:00:00". If omitted, call history will be retrieved up to the current point of time.</param>
        /// <param name="page">(optional) Each request returns a maximum of 100 entries. This parameter indicates which subset of entries to return new HoiioResponse(. If omitted, the first page will be retrieved.</param>
        /// <returns></returns>
        public TransactionHistory callGetHistory(DateTime from, DateTime to, int page)
        {
            Dictionary<string, object> res = CallService.getHistory(appId, accessToken, from, to, page);
            TransactionHistory callTxList = new TransactionHistory(res, HoiioTransactionType.CallTransaction, from, to, page);
            return callTxList;
        }

        /// <summary>
        /// This API will dial out to a list of destination numbers and place them together in a conference call.
        /// </summary>
        /// <param name="dest">A comma-seperated list of destination numbers in E.164 format to be called and placed in the conference room</param>
        /// <param name="room">(optional) A text string representing the conference room ID. Valid characters are a-z, A-Z, 0-9, period (.) and underscore (_) characters. Max 32 characters.</param>
        /// <param name="callerId">(optional) The Caller ID that each destination number will see on their incoming call. </param>
        /// <param name="tag">(optional) This is a text string containing your own reference ID for this transaction</param>
        /// <param name="notifyUrl">(optional) A fully-qualified HTTP/S callback URL on your web server to be notified when a call ends</param>
        /// <returns></returns>
        public ConferenceTransaction callCreateConference(String dest, String room, String callerId,
                String tag, String notifyUrl)
        {
            return new ConferenceTransaction(CallService.createConference(appId, accessToken, dest, room, callerId, tag, notifyUrl));
        }

        /// <summary>
        /// This API will hangup a call that is currently in progress. You can call this API at any point of time when a call is in progress to hang up the call.
        /// </summary>
        /// <param name="txnRef">The unique reference ID for the call you want to hangup.</param>
        /// <returns></returns>
        public HoiioResponse callHangUp(String txnRef)
        {
            return new HoiioResponse(CallService.hangup(appId, accessToken, txnRef));
        }
        #endregion

        #region SMS APIs
        /// <summary>
        /// This API allows you to send SMS to mobile numbers in over 200 countries.
        /// </summary>
        /// <param name="dest">The recipient number of the SMS in E.164 format (e.g. +6511111111).</param>
        /// <param name="senderName">(optional) The sender name that the recipient of your SMS will see</param>
        /// <param name="msg">Contents of the SMS message.</param>
        /// <param name="tag">(optional) This is a text string containing your own reference ID for this transaction</param>
        /// <param name="notifyUrl">(optional) A fully-qualified HTTP/S callback URL on your web server to be notified when the SMS has been delivered</param>
        /// <returns></returns>
        public HoiioResponse smsSend(String dest, String senderName, String msg, String tag, String notifyUrl)
        {
            return new HoiioResponse(SMSService.send(appId, accessToken, dest, senderName, msg, tag, notifyUrl));
        }

        /// <summary>
        /// This API retrieves the billable rate that will be charged for each multipart SMS message sent or receive.
        /// </summary>
        /// <param name="dest">The recipient number of the SMS in E.164 format (e.g. +6511111111).</param>
        /// <param name="msg">(optional) If provided, an estimate of the number of multipart SMS and total cost of sending this message will be included in the response. Otherwise, the rate per multipart SMS will be returned.</param>
        /// <returns></returns>
        public SMSRate smsGetRate(String dest, String msg)
        {
            return new SMSRate(SMSService.getRate(appId, accessToken, dest, msg));
        }

        /// <summary>
        /// This API allows you to query the current status of a SMS sent previously.
        /// </summary>
        /// <param name="txnRef">The unique reference ID for the required transaction.</param>
        /// <returns></returns>
        public SMSTransaction smsQueryStatus(String txnRef)
        {
            return new SMSTransaction(SMSService.queryStatus(appId, accessToken, txnRef));
        }

        /// <summary>
        /// This API will retrieve the history of SMS sent or received by this application.
        /// </summary>
        /// <param name="from">(optional) Retrieve SMS history made by this app starting from this date/time in "YYYY-MM-DD HH:MM:SS" (GMT+8) format. E.g. "2010-01-01 00:00:00". If omitted, SMS history will be retrieved from the earliest transaction.</param>
        /// <param name="to">(optional) Retrieve SMS history made by this app before this date/time in "YYYY-MM-DD HH:MM:SS" (GMT+8) format. E.g. "2010-01-01 00:00:00". If omitted, SMS history will be retrieved up to the current point of time.</param>
        /// <param name="page">(optional) Each request returns a maximum of 100 entries. This parameter indicates which subset of entries to return new HoiioResponse(. If omitted, the first page will be retrieved.</param>
        public TransactionHistory smsGetHistory(DateTime from, DateTime to, int page)
        {
            Dictionary<string, object> res = SMSService.getHistory(appId, accessToken, from, to, page);
            TransactionHistory smsTxList = new TransactionHistory(res, HoiioTransactionType.SMSTransaction, from, to, page);
            return smsTxList;
        }
        #endregion

        #region IVR APIs
        /// <summary>
        /// Start an IVR session by dialing out to a destination number. 
        /// </summary>
        /// <param name="msg">(optional) The message that you want to play after the call is answered. Max 500 characters.</param>
        /// <param name="dest">The destination number to call in E.164 format (e.g. +6511111111).</param>
        /// <param name="callerID">(optional) The Caller ID that the destination number will see on their incoming call</param>
        /// <param name="tag">(optional) This is a text string containing your own reference ID for this transaction</param>
        /// <param name="notifyUrl">(optional) A fully-qualified HTTP/S URL on your web server to be notified when this action has completed execution</param>
        /// <returns></returns>
        public IVRTransaction ivrDial(String msg, String dest, String callerID, String tag, String notifyUrl)
        {
            return new IVRTransaction(IVRService.dial(appId, accessToken, dest, msg, callerID, tag, notifyUrl));
        }

        /// <summary>
        /// Play a voice message to an existing IVR session using text-to-Speech service.
        /// </summary>
        /// <param name="session">The unique session ID for this particular call. (returned in one of the start blocks: dial or answer)</param>
        /// <param name="msg">(optional) This is the message that you want to play to the user. Max 500 characters. Our Text-to-Speech service will automatically generate the voice recording for the message you indicated in your API request.</param>
        /// <param name="tag"(optional) This is a text string containing your own reference ID for this transaction></param>
        /// <param name="notifyUrl">(optional) A fully-qualified HTTP/S URL on your web server to be notified when this action has completed execution</param>
        /// <returns></returns>
        public HoiioResponse ivrPlay(String session, String msg, String tag, String notifyUrl)
        {
            return new HoiioResponse(IVRService.play(appId, accessToken, session, msg, tag, notifyUrl));
        }

        /// <summary>
        /// Get user DTMF input during an IVR session. 
        /// </summary>
        /// <param name="session">The unique session ID for this particular call. (returned in one of the start blocks: dial or answer)</param>
        /// <param name="msg">(optional) This is the message that you want to play to the user. Max 500 characters.</param>
        /// <param name="maxDigits">(optional) the maximum digits people can enter</param>
        /// <param name="timeout">(optional) the time that user need to input the response</param>
        /// <param name="attempts">(optional) number of user attempts</param>
        /// <param name="tag">(optional) This is a text string containing your own reference ID for this transaction></param>
        /// <param name="notifyUrl">A fully-qualified HTTP/S URL on your web server to be notified when this action has completed execution</param>
        /// <returns></returns>
        public HoiioResponse ivrGather(String session, String msg, int maxDigits, int timeout, int attempts, String tag, String notifyUrl)
        {
            return new HoiioResponse(IVRService.gather(appId, accessToken, session, msg, maxDigits, timeout, attempts, tag, notifyUrl));
        }

        /// <summary>
        /// Allows you to record voice messages from the user during an IVR session. 
        /// </summary>
        /// <param name="session">The unique session ID for this particular call. (returned in one of the start blocks: dial or answer)</param>
        /// <param name="msg">(optional) This is the message that you want to play to the user. Max 500 characters.</param>
        /// <param name="maxDuration">(optional) maximum time of the record</param>
        /// <param name="tag">(optional) This is a text string containing your own reference ID for this transaction</param>
        /// <param name="notifyUrl">A fully-qualified HTTP/S URL on your web server to be notified when this action has completed execution</param>
        /// <returns></returns>
        public HoiioResponse ivrRecord(String session, String msg, String maxDuration, String tag, String notifyUrl)
        {
            return new HoiioResponse(IVRService.record(appId, accessToken, session, msg, maxDuration, tag, notifyUrl));
        }

        /// <summary>
        /// Transfer the current IVR call to another destination number anywhere in the world or a voice conference room.
        /// </summary>
        /// <param name="session">The unique session ID for this particular call. (returned in one of the start blocks: dial or answer)</param>
        /// <param name="msg">(optional) This is the message that you want to play to the user. Max 500 characters.</param>
        /// <param name="dest">the destination to transfer the call to</param>
        /// <param name="callerID">(optional) the callerID of the call</param>
        /// <param name="tag">(optional) This is a text string containing your own reference ID for this transaction</param>
        /// <param name="notifyUrl">(optional) A fully-qualified HTTP/S URL on your web server to be notified when this action has completed execution</param>
        /// <returns></returns>
        public HoiioResponse ivrTransfer(String session, String msg, String dest, String callerID, String tag, String notifyUrl)
        {
            return new HoiioResponse(IVRService.transfer(appId, accessToken, session, msg, dest, callerID, tag, notifyUrl));
        }

        /// <summary>
        /// Terminate an existing IVR session by hanging up the call
        /// </summary>
        /// <param name="session">The unique session ID for this particular call. (returned in one of the start blocks: dial or answer)</param>
        /// <param name="msg">(optional) This is the message that you want to play to the user. Max 500 characters.</param>
        /// <param name="tag">(optional) This is a text string containing your own reference ID for this transaction</param>
        /// <param name="notifyUrl">(optional) A fully-qualified HTTP/S URL on your web server to be notified when this action has completed execution</param>
        /// <returns></returns>
        public HoiioResponse ivrHangup(String session, String msg, String tag, String notifyUrl)
        {
            return new HoiioResponse(IVRService.hangup(appId, accessToken, session, msg, tag, notifyUrl));
        }
        #endregion

        #region URL Notification Parser
        /// <summary>
        /// Convert a HTTP POST body in name-value-pair format into a Dictionary
        /// </summary>
        private static Dictionary<string, object> postStringToDictionary(string notifyStr)
        {
            Dictionary<string, object> retVal = new Dictionary<string, object>();

            NameValueCollection nvc =  HttpUtility.ParseQueryString(notifyStr);

            foreach (string key in nvc.AllKeys)
            {
                if (!retVal.ContainsKey(key))
                {
                    retVal.Add(key, nvc[key]);
                }
            }

            /*
            foreach (var item in notifyStr.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var tokens = item.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length < 2)
                {
                    continue;
                }
                var paramName = tokens[0];
                var paramValue = tokens[1];
                var values = paramValue.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var value in values)
                {
                    var decodedValue = HttpUtility.UrlDecode(paramValue);
                    retVal.Add(paramName, decodedValue);
                }
            }
            */


            return retVal;

        }

        /// <summary>
        /// Parse the POST request that was sent by the Hoiio server to a remote notification URL after an IVR session is completed,
        /// or when an IVR recording is created, or DTMF is received.
        /// </summary>
        public static IVRNotification parseIVRNotify(string notifyStr)
        {
            Dictionary<String, Object> post_var = postStringToDictionary(notifyStr);

            CallStatusTypes dialStatus = post_var.ContainsKey("dial_status") ? CallStatusHelper.CallStatusFromString((string)post_var["dial_status"]) : CallStatusTypes.UNDEFINED;
            IVRStatusTypes callState = post_var.ContainsKey("call_state") ? IVRStatusHelper.IVRStatusFromString((string)post_var["call_state"]) : IVRStatusTypes.UNDEFINED;

            string digits = post_var.ContainsKey("digits") ? (string)post_var["digits"] : "";
            string recordURL = post_var.ContainsKey("record_url") ? (string)post_var["record_url"] : "";
            CallStatusTypes transferStatus = post_var.ContainsKey("transfer_status") ? CallStatusHelper.CallStatusFromString((string)post_var["transfer_status"]) : CallStatusTypes.UNDEFINED;
            string from = post_var.ContainsKey("from") ? (string)post_var["from"] : "";
            string to = post_var.ContainsKey("to") ? (string)post_var["to"] : "";
            string dest = post_var.ContainsKey("dest") ? (string)post_var["dest"] : "";
            string txn_ref = post_var.ContainsKey("txn_ref") ? (string)post_var["txn_ref"] : "";

            DateTime date = post_var.ContainsKey("date") ? StringUtil.stringToDate((string)post_var["date"]) : DateTime.MinValue;
            string currency = post_var.ContainsKey("currency") ? (string)post_var["currency"] : "";
            double rate = post_var.ContainsKey("rate") ? Convert.ToDouble((post_var["rate"])) : 0;
            int duration = post_var.ContainsKey("duration") ? Convert.ToInt32(post_var["duration"]) : 0;
            double debit = post_var.ContainsKey("debit") ? Convert.ToDouble(post_var["debit"]) : 0;

            string tag = post_var.ContainsKey("tag") ? (string)post_var["tag"] : "";

            return new IVRNotification(callState, (string)post_var["session"], txn_ref, dialStatus, digits, recordURL,
                                        transferStatus, from, to, dest, date, currency, rate, duration, debit, tag);
        }

        /// <summary>
        /// Parse the POST request that was sent by the Hoiio server to a remote notification URL after a call session is completed.
        /// </summary>
        public static CallTransaction parseCallNotify(string notifyStr) 
        {
            Dictionary<String, Object> post_var = postStringToDictionary(notifyStr);
            return new CallTransaction(post_var);
        }

        /// <summary>
        /// Parse the POST request that was sent by the Hoiio server to a remote notification URL after an SMS transaction is completed.
        /// </summary>
        public static SMSTransaction parseSMSNotify(string notifyStr)
        {
            Dictionary<String, Object> post_var = postStringToDictionary(notifyStr);
            return new SMSTransaction(post_var);
        }
        #endregion
    }
}

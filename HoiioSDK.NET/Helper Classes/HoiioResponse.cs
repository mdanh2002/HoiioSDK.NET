using System;
using System.Collections.Generic;

using System.Text;
using Newtonsoft.Json;

namespace HoiioSDK.NET
{
    /// <summary>
    /// The different status of a Hoiio request
    /// </summary>
    public enum HoiioResponseStatus
    {
        ERROR_CONCURRENT_CALL_LIMIT_REACHED,
        ERROR_INSUFFICIENT_CREDIT,
        ERROR_INTERNAL_SERVER_ERROR,
        ERROR_INVALID_ACCESS_TOKEN,
        ERROR_INVALID_APP_ID,
        ERROR_INVALID_HTTP_METHOD,
        ERROR_INVALID_NOTIFY_URL,
        ERROR_MALFORMED_PARAMS,
        ERROR_OTHER,
        ERROR_PARAM_INVALID,
        ERROR_PARAM_MISSING,
        ERROR_PARAM_NOT_SUPPORTED,
        ERROR_RATE_LIMIT_EXCEEDED,
        ERROR_TAG_INVALID_LENGTH,
        ERROR_UNABLE_TO_RESOLVE_NOTIFY_URL,
        SUCCESS_OK,

        /// <summary>
        /// The Response Status string could not be resolved into any predefined codes
        /// </summary>
        UNDEFINED
    }

    /// <summary>
    /// The response received after every Hoiio API call
    /// </summary>
    public class HoiioResponse
    {
        private const string API_OUT_STATUS = "status";
        private const string API_OUT_SUCCESS = "success_ok";

        protected Dictionary<string, object> _rawResponse;
        /// <summary>
        /// The original JSON response from the server
        /// </summary>
        public Dictionary<string, object> rawResponse
        {
            get
            {
                return _rawResponse;
            }
        }

        /// <summary>
        /// The formatted JSON raw response, for printing only
        /// </summary>
        public string rawResponseFormatted
        {
            get
            {
                return JsonConvert.SerializeObject(_rawResponse, Formatting.Indented);
            }
        }

        protected bool _success;
        /// <summary>
        /// Whether or not the response indicates a success
        /// </summary>
        public bool success
        {
            get
            {
                return _success;
            }
        }

        protected string _txnRef;
        /// <summary>
        /// Transaction Reference of this transaction.
        /// </summary>
        public string txnRef
        {
            get
            {
                return _txnRef;
            }
        }

        protected string _tag;
        /// <summary>
        /// Your own reference ID used when making the transaction.
        /// This will be empty if you didn't use the "tag" parameter earlier
        /// </summary>
        public string tag
        {
            get
            {
                return _tag;
            }
        }

        /// <summary>
        /// The string representing the status code of the request
        /// </summary>
        public string statusString
        {
            get
            {
                if (_rawResponse != null && _rawResponse.ContainsKey(API_OUT_STATUS))
                    return (string)_rawResponse[API_OUT_STATUS];
                return "";
            }
        }

        /// <summary>
        /// The enum representing the status of the request
        /// </summary>
        public HoiioResponseStatus statusCode
        {
            get
            {
                switch (statusString)
                {
                    case "success_ok":
                        return HoiioResponseStatus.SUCCESS_OK;
                    case "error_invalid_http_method":
                        return HoiioResponseStatus.ERROR_INVALID_HTTP_METHOD;
                    case "error_malformed_params":
                        return HoiioResponseStatus.ERROR_MALFORMED_PARAMS;
                    case "error_invalid_access_token":
                        return HoiioResponseStatus.ERROR_INVALID_ACCESS_TOKEN;
                    case "error_invalid_app_id":
                        return HoiioResponseStatus.ERROR_INVALID_APP_ID;
                    case "error_internal_server_error":
                        return HoiioResponseStatus.ERROR_INTERNAL_SERVER_ERROR;
                    case "error_insufficient_credit":
                        return HoiioResponseStatus.ERROR_INSUFFICIENT_CREDIT;
                    case "error_tag_invalid_length":
                        return HoiioResponseStatus.ERROR_TAG_INVALID_LENGTH;
                    case "error_rate_limit_exceeded":
                        return HoiioResponseStatus.ERROR_RATE_LIMIT_EXCEEDED;
                    case "error_invalid_notify_url":
                        return HoiioResponseStatus.ERROR_INVALID_NOTIFY_URL;
                    case "error_unable_to_resolve_notify_url":
                        return HoiioResponseStatus.ERROR_UNABLE_TO_RESOLVE_NOTIFY_URL;
                    case "error_concurrent_call_limit_reached":
                        return HoiioResponseStatus.ERROR_CONCURRENT_CALL_LIMIT_REACHED;
                    default:
                        {
                            if (statusString.ToUpper().Contains("PARAM_MISSING"))
                                return HoiioResponseStatus.ERROR_PARAM_MISSING;

                            if (statusString.ToUpper().Contains("INVALID"))
                                return HoiioResponseStatus.ERROR_PARAM_INVALID;

                            if (statusString.ToUpper().Contains("NOT_SUPPORTED"))
                                return HoiioResponseStatus.ERROR_PARAM_NOT_SUPPORTED;

                            if (statusString.ToUpper().StartsWith("ERROR"))
                                return HoiioResponseStatus.ERROR_OTHER;

                            return HoiioResponseStatus.UNDEFINED;
                        }
                }
            }
        }

        public HoiioResponse()
        {
        }

        /// <summary>
        /// Construct the Hoiio response object from a dictionary of name-value pairs
        /// </summary>
        /// <param name="res"></param>
        public HoiioResponse(Dictionary<string, object> res)
        {
            _rawResponse = res;
            _success = (this.statusString == API_OUT_SUCCESS || this.statusString == ""); //no status returned, assume success

            if (res.ContainsKey("txn_ref"))
                _txnRef = (string)res["txn_ref"];

            if (res.ContainsKey("tag"))
                _tag = (string)res["tag"];
        }
    }
}

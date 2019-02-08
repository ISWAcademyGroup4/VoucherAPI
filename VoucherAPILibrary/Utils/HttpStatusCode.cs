using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Utils
{
    public enum HttpStatusCode
    {
        [DefaultValue("OK")]
        [Description("Your request was successful")]
        OK = 200,

        [DefaultValue("Processed")]
        [Description("Your request was processed successfully")]
        Created = 201,

        [DefaultValue("Accepted")]
        [Description("Your request was accepted Successfully")]
        Accepted = 202,

        [DefaultValue("No Content")]
        [Description("No Content was received from the server")]
        No_Content = 204,

        [DefaultValue("Bad Request")]
        [Description("The request was invalid. It may occur for various reasons - a malformed JSON or a violated rule (e.g. an attempt to redeem an expired voucher).")]
        Bad_Request = 400,

        [DefaultValue("Unauthorized")]
        [Description("Authentication has failed or has not been provided yet.")]
        Unauthorized = 401,

        [DefaultValue("Payment Required")]
        [Description("The request exceeded your current pricing plan (we send friendly reminders first).")]
        Payment_Required = 402,

        [DefaultValue("Not Found")]
        [Description("The requested resource could not be found.")]
        Not_Found = 404,

        [DefaultValue("Method Not Allowed")]
        [Description("The request used a method (GET, POST, etc.) that is not available for given resource. Error details include a hint on which methods are allowed.")]
        Method_Not_Allowed = 405,

        [DefaultValue("Not Acceptable")]
        [Description("The API is unable to produce a response in a format specified by the Accept header. In most cases the only available response format is application/json.")]
        Not_Acceptable = 406,

        [DefaultValue("Request Timeout")]
        [Description("The Request has timed out")]
        Request_Timeout = 408,

        [DefaultValue("Request Entity too large")]
        [Description("The request was invalid. It may occur for various reasons - a malformed JSON or a violated rule (e.g. an attempt to redeem an expired voucher).")]
        Request_Entity_too_Large = 413,

        [DefaultValue("Unsupported Media Type")]
        [Description("The API is unable to consume a request in a format specified by the Content-Type header.")]
        Unsupported_Media_Type = 415,

        [DefaultValue("Too many Requests")]
        [Description("You have sent too many requests")]
        Too_many_Requests = 429,

        [DefaultValue("Something went wrong")]
        [Description("An internal API error occurred. Don't worry, we track and verify all such errors and try to react as asap as possible.")]
        Internal_Server_Error = 500,

        
    }
}

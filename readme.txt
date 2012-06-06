
HoiioSDK.NET 
----------------------
HoiioSDK.NET is a .NET library for Hoiio's Developer API (http://developer.hoiio.com/)
It encapsulates the HTTP communications and let developers use the API via a few simple classes.

Currently, HoiioSDK.NET supports the Call, SMS, Account and IVR API. 


Requirements
----------------------
- .NET Framework Version 2.0 or later
- Visual Studio 2005 or later to use the library. 
- Visual Studio 2010 or later to compile the library source code.

HoiioSDK.NET library uses the open-source NewtonSoft JSON.NET library. which is also included with this download. 
Latest binary and source code is available at http://json.codeplex.com/


Installation
----------------------
Just download the HoiioSDK.NET libray and add a reference to the DLL into your project. 
Also include the library namespace in your source code:

using HoiioSDK.NET; (C#)
Imports HoiioSDK.NET (VB.NET)


Usage
----------------------
You will be able to access all of Hoiio's API via the HoiioService wrapper class. 
First initialize the class with your Hoiio API access tokane and application ID

HoiioService service = new HoiioService(AppID, AccessToken);

Then call the method of your choice:

// Making Call
HoiioResponse res = service.callMakeCall(CallFrom, CallTo, CallerID, null, null);

// Sending SMS            
HoiioResponse res = service.smsSend(SMSTo, SenderID, SMSText, null, null);

// Starting an IVR session
IVRTransaction res = service.ivrDial(IVRMsg, IVRNumber, null, null, null);

// Checking account balance
AccountBalance accBalance = service.accountGetBalance();

It is recommended that you catch and handle all exceptions after making any API call.

To access the raw API methods, you may use the IVRService, CallService, SMSService, or AccountService classes. 
However, this is not needed in most cases.

Some Hoiio APIs (IVR, Call, SMS) also accepts a notify URL, 
which will be called via POST with the transaction details after a request is completed.
To parse the POST string into a Hoiio API object, use the following methods:

HoiioService.parseCallNotify()
HoiioService.parseSMSNotify()
HoiioService.parseIVRNotify()

A CallTransaction, SMSTransaction or IVRNotification object will be returned respectively for each of the above methods.

Refer to the demo app or our online API documentation (http://developer.hoiio.com/docs/) for more information.


License
----------------------
This project is under MIT License (http://en.wikipedia.org/wiki/MIT_License).
See LICENSE.TXT file for details.


Contacts
----------------------
If you have any questions, please feel free to contact us:

Twitter:        @hoiiotweets
Google Groups:  https://groups.google.com/forum/#!forum/hoiio-developers
Facebook:       http://www.facebook.com/Hoiio
Blog:           http://devblog.hoiio.com/


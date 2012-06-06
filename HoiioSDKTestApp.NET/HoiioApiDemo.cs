using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json;
using System.IO;
using System.Web;
using HoiioSDK.NET;

namespace HoiioSDKTestApp.NET
{
    /// <summary>
    /// Hoiio API (Call, SMS, IVR, Account) Demo App
    /// </summary>
    public partial class HoiioApiDemo : Form
    {
        // server URL to be notified when API call is done
        private string NotifyURL = "";

        public HoiioApiDemo()
        {
            InitializeComponent();

            this.txtHistoryFrom.Value = DateTime.Now.AddDays(-30);
            this.txtHistoryTo.Value = DateTime.Now;

            this.txtSMSHistoryFrom.Value = DateTime.Now.AddDays(-30);
            this.txtSMSHistoryTo.Value = DateTime.Now;

            NotifyURL = this.txtCallbackURL.Text;
        }

        #region Account API
        private void btnCheckBalance_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
            AccountBalance accBalance = service.accountGetBalance();
            Cursor.Current = Cursors.Default;

            if (accBalance.success)
            {
                string output = String.Format("currency: {0}{4}points: {1}{4}bonus: {2}{4}balance: {3}{4}",
                    accBalance.currency, accBalance.points, accBalance.bonus, accBalance.balance, Environment.NewLine);

                MessageBox.Show(output, "Account Balance");
            }
            else
            {
                MessageBox.Show(accBalance.statusString, "Failed");
            }
        }

        private void btnGetAccountInfo_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
            AccountInfo accInfo = service.accountGetInfo();
            Cursor.Current = Cursors.Default;

            if (accInfo.success)
            {
                string output = String.Format("country: {0}{7}currency: {1}{7}email: {2}{7}mobile_number: {3}{7}name: {4}{7}prefix: {5}{7}uid: {6}{7}",
                                                accInfo.country, accInfo.currency, accInfo.email, accInfo.mobile_number, accInfo.name, accInfo.prefix, accInfo.uid, Environment.NewLine);

                MessageBox.Show(output, "Account Info");
            }
            else
            {
                MessageBox.Show(accInfo.statusString, "Failed");
            }
        }
        #endregion

        #region Call API
        private void btnCall_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
            HoiioResponse res = service.callMakeCall(this.txtCallFrom.Text, this.txtCallTo.Text, this.txtCallerID.Text, null, NotifyURL);

            if (res.success)
            {
                this.txtCheckTxnStatus.Text = res.txnRef;
                MessageBox.Show(res.txnRef, "Call Txn Ref");
            }
            else
            {
                MessageBox.Show( String.Format("{0} ({1})", res.statusCode, res.statusString), "Call Failed");
            }

            // reset date range so that history always include the latest calls
            this.txtHistoryTo.Value = DateTime.Now;

            Cursor.Current = Cursors.Default;
        }

        private void btnCheckRate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
            CallRate callRate = service.callGetRate(this.txtCallFrom.Text, this.txtCallTo.Text);
            Cursor.Current = Cursors.Default;

            if (callRate.success)
            {
                string rateStr = String.Format("{0} {1}/min. Talktime: {2} sec", callRate.rate, callRate.currency, callRate.talkTime);
                MessageBox.Show(rateStr, "Call Rate");
            }
            else
            {
                MessageBox.Show(callRate.statusString, "Failed");
            }
        }

        private void btnCheckCallStatus_Click(object sender, EventArgs e)
        {
            if (this.txtCheckTxnStatus.Text.Length == 0)
            {
                MessageBox.Show("Please enter a txn ref");
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
                CallTransaction tx = service.callQueryStatus(this.txtCheckTxnStatus.Text);
                Cursor.Current = Cursors.Default;

                if (tx.success)
                {
                    string callStatusStr=  String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", tx.date, tx.from, tx.to, tx.fromStatus, tx.toStatus, tx.duration, tx.rate, tx.debit, tx.currency, tx.txnRef);

                    MessageBox.Show(callStatusStr, "Call Status");
                }
                else
                {
                    MessageBox.Show(tx.statusString, "Failed");
                }
            }
        }

        private void btnHangupCall_Click(object sender, EventArgs e)
        {
            if (this.txtCheckTxnStatus.Text.Length == 0)
            {
                MessageBox.Show("Please enter a txn ref");
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
                HoiioResponse res = service.callHangUp(this.txtCheckTxnStatus.Text);
                Cursor.Current = Cursors.Default;

                if (res.success)
                {
                    MessageBox.Show("Call hangup OK");
                }
                else
                {
                    MessageBox.Show(res.statusString, "Call hangup Failed");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.txtHistoryPage.Text.Length == 0)
            {
                MessageBox.Show("Please enter a page number");
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
                TransactionHistory txHistory = service.callGetHistory(this.txtHistoryFrom.Value, this.txtHistoryTo.Value, Convert.ToInt32(this.txtHistoryPage.Text));
                Cursor.Current = Cursors.Default;

                if (txHistory.rawResponse.success)
                {
                    string info = "date from to fromStatus toStatus duration rate debit currency txnRef";
                    foreach (CallTransaction tx in txHistory.transactions)
                    {
                        info += Environment.NewLine;
                        info += String.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", tx.date, tx.from, tx.to, tx.fromStatus, tx.toStatus, tx.duration, tx.rate, tx.debit, tx.currency, tx.txnRef);
                    }

                    // history may be too big, write to a text file and open
                    string filename = "history.txt";
                    File.WriteAllText(filename, info);
                    Process.Start(filename);
                }
                else
                {
                    MessageBox.Show(txHistory.rawResponse.statusString, "Failed");
                }
            }
        }

        private void btnConference_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
            ConferenceTransaction confTx = service.callCreateConference(this.txtConferenceDest.Text, null, this.txtConferenceCallerID.Text, null, NotifyURL);
            Cursor.Current = Cursors.Default;

            if (confTx.success)
            {
                string txnRefs = "";
                foreach (string txnref in confTx.txnRefs)
                {
                    txnRefs += txnref + Environment.NewLine;
                }

                MessageBox.Show(txnRefs, "Confernece Txn Ref");
            }
            else
            {
                MessageBox.Show(confTx.statusString, "Confernece Failed");
            }

            // reset date range so that history always include the latest calls
            this.txtHistoryTo.Value = DateTime.Now;

            //showAPICallResult(res, "Conference Result");
        }
        #endregion

        #region SMS API
        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
            HoiioResponse res = service.smsSend(this.txtSMSTo.Text, this.txtSenderID.Text, this.txtSMSText.Text, null, NotifyURL);
            Cursor.Current = Cursors.Default;

            if (res.success)
            {
                this.txtSMSTxnRef.Text = res.txnRef;
                MessageBox.Show(res.txnRef, "SMS Txn Ref");
            }
            else
            {
                MessageBox.Show(res.statusString, "SMS Failed");
            }

            // reset date range so that history always include the latest SMS
            this.txtSMSHistoryTo.Value = DateTime.Now;
        }

        private void btnCheckSMSRate_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
            SMSRate smsRate = service.smsGetRate(this.txtSMSTo.Text, this.txtSMSText.Text);
            Cursor.Current = Cursors.Default;

            if (smsRate.success)
            {
                string rateStr = String.Format("Per SMS: {0} {1}\nTotal SMS Required: {2}\nTotal Cost: {3}", smsRate.rate, smsRate.currency, smsRate.splitCount, smsRate.totalCost);
                MessageBox.Show(rateStr, "SMS Rate");
            }
            else
            {
                MessageBox.Show(smsRate.statusString, "Failed");
            }
        }

        private void btnGetSMSStatus_Click(object sender, EventArgs e)
        {
            if (this.txtSMSTxnRef.Text.Length == 0)
            {
                MessageBox.Show("Please enter a txn ref");
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
                SMSTransaction tx = service.smsQueryStatus(this.txtSMSTxnRef.Text);
                Cursor.Current = Cursors.Default;

                if (tx.success)
                {
                    string callStatusStr = String.Format("{0} {1} {2} {3} {4} {5} {6}", tx.date, tx.dest, tx.smsStatus, tx.rate, tx.debit, tx.currency, tx.txnRef);

                    MessageBox.Show(callStatusStr, "SMS Status");
                }
                else
                {
                    MessageBox.Show(tx.statusString, "Failed");
                }
            }
        }

        private void btnSmsGetHistory_Click(object sender, EventArgs e)
        {
            if (this.txtSMSHistoryPage.Text.Length == 0)
            {
                MessageBox.Show("Please enter a page number");
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
                TransactionHistory txHistory = service.smsGetHistory(this.txtSMSHistoryFrom.Value, this.txtSMSHistoryTo.Value, Convert.ToInt32(this.txtSMSHistoryPage.Text));
                Cursor.Current = Cursors.Default;

                if (txHistory.rawResponse.success)
                {
                    string info = "date dest smsStatus rate debit currency txnRef";
                    foreach (SMSTransaction tx in txHistory.transactions)
                    {
                        info += Environment.NewLine;
                        info += String.Format("{0} {1} {2} {3} {4} {5} {6}", tx.date, tx.dest, tx.smsStatus, tx.rate, tx.debit, tx.currency, tx.txnRef);
                    }

                    // history may be too big, write to a text file and open
                    string filename = "history.txt";
                    File.WriteAllText(filename, info);
                    Process.Start(filename);
                }
                else
                {
                    MessageBox.Show(txHistory.rawResponse.statusString, "Failed");
                }
            }
        }
        #endregion

        #region IVR API
        private void btnStartIVR_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
            IVRTransaction res = service.ivrDial(this.txtIVRFirstMsg.Text, this.txtIVRNumber.Text, null, null, NotifyURL);
            Cursor.Current = Cursors.Default;

            if (res.success)
            {
                this.txtIVRTxnRef.Text = res.session;

                MessageBox.Show(String.Format("txnRef={0} session={1}", res.txnRef, res.session), "IVR Created");
            }
            else
            {
                MessageBox.Show(res.statusString, "Failed");
            }
        }

        private void btnPlayIVRMsg_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
            HoiioResponse res = service.ivrPlay(this.txtIVRTxnRef.Text, this.txtIVRMessage.Text, null, NotifyURL);
            Cursor.Current = Cursors.Default;

            if (res.success)
            {
                MessageBox.Show("OK", "IVR Play Result");
            }
            else
            {
                MessageBox.Show(res.statusString, "Failed");
            }
        }

        private void btnIVRGather_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
            HoiioResponse res = service.ivrGather(this.txtIVRTxnRef.Text, this.txtIVRGatherMsg.Text, -1, -1, -1, null, NotifyURL);
            Cursor.Current = Cursors.Default;

            if (res.success)
            {
                MessageBox.Show("OK", "IVR Gather Result");
            }
            else
            {
                MessageBox.Show(res.statusString, "Failed");
            }
        }

        private void btnIVRRecord_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
            HoiioResponse res = service.ivrRecord(this.txtIVRTxnRef.Text, this.txtIVRRecordPromptMsg.Text, null, null, NotifyURL);
            Cursor.Current = Cursors.Default;

            if (res.success)
            {
                MessageBox.Show("OK", "IVR Record Result");
            }
            else
            {
                MessageBox.Show(res.statusString, "Failed");
            }
        }

        private void btnIVRTransfer_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
            HoiioResponse res = service.ivrTransfer(this.txtIVRTxnRef.Text, this.txtIVRTransferMsg.Text, this.txtIVRTransferDest.Text, null, null, NotifyURL);
            Cursor.Current = Cursors.Default;

            if (res.success)
            {
                MessageBox.Show("OK", "IVR Transfer Result");
            }
            else
            {
                MessageBox.Show(res.statusString, "Failed");
            }
        }

        private void btnHangupIVR_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            HoiioService service = new HoiioService(this.txtAppID.Text, this.txtAccessToken.Text);
            HoiioResponse res = service.ivrHangup(this.txtIVRTxnRef.Text, this.txtIVRHangupMsg.Text, null, NotifyURL);
            Cursor.Current = Cursors.Default;

            if (res.success)
            {
                MessageBox.Show("OK", "IVR Hangup Result");
            }
            else
            {
                MessageBox.Show(res.statusString, "Failed");
            }
        }
        #endregion

        #region Parser API
        private void btnParseIVRNotify_Click(object sender, EventArgs e)
        {
            IVRNotification ivrNotify =  HoiioService.parseIVRNotify(this.txtIVRNotifyPost.Text);

            string output = String.Format(@"callState: {0}{16}currency: {1}{16}date: {2}{16}debit: {3}{16}dest: {4}{16}dialStatus: {5}{16}digits: {6}{16}duration: {7}{16}from: {8}{16}rate: {9}{16}recordURL: {10}{16}session: {11}{16}tag: {12}{16}to: {13}{16}transferStatus: {14}{16}txnRef: {15}{16}", 
                                           ivrNotify.callState, ivrNotify.currency, ivrNotify.date, ivrNotify.debit, ivrNotify.dest, 
                                           ivrNotify.dialStatus, ivrNotify.digits, ivrNotify.duration, 
                                           ivrNotify.from, ivrNotify.rate, ivrNotify.recordURL, ivrNotify.session, ivrNotify.tag, 
                                           ivrNotify.to, ivrNotify.transferStatus, ivrNotify.txnRef, Environment.NewLine);

            MessageBox.Show(output, "Result");

        }

        private void btnParseCallNotify_Click(object sender, EventArgs e)
        {
            CallTransaction callTx  = HoiioService.parseCallNotify(this.txtCallNotifyPost.Text);

            string output = String.Format(@"from: {0}{12}to: {1}{12}date: {2}{12}fromStatus: {3}{12}toStatus: {4}{12}currency: {5}{12}date: {6}{12}debit: {7}{12}duration: {8}{12}rate: {9}{12}tag: {10}{12}txnRef: {11}{12}",
                               callTx.from, callTx.to, callTx.date, callTx.fromStatus, callTx.toStatus, callTx.currency, callTx.date, callTx.debit, 
                               callTx.duration,callTx.rate, callTx.tag, callTx.txnRef, Environment.NewLine);

            MessageBox.Show(output, "Result");
        }

        private void btnParseSMSNotify_Click(object sender, EventArgs e)
        {
            SMSTransaction smsTx = HoiioService.parseSMSNotify(this.txtSMSNotifyPost.Text);
            string output = String.Format("currency: {0}{9}date: {1}{9}debit: {2}{9}dest: {3}{9}rate: {4}{9}smsStatus: {5}{9}splitCount: {6}{9}tag: {7}{9}txnRef: {8}{9}", 
                                            smsTx.currency, smsTx.date, smsTx.debit, smsTx.dest, smsTx.rate, smsTx.smsStatus, smsTx.splitCount, smsTx.tag, smsTx.txnRef, Environment.NewLine);
            MessageBox.Show(output, "Result");
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Management;
using System.Data.OleDb;
using System.Drawing;
using System.Data.SqlClient;
using System.Data.Sql;

namespace FPS
{
    public partial class Transactions_View : Form
    {
        public Transactions_View()
        {
            InitializeComponent();

        }


        public static List<DB.TransStruct> lCompletedTrans = new List<DB.TransStruct>();


        string date_formate = "";

        string lblMonth = "";
        string[] month_arr = new string[] { "Jan", "Feb", "March", "Apirl", "May", "June", "July", "Aug", "Sep", "Oct", "Nov", "Dec" };
        int m_inc_dec = 0;
        private void previous_month_Click(object sender, EventArgs e)
        {
            lblMonth = month_year_lbl.Text.ToString().Split(',')[0];

            int m = VerifyMonth(lblMonth);
            m_inc_dec = m - 1;
           
            m_inc_dec--;

            if (m_inc_dec <= 0)
            {
                ButtonVisibility(previous_month, false);
            }

            if (m_inc_dec > 1)
            {
                ButtonVisibility(next_month, true);
            }

            month_year_lbl.Text = month_arr[m_inc_dec].ToString() + ",2018";



            /*if (m_inc_dec + 1 <= 9)
            {

                date_formate = day_lbl.Text.ToString() + "-0" + (m_inc_dec + 1) + "-2018";
            }
            else
            {
                date_formate = day_lbl.Text.ToString() + "-" + (m_inc_dec + 1) + "-2018";
            }*/


            //GetChooseTransations(date_formate);
            // MessageBox.Show(date_formate + "");

        }

        private void next_month_Click(object sender, EventArgs e)
        {
            lblMonth = month_year_lbl.Text.ToString().Split(',')[0];

            int m = VerifyMonth(lblMonth);
            m_inc_dec = m - 1;
            m_inc_dec++;

            if (m_inc_dec == 11)
            {
                ButtonVisibility(next_month, false);
            }
            else
            {
                ButtonVisibility(next_month, true);
            }

            if (m_inc_dec >= 1)
            {

                ButtonVisibility(previous_month, true);
            }

            month_year_lbl.Text = month_arr[m_inc_dec].ToString() + ",2018";

            /*if (m_inc_dec + 1 <= 9)
            {

                date_formate = day_lbl.Text.ToString() + "-0" + (m_inc_dec + 1) + "-2018";
            }
            else {
                date_formate = day_lbl.Text.ToString() + "-" + (m_inc_dec + 1) + "-2018";
            }*/





            // GetChooseTransations(date_formate);




            //MessageBox.Show(date_formate + "");
        }


        int day_inc_dec;
        private void previous_day_Click(object sender, EventArgs e)
        {
            day_inc_dec = Convert.ToInt32(day_lbl.Text.ToString());
            day_inc_dec--;
            if (day_inc_dec <= 9)
            {
                day_lbl.Text = "0" + day_inc_dec.ToString();
            }
            else
            {
                day_lbl.Text = day_inc_dec.ToString();
            }

            if (day_inc_dec <= 1)
            {

                ButtonVisibility(previous_day, false);
            }

            if (day_inc_dec <= 30)
            {
                ButtonVisibility(next_day, true);
            }


            lblMonth = month_year_lbl.Text.ToString().Split(',')[0];

            int m = VerifyMonth(lblMonth);

            if (m <= 9)
            {

                date_formate = day_lbl.Text.ToString() + "-0" + (m) + "-2018";
            }
            else
            {
                date_formate = day_lbl.Text.ToString() + "-" + (m) + "-2018";
            }

            GetChooseTransations(date_formate);

            // MessageBox.Show(date_formate + "");
        }

        private void next_day_Click(object sender, EventArgs e)
        {
            day_inc_dec = Convert.ToInt32(day_lbl.Text.ToString());

            day_inc_dec++;

            if (day_inc_dec <= 9)
            {
                day_lbl.Text = "0" + day_inc_dec.ToString();
            }
            else
            {
                day_lbl.Text = day_inc_dec.ToString();
            }


            if (day_inc_dec >= 31)
            {

                ButtonVisibility(next_day, false);
            }

            if (day_inc_dec > 1)
            {
                ButtonVisibility(previous_day, true);
            }


            lblMonth = month_year_lbl.Text.ToString().Split(',')[0];

            int m = VerifyMonth(lblMonth);

            if (m <= 9)
            {

                date_formate = day_lbl.Text.ToString() + "-0" + (m) + "-2018";
            }
            else
            {
                date_formate = day_lbl.Text.ToString() + "-" + (m) + "-2018";
            }

            GetChooseTransations(date_formate);
            //MessageBox.Show(date_formate + "");

        }

        int iPage = 1;
        private void previous_btn_Click(object sender, EventArgs e)
        {

            ClearSelection();
            ClearTransactionsDetails();
            int iButtonIndex;
            int iTranIndex;

            iPage--;

            iButtonIndex = 0;
            for (iTranIndex = (6 * (iPage - 1)); iTranIndex < (6 * iPage); iTranIndex++)
            {
                if (iTranIndex < lCompletedTrans.Count)
                {
                    iButtonIndex++;
                    Update_Transactions_ButtonText(iButtonIndex, "PUMP: " + lCompletedTrans[iTranIndex].sPump + " @ " + lCompletedTrans[iTranIndex].sShowTime + "\nPAID: $" + lCompletedTrans[iTranIndex].sDeposit + "  CHANGE: $" + lCompletedTrans[iTranIndex].sChange);
                }
            }

            if (lCompletedTrans.Count > 6 * iPage)
            {
                ButtonVisibility(next_btn, true);
            }

            if (iPage == 1)
            {
                ButtonVisibility(previous_btn, false);
            }
        }

        private void next_btn_Click(object sender, EventArgs e)
        {

            ClearButtonTexts();
            ClearSelection();
            ClearTransactionsDetails();


            int iButtonIndex;
            int iTranIndex;

            iPage++;

            iButtonIndex = 0;
            for (iTranIndex = (6 * (iPage - 1)); iTranIndex < (6 * iPage); iTranIndex++)
            {
                if (iTranIndex < lCompletedTrans.Count)
                {
                    iButtonIndex++;

                    Update_Transactions_ButtonText(iButtonIndex, "PUMP: " + lCompletedTrans[iTranIndex].sPump + " @ " + lCompletedTrans[iTranIndex].sShowTime + "\nPAID: $" + lCompletedTrans[iTranIndex].sDeposit + "  CHANGE: $" + lCompletedTrans[iTranIndex].sChange);
                }
            }

            if (lCompletedTrans.Count <= 6 * iPage)
            {

                ButtonVisibility(next_btn, false);
            }

            if (iPage == 2)
            {
                ButtonVisibility(previous_btn, true);
            }







        }

        private void print_transaction_Click(object sender, EventArgs e)
        {

        }

        private void go_back_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void One_Click(object sender, EventArgs e)
        {
            if (One.Text.Trim() != "")
            {

                SetButton(1);
                SetTransactionsDetails(1);
            }
        }

        private void Two_Click(object sender, EventArgs e)
        {
            if (Two.Text.Trim() != "")
            {

                SetButton(2);
                SetTransactionsDetails(2);
            }
        }

        private void Three_Click(object sender, EventArgs e)
        {
            if (Three.Text.Trim() != "")
            {

                SetButton(3);
                SetTransactionsDetails(3);
            }
        }

        private void Four_Click(object sender, EventArgs e)
        {
            if (Four.Text.Trim() != "")
            {

                SetButton(4);
                SetTransactionsDetails(4);
            }
        }

        private void Five_Click(object sender, EventArgs e)
        {
            if (Five.Text.Trim() != "")
            {

                SetButton(5);
                SetTransactionsDetails(5);
            }
        }

        private void Six_Click(object sender, EventArgs e)
        {
            if (Six.Text.Trim() != "")
            {

                SetButton(6);
                SetTransactionsDetails(6);
            }
        }

        public void SetButton(int index)
        {

            Button[] btnarr = new Button[] { One, Two, Three, Four, Five, Six };

            foreach (Button btn in btnarr)
            {
                btn.BackColor = Color.White;

            }

            if (index == 1)
            {

                Transactions_View.SetButtonColor(One, Color.Yellow);
            }

            if (index == 2)
            {

                Transactions_View.SetButtonColor(Two, Color.Yellow);
            }

            if (index == 3)
            {

                Transactions_View.SetButtonColor(Three, Color.Yellow);
            }

            if (index == 4)
            {

                Transactions_View.SetButtonColor(Four, Color.Yellow);
            }

            if (index == 5)
            {

                Transactions_View.SetButtonColor(Five, Color.Yellow);
            }

            if (index == 6)
            {

                Transactions_View.SetButtonColor(Six, Color.Yellow);
            }

        }

        public static void SetButtonColor(Button btn, Color color)
        {
            btn.BackColor = color;
            btn.FlatAppearance.MouseOverBackColor = color;
        }





        public static void SetButtonText(Button btn, string lbl)
        {

            btn.Text = lbl;
        }

        private void Transactions_View_Load(object sender, EventArgs e)
        {
            previous_btn.Visible = false;
            UpdateCompletedTransView(DateTime.Now.ToString("dd-MM-yyyy"));


            if (month_year_lbl.Text.ToString().Split(',')[0] == "Jan")
            {

                ButtonVisibility(previous_month, false);
            }

            if (day_lbl.Text.ToString() == "01")
            {

                ButtonVisibility(previous_day, false);
            }


            string day = DateTime.Now.ToString("dd");
            string month = DateTime.Now.ToString("MM");
            string year = DateTime.Now.ToString("yyyy");

            month = getMonth(month);

            month_year_lbl.Text = month + "," + year;
            day_lbl.Text = day;





            /* if (lCompletedTrans.Count <= 6 * iPage)
             {

                 ButtonVisibility(next_btn, false);
             }*/


        }

        public void Update_Transactions_ButtonText(int index, string lbl)
        {



            if (index == 1)
            {

                Transactions_View.SetButtonText(One, lbl);
            }

            if (index == 2)
            {
                Transactions_View.SetButtonText(Two, lbl);
            }

            if (index == 3)
            {
                Transactions_View.SetButtonText(Three, lbl);
            }

            if (index == 4)
            {
                Transactions_View.SetButtonText(Four, lbl);
            }

            if (index == 5)
            {
                Transactions_View.SetButtonText(Five, lbl);
            }

            if (index == 6)
            {
                Transactions_View.SetButtonText(Six, lbl);
            }
        }


        int iCount;

        public void UpdateCompletedTransView(string datetimeformate)
        {
            int iIndex;
            string sQuery;
            OleDbCommand dbCmd;
            OleDbDataReader drRecordSet;
            /* SqlCommand dbCmd;
             SqlDataReader drRecordSet;*/
            DB.TransStruct myTransStruct;





            Debug.WriteLine("UPDATE COMPLETE TRANSACTIONS VIEW");

            SQL_SERVER.Set_Sql_Server_Conn();
            SQL_SERVER.Open_Sql_Server_Conn();


            //sQuery = "SELECT COMPLETED_TIME, PIC, PUMP, DEPOSIT, PURCHASE, PRICE, CHANGE, GRADE, VOLUME, SHOW_TIME, TRAN_ID FROM TRANSACTIONS ORDER BY COMPLETED_TIME DESC";

            sQuery = "SELECT COMPLETED_TIME, PIC, PUMP, DEPOSIT, PURCHASE, PRICE, GRADE, VOLUME, SHOW_TIME, TRAN_ID,CHANGE FROM TRANSACTIONS WHERE SHOW_TIME LIKE '%" + datetimeformate + "%' ORDER BY COMPLETED_TIME DESC;";
            dbCmd = SQL_SERVER.Set_Sql_Server_Cmd(sQuery);

            drRecordSet = dbCmd.ExecuteReader();

            Debug.WriteLine(sQuery);
            Debug.WriteLine(drRecordSet.HasRows);

            iCount = 0;
            lCompletedTrans.Clear();
            while (drRecordSet.Read())
            {
                myTransStruct.sPIC = drRecordSet["PIC"].ToString();
                myTransStruct.sPump = drRecordSet["PUMP"].ToString();
                myTransStruct.sDeposit = drRecordSet["DEPOSIT"].ToString();
                myTransStruct.sPurchase = drRecordSet["PURCHASE"].ToString();
                myTransStruct.sPrice = drRecordSet["PRICE"].ToString();
                myTransStruct.sChange = drRecordSet["CHANGE"].ToString();
                myTransStruct.sGrade = drRecordSet["GRADE"].ToString();
                myTransStruct.sVolume = drRecordSet["VOLUME"].ToString();
                myTransStruct.sShowTime = drRecordSet["SHOW_TIME"].ToString();
                myTransStruct.sTranId = drRecordSet["TRAN_ID"].ToString();

                lCompletedTrans.Add(myTransStruct);
                iCount++;
            }

            for (iIndex = 0; iIndex < 6; iIndex++)
            {
                if (iIndex < iCount)
                {
                    Update_Transactions_ButtonText(iIndex + 1, "PUMP: " + lCompletedTrans[iIndex].sPump + " @ " + lCompletedTrans[iIndex].sShowTime + "PAID: $" + lCompletedTrans[iIndex].sDeposit + "  CHANGE: $" + lCompletedTrans[iIndex].sChange);
                }
            }
            dbCmd.Dispose();
            drRecordSet.Dispose();
            SQL_SERVER.Close_Sql_Sever_Conn();
        }


        private void SetTransactionsDetails(int index)
        {
            pump_no.Text = lCompletedTrans[index - 1].sPump;
            deposit.Text = lCompletedTrans[index - 1].sDeposit;
            change.Text = lCompletedTrans[index - 1].sChange;
            total.Text = lCompletedTrans[index - 1].sPurchase;
            date_time.Text = lCompletedTrans[index - 1].sShowTime;
            gal.Text = lCompletedTrans[index - 1].sVolume;
        }



        private void ClearButtonTexts()
        {
            Button[] btnarr = new Button[] { One, Two, Three, Four, Five, Six };

            foreach (Button btn in btnarr)
            {
                btn.Text = "";
                btn.BackColor = Color.White;
                btn.FlatAppearance.MouseOverBackColor = Color.White;
            }
        }

        private void ClearSelection()
        {

            Button[] btnarr = new Button[] { One, Two, Three, Four, Five, Six };

            foreach (Button btn in btnarr)
            {

                btn.BackColor = Color.White;
                btn.FlatAppearance.MouseOverBackColor = Color.White;
            }
        }

        private void ClearTransactionsDetails()
        {

            pump_no.Text = "";
            deposit.Text = "";
            change.Text = "";
            total.Text = "";
            date_time.Text = "";
            gal.Text = "";
        }



        private void ButtonVisibility(Button btn, bool visiblity)
        {

            btn.Visible = visiblity;
        }

        int monthNum;
        private int VerifyMonth(string month)
        {

            if (month == "Jan")
            {

                //ButtonVisibility(previous_month, false);
                monthNum = 01;

            }

            /* if (month != "Jan") {
                 ButtonVisibility(previous_month, true);
             }*/

            if (month == "Feb")
            {
                monthNum = 02;
            }

            if (month == "March")
            {
                monthNum = 03;
            }

            if (month == "Apirl")
            {
                monthNum = 04;
            }

            if (month == "May")
            {
                monthNum = 05;
            }

            if (month == "June")
            {
                monthNum = 06;
            }

            if (month == "July")
            {
                monthNum = 07;
            }

            if (month == "Aug")
            {
                monthNum = 08;
            }

            if (month == "Sep")
            {
                monthNum = 09;
            }

            if (month == "Oct")
            {
                monthNum = 10;
            }

            if (month == "Nov")
            {
                monthNum = 11;
            }

            if (month == "Dec")
            {
                monthNum = 12;

                //ButtonVisibility(next_month, false);
            }

            /* if (month != "Dec")
             {
                 ButtonVisibility(next_month, true);
             }*/
            return monthNum;
        }

        private string getMonth(string month)
        {
            string ret_month = "";
            if (month == "01")
            {
                ret_month = "Jan";
            }

            if (month == "01")
            {
                ret_month = "Jan";
            }

            if (month == "02")
            {
                ret_month = "Feb";
            }

            if (month == "03")
            {
                ret_month = "March";
            }

            if (month == "04")
            {
                ret_month = "Apirl";

            }

            if (month == "05")
            {
                ret_month = "May";
            }

            if (month == "06")
            {
                ret_month = "June";
            }

            if (month == "07")
            {
                ret_month = "July";
            }

            if (month == "08")
            {
                ret_month = "Aug";
            }

            if (month == "09")
            {
                ret_month = "Sep";
            }

            if (month == "10")
            {
                ret_month = "Oct";
            }

            if (month == "11")
            {
                ret_month = "Nov";
            }

            if (month == "12")
            {
                ret_month = "Dec";
            }



            return ret_month;
        }


        private void GetChooseTransations(string datetimeformate)
        {

            int iIndex;
            string sQuery;
            OleDbCommand dbCmd;
            OleDbDataReader drRecordSet;
            /* SqlCommand dbCmd;
             SqlDataReader drRecordSet;*/
            DB.TransStruct myTransStruct;





            Debug.WriteLine("UPDATE COMPLETE TRANSACTIONS VIEW");

            SQL_SERVER.Set_Sql_Server_Conn();
            SQL_SERVER.Open_Sql_Server_Conn();


            //sQuery = "SELECT COMPLETED_TIME, PIC, PUMP, DEPOSIT, PURCHASE, PRICE, CHANGE, GRADE, VOLUME, SHOW_TIME, TRAN_ID FROM TRANSACTIONS ORDER BY COMPLETED_TIME DESC";

            sQuery = "SELECT COMPLETED_TIME, PIC, PUMP, DEPOSIT, PURCHASE, PRICE, GRADE, VOLUME, SHOW_TIME, TRAN_ID,CHANGE FROM TRANSACTIONS WHERE SHOW_TIME LIKE '%" + datetimeformate + "%' ORDER BY COMPLETED_TIME DESC;";
            dbCmd = SQL_SERVER.Set_Sql_Server_Cmd(sQuery);

            drRecordSet = dbCmd.ExecuteReader();

            Debug.WriteLine(sQuery);
            Debug.WriteLine(drRecordSet.HasRows);


            if (drRecordSet.HasRows)
            {
                iCount = 0;
                lCompletedTrans.Clear();
                ClearButtonTexts();
                ClearSelection();
                ClearTransactionsDetails();

                while (drRecordSet.Read())
                {
                    myTransStruct.sPIC = drRecordSet["PIC"].ToString();
                    myTransStruct.sPump = drRecordSet["PUMP"].ToString();
                    myTransStruct.sDeposit = drRecordSet["DEPOSIT"].ToString();
                    myTransStruct.sPurchase = drRecordSet["PURCHASE"].ToString();
                    myTransStruct.sPrice = drRecordSet["PRICE"].ToString();
                    myTransStruct.sChange = drRecordSet["CHANGE"].ToString();
                    myTransStruct.sGrade = drRecordSet["GRADE"].ToString();
                    myTransStruct.sVolume = drRecordSet["VOLUME"].ToString();
                    myTransStruct.sShowTime = drRecordSet["SHOW_TIME"].ToString();
                    myTransStruct.sTranId = drRecordSet["TRAN_ID"].ToString();

                    lCompletedTrans.Add(myTransStruct);
                    iCount++;
                }

                for (iIndex = 0; iIndex < 6; iIndex++)
                {
                    if (iIndex < iCount)
                    {
                        Update_Transactions_ButtonText(iIndex + 1, "PUMP: " + lCompletedTrans[iIndex].sPump + " @ " + lCompletedTrans[iIndex].sShowTime + "PAID: $" + lCompletedTrans[iIndex].sDeposit + "  CHANGE: $" + lCompletedTrans[iIndex].sChange);
                    }
                }


                iPage = 1;

                if (lCompletedTrans.Count <= 6 * iPage)
                {

                    ButtonVisibility(next_btn, false);
                }

            }
            else
            {
                Display.ShowMessageBox("No data Available", 3);
            }
            dbCmd.Dispose();
            drRecordSet.Dispose();
            SQL_SERVER.Close_Sql_Sever_Conn();

        }

    }
}

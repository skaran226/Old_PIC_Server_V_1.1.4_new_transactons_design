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



        private void previous_month_Click(object sender, EventArgs e)
        {

        }

        private void next_month_Click(object sender, EventArgs e)
        {

        }

        private void previous_day_Click(object sender, EventArgs e)
        {

        }

        private void next_day_Click(object sender, EventArgs e)
        {
            
        }

        int start = 6, end = 12;
        private void previous_btn_Click(object sender, EventArgs e)
        {

            ClearSelection();
            ClearTransactionsDetails();
            next_btn.Enabled = true;
            start -= 6;
            end -= 6;
            if (start >= 0) {

                Pre_Limit(start, end);
            }
            if (start <= 0) {
                previous_btn.Enabled = false;
                start = 6;
                end = 12;

            }
        }

        private void next_btn_Click(object sender, EventArgs e)
        {
            previous_btn.Enabled = true;
            ClearButtonTexts();
            ClearSelection();
            ClearTransactionsDetails();

           

            if (iCount > start)
            {
                Next_Limit(start, end);

               

            }
            else {

                next_btn.Enabled = false;

            }




            
        }

        private void print_transaction_Click(object sender, EventArgs e)
        {

        }

        private void go_back_Click(object sender, EventArgs e)
        {

        }

        private void One_Click(object sender, EventArgs e)
        {
            if (One.Text.Trim() != "") {

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

        public void SetButton(int index) {

            Button[] btnarr = new Button[] { One, Two, Three, Four, Five, Six };

            foreach (Button btn in btnarr)
            {
                btn.BackColor = Color.White;

            }

            if (index == 1) {

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
            btn.FlatAppearance.MouseOverBackColor =color;
        }


        
       

        public static void SetButtonText(Button btn, string lbl)
        {

            btn.Text = lbl;
        }

        private void Transactions_View_Load(object sender, EventArgs e)
        {
            UpdateCompletedTransView();
        }
   
        public  void Update_Transactions_ButtonText(int index, string lbl)
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
        
        public void UpdateCompletedTransView()
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
            
            sQuery = "SELECT COMPLETED_TIME, PIC, PUMP, DEPOSIT, PURCHASE, PRICE, GRADE, VOLUME, SHOW_TIME, TRAN_ID,CHANGE FROM TRANSACTIONS ORDER BY COMPLETED_TIME DESC;";
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

            for (iIndex = 0; iIndex <6; iIndex++)
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
            pump_no.Text = lCompletedTrans[index-1].sPump;
            deposit.Text = lCompletedTrans[index-1].sDeposit;
            change.Text = lCompletedTrans[index-1].sChange;
            total.Text = lCompletedTrans[index-1].sPurchase;
            date_time.Text = lCompletedTrans[index-1].sShowTime;
            gal.Text = lCompletedTrans[index-1].sVolume;
        }


        private void Next_Limit(int start, int end) {
            int begining_index = 0;
            for (int i = start; i < end; i++) {
                begining_index++;
                if (i < iCount)
                {

                    Update_Transactions_ButtonText(begining_index, "PUMP: " + lCompletedTrans[i].sPump + " @ " + lCompletedTrans[i].sShowTime + "PAID: $" + lCompletedTrans[i].sDeposit + "  CHANGE: $" + lCompletedTrans[i].sChange);
                }
                else {
                    next_btn.Enabled = false;
                    break;
                }
            }
        
        }

        private void Pre_Limit(int start, int end)
        {

            for (int i = start; i < end; i++)
            {

                if (i >= 0)
                {

                    Update_Transactions_ButtonText(i+1, "PUMP: " + lCompletedTrans[i].sPump + " @ " + lCompletedTrans[i].sShowTime + "PAID: $" + lCompletedTrans[i].sDeposit + "  CHANGE: $" + lCompletedTrans[i].sChange);
                }
                else
                {
                    next_btn.Enabled = false;
                    break;
                }
            }

        }

        private void ClearButtonTexts() {
            Button[] btnarr = new Button[] { One, Two, Three, Four, Five, Six };

            foreach (Button btn in btnarr)
            {
                btn.Text = "";
                btn.BackColor = Color.White;
                btn.FlatAppearance.MouseOverBackColor = Color.White;
            }
        }

        private void ClearSelection() {

            Button[] btnarr = new Button[] { One, Two, Three, Four, Five, Six };

            foreach (Button btn in btnarr)
            {
                
                btn.BackColor = Color.White;
                btn.FlatAppearance.MouseOverBackColor = Color.White;
            }
        }

        private void ClearTransactionsDetails() {

            pump_no.Text = "";
            deposit.Text = "";
            change.Text = "";
            total.Text = "";
            date_time.Text = "";
            gal.Text = "";
        }

      

        
    }
}

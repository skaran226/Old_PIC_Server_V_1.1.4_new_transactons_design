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

        private void previous_btn_Click(object sender, EventArgs e)
        {

        }

        private void next_btn_Click(object sender, EventArgs e)
        {

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
            }
        }

        private void Two_Click(object sender, EventArgs e)
        {
            if (Two.Text.Trim() != "")
            {

               SetButton(2);
            }
        }

        private void Three_Click(object sender, EventArgs e)
        {
            if (Three.Text.Trim() != "")
            {

                SetButton(3);
            }
        }

        private void Four_Click(object sender, EventArgs e)
        {
            if (Four.Text.Trim() != "")
            {

               SetButton(4);
            }
        }

        private void Five_Click(object sender, EventArgs e)
        {
            if (Five.Text.Trim() != "")
            {

                SetButton(5);
            }
        }

        private void Six_Click(object sender, EventArgs e)
        {
            if (Six.Text.Trim() != "")
            {

               SetButton(6);
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



        
        public void UpdateCompletedTransView()
        {
            int iIndex, iCount;
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

            for (iIndex = 0; iIndex <= 6; iIndex++)
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

        
    }
}

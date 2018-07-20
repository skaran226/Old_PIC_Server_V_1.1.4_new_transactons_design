using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FPS
{
    public partial class Transactions_View : Form
    {
        public Transactions_View()
        {
            InitializeComponent();
        }

       
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

                this.SetButton(1);
            }
        }

        private void Two_Click(object sender, EventArgs e)
        {
            if (Two.Text.Trim() != "")
            {

                this.SetButton(2);
            }
        }

        private void Three_Click(object sender, EventArgs e)
        {
            if (Three.Text.Trim() != "")
            {

                this.SetButton(3);
            }
        }

        private void Four_Click(object sender, EventArgs e)
        {
            if (Four.Text.Trim() != "")
            {

                this.SetButton(4);
            }
        }

        private void Five_Click(object sender, EventArgs e)
        {
            if (Five.Text.Trim() != "")
            {

                this.SetButton(5);
            }
        }

        private void Six_Click(object sender, EventArgs e)
        {
            if (Six.Text.Trim() != "")
            {

                this.SetButton(6);
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
            DB.UpdateCompletedTransView();
        }

        
    }
}

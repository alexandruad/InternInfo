using InternInfo.InternInfoClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternInfo
{
    public partial class InternInfoGUI : Form
    {
        public InternInfoGUI()
        {
            InitializeComponent();
        }

        InternInfoEntry a = new InternInfoEntry();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // get value from input fields
            a.Company = txtBoxCompany.Text;
            a.PositionProp = txtBoxPosition.Text;
            a.DateForApplication = DateTime.ParseExact(txtBoxDateApp.Text,"d",null);
            a.DateForResponse = DateTime.ParseExact(txtBoxDateResponse.Text,"d",null);
            a.Response = txtBoxResponse.Text;
            a.FollowUp = txtBoxFollow.Text;
            a.AdditionalInfo = txtBoxInfo.Text;
            a.Active = cmbActive.Text;

            //inserting data into the database
            bool success = a.Insert(a);
            if(success)
            {
                MessageBox.Show("New entry added!");
                //call the method to clear the fields
                Clear();
            }else
            {
                MessageBox.Show("Failed to ADD,Try again!");
            }


            //Load data on GridView
            RefreshView();


        }

        private void lblPosition_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RefreshView();

        }

        private void pictureBoxExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //method to clear fields
        public void Clear()
        { 
            txtBoxCompany.Text ="";
            txtBoxPosition.Text = "";
            txtBoxDateApp.Text = "";
            txtBoxDateResponse.Text = "";
            txtBoxResponse.Text = "";
            txtBoxFollow.Text = "";
            txtBoxInfo.Text = "";
            cmbActive.Text = "";
        }
        public void RefreshView()
        {
            //Load data on GridView
            DataTable dt = a.Select();
            dgvInternshipList.DataSource = dt;
        }
         


        private void dgvInternshipList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get data from data grid view and put it to the textboxes
            //identify the row which is clicked
            int rowIndex = e.RowIndex;
            txtBoxCompany.Text = dgvInternshipList.Rows[rowIndex].Cells[0].Value.ToString();
            txtBoxPosition.Text = dgvInternshipList.Rows[rowIndex].Cells[1].Value.ToString();
            txtBoxDateApp.Text = dgvInternshipList.Rows[rowIndex].Cells[2].Value.ToString();
            txtBoxDateResponse.Text = dgvInternshipList.Rows[rowIndex].Cells[3].Value.ToString();
            txtBoxResponse.Text = dgvInternshipList.Rows[rowIndex].Cells[4].Value.ToString();
            txtBoxFollow.Text = dgvInternshipList.Rows[rowIndex].Cells[5].Value.ToString();
            txtBoxInfo.Text = dgvInternshipList.Rows[rowIndex].Cells[6].Value.ToString();
            cmbActive.Text = dgvInternshipList.Rows[rowIndex].Cells[7].Value.ToString();




        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //updating with the new values
            a.Company = txtBoxCompany.Text;
            a.PositionProp = txtBoxPosition.Text;
            a.DateForApplication = DateTime.Parse(txtBoxDateApp.Text);
            a.DateForResponse = DateTime.Parse(txtBoxDateResponse.Text);
            a.Response = txtBoxResponse.Text;
            a.FollowUp = txtBoxFollow.Text;
            a.AdditionalInfo = txtBoxInfo.Text;
            a.Active = cmbActive.Text;

            //Update data in database

            if (a.Update(a))
            {
                MessageBox.Show("Entry updated succesfully!");
                RefreshView();
            }
            else

                MessageBox.Show("Failed to update . \n Please try again");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //Clear all the data from the fields
            Clear();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            a.Company = txtBoxCompany.Text;
            if (a.Delete(a))
            {
                MessageBox.Show("Entry deleted succesfully.");
                Clear();
                RefreshView(); //Load data on GridView
                DataTable dt = a.Select();
                dgvInternshipList.DataSource = dt;

            }
            else
            {
                MessageBox.Show("Failed to delete entry. \n Please try again.");
            }
        }

        static String myConnString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //get the value from textBox
            string keyword = txtBoxSearch.Text;

            SqlConnection conn = new SqlConnection(myConnString);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM internship WHERE company LIKE '%" + keyword + "%' OR position LIKE '%" + keyword + "%'", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgvInternshipList.DataSource = dt;

        }
    }
}

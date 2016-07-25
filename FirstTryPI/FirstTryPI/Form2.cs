using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FirstTryPI
{
    public partial class addNewItem : Form
    {

        
        public addNewItem()
        {
            InitializeComponent();
            
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\USERS\ИВАН\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\FIRSTTRYPI\FIRSTTRYPI\UrlsDatabase.mdf;Integrated Security=True");
            con.Open();
            try
            {
                

                SqlCommand cmdInsert = new SqlCommand("insert into urls (url) values('"+tbNewLink.Text+"' )", con);                
                int ab = cmdInsert.ExecuteNonQuery();

                MessageBox.Show("Added");
                this.tbNewLink.Text = "";
                
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) {
                    MessageBox.Show("This URL already exists.");
                }
                MessageBox.Show(ex.Message);
            }
            con.Close();
         
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\USERS\ИВАН\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\FIRSTTRYPI\FIRSTTRYPI\UrlsDatabase.mdf;Integrated Security=True");
                con.Open();
                try
                {                    
                    SqlCommand cmdUpdate = new SqlCommand("update urls set  url='" + tbNewLink.Text + "' where (url='"+tbEdit.Text+"')", con);
                    int ab = cmdUpdate.ExecuteNonQuery();

                    MessageBox.Show(" Changes were saved successfully.");
                    this.tbNewLink.Text = "";
                    this.tbEdit.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                con.Close();
            
        }

        private void btnDel_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\USERS\ИВАН\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\FIRSTTRYPI\FIRSTTRYPI\UrlsDatabase.mdf;Integrated Security=True");
            con.Open();
            try
            {
                SqlCommand cmdDelete = new SqlCommand("delete from urls where ( url='" + tbEdit.Text + "')", con);
                int ab = cmdDelete.ExecuteNonQuery();
             //   SqlCommand cmdResetId = new SqlCommand("DBCC CHECKIDENT ('urls', RESEED, 0)",con);
               // int bc = cmdResetId.ExecuteNonQuery();
                this.tbNewLink.Text = "";
                this.tbEdit.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
            MessageBox.Show("Deleted");
        }

        private void addNewItem_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            
            
        }

   


    }
}

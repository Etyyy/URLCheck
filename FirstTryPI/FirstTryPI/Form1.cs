using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using FirstTryPI.Properties;

namespace FirstTryPI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
            FillComboBox();
            cbList.SelectedItem = Settings.Default["cbList"].ToString();
        }
        private void Form1_Resize(object sender, System.EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }
     
        void FillComboBox() {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\USERS\ИВАН\DOCUMENTS\VISUAL STUDIO 2013\PROJECTS\FIRSTTRYPI\FIRSTTRYPI\UrlsDatabase.mdf;Integrated Security=True");
            con.Open();

            SqlCommand cmd = new SqlCommand("select url from Urls", con);
            SqlDataReader dr;
            try {
               //con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                   // string urls = dr.GetString(2);
                    cbList.Items.Add(dr["url"]);
                }
                dr.Close();
                dr.Dispose();
                
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            con.Close();
           
        }
    
        private bool Ping(string url)
{
    this.label9.Text = DateTime.Now.ToString("h:mm:ss tt");
    try
    {
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
        request.Timeout = 3000;
        request.AllowAutoRedirect = false; // checks if url is available without being redirected
        request.Method = "HEAD";

        using (var response = request.GetResponse())
        {
            return true;
        }
    }
    catch
    {
        return false;
    }
}
        private void button1_Click(object sender, EventArgs e)
        {

            timer1.Stop();
            label1.Text = "The check is stopped";
        }
        public static int br = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {

            int sec = 0;
           
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    sec = 2000;
                    break;
                case 1: 
                    sec = 3000;
                    break;
                case 2:
                    sec = 5000;
                    break;
                case 3:
                    sec = 10000;
                    break;
                case 4:
                    sec = 15000;
                    break;
               
            }
            
            if (sec==0) {
                comboBox1.SelectedIndex = 0;
            
            }
            else { 
            timer1.Interval = sec;
            }
            string selected = this.cbList.GetItemText(this.cbList.SelectedItem);
            this.label8.Text=selected;
            bool result = Ping(selected);

            
            if (result)
            {
                this.label1.Text = "UP";
                br = 0;
                this.label10.Text = DateTime.Now.ToString("h:mm:ss tt");
                //this.BackColor = Color.Green;
            }
            else
            {
                this.label1.Text = "DOWN";
                br++;
                if (br >= 2 && WindowState==FormWindowState.Minimized)
                { 

                notifyIcon1.ShowBalloonTip(2000,"URL is NOT available!", "Open application for more options.",ToolTipIcon.Warning);
                }
               // this.BackColor = Color.Red;

            }
            
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'urlsDatabaseDataSet.Urls' table. You can move, or remove it, as needed.
         //   this.urlsTableAdapter.Fill(this.urlsDatabaseDataSet.Urls);
            // TODO: This line of code loads data into the 'urlsDatabaseDataSet.Urls' table. You can move, or remove it, as needed.
           // this.urlsTableAdapter.Fill(this.urlsDatabaseDataSet.Urls);

        }
       

        private void button2_Click(object sender, EventArgs e)
        {
            addNewItem newItem = new addNewItem();
            newItem.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            Show();
            WindowState = FormWindowState.Normal;
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Start();

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default["cbList"] = cbList.SelectedItem.ToString();
            Settings.Default.Save();

        }

      
        

       
    }
}

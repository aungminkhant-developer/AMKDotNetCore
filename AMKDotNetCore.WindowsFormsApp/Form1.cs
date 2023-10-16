
using AMKDotNetCore.WindowsFormsApp.Models;
using Dapper;
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

namespace AMKDotNetCore.WindowsFormsApp
{
    public partial class Form1 : Form
    {
        private readonly AppDbContext _context;
        private readonly SqlConnection _sqlConnection;
        public Form1()
        {
            InitializeComponent();
            AppConfigService appConfigService = new AppConfigService();
            _context = new AppDbContext(appConfigService.GetDbConnection());
            _sqlConnection = new SqlConnection(appConfigService.GetDbConnection());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Hello World");
            BlogDataModel blog = new BlogDataModel
            {
                Blog_Author = txtAuthor.Text,
                Blog_Content = txtContent.Text,
                Blog_Title = txtTitle.Text,
            };
            #region EF

            //_context.Blogs.Add(blog);
            //var result=_context.SaveChanges();
            //string message = result > 0 ? "Saving Successful." : "Saving Failed";
            //MessageBox.Show(message,"Alert",MessageBoxButtons.OK,result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            #endregion

            #region Dapper
            //       string query = $@"INSERT INTO [dbo].[Tbl_Blog]
            //      ([Blog_Title]
            //      ,[Blog_Author]
            //      ,[Blog_Content])
            //VALUES
            //      (@Blog_Title
            //      ,@Blog_Author
            //      ,@Blog_Content)";



            //       using (IDbConnection db = new SqlConnection(_sqlConnection.ConnectionString))
            //       {
            //           var result = db.Execute(query, blog);
            //           string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            //           MessageBox.Show(message, "Alert", MessageBoxButtons.OK, result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            //       }
            #endregion

            #region ADO.Net
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
                ([Blog_Title]
                ,[Blog_Author]
                ,[Blog_Content])
            VALUES
                (@Blog_Title
                ,@Blog_Author
                ,@Blog_Content)";

            SqlConnection connection = new SqlConnection(_sqlConnection.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Title", txtTitle.Text);
            cmd.Parameters.AddWithValue("@Blog_Author", txtAuthor.Text);
            cmd.Parameters.AddWithValue("@Blog_Content", txtContent.Text);
            int result = cmd.ExecuteNonQuery();
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            MessageBox.Show(message, "Alert", MessageBoxButtons.OK, result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            #endregion

            txtAuthor.Clear();
            txtContent.Clear();
            txtTitle.Clear();
            txtTitle.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    } 
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LearningTaskWebform
{
    public partial class Edit : System.Web.UI.Page
    {
        private static string ConnectionStrings;

        protected void Page_Load(object sender, EventArgs e)
        {
             ConnectionStrings = System.Configuration.ConfigurationManager.
                ConnectionStrings["SchoolManagementDBConnectionString"].ConnectionString;
        }
        public static void getConnection()
        {
            ConnectionStrings = System.Configuration.ConfigurationManager.
             ConnectionStrings["SchoolManagementDBConnectionString"].ConnectionString;

        }

        [WebMethod]
        public static bool EditData(int Id)
        {
            getConnection();
            string query = "SELECT * FROM dbo.[Tb_StudentMaster] WHERE Id=@Id";
            SqlConnection conn = new SqlConnection(ConnectionStrings);
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", Id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return true;
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO dbo.[Tb_StudentMaster] (" +
                               "FirstName, " +
                               "Lastname, " +
                               "DateOfBirth, " +
                               "Gender," +
                               " Category, " +
                               "Contact, " +
                               "UploadStudentImage)" +
                 "VALUES (" +
                 "@FirstName, " +
                 "@Lastname, " +
                 "@DateOfBirth, " +
                 "@Gender," +
                 "@Category, " +
                 "@Contact, " +
                 "@UploadStudentImage)";

            using (SqlConnection cn = new SqlConnection(ConnectionStrings))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName.Text;
                cmd.Parameters.Add("@Lastname", SqlDbType.VarChar).Value = LastName.Text;
                cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = DateOfBirth.Text;
                cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = DropDownList1.SelectedValue;
                cmd.Parameters.Add("@Category", SqlDbType.VarChar).Value = RadioButtonList1.SelectedValue;
                cmd.Parameters.Add("@Contact", SqlDbType.VarChar).Value = Contact.Text;
                cmd.Parameters.Add("@UploadStudentImage", SqlDbType.VarChar).Value = Convert.ToString(UploadStudentImage.PostedFile.FileName);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Server.Transfer("Index.aspx");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LearningTaskWebform
{


    public partial class Index : System.Web.UI.Page
    {
        private static string ConnectionStrings;

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        [WebMethod]
        public static IEnumerable<StudentGet> GetData()
        {
            getConnection();
            string query = "SELECT * FROM dbo.[Tb_StudentMaster] WHERE Isdeleted=@IsDelete";
            SqlConnection conn = new SqlConnection(ConnectionStrings);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@IsDelete", SqlDbType.Bit).Value = 1;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt);
            conn.Close();

            foreach (DataRow row in dt.Rows)
            {
                yield return new StudentGet
                {
                    Id = Convert.ToInt32(row["Id"]),
                    FirstName = Convert.ToString(row["FirstName"]),
                    LastName = Convert.ToString(row["LastName"]),
                    DOB = Convert.ToString(row["DateOfBirth"]),
                    Gender = Convert.ToString(row["Gender"]),
                    Category = Convert.ToString(row["Category"]),
                    Contact = Convert.ToString(row["Contact"]),
                    UploadStudentImage = Convert.ToString(row["UploadStudentImage"])
                };
            }
        }

        [WebMethod]
        
        public static bool DeleteData( int Id)
        {
           if(Id>=1)
            {
                getConnection();
                string query = "UPDATE dbo.[Tb_StudentMaster] SET Isdeleted=0 WHERE Id=@Id";

                SqlConnection conn = new SqlConnection(ConnectionStrings);
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(Id);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                
                conn.Close();
                if (i >= 1) 
                    return true;
                return false;
            }
            return false;
        }
        public static void getConnection()
        {
            ConnectionStrings = System.Configuration.ConfigurationManager.
             ConnectionStrings["SchoolManagementDBConnectionString"].ConnectionString;
            
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Server.Transfer("Student.aspx");
        }
    }
}
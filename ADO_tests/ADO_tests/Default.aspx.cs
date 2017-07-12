using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace ADO_tests
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string myDBCS=ConfigurationManager.ConnectionStrings["myDBCS"].ConnectionString;

            //using here makes sure all resources get released once execution gets out of its scope
            using (SqlConnection myConnection= new SqlConnection(myDBCS))
            {
                //schema can be included in the command string
                SqlCommand myTSQLCommand = new SqlCommand("select*from [tst].Usuariostest;", myConnection);
                myConnection.Open();
                this.GridView1.DataSource = myTSQLCommand.ExecuteReader();
                GridView1.DataBind();
            }

        }
    }
}
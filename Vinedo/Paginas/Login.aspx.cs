using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vinedo.Paginas
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
        }

        protected void ingresar_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection("server=localhost;Database=portalweb;Uid=root;Password="))
            {

                MySqlCommand cmd = new MySqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "CALL sp_Login(?a, ?b)";
                cmd.Parameters.Add("?a", MySqlDbType.VarChar).Value = usuario.Text;
                cmd.Parameters.Add("?b", MySqlDbType.VarChar).Value = clave.Text;
                MySqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    Session["Id_rol"] = rd[5].ToString();
                    Session["usuario"] = rd[1].ToString();
                    Response.Redirect("index.aspx");
                }
                con.Close();
            }
        }
    }
}
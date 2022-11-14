using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Vinedo.Paginas
{
    public partial class Registrarse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");
        }

        void Limpiar()
        {
            nombre.Text = string.Empty;
            edad.Text = string.Empty;
            usuario.Text = string.Empty;
            clave.Text = string.Empty;
            Id_Rol.Text = string.Empty;
        }

        protected void Registrar_Click(object sender, EventArgs e)
        {
            using (MySqlConnection con = new MySqlConnection("server=localhost;Database=portalweb;Uid=root;Password="))
            {
                MySqlCommand cmd = new MySqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "CALL sp_registrar(?a, ?b, ?c, ?d, ?f)";
                cmd.Parameters.Add("?a", MySqlDbType.VarChar).Value = nombre.Text;
                cmd.Parameters.Add("?b", MySqlDbType.Int32).Value = edad.Text;
                cmd.Parameters.Add("?c", MySqlDbType.VarChar).Value = usuario.Text;
                cmd.Parameters.Add("?d", MySqlDbType.VarChar).Value = clave.Text;
                cmd.Parameters.Add("?f", MySqlDbType.Int32).Value = Id_Rol.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                Limpiar();
                Response.Redirect("Login.aspx");
            }
        }
    }
}
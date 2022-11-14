using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace Vinedo.Paginas
{
    public partial class Index : System.Web.UI.Page
    {
        int id_rol = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Cache-Control", "no-store");

            if (!IsPostBack && Session["usuario"] != null)
            {
                id_rol = Convert.ToInt32(Session["id_rol"].ToString());
                Datos();
                Permisos(id_rol);
            }
        }
        void Datos()
        {
            using (MySqlConnection con = new MySqlConnection("server=localhost;Database=portalweb;Uid=root;Password="))
            {
                MySqlCommand cmd = new MySqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "CALL sp_Datos";
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                datos.DataSource = dt;
                datos.DataBind();
                con.Close();
            }
        }


        void Permisos(int id_rol)
        {
            using (MySqlConnection con = new MySqlConnection("server=localhost;Database=portalweb;Uid=root;Password="))
            {
                MySqlCommand cmd = new MySqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "CALL sp_Permisos(?a)";
                cmd.Parameters.Add("?a", MySqlDbType.Int32).Value = id_rol;
                MySqlDataReader reader = cmd.ExecuteReader();

                int Read, Update, Delete;
                while (reader.Read())
                {
                    foreach (GridViewRow fila in datos.Rows)
                    {

                        switch (reader[0].ToString())
                        {
                            case "Create":
                                int Create; 
                                Create = Convert.ToInt32(reader[1].ToString());

                                if (Create > 0 )
                                {
                                    btncreate.Visible = false;
                                }
                                else
                                {
                                    btncreate.Visible = true;
                                }
                                break;
                            case "Read":
                                Read = Convert.ToInt32(reader[1].ToString());
                                Button btn1 = fila.FindControl("btnread") as Button;
                                if (Read>0)
                                {
                                    btn1.Visible = true;
                                    datos.Visible = true;
                                }
                                else
                                {
                                    btn1.Visible = false;
                                    datos.Visible = false;
                                }

                                break;
                            case "Update":
                                Update = Convert.ToInt32(reader[1].ToString());
                                Button btn2 = fila.FindControl("btnupdate") as Button;

                                if (Update>0)
                                    btn2.Visible = false;
                                else
                                    btn2.Visible = true;

                                break;
                            case "Delete":
                                Delete = Convert.ToInt32(reader[1].ToString());
                                Button btn3 = fila.FindControl("btndelete") as Button;

                                if (Delete>0)
                                    btn3.Visible = false;
                                else
                                    btn3.Visible = true;

                                break;
                        }
                    }
                }


                con.Close();
                reader.Close();
            }
        }
    }
}
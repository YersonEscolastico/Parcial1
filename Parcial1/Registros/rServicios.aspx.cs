using BLL;
using Entidades;
using Parcial1.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Parcial1.Registros
{
    public partial class rServicios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                int id = Utils.ToInt(Request.QueryString["id"]);
                if (id > 0)
                {
                    RepositorioBase<Servicios> repositorio = new RepositorioBase<Servicios>();
                    Servicios user = repositorio.Buscar(id);
                    if (user == null)
                        Utils.ShowToastr(this, "Id no existe", "Error", "error");
                    else
                        LlenarCombo();
                    LLenarCampo(user);
                }
                LlenarCombo();
                ViewState["Servicios"] = new Servicios();
            }
        }
        private Servicios LlenarClase()
        {
            Servicios servicios = new Servicios();
            servicios = (Servicios)ViewState["Servicios"];
            servicios.EvaluacionID = Convert.ToInt32(IdTextBox.Text);
            servicios.EstudianteID = EstudianteDropdownList.SelectedValue.Length;
            servicios.Total = Utils.ToInt(TotalTextBox.Text);
            return servicios;
        }

        private void LLenarCampo(Servicios servicios)
        {
            Limpiar();
            IdTextBox.Text = servicios.EvaluacionID.ToString();
            fechaTextBox.Text = servicios.Fecha.ToString();
            EstudianteDropdownList.SelectedValue = servicios.EstudianteID.ToString();
            TotalTextBox.Text = servicios.Total.ToString();
            ViewState["Servicios"] = servicios;
            this.BindGrid();
        }
        private void LlenarCombo()
        {
            EstudianteDropdownList.Items.Clear();
            RepositorioBase<Estudiantes> repositorio = new RepositorioBase<Estudiantes>();
            EstudianteDropdownList.DataSource = repositorio.GetList(x => true);
            EstudianteDropdownList.DataValueField = "EstudianteId";
            EstudianteDropdownList.DataTextField = "Nombres";
            EstudianteDropdownList.DataBind();
        }
        public void Limpiar()
        {
            IdTextBox.Text = "0";
            EstudianteDropdownList.ClearSelection();
            CantidadTextBox.Text = 0.ToString();
            PrecioTextBox.Text = 0.ToString();
            TotalTextBox.Text = 0.ToString();
            fechaTextBox.Text = DateTime.Now.ToString();
            ViewState["Servicios"] = new Servicios();
            GridView.DataSource = null;
            this.BindGrid();
        }
        private bool ValidarAgregar()
        {
            bool estato = false;

            if (String.IsNullOrWhiteSpace(EstudianteDropdownList.Text))
            {
                Utils.ShowToastr(this, "Debe llenar el campo estudiante", "Error", "error");
                estato = true;
            }

            if (String.IsNullOrWhiteSpace(CantidadTextBox.Text))
            {
                Utils.ShowToastr(this, "Debe llenar el campo valor", "Error", "error");
                estato = true;
            }
            if (String.IsNullOrWhiteSpace(PrecioTextBox.Text))
            {
                Utils.ShowToastr(this, "Debe llenar el campo logrado", "Error", "error");
                estato = true;
            }
            return estato;
        }

        private bool Validar()
        {
            bool estato = false;

            if (GridView.Rows.Count == 0)
            {
                Utils.ShowToastr(this, "Debe agregar detalle.", "Error", "error");
                estato = true;
            }
            if (String.IsNullOrWhiteSpace(IdTextBox.Text))
            {
                Utils.ShowToastr(this, "Debe tener un Id para guardar", "Error", "error");
                estato = true;
            }
            return estato;
        }
        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Servicios> rep = new RepositorioBase<Servicios>();
            Servicios a = rep.Buscar(Convert.ToInt32(IdTextBox.Text));
            if (a != null)
                LLenarCampo(a);
            else
            {
                Limpiar();
                Utils.ShowToastr(this.Page, "Id no exite", "Error", "error");

            }
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            GridViewRow grid = GridView.SelectedRow;
            RepositorioServicios repositorio = new RepositorioServicios();
            int id = Utils.ToInt(IdTextBox.Text);
            var servicios = repositorio.Buscar(id);

            if (servicios != null)
            {
                if (repositorio.Eliminar(id))
                {
                    Utils.ShowToastr(this.Page, "Exito Eliminado", "success");
                    Limpiar();
                }
                else
                    Utils.ShowToastr(this.Page, "No Eliminado", "error");
            }


        }
        protected void BindGrid()
        {
            GridView.DataSource = ((Servicios)ViewState["Servicios"]).Detalles;
            GridView.DataBind();
        }
        protected void AgregarButton_Click(object sender, EventArgs e)
        {
            Servicios servicios = new Servicios();
            decimal total = 0;
            servicios.Detalles = new List<ServiciosDetalle>();
            servicios = (Servicios)ViewState["Servicios"];
            decimal importe = Convert.ToDecimal(CantidadTextBox.Text) * Convert.ToDecimal(PrecioTextBox.Text);
            servicios.Detalles.Add(new ServiciosDetalle(0, 0, Convert.ToDecimal(CantidadTextBox.Text),
            Convert.ToDecimal(PrecioTextBox.Text), importe));
            ViewState["Servicios"] = servicios;
            this.BindGrid();
            foreach (var item in servicios.Detalles)
            {
                total += item.Importe;
            }
            TotalTextBox.Text = total.ToString();
            
        }
        protected void RemoveLinkButton_Click(object sender, EventArgs e)
        {
            if (GridView.Rows.Count > 0 && GridView.SelectedIndex >= 0)
            {
                decimal total = 0;
                Servicios servicios = new Servicios();
                servicios = (Servicios)ViewState["Servicios"];
                GridViewRow row = (sender as Button).NamingContainer as GridViewRow;
                servicios.RemoverDetalle(row.RowIndex);
                ViewState["Servicios"] = servicios;
                this.BindGrid();

                foreach (var item in servicios.Detalles)
                {
                    total -= item.Importe;
                }
                TotalTextBox.Text = total.ToString();
            }

        }
        protected void GuardarButton_Click(object sender, EventArgs e)
        {
            RepositorioServicios repositorio = new RepositorioServicios();
            Servicios servicios = repositorio.Buscar(Utils.ToInt(IdTextBox.Text));

            if (Validar())
            {
                return;
            }
            if (servicios == null)
            {
                if (repositorio.Guardar(LlenarClase()))
                {

                    Utils.ShowToastr(this, "Guardado", "Exito", "success");
                    Limpiar();
                }
                else
                {
                    Utils.ShowToastr(this, "No existe", "Error", "error");
                    Limpiar();
                }

            }
            else
            {
                if (repositorio.Modificar(LlenarClase()))
                {
                    Utils.ShowToastr(this.Page, "Modificado con exito!!", "Guardado", "success");
                    Limpiar();
                }
                else
                {
                    Utils.ShowToastr(this.Page, "No se puede modificar", "Error", "error");
                    Limpiar();
                }
            }

        }
    }
}
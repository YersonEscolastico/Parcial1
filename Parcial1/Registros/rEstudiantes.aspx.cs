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
    public partial class rEstudiantes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            fechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            int ID = Utils.ToInt(Request.QueryString["id"]);
            if (ID > 0)
            {
                RepositorioBase<Estudiantes> repositorio = new RepositorioBase<Estudiantes>();
                var us = repositorio.Buscar(ID);


                if (us == null)
                {
                    Utilitarios.Utils.ShowToastr(this.Page, "Registro No encontrado", "Error", "error");
                }
                else
                {
                    LlenaCampo(us);
                }
            }
        }
    

    private void Limpiar()
    {
        EstudianteIdTextBox.Text = string.Empty;
        NombresTextBox.Text = string.Empty;

        fechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");

    }


    public Estudiantes LlenaClase()
    {
        Estudiantes estudiante = new Estudiantes();
        estudiante.EstudianteId = Convert.ToInt32(EstudianteIdTextBox.Text);
        estudiante.Nombres = NombresTextBox.Text;
        estudiante.Fecha = Convert.ToDateTime(fechaTextBox.Text);
        return estudiante;
    }


    private void LlenaCampo(Estudiantes estudiante)
    {
        EstudianteIdTextBox.Text = estudiante.EstudianteId.ToString();
        NombresTextBox.Text = estudiante.Nombres;
        fechaTextBox.Text = estudiante.Fecha.ToString("yyyy-MM-dd");
    }

    private bool ExisteEnLaBaseDeDatos()
    {
        RepositorioBase<Estudiantes> db = new RepositorioBase<Estudiantes>();
        Estudiantes estudiante = db.Buscar(Convert.ToInt32(EstudianteIdTextBox.Text));
        return (estudiante != null);
    }

    private bool Validar()
    {
        bool paso = true;
        if (NombresTextBox.Text == string.Empty)
        {
            Utilitarios.Utils.ShowToastr(this.Page, "El campo Nombres no puede estar vacio", "Error", "error");
            NombresTextBox.Focus();

            Utilitarios.Utils.ShowToastr(this.Page, "El campo Fecha no puede estar vacio", "Error", "error");
            fechaTextBox.Focus();
            paso = false;
        }
        return paso;
    }

    protected void NuevoButton_Click(object sender, EventArgs e)
    {
        Limpiar();

    }

    protected void GuardarButton_Click1(object sender, EventArgs e)
    {
        RepositorioBase<Estudiantes> db = new RepositorioBase<Estudiantes>();
        Estudiantes estudiante;
        bool paso = false;

        if (!Validar())
            return;

        estudiante = LlenaClase();


        if (EstudianteIdTextBox.Text == Convert.ToString(0))
        {
            paso = db.Guardar(estudiante);
        }
        else
        {
            if (!ExisteEnLaBaseDeDatos())
            {
                Utilitarios.Utils.ShowToastr(this.Page, "No existe", "Error", "error");
                return;
            }
            paso = db.Modificar(estudiante);
        }

        if (paso)
            Utilitarios.Utils.ShowToastr(this.Page, "Guardado con exito ", "Exito", "success");
        else
            Utilitarios.Utils.ShowToastr(this.Page, "Error al guardar", "Error", "error");
        Limpiar();

    }

    protected void BuscarButton_Click(object sender, EventArgs e)
    {
        int id;

        RepositorioBase<Estudiantes> db = new RepositorioBase<Estudiantes>();
        Estudiantes estudiante = new Estudiantes();
        int.TryParse(EstudianteIdTextBox.Text, out id);
        Limpiar();

        estudiante = db.Buscar(id);

        if (estudiante != null)
        {
            LlenaCampo(estudiante);
        }
        else
        {
            Utilitarios.Utils.ShowToastr(this.Page, "No existe", "Error", "error");
        }
    }
    protected void EliminarButton_Click1(object sender, EventArgs e)
    {
        if (Utilitarios.Utils.ToInt(EstudianteIdTextBox.Text) > 0)
        {
            int id = Convert.ToInt32(EstudianteIdTextBox.Text);
            RepositorioBase<Estudiantes> repositorio = new RepositorioBase<Estudiantes>();
            if (repositorio.Eliminar(id))
            {
                Utilitarios.Utils.ShowToastr(this.Page, "Eliminado con exito!!", "Eliminado", "info");
            }
            else
                Utilitarios.Utils.ShowToastr(this.Page, "Fallo al Eliminar :(", "Error", "error");
            Limpiar();
        }

        else
        {
            Utilitarios.Utils.ShowToastr(this.Page, "No se puede eliminar", "error", "error");
        }
    }
}
}
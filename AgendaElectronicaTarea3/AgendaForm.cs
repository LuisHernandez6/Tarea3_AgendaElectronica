using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Configuration;
using AgendaElectronicaTarea3.CapaDeDatos;

namespace AgendaElectronicaTarea3.CapaPresentacion
{
    public partial class AgendaForm : Form
    {
        private ContactoCRUD repo;
        private BindingList<Contacto> listaContactos;
        private BindingSource bindingSource = new BindingSource();

        public AgendaForm()
        {
            InitializeComponent();
            repo = new ContactoCRUD(ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString);
            CargarDatos();

            dgvContactos.Columns["Id"].HeaderText = "ID";
            dgvContactos.Columns["Nombre"].HeaderText = "Nombre(s)";
            dgvContactos.Columns["Apellido"].HeaderText = "Apellido(s)";
            dgvContactos.Columns["FechaNacimiento"].HeaderText = "Fecha de nacimiento";
            dgvContactos.Columns["EstadoCivil"].HeaderText = "Estado civil";
            dgvContactos.Columns["CorreoElectronico"].HeaderText = "Correo Electrónico";

            dgvContactos.Columns["Id"].Width = 35;
            dgvContactos.Columns["FechaNacimiento"].Width = 70;
            dgvContactos.Columns["Genero"].Width = 65;
            dgvContactos.Columns["EstadoCivil"].Width = 65;
            dgvContactos.Columns["Telefono"].Width = 75;
            dgvContactos.Columns["Movil"].Width = 85;
            dgvContactos.Columns["CorreoElectronico"].Width = 110;
        }

        private void CargarDatos()
        {
            var lista = repo.ObtenerTodos();
            listaContactos = new BindingList<Contacto>(lista);
            bindingSource.DataSource = listaContactos;
            dgvContactos.DataSource = bindingSource;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //TODO
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //TODO
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //TODO
        }

        private void CargarEnFormulario(Contacto c)
        {
            txtId.Text = c.Id.ToString();
            txtNombre.Text = c.Nombre;
            txtApellido.Text = c.Apellido;
            dtpFechaNacimiento.Value = c.FechaNacimiento;
            txtDireccion.Text = c.Direccion;
            cbGenero.SelectedItem = c.Genero;
            cbEstadoCivil.SelectedItem = c.EstadoCivil;
            txtMovil.Text = c.Movil;
            txtTelefono.Text = c.Telefono;
            txtCorreo.Text = c.CorreoElectronico;
        }

        private void dgvContactos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ignorar encabezado
            {
                DataGridViewRow fila = dgvContactos.Rows[e.RowIndex];

                var id = Convert.ToInt32(fila.Cells["Id"].Value);
                var c = repo.BuscarPorId(id);

                if (c != null) CargarEnFormulario(c);
            }
        }
    }
}

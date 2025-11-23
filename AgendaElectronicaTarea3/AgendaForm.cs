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

        private Contacto LeerFormulario()
        {
            Contacto c = new Contacto
            {
                Id = 0,
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                FechaNacimiento = dtpFechaNacimiento.Value.Date,
                Direccion = txtDireccion.Text,
                Genero = cbGenero.SelectedItem?.ToString() ?? "",
                EstadoCivil = cbEstadoCivil.SelectedItem?.ToString() ?? "",
                Movil = txtMovil.Text,
                Telefono = txtTelefono.Text,
                CorreoElectronico = txtCorreo.Text
            };

            if (int.TryParse(txtId.Text, out var id))
            {
                c.Id = id;
            }

            return c;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            var c = LeerFormulario();
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debe de colocarle un nombre al contacto.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            repo.Insertar(c);
            CargarDatos();
            MessageBox.Show("Contacto añadido.");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out var id) && !string.IsNullOrWhiteSpace(txtId.Text))
            {
                var c = repo.BuscarPorId(id);
                if (c != null)
                {
                    repo.Actualizar(LeerFormulario());
                    CargarDatos();
                    MessageBox.Show("Contacto actualizado.");
                    //Limpiar();
                    return;
                }
            }
            MessageBox.Show("No tiene un contacto seleccionado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtId.Text, out var id))
            {
                var c = repo.BuscarPorId(id);
                if (c != null)
                {
                    repo.Eliminar(id);
                    MessageBox.Show("Contacto eliminado.");
                    CargarDatos();
                    Limpiar();
                    return;
                }
            }
            MessageBox.Show("No tiene un contacto seleccionado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Limpiar()
        {
            txtId.Clear(); //txtNombre.Clear();
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

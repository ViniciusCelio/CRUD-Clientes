using System.ComponentModel.DataAnnotations;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace CRUD_Clientes
{
    public partial class Form1 : Form
    {
        ClienteDAO clienteDAO;
        public Form1()
        {
            InitializeComponent();
            clienteDAO = new ClienteDAO();
            clienteDAO.Carregar();
            dataGridView1.DataSource = clienteDAO.GetClientes();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            var cliente = new Cliente()
            {
                Nome = textBox1.Text,
                Email = textBox2.Text
            };

            if (clienteDAO.GetClientes().Any(x => x.Nome.Equals(cliente.Nome)
                                            && x.Email.Equals(cliente.Email)))
            {
                MessageBox.Show("Já existe esse cliente na base", "Atenção");
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                clienteDAO.Adicionar(cliente);
                clienteDAO.Salvar();

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = clienteDAO.GetClientes();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var cliente = new Cliente()
            {
                Nome = textBox1.Text,
                Email = textBox2.Text
            };

            if (!clienteDAO.GetClientes().Any(x => x.Nome.Equals(cliente.Nome)
                                            && x.Email.Equals(cliente.Email)))
            {
                MessageBox.Show("Esse cliente não existe na base", "Atenção");
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                clienteDAO.Remover(cliente);
                clienteDAO.Salvar();

                dataGridView1.DataSource = null;
                dataGridView1.DataSource = clienteDAO.GetClientes();
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string propAlterada = dataGridView1.SelectedCells[0].Value.ToString();
            MessageBox.Show($"Valor alterado para: {propAlterada}", "Alteração realizada");
        }
    }
}
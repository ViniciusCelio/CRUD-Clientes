using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CRUD_Clientes
{
    internal class ClienteDAO
    {
        //Manipulação em memória primária 
        private List<Cliente> clientes;

        public ClienteDAO()
        {
            this.clientes = new List<Cliente>();
        }

        public void Adicionar (Cliente cliente)
        {
                this.clientes.Add(cliente);
        }

        public void Remover (Cliente cliente)
        {
            this.clientes.RemoveAll(x => x.Nome.Equals(cliente.Nome)
                                    && x.Email.Equals(cliente.Email));
        }

        public List<Cliente> GetClientes()
        {
            return this.clientes;
        }

        //manipulação em arquivo XML
        public void Salvar()
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Cliente>));
            FileStream fs = new FileStream("C://temp//Clientes.xml", FileMode.OpenOrCreate);
            ser.Serialize(fs, this.clientes);
            fs.Close();
        }

        public void Carregar()
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Cliente>));
            FileStream fs = new FileStream("C://temp//Clientes.xml", FileMode.OpenOrCreate);

            try{
                this.clientes = ser.Deserialize(fs) as List<Cliente>;
            } catch (InvalidOperationException) {
                ser.Serialize(fs, this.clientes);
            } finally { 
                fs.Close(); 
            }
        }
    }
}

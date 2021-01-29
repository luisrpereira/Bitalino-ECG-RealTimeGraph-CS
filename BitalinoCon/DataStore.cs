using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace BitalinoCon

{
    public class DataStore
    {
        public static string DATATABLE_CLIENTES = "Ficha de Clientes:";
        public static string CLIENTE_NOME = "Nome";
        public static string CLIENTE_IDADE = "Idade";
        public static string CLIENTE_GENERO = "Genero";
        public static string CLIENTE_DESPORTO = "Pratica Desporto";
        public static string CLIENTE_MORADA = "Morada";
        public static string CLIENTE_PROFISSAO = "Profissão";
        public static string CLIENTE_ID = "ID";

        public static string DATATABLE_EXAMES = "Lista de Exames:";
        public static string EXAME_DATE = "Data (dd/mm/yyyy hh:mm:ss)";
        public static string EXAME_CLIENT_ID = "Cliente_ID";
        public static string EXAME_ANNOTATIONS = "Anotações";
        public static string EXAM_ID ="EXAM_ID";
        public static string EXAM_DADOS = "Dados";

        DataSet dataSet;
        DataTable tableClientes;
        DataTable tableExames;

        public DataTable TableClientes
        {
            get
            {
                return tableClientes;
            }

            set
            {
                tableClientes = value;
            }
        }
        public DataSet DataSet
        {
            get
            {
                return dataSet;
            }

            set
            {
                dataSet = value;
            }
        }
        public DataTable TableExames
        {
            get
            {
                return tableExames;
            }

            set
            {
                tableExames = value;
            }
        }

        String FICHEIRO = "ecg.xml";

        public DataStore()
        {
            DataSet = new DataSet("ecg_dataset");

            TableClientes = new DataTable(DATATABLE_CLIENTES);
            TableClientes.Columns.Add(CLIENTE_NOME);
            TableClientes.Columns.Add(CLIENTE_IDADE);
            TableClientes.Columns.Add(CLIENTE_GENERO);
            TableClientes.Columns.Add(CLIENTE_DESPORTO);
            TableClientes.Columns.Add(CLIENTE_MORADA);
            TableClientes.Columns.Add(CLIENTE_PROFISSAO);
            TableClientes.Columns.Add(CLIENTE_ID);

            TableExames = new DataTable(DATATABLE_EXAMES);
            TableExames.Columns.Add(EXAME_DATE);
            TableExames.Columns.Add(EXAME_ANNOTATIONS);
            TableExames.Columns.Add(EXAME_CLIENT_ID);
            TableExames.Columns.Add(EXAM_ID);
            TableExames.Columns.Add(EXAM_DADOS);


            DataSet.Tables.Add(TableClientes);
            DataSet.Tables.Add(TableExames);

            Carregar();
        }

        public void Guardar()
        {
            DataSet.WriteXml(FICHEIRO);
        }

        public void Carregar()
        {
            try
            {
                DataSet.ReadXml(FICHEIRO);
            }
            catch (FileNotFoundException e)
            {

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitalinoCon
{
    public enum GenderType { Masculino, Feminino };
    public enum DesportoType { Sim, Não };

    public class DadosPaciente
    {
        private string nome;
        private int idade;
        private string morada;
        private string profissao;
        private GenderType genero;
        private DesportoType desporto;
        private long id;

        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
            }
        }

        public int Idade
        {
            get
            {
                return idade;
            }

            set
            {
                idade = value;
            }
        }

        public GenderType Genero
        {
            get
            {
                return genero;
            }

            set
            {
                genero = value;
            }
        }
        public DesportoType Desporto
        {
            get
            {
                return desporto;
            }

            set
            {
                desporto = value;
            }
        }
        public string Morada
        {
            get
            {
                return morada;
            }
            set
            {
                morada = value;
            }
        }

        public string Profissao
        {
            get
            {
                return profissao;
            }
            set
            {
                profissao = value;
            }
        }

        public long Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public DadosPaciente(string nome, int idade, GenderType genero, DesportoType desporto, string morada, string profissao)
        {
            this.Nome = nome;
            this.Idade = idade;
            this.Genero = genero;
            this.Desporto = desporto;
            this.Profissao = profissao;
            this.Morada = morada;
            this.id= Utils.CurrentTimeMillis();
        }

        public DadosPaciente(string nome, int idade, GenderType genero, DesportoType desporto, string morada, string profissao,long id)
        {
            this.Nome = nome;
            this.Idade = idade;
            this.Genero = genero;
            this.Desporto = desporto;
            this.Profissao = profissao;
            this.Morada = morada;
            this.id = id;
        }


        // Create: Adicionar clientes à base de dados
        public static void AdicionarParaDataBase(DataStore datahelper, DadosPaciente cliente)
        {
            DataRow datarow = datahelper.TableClientes.NewRow();

            datarow[DataStore.CLIENTE_NOME] = cliente.Nome;
            datarow[DataStore.CLIENTE_IDADE] = cliente.Idade;
            datarow[DataStore.CLIENTE_GENERO] = cliente.Genero;
            datarow[DataStore.CLIENTE_DESPORTO] = cliente.Desporto;
            datarow[DataStore.CLIENTE_MORADA] = cliente.Morada;
            datarow[DataStore.CLIENTE_PROFISSAO] = cliente.Profissao;
            datarow[DataStore.CLIENTE_ID] = cliente.Id;

            datahelper.TableClientes.Rows.Add(datarow);
            datahelper.Guardar();
        }

        public static DadosPaciente lerNaBaseDados(DataStore datahelper, int index)
        {
            DadosPaciente cliente;

            DataRow datarow = datahelper.TableClientes.Rows[index];

            cliente = new DadosPaciente(
                 (String)datarow[DataStore.CLIENTE_NOME],
                 int.Parse((String)datarow[DataStore.CLIENTE_IDADE]),
                 parseGender((String)datarow[DataStore.CLIENTE_GENERO]),
                 parseDesporto((String)datarow[DataStore.CLIENTE_DESPORTO]),
                 (String)datarow[DataStore.CLIENTE_MORADA],
                 (String)datarow[DataStore.CLIENTE_PROFISSAO],
                 long.Parse((String)datarow[DataStore.CLIENTE_ID])); 

            return cliente;
        }

        public static void removerDaBaseDados(DataStore datahelper, int indexParaRemover)
        {
            datahelper.TableClientes.Rows.RemoveAt(indexParaRemover);
            datahelper.Guardar();
        }


        // Converter GenderType em string para utilizar no Read
        public static GenderType parseGender(String strGender)
        {
            if (strGender.CompareTo("Masculino") == 0)
            {
                return GenderType.Masculino;
            }
            else
            {
                return GenderType.Feminino;
            }
        }

        // Converter DesportoType em string para utilizar no Read
        public static DesportoType parseDesporto(String strDesporto)
        {
            if (strDesporto.CompareTo("Sim") == 0)
            {
                return DesportoType.Sim;
            }
            else
            {
                return DesportoType.Não;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitalinoCon
{
   


    public class DadosExame
    {

        private DateTime date;
        private string annotations;
        private long id;
        private long clienteId;
        private string leitura;
        public DateTime Date

        {
            get
            {
                return date;
            }

            set
            {
                date = value;
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
        public long ClienteId
        {
            get
            {
                return clienteId;
            }

            set
            {
                clienteId = value;
            }
        }

        public string Annotations
        {
            get
            {
                return annotations;
            }
            set
            {
                annotations = value;
            }
        }

        public string Leitura
        {
            get
            {
                return leitura;
            }
            set
            {
                leitura = value;
            }
        }




        public DadosExame(DateTime date, long clienteId, string annotations, string leitura)
        {
            this.Date= date;
            this.ClienteId = clienteId;
            this.id = Utils.CurrentTimeMillis();
            this.annotations = annotations;
            this.leitura = leitura;
        }

        public static void AdicionarExameParaDataBase(DataStore datahelper, DadosExame exames)
        {
            DataRow datarow = datahelper.TableExames.NewRow();

            datarow[DataStore.EXAME_DATE] = exames.date;
            datarow[DataStore.EXAME_CLIENT_ID] = exames.ClienteId;
            datarow[DataStore.EXAME_ANNOTATIONS] = exames.annotations;
            datarow[DataStore.EXAM_ID] = exames.id;
            datarow[DataStore.EXAM_DADOS] = exames.leitura;
            datahelper.TableExames.Rows.Add(datarow);
            datahelper.Guardar();
        }

      
            public static void removerDaBaseDados(DataStore datahelper, int index, long idPaciente)
        {
            int indexToRemove = 0, indexFilter = 0, linha = 0;
            while (linha < datahelper.TableExames.Rows.Count)
            {


                DataRow dr = datahelper.TableExames.Rows[linha];

                if (long.Parse((String)dr[DataStore.EXAME_CLIENT_ID]) == idPaciente)
                {
                    if (index == indexFilter)
                    {
                        indexToRemove = linha;
                    }
                    indexFilter++;

                }
                linha++;
            }
            datahelper.TableExames.Rows.RemoveAt(indexToRemove);
            datahelper.Guardar();
        }

    }

}


using MazeFire;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCGApp
{
    public partial class frmPrincipal : Form
    {
        /// <summary>
        /// Parâmetros de Configuração
        /// </summary>
        private MazeFire.DataBaseTools.Configurator WRKCFG = new MazeFire.DataBaseTools.Configurator();

        private bool busy = false;
        List<String> TableList = new List<String>();


        private string _PATHMODELBASE = @"c:\repositoriobase\modelos\dbtools.xml";
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btn_proc_Click(object sender, EventArgs e)
        {

            if (FillTableList() > 0)
            {
                for (int i = 0; i < this.TableList.Count; i++)
                {
                    MazeFire.DataBaseTools.CreateSP sp = new MazeFire.DataBaseTools.CreateSP(GetId(), _PATHMODELBASE, this.TableList[i]);
                }
                MessageBox.Show("Concluido");
            }
            else
                MessageBox.Show("Não existem elemetos selecionados");

        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            _PATHMODELBASE = MazeFire.Configuration.GetAppValue("PATHMODELBASE");
            LoadCliente();
        }


        #region -- Métodos Privados --


        public int GetId()
        {
            return ((ItemValue)fcliente.SelectedItem).KeyValue;
        }
        private void LoadCliente()
        {
            var _list = WRKCFG.GetIdList(_PATHMODELBASE);
            fcliente.Items.Clear();
            foreach (var item in _list)
            {
                fcliente.Items.Add(item);
            }
            fcliente.SelectedIndex = 3;
        }

        private int FillTableList()
        {
            TreeNodeCollection nodes = this.tvwdata.Nodes;
            this.TableList.Clear();
            getCheckedNodes(nodes);
            return this.TableList.Count;
        }

        private void LoadTables(List<string> _list)
        {
            tvwdata.BeginUpdate();
            tvwdata.Nodes.Clear();
            if (_list.Count > 0)
            {

                tvwdata.Nodes.Add(new TreeNode("Tabelas"));
                foreach (string s in _list)
                {
                    tvwdata.Nodes[0].Nodes.Add(new TreeNode(s));
                }
            }
            tvwdata.EndUpdate();
        }

        private bool GetContext(int id)
        {
            bool _ret = false;
            if (id > 0)
            {

                try
                {

                    WRKCFG.Load(id, _PATHMODELBASE);

                    if (!WRKCFG.Ready)
                    {
                        MessageBox.Show(WRKCFG.LastError);
                    }
                    _ret = WRKCFG.Ready;
                }
                catch (Exception Error)
                {

                    MessageBox.Show(Error.Message);
                }
            }
            return _ret;
        }

        #endregion


        #region -- TreeView --
        private void tvwdata_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (busy) return;
            busy = true;
            try
            {
                checkNodes(e.Node, e.Node.Checked);
            }
            finally
            {
                busy = false;
            }
        }

        private void checkNodes(TreeNode node, bool check)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = check;
                checkNodes(child, check);
            }
        }

        private void getCheckedNodes(TreeNodeCollection tvw)
        {
            if (tvw != null)
            {
                foreach (TreeNode item in tvw)
                {
                    if (item.Checked)
                    {
                        TableList.Add(item.Text);
                    }
                    if (item.Nodes.Count != 0)
                        getCheckedNodes(item.Nodes);
                }
            }
        }
        #endregion


        private void fcliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fcliente.SelectedItem != null)
            {
                int id = GetId();

                if (GetContext(id))
                {
                    LoadTables(WRKCFG.Utils.ListTables());
                }

            }
        }

        private void btn_csharp_Click(object sender, EventArgs e)
        {
            if (FillTableList() > 0)
            {
                int value = 0;
                for (int i = 0; i < this.TableList.Count; i++)
                {
                    MazeFire.DataBaseTools.CreateCSharp sp = new MazeFire.DataBaseTools.CreateCSharp();

                    value = sp.Execute(GetId(), _PATHMODELBASE, this.TableList[i]);

                    if (value != 1)
                    {
                        MessageBox.Show(sp.LastError);
                        break;
                    }
                }


                MessageBox.Show("Concluido");

            }
            else
                MessageBox.Show("Não existem elemetos selecionados");
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_dic_Click(object sender, EventArgs e)
        {
            bool go = MazeFire.DataBaseTools.Schemas.WriteDictionaryData(WRKCFG);
            MessageBox.Show(MazeFire.DataBaseTools.Schemas.TrappedError.UserError);
        }

        private void btn_printschema_Click(object sender, EventArgs e)
        {
            bool go = MazeFire.DataBaseTools.Schemas.WriteProcedures(WRKCFG);
            MessageBox.Show(MazeFire.DataBaseTools.Schemas.TrappedError.UserError);
        }


        private void print_table_Click(object sender, EventArgs e)
        {
            bool go = MazeFire.DataBaseTools.Schemas.WriteTables(WRKCFG);
            MessageBox.Show(MazeFire.DataBaseTools.Schemas.TrappedError.UserError);
        }

        private void btn_lsterros_Click(object sender, EventArgs e)
        {
            bool go = MazeFire.DataBaseTools.Schemas.WriteErrorData(WRKCFG);
            MessageBox.Show(MazeFire.DataBaseTools.Schemas.TrappedError.UserError);
        }

        private void tvwdata_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}

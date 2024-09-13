using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Windows.Forms.VisualStyles;

namespace ProcessManagerGui
{
    public partial class Form1 : Form
    {
        int? index = null;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          
            Utils.Utils.UpdateListView(listView1);
            if (index != null)
            {
                try
                {
                    listView1.Items[index.Value].Selected = true;
                    listView1.EnsureVisible(index.Value);
                }
                catch { }
                
                
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lv = (ListView) sender;
            ListView.SelectedIndexCollection indexCollection = lv.SelectedIndices;

            if (indexCollection.Count == 1)
            {
                index = indexCollection[0];
                
            }
            else
            {
                index = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            ListView.SelectedListViewItemCollection items = listView1.SelectedItems;
            if (items.Count == 1)
            {
                ListViewItem item = items[0];
                if (item != null)
                {
                    try
                    {
                        int pid = int.Parse(item.Text);
                        Process p = Process.GetProcessById(pid);
                        p.Kill();
                        Utils.Utils.UpdateListView(listView1);

                    }
                    catch { }
                    index = null;
                }
            }
                                
        }
    }

    namespace Utils
    {
        public static class Utils
        {
           
            public static void UpdateListView(ListView listView)
            {
                Process[] processes = Process.GetProcesses();
                listView.Clear();
                listView.Columns.Add("PID");
                listView.Columns.Add("Name");
                listView.Columns.Add("Phy.Mem. allocated (MB)");
                //listView.Columns[0].Width = 100;
                listView.Columns[1].Width = 100;
                listView.Columns[2].Width = 200;
                
                foreach (var proc in processes)
                {
                    string[] row = { proc.Id.ToString(), proc.ProcessName, $"{proc.WorkingSet64/1000000.0:F2}"};
                    ListViewItem item = new ListViewItem(row);
                    listView.Items.Add(item);
                }
            }
        }

    }

}

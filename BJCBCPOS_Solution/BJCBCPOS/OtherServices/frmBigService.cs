using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BJCBCPOS.OtherServices.Classes;

namespace BJCBCPOS.OtherServices {

  public partial class frmBigService : Form
  {

        public string menu_lang = "EN";
        public int main_menu = 0;
        public int sub_menu;
        public string menu_child = "";
        public string menu_parents = "";


    public frmBigService()
    {
      InitializeComponent();
    }

    ImageList imageListM = new ImageList();
    string[] imgfileM = new string[] { };
    string[] menuIDM = new string[] { };
    string[] menuNameM = new string[] { };
    DataTable dt = new DataTable();

    private void BigService_Load(object sender, EventArgs e)
    {
      Array.Clear(imgfileM, 0, imgfileM.Length);
      Array.Clear(menuNameM, 0, menuNameM.Length);
      imageListM.Images.Clear();
      imageListM.Images.Clear();
      listView1.Items.Clear();
      dt.Rows.Clear();

      string SQL = " select b.mainmenu_id, b.submenu_id, b.menu_id, b.menu_name, b.menu_lang, isnull(b.menu_image,'images/DefaultImage.png') menu_image, b.menu_child, b.menu_parents " +
            " from dbo.MENUBIGSERVICE a left join dbo.menumapping b on a.MENU_ID = b.SUBMENU_ID " +
            " where a.status = 'A' and b.mainmenu_id = 0 and b.menu_lang = 'EN' order by b.menu_id ";

      var db = new Database();
      var ds = db.GetDataset(SQL);
      if (ds.Tables[0].Rows.Count == 0)
      {
        MessageBox.Show("This menu not available", "Warning");
      }
      else
      {
        dt = ds.Tables[0];
        listView1.View = View.LargeIcon;
        imageListM.ColorDepth = ColorDepth.Depth32Bit;
        imageListM.ImageSize = new Size(220, 150);

        imgfileM = dt.Rows.OfType<DataRow>().Select(k => k[5].ToString()).ToArray();
        foreach (string imgPathM in imgfileM)
        {          
                    try
                    {
                        imageListM.Images.Add(Image.FromFile(imgPathM));
                    }
                    catch (Exception ex)
                    {
                        imageListM.Images.Add(Image.FromFile(Application.StartupPath+"\\images\\DefaultImage.png"));
                    }
                }

        main_menu = Convert.ToInt32(ds.Tables[0].Rows[0]["submenu_id"].ToString());
        menuIDM = dt.Rows.OfType<DataRow>().Select(k => k[2].ToString()).ToArray();
        menuNameM = dt.Rows.OfType<DataRow>().Select(k => k[3].ToString()).ToArray();
        listView1.LargeImageList = imageListM;
        for (int j = 0; j < this.imageListM.Images.Count; j++)
        {
          this.listView1.Items.Add(menuIDM[j], menuNameM[j], j);
        }
      }

    }

    private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
      if (listView1.SelectedItems.Count == 0) return;

      textMenuID.Text = e.Item.Name.ToString();
      textMenuName.Text = e.Item.Text.ToString();

      string SQL = " select top 1 submenu_id, menu_child, menu_parents from dbo.menumapping " +
        " where status = 'A' and submenu_id = " + Convert.ToInt32(textMenuID.Text) + " and menu_lang = '" + menu_lang + "' order by menu_id ";

      var db = new Database();
      var ds = db.GetDataset(SQL);
      if (ds.Tables[0].Rows.Count == 0)
      {
        MessageBox.Show("This menu not available", "Warning");
      }
      else
      {        
        this.sub_menu = Convert.ToInt32(ds.Tables[0].Rows[0]["submenu_id"].ToString());
        this.menu_child = ds.Tables[0].Rows[0]["menu_child"].ToString();
        this.menu_parents = ds.Tables[0].Rows[0]["menu_parents"].ToString();        
        
        if (menu_child == "Y")
        {

            // setting
            string formName = menu_parents;

            Type t = Type.GetType(formName);
            if (t != null)
            {
                //Create a new instance
                Form frm = Activator.CreateInstance(t) as Form;
                if (frm != null)
                    frm.ShowDialog();
            }           

        }
        else
        {
          MessageBox.Show("This menu have no sub-menu", "Warning");
                    
                }
      }
    }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Back to Main Menu", "Warning");
            Environment.Exit(0);
        }
    }
}

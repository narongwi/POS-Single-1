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
  public partial class frmBillPayment : Form
  {

        public string menu_lang = "EN";
        public int main_menu = 1;
        public int sub_menu = 11;
        public string menu_child;
        public string menu_parents;

    public frmBillPayment()
    {
      InitializeComponent();
    }

    ImageList imageListM = new ImageList();
    ImageList imageListC = new ImageList();
    string[] imgfileM = new string[] { };
    string[] imgfileC = new string[] { };
    string[] menuIDM = new string[] { };
    string[] menuIDC = new string[] { };
    string[] menuNameM = new string[] { };
    string[] menuNameC = new string[] { };
    DataTable dt = new DataTable();
    private void BillPayment_Load(object sender, EventArgs e)
    {
      Array.Clear(imgfileM, 0, imgfileM.Length);
      Array.Clear(imgfileC, 0, imgfileC.Length);
      Array.Clear(menuNameM, 0, menuNameM.Length);
      Array.Clear(menuNameC, 0, menuNameC.Length);
      imageListM.Images.Clear();
      imageListM.Images.Clear();
      listView1.Items.Clear();
      dt.Rows.Clear();

      string SQL = " select mainmenu_id, submenu_id, menu_id, menu_name, menu_lang, isnull(menu_image,'images/DefaultImage.png') menu_image, menu_child, menu_parents from dbo.menumapping " +
        " where status = 'A' and mainmenu_id = " + main_menu + " and submenu_id = " + sub_menu + " and menu_lang = '" + menu_lang + "' order by menu_id ";

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
        imageListM.ImageSize = new Size(90, 70);

        imgfileM = dt.Rows.OfType<DataRow>().Select(k => k[5].ToString()).ToArray();
        foreach (string imgPathM in imgfileM)
        {          
                    try
                    {
                        imageListM.Images.Add(Image.FromFile(imgPathM));
                    }
                    catch (Exception ex)
                    {
                        imageListM.Images.Add(Image.FromFile("images/DefaultImage.png"));
                    }
                }

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

      string SQL = " select mainmenu_id, submenu_id, menu_id, menu_name, menu_lang, isnull(menu_image,'images/DefaultImage.png') menu_image, menu_child, menu_parents from dbo.menumapping " +
        " where status = 'A' and submenu_id = " + Convert.ToInt32(textMenuID.Text) + " and menu_lang = '" + menu_lang + "' order by menu_id ";

      var db = new Database();
      var ds = db.GetDataset(SQL);
      if (ds.Tables[0].Rows.Count == 0)
      {
                //MessageBox.Show("This menu not available", "Warning");
                this.textMember.Focus();
            }
      else
      {
        Array.Clear(imgfileC, 0, imgfileC.Length);
        Array.Clear(menuNameC, 0, menuNameC.Length);
        imageListC.Images.Clear();
        listView1.Items.Clear();
        dt.Rows.Clear();

        dt = ds.Tables[0];
        listView1.Items.Clear();
        listView1.View = View.LargeIcon;
        imageListC.ColorDepth = ColorDepth.Depth32Bit;
        imageListC.ImageSize = new Size(90, 70);

        sub_menu = Convert.ToInt32(ds.Tables[0].Rows[0]["mainmenu_id"].ToString());

        imgfileC = dt.Rows.OfType<DataRow>().Select(k => k[5].ToString()).ToArray();
        foreach (string imgPathC in imgfileC)
        {
                    try
                    {
                        imageListC.Images.Add(Image.FromFile(imgPathC));
                    } catch (Exception ex)
                    {
                        imageListC.Images.Add(Image.FromFile("images/DefaultImage.png"));
                    }
          
        }

        menuIDC = dt.Rows.OfType<DataRow>().Select(k => k[2].ToString()).ToArray();
        menuNameC = dt.Rows.OfType<DataRow>().Select(k => k[3].ToString()).ToArray();
        listView1.LargeImageList = imageListC;
        for (int j = 0; j < this.imageListC.Images.Count; j++)
        {
          this.listView1.Items.Add(menuIDC[j], menuNameC[j], j);
        }
      }

    }

    private void listView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

        private void btnBackMenu_Click(object sender, EventArgs e)
        {
            Array.Clear(imgfileM, 0, imgfileM.Length);
            Array.Clear(imgfileC, 0, imgfileC.Length);
            Array.Clear(menuNameM, 0, menuNameM.Length);
            Array.Clear(menuNameC, 0, menuNameC.Length);
            imageListM.Images.Clear();
            imageListM.Images.Clear();
            listView1.Items.Clear();
            dt.Rows.Clear();
            textMenuID.Text = "";
            textMenuName.Text = "";

            string SQL = " select mainmenu_id, submenu_id, menu_id, menu_name, menu_lang, isnull(menu_image,'images/DefaultImage.png') menu_image, menu_child, menu_parents from dbo.menumapping " +
              " where status = 'A' and mainmenu_id = " + main_menu + " and submenu_id = " + sub_menu + " and menu_lang = '" + menu_lang + "' order by menu_id ";

            var db = new Database();
            var ds = db.GetDataset(SQL);
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("This menu not available", "Warning");
            }
            else
            {
                dt = ds.Tables[0];
                listView1.Items.Clear();
                listView1.View = View.LargeIcon;
                imageListM.ColorDepth = ColorDepth.Depth32Bit;
                imageListM.ImageSize = new Size(90, 70);

                main_menu = Convert.ToInt32(ds.Tables[0].Rows[0]["mainmenu_id"].ToString());

                imgfileM = dt.Rows.OfType<DataRow>().Select(k => k[5].ToString()).ToArray();
                foreach (string imgPathM in imgfileM)
                {
                    try
                    {
                        imageListM.Images.Add(Image.FromFile(imgPathM));
                    }
                    catch (Exception ex)
                    {
                        imageListM.Images.Add(Image.FromFile("images/DefaultImage.png"));
                    }
                }

                menuIDM = dt.Rows.OfType<DataRow>().Select(k => k[2].ToString()).ToArray();
                menuNameM = dt.Rows.OfType<DataRow>().Select(k => k[3].ToString()).ToArray();
                listView1.LargeImageList = imageListM;
                for (int j = 0; j < this.imageListM.Images.Count; j++)
                {
                    this.listView1.Items.Add(menuIDM[j], menuNameM[j], j);
                }
            }
        }

        private void btnBigService_Click(object sender, EventArgs e)
        {
            this.Close();
            Form BigService = new frmBigService();
            BigService.ShowDialog();
        }
    }
}

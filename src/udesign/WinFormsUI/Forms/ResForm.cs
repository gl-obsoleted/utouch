using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using udesign;
using ulib;
using ulib.Base;

using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using System.Drawing.Imaging;

namespace udesign
{
    public partial class ResForm : Form
    {
        public ResForm()
        {
            InitializeComponent();
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void ResForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                m_metroTilePanel.Items.Clear();
                AddResGroup(ResourceManager.Instance.DefaultResGroup.ResFilePath, ResourceManager.Instance.DefaultResGroup);
                foreach (var pair in ResourceManager.Instance.ResGroups)
                {
                    AddResGroup(pair.Key, pair.Value);
                }
            }
        }

        private void AddResGroup(string name, ImageResourceGroup group)
        {
            Image img = LoadResourceImageFile(group.ResFilePath);
            if (img == null)
            {
                Session.Log("加载资源文件 '{0}' 失败。", group.ResFilePath);
                return;
            }

            ItemContainer ic = new ItemContainer();
            ic.MultiLine = true;
            ic.TitleText = name;
            ic.Name = name;
            foreach (var res in group.ResLut)
            {
                MetroTileItem tile = new MetroTileItem(res.Key, res.Key);
                tile.TitleText = res.Key;
                tile.Text = res.Key.ToLower();
                tile.Image = GetAtlasThumbnail(img, res.Value, tile.TileSize);
                tile.Click += Tile_Clicked;
                tile.DoubleClick += Tile_DoubleClicked;
                tile.ContainerControl = ic;
                ic.SubItems.Add(tile);
            }
            m_metroTilePanel.Items.Add(ic);
        }

        private void Tile_Clicked(object sender, EventArgs e)
        {
            MetroTileItem ti = sender as MetroTileItem;
            if (ti != null && ti.Parent != null)
            {
                ItemContainer ic = ti.Parent as ItemContainer;
                if (ic != null)
                {
                    m_selectedResourceURL = BaseUtil.ComposeResURL(ic.Name, ti.Name);
                }
            }
        }

        private void Tile_DoubleClicked(object sender, EventArgs e)
        {
            MetroTileItem ti = sender as MetroTileItem;
            if (ti != null && ti.Parent != null)
            {
                ItemContainer ic = ti.Parent as ItemContainer;
                if (ic != null)
                {
                    m_selectedResourceURL = BaseUtil.ComposeResURL(ic.Name, ti.Name);
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                }
            }
        }

        public string SelectedResourceURL { get { return m_selectedResourceURL; } }
        private string m_selectedResourceURL;

        bool GetThumbnailImageAbort()
        {
            return true;
        }

        private Image LoadResourceImageFile(string resPath)
        {
            string imageFile = resPath + Constants.ResImageFilePostfix;
            Image img = Image.FromFile(imageFile);
            return img;
        }

        private Image GetAtlasThumbnail(Image sourceImage, ImageResource ir, Size targetClampSize)
        {
            // 创建 atlas 预览小图
            Bitmap bmp32bppArgb = new Bitmap(ir.Size.Width, ir.Size.Height, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(bmp32bppArgb))
            {
                g.DrawImage(sourceImage,
                    new Rectangle(0, 0, ir.Size.Width, ir.Size.Height),
                    new Rectangle(ir.Position.X, ir.Position.Y, ir.Size.Width, ir.Size.Height), GraphicsUnit.Pixel);
            }

            // 判断是否需要缩放
            float widthScale = (float)targetClampSize.Width / (float)bmp32bppArgb.Width;
            float heightScale = (float)targetClampSize.Height / (float)bmp32bppArgb.Height;
            if (widthScale >= 1.0f && heightScale >= 1.0f)
                return bmp32bppArgb;

            // 缩放
            float downScale = System.Math.Min(widthScale, heightScale);
            float scaledWidth = System.Math.Max((float)bmp32bppArgb.Width * downScale, 1.0f);
            float scaledHeight = System.Math.Max((float)bmp32bppArgb.Height * downScale, 1.0f);
            IntPtr ip = System.IntPtr.Zero;
            return bmp32bppArgb.GetThumbnailImage((int)scaledWidth, (int)scaledHeight, GetThumbnailImageAbort, ip);
        }

        Size m_hidden = new Size(0, 0);
        Size m_show = new Size(180, 90);
        private void m_searchBox_TextChanged(object sender, EventArgs e)
        {
            string text = m_searchBox.Text.ToLower();   // force lowering to make case-insensitive comparing faster

            foreach (var item in m_metroTilePanel.Items)
            {
                ItemContainer ic = item as ItemContainer;
                if (ic != null)
                {
                    foreach (var tile in ic.SubItems)
                    {
                        MetroTileItem mt = tile as MetroTileItem;
                        if (mt != null)
                        {
                            if (text.Length == 0)
                            {
                                mt.TileSize = m_show;
                            }
                            else
                            {
                                if (mt.Text.Contains(text))
                                {
                                    mt.TileSize = m_show;
                                }
                                else
                                {
                                    mt.TileSize = m_hidden;
                                }
                            }
                        }
                    }
                }
            }

            m_metroTilePanel.Refresh();
        }

        private void ResForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedResourceURL) && DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                Session.Message("请先选中一个图片，或点击 Cancel 撤销本次操作。");
                e.Cancel = true;
            }
        }
    }
}

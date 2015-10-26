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
using System.IO;
using ucore;

namespace udesign
{
    public partial class ResForm : Form
    {
        public ResForm()
        {
            InitializeComponent();
            DialogResult = System.Windows.Forms.DialogResult.Cancel;

            m_nineGridSettings.BorderChanged += m_nineGridSettings_BorderChanged;
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
                Logging.Instance.Log("加载资源文件 '{0}' 失败。", group.ResFilePath);
                return;
            }

            ItemContainer ic = new ItemContainer();
            ic.MultiLine = true;
            ic.TitleText = name;
            ic.Name = name;
            ic.Tag = group;
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

        private bool RefreshSelectedTile(MetroTileItem ti)
        {
            if (ti == null || ti.Parent == null)
                return false;

            ItemContainer ic = ti.Parent as ItemContainer;
            if (ic == null)
                return false;

            ImageResourceGroup group = ic.Tag as ImageResourceGroup;
            if (group == null)
                return false;

            Image img = LoadResourceImageFile(group.ResFilePath);
            ImageResource ir;
            if (group.ResLut.TryGetValue(ti.Name, out ir))
            {
                m_previewImage.Image = GetAtlasThumbnail(img, ir, m_previewImage.Size);
            }

            m_nineGridSettings.RefreshUI(ic.Name, ti.Name, ir.Size);
            UpdateSelectedResourceURL(ic.Name, ti.Name, ir.Size);
            return true;
        }

        private void Tile_Clicked(object sender, EventArgs e)
        {
            MetroTileItem ti = sender as MetroTileItem;
            RefreshSelectedTile(ti);
        }

        private void Tile_DoubleClicked(object sender, EventArgs e)
        {
            MetroTileItem ti = sender as MetroTileItem;
            if (RefreshSelectedTile(ti))
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        private void UpdateSelectedResourceURL(string fileName, string tileName, Size s)
        {
            string newURL = ResProtocol.ComposeURL(fileName, tileName);
            m_selectedResourceURL = newURL;
            m_imgFullQualifiedLocation.Text = string.Format("尺寸: {0},{1} 路径: {2}", s.Width, s.Height, newURL);
        }

        public string SelectedResourceURL { get { return m_selectedResourceURL; } }
        private string m_selectedResourceURL;

        bool GetThumbnailImageAbort()
        {
            return true;
        }

        private Image LoadResourceImageFile(string resPath)
        {
            string imageFile = resPath + ResProtocol.ResImageFilePostfix;
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
                Logging.Instance.Message("请先选中一个图片，或点击 Cancel 撤销本次操作。");
                e.Cancel = true;
            }
        }

        private int m_borderLeft;
        private int m_borderRight;
        private int m_borderTop;
        private int m_borderBottom;
        private Size m_originalTileSize;
        void m_nineGridSettings_BorderChanged(int left, int right, int top, int bottom, Size originalTileSize)
        {
            m_borderLeft = left;
            m_borderRight = right;
            m_borderTop = top;
            m_borderBottom = bottom;
            m_originalTileSize = originalTileSize;
            m_previewImage.Refresh();
        }

        private void m_previewImage_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = this.ClientRectangle;
            if (m_previewImage.Image == null)
                return;
                    
            rect.Size = m_previewImage.Image.Size;

            using(Pen pen = new Pen(Color.LightGray, 3))
            {
                if (m_borderLeft != 0)
                {
                    int scaledLeft = (int)((float)m_borderLeft * ((float)rect.Size.Width / (float)m_originalTileSize.Width));
                    e.Graphics.DrawLine(pen, new Point(scaledLeft, rect.Top), new Point(scaledLeft, rect.Bottom));
                }
                if (m_borderRight != 0)
                {
                    int scaledRight = (int)((float)m_borderRight * ((float)rect.Size.Width / (float)m_originalTileSize.Width));
                    e.Graphics.DrawLine(pen, new Point(scaledRight, rect.Top), new Point(scaledRight, rect.Bottom));
                }
                if (m_borderTop != 0)
                {
                    int scaledTop = (int)((float)m_borderTop * ((float)rect.Size.Height / (float)m_originalTileSize.Height));
                    e.Graphics.DrawLine(pen, new Point(rect.Left, scaledTop), new Point(rect.Right, scaledTop));
                }
                if (m_borderBottom != 0)
                {
                    int scaledBottom = (int)((float)m_borderBottom * ((float)rect.Size.Height / (float)m_originalTileSize.Height));
                    e.Graphics.DrawLine(pen, new Point(rect.Left, scaledBottom), new Point(rect.Right, scaledBottom));
                }
            }
        }
    }
}

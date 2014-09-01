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
                tile.Image = GetAtlasThumbnail(img, res.Value, tile.TileSize);
                tile.DoubleClick += Tile_DoubleClicked;
                ic.SubItems.Add(tile);
            }
            m_metroTilePanel.Items.Add(ic);
        }

        private void Tile_DoubleClicked(object sender, EventArgs e)
        {
            MetroTileItem ti = sender as MetroTileItem;
            if (ti != null && ti.Parent != null)
            {
                ItemContainer ic = ti.Parent as ItemContainer;
                if (ic != null)
                {
                    string newLoc = BaseUtil.ComposeResURL(ic.Name, ti.Name);
                    Clipboard.SetText(newLoc);
                }
            }
        }

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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ulib.Base;
using System.IO;
using Newtonsoft.Json.Linq;
using ulib;
using ucore;

namespace udesign
{
    public delegate void BorderChangeHandler(int left, int right, int top, int bottom, Size originalTileSize);

    public partial class UINineGridSettings : UserControl
    {
        private const int STRETCH_HALF_SIZE = 5;

        public UINineGridSettings()
        {
            InitializeComponent();
        }

        string m_atlasFileName;
        string m_atlasTileName;
        Size m_atlasTileSize;

        public void RefreshUI(string atlasFileName, string atlasTileName, Size atlasTileSize)
        {
            ResetHoriSettingsToDefault();
            ResetVertSettingsToDefault();

            m_atlasFileName = atlasFileName;
            m_atlasTileName = atlasTileName;
            m_atlasTileSize = atlasTileSize;

            string descFilePath = atlasFileName + Constants.ResDescFilePostfix;
            if (File.Exists(descFilePath))
            {
                JObject jobj = ucore.JsonHelpers.ReadTextIntoJObject(descFilePath);
                JProperty jprop = jobj.Property(atlasTileName);
                if (jprop != null)
	            {
                    JObject jsub = jprop.Value as JObject;
                    try
                    {
                        if (jsub.Property("horiLeft") != null && jsub.Property("horiRight") != null)
                        {
                            int horiLeft = int.Parse((string)jsub["horiLeft"]);
                            int horiRight = int.Parse((string)jsub["horiRight"]);
                            m_enable9gridHori.Checked = true;
                            m_9gridHoriPanel.Enabled = true;
                            m_9gridHoriLeft.Text = horiLeft.ToString();
                            m_9gridHoriRight.Text = horiRight.ToString();
                        }
                        if (jsub.Property("vertTop") != null && jsub.Property("vertBottom") != null)
                        {
                            int vertTop = int.Parse((string)jsub["vertTop"]);
                            int vertBottom = int.Parse((string)jsub["vertBottom"]);
                            m_enable9gridVert.Checked = true;
                            m_9gridVertPanel.Enabled = true;
                            m_9gridVertTop.Text = vertTop.ToString();
                            m_9gridVertBottom.Text = vertBottom.ToString();
                        }
                    }
                    catch (Exception e)
                    {
                        // 这里不高兴处理各种琐碎的错误了，要是格式不符合就直接跳过吧
                        // 不过问题还是记在 log 里了，想看还是可以看的
                        Logging.Instance.LogExceptionDetail(e);

                        ResetHoriSettingsToDefault();
                        ResetVertSettingsToDefault();
                    }
                }
            }
        }

        private void ResetHoriSettingsToDefault()
        {
            m_enable9gridHori.Checked = false;
            m_9gridHoriPanel.Enabled = false;
            m_9gridHoriLeft.Text = "0";
            m_9gridHoriRight.Text = "0";
        }

        private void ResetVertSettingsToDefault()
        {
            m_enable9gridVert.Checked = false;
            m_9gridVertPanel.Enabled = false;
            m_9gridVertTop.Text = "0";
            m_9gridVertBottom.Text = "0";
        }

        private void m_enable9gridHori_CheckedChanged(object sender, EventArgs e)
        {
            if (m_enable9gridHori.Checked)
            {
                m_9gridHoriPanel.Enabled = true;
                if (m_9gridHoriLeft.Text == "0" && m_9gridHoriRight.Text == "0")
                {
                    GenerateHoriValues();
                }
            }
            else
            {
                ResetHoriSettingsToDefault();
            }

            OnBorderChanged();
        }

        private void m_enable9gridVert_CheckedChanged(object sender, EventArgs e)
        {
            if (m_enable9gridVert.Checked)
            {
                m_9gridVertPanel.Enabled = true;
                if (m_9gridVertTop.Text == "0" && m_9gridVertBottom.Text == "0")
                {
                    GenerateVertValues();
                }
            }
            else
            {
                ResetVertSettingsToDefault();
            }

            OnBorderChanged();
        }

        private void m_btSetToDefaultHori_Click(object sender, EventArgs e)
        {
            GenerateHoriValues();
            OnBorderChanged();
        }

        private void m_btSetToDefaultVert_Click(object sender, EventArgs e)
        {
            GenerateVertValues();
            OnBorderChanged();
        }

        private void GenerateHoriValues()
        {
            m_9gridHoriLeft.Text = (m_atlasTileSize.Width / 2 - STRETCH_HALF_SIZE).ToString();
            m_9gridHoriRight.Text = (m_atlasTileSize.Width / 2 + STRETCH_HALF_SIZE).ToString();
        }

        private void GenerateVertValues()
        {
            m_9gridVertTop.Text = (m_atlasTileSize.Height / 2 - STRETCH_HALF_SIZE).ToString();
            m_9gridVertBottom.Text = (m_atlasTileSize.Height / 2 + STRETCH_HALF_SIZE).ToString();
        }

        public event BorderChangeHandler BorderChanged;

        private void OnBorderChanged()
        {
            try
            {
                var h = BorderChanged;
                if (h != null)
                {
                    h(int.Parse(m_9gridHoriLeft.Text), int.Parse(m_9gridHoriRight.Text),
                        int.Parse(m_9gridVertTop.Text), int.Parse(m_9gridVertBottom.Text), m_atlasTileSize);
                }
            }
            catch (Exception)
            {
            }

            JObject jobj;
            string descFilePath = m_atlasFileName + Constants.ResDescFilePostfix;
            if (File.Exists(descFilePath))
            {
                jobj = ucore.JsonHelpers.ReadTextIntoJObject(descFilePath);
            }
            else
            {
                jobj = new JObject();
            }

            JProperty jprop = jobj.Property(m_atlasTileName);
            if (jprop == null)
            {
                jobj.Add(m_atlasTileName, new JObject());
                jprop = jobj.Property(m_atlasTileName);
            }


            {
                JObject jsub = jprop.Value as JObject;
                try
                {
                    if (m_9gridHoriLeft.Text != "0")
                    {
                        if (jsub.Property("horiLeft") == null)
                        {
                            jsub.Add("horiLeft", m_9gridHoriLeft.Text);
                        }
                        else
                        {
                            jsub["horiLeft"] = m_9gridHoriLeft.Text;
                        }
                    }
                    if (m_9gridHoriRight.Text != "0")
                    {
                        if (jsub.Property("horiRight") == null)
                        {
                            jsub.Add("horiRight", m_9gridHoriRight.Text);
                        }
                        else
                        {
                            jsub["horiRight"] = m_9gridHoriRight.Text;
                        }
                    }
                    if (m_9gridVertTop.Text != "0")
                    {
                        if (jsub.Property("vertTop") == null)
                        {
                            jsub.Add("vertTop", m_9gridVertTop.Text);
                        }
                        else
                        {
                            jsub["vertTop"] = m_9gridVertTop.Text;
                        }
                    }
                    if (m_9gridVertBottom.Text != "0")
                    {
                        if (jsub.Property("vertBottom") == null)
                        {
                            jsub.Add("vertBottom", m_9gridVertBottom.Text);
                        }
                        else
                        {
                            jsub["vertBottom"] = m_9gridVertBottom.Text;
                        }
                    }

                    File.WriteAllText(descFilePath, jobj.ToString());
                }
                catch (Exception e)
                {
                    // 这里不高兴处理各种琐碎的错误了，要是格式不符合就直接跳过吧
                    // 不过问题还是记在 log 里了，想看还是可以看的
                    Logging.Instance.LogExceptionDetail(e);

                    ResetHoriSettingsToDefault();
                    ResetVertSettingsToDefault();
                }
            }
        }

        private void m_EditValidating(object sender, CancelEventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                int value = int.Parse(tb.Text);
            }
            catch (Exception)
            {
                e.Cancel = true;
            }
        }

        private void m_EditValidated(object sender, EventArgs e)
        {
            OnBorderChanged();
        }

        private void m_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                int value = int.Parse(tb.Text);

                OnBorderChanged();
            }
            catch (Exception)
            {
            }
        }
    }
}

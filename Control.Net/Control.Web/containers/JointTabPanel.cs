using System.Web.UI;

namespace Control.Web
{
    /// <summary>
    /// Tab组控件
    /// </summary>
    [ParseChildren(false)]
    [PersistChildren(true)]
    [ToolboxData("<{0}:JointTabPanel runat=server></{0}:JointTabPanel>")]
    public class JointTabPanel : WebControlsBase
    {
        public JointTabPanel()
            : base()
        {
            this.AllowClose = false;
            this.FirstCloseable = true;
            this.ControlType = ControlsType.JointTabPanel;
            this.Width = "98%";
            this.Height = "98%";
            this.DefaultOpenTab = 0;
            this.LaySkin = "layui-tab-card";
        }

        #region 控件属性

        /// <summary>
        /// Tab宽度，默认100%
        /// </summary>
        public new string Width { get; set; }

        /// <summary>
        /// Tab高度，默认100%
        /// </summary>
        public new string Height { get; set; }

        /// <summary>
        /// 标签页标题
        /// 标题1^标题2^标题三^
        /// </summary>
        public string TabTitles { get; set; }

        /// <summary>
        /// 特殊选项卡皮肤风格。默认值为空
        /// 简洁风格(layui-tab-brief)
        /// 卡片风格(layui-tab-card)
        /// </summary>
        public string LaySkin { get; set; }

        /// <summary>
        /// 是否允许关闭选项卡。默认值为false
        /// </summary>
        public bool AllowClose { get; set; }

        /// <summary>
        /// 第一项是否允许关闭，默认值为true。
        /// 用于特殊情况，开启允许关闭模式，但是第一个选项卡不想关闭的情况
        /// </summary>
        public bool FirstCloseable { get; set; }

        /// <summary>
        /// 默认打开选项卡名称，默认为第一个选项卡
        /// </summary>
        public int DefaultOpenTab { get; set; }

        #endregion

        /// <summary>
        /// 选项卡激活改变后触发事件
        /// </summary>
        public string OnTabChange { get; set; }

        /// <summary>
        /// 重写控件前标志
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-tab" + (string.IsNullOrEmpty(this.LaySkin) ? "" : " " + this.LaySkin));
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Width);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            //writer.AddAttribute("lay-allowClose", this.AllowClose ? "true" : "false");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-tab-title");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_ul");
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
            for (int cc = 0; cc < this.TabTitles.Split('^').Length; cc++)
            {
                if (this.DefaultOpenTab == cc)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-this");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write(this.TabTitles.Split('^')[cc]);
                if (this.AllowClose)
                {
                    if (cc != 0 || this.FirstCloseable)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-icon layui-unselect layui-tab-close");
                        writer.RenderBeginTag(HtmlTextWriterTag.I);
                        writer.Write("ဆ");
                        writer.RenderEndTag();
                    }
                }
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_div");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-tab-content");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "100%");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        //重新控件后标志
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.Write("<script type='text/javascript' language='javascript'>JointTabPanel.Init('" + this.ClientID + "');</script>");
        }
    }
}

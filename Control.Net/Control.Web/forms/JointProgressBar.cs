using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// 进度条控件，主要用于动态的js控制
    /// </summary>
    [ToolboxData("<{0}:JointProgressBar runat=server></{0}:JointProgressBar>")]
    public class JointProgressBar : FormsControlBase
    {
        public JointProgressBar()
            : base()
        {
            this.CssClass = "layui-progress";
            this.ControlType = ControlsType.JointProgressBar;
            this.SizeMode = LayProgressSize.empty;
            this.BgColor = LayBgColor.green;
            this.LabelText = "";
            this.LabelWidth = 0;
            this.ShowPercent = true;
            this.InitPercent = 0;
        }

        #region 控件属性

        /// <summary>
        /// 背景色
        /// </summary>
        public LayBgColor BgColor { get; set; }

        /// <summary>
        /// 尺寸大小
        /// </summary>
        public LayProgressSize SizeMode { get; set; }

        /// <summary>
        /// 是否显示百分比的文本
        /// </summary>
        public bool ShowPercent { get; set; }

        /// <summary>
        /// 初始百分比
        /// </summary>
        public int InitPercent { get; set; }

        #endregion

        /// <summary>
        /// 标签控件的输出
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderLabelOut(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-form-label");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_Label");
            if (string.IsNullOrEmpty(this.LabelColor) && this.IsRequired)
                writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "red");
            else if (!string.IsNullOrEmpty(this.LabelColor))
                writer.AddStyleAttribute(HtmlTextWriterStyle.Color, this.LabelColor);
            writer.AddAttribute("for", this.ClientID);
            writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, this.LabelAlign);
            if (!this.IsShowTable)
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.LabelWidth.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            if (!string.IsNullOrEmpty(this.LabelText))
                writer.Write(this.LabelText + "：");
            writer.RenderEndTag();
        }

        /// <summary>
        /// 重写文本输入框的输出
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderTextBoxOut(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-progress" + (this.SizeMode == LayProgressSize.big ? " layui-progress-big" : ""));
            if (this.ShowPercent)
                writer.AddAttribute("lay-showPercent", "yes");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, ((Unit)(this.Width.Value - this.LabelWidth.Value)).ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-progress-bar layui-bg-" + this.BgColor.ToString());
            if (this.InitPercent > 0)
            {
                writer.AddAttribute("lay-percent", this.InitPercent + "%");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.InitPercent + "%");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (this.ShowPercent)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-progress-text");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(this.InitPercent + "%");
                writer.RenderEndTag();
            }
            writer.RenderEndTag();
            writer.RenderEndTag();
        }
    }
}

using System.Web.UI;

namespace Control.Web
{
    /// <summary>
    /// 换行分割线
    /// </summary>
    [ToolboxData("<{0}:JointSplitLine runat=server></{0}:JointSplitLine>")]
    public class JointSplitLine : WebControlsBase
    {
        public JointSplitLine()
            : base()
        {
            this.ControlType = ControlsType.JointSplitLine;
            this.BgStyle = LineStyle.empty;
        }

        /// <summary>
        /// 分割线风格,默认无
        /// </summary>
        public LineStyle BgStyle { get; set; }

        /// <summary>
        /// 控件输出
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.BgStyle != LineStyle.empty)
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-bg-" + this.BgStyle.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Hr);
            writer.RenderEndTag();
        }
    }
}

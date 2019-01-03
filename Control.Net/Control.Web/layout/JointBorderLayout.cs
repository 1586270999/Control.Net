using System.Web.UI;

namespace Control.Web
{
    /// <summary>
    /// 布局控件(外部Table)
    /// </summary>
    [ParseChildren(false)]
    [PersistChildren(true)]
    [ToolboxData("<{0}:JointBorderLayout runat=server></{0}:JointBorderLayout>")]
    public class JointBorderLayout : WebControlsBase
    {
        public JointBorderLayout()
            : base()
        {
            this.ControlType = ControlsType.JointBorderLayout;
        }

        /// <summary>
        /// 重写控件的头部标签输出
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-auto");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        /// <summary>
        /// 重写控件的尾部标签输出
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
        }
    }
}

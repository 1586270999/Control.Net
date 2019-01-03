using System.Web.UI;

namespace Control.Web
{
    /// <summary>
    /// 手风琴控件，可折叠模板
    /// </summary>
    [ParseChildren(false)]
    [PersistChildren(true)]
    [ToolboxData("<{0}:JointAccordion runat=server></{0}:JointAccordion>")]
    public class JointAccordion : WebControlsBase
    {
        public JointAccordion()
            : base()
        {
            this.ControlType = ControlsType.JointAccordion;
            this.IsAccordion = false;
        }

        /// <summary>
        /// 是否启用手风琴模式，默认false
        /// </summary>
        public bool IsAccordion { get; set; }

        /// <summary>
        /// 重写控件的头部标签输出
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-collapse");
            if (this.IsAccordion)
                writer.AddAttribute("accordion", "true");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
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

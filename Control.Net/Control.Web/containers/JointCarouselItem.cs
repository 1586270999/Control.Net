using System.Web.UI;

namespace Control.Web
{
    /// <summary>
    /// 轮播明细项控件
    /// </summary>
    [ParseChildren(false)]
    [PersistChildren(true)]
    [ToolboxData("<{0}:JointCarouselItem runat=server></{0}:JointCarouselItem>")]
    public class JointCarouselItem : WebControlsBase
    {
        public JointCarouselItem()
            : base()
        {
            this.ControlType = ControlsType.JointCarouselItem;
        }

        /// <summary>
        /// 是否默认显示
        /// </summary>
        public bool IsDefaultShow { get; set; }

        /// <summary>
        /// 重写控件的头部标签输出
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            if (this.IsDefaultShow)
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-this");
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

using System.Web.UI;

namespace Control.Web
{
    /// <summary>
    /// TabPanel明细项控件
    /// </summary>
    [ParseChildren(false)]
    [PersistChildren(true)]
    [ToolboxData("<{0}:JointTabPanelItem runat=server></{0}:JointTabPanelItem>")]
    public class JointTabPanelItem : WebControlsBase
    {
        public JointTabPanelItem()
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
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-tab-item layui-show");
            else
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-tab-item");
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

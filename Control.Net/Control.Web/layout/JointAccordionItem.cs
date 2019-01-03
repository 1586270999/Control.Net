using System.Web.UI;

namespace Control.Web
{
    /// <summary>
    /// 折叠模板明细项
    /// </summary>
    [ParseChildren(false)]
    [PersistChildren(true)]
    [ToolboxData("<{0}:JointAccordionItem runat=server></{0}:JointAccordionItem>")]
    public class JointAccordionItem : WebControlsBase
    {
        public JointAccordionItem()
            : base()
        {
            this.ControlType = ControlsType.JointAccordionItem;
            this.IsDefaultShow = false;
        }

        #region 控件属性

        /// <summary>
        /// 是否默认显示
        /// </summary>
        public bool IsDefaultShow { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        #endregion

        /// <summary>
        /// 重写控件的头部标签输出
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-colla-item");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-colla-title");
            writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "fnShowAndHideAccord(this);");
            writer.RenderBeginTag(HtmlTextWriterTag.H2);  //<h2>            
            writer.Write(this.Title);
            if (this.IsDefaultShow)
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-icon layui-colla-icon layui-icon-down");
            else
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-icon layui-colla-icon layui-icon-right");
            writer.RenderBeginTag(HtmlTextWriterTag.I);
            writer.RenderEndTag();
            writer.RenderEndTag();  //</h2>
            if (this.IsDefaultShow)
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-colla-content layui-show");
            else
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-colla-content");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        /// <summary>
        /// 重写控件的尾部标签输出
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
            writer.RenderEndTag();
        }
    }
}

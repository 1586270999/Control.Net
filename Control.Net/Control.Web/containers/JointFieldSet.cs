using System.Web.UI;

namespace Control.Web
{
    /// <summary>
    /// 带标题的字段集区块控件
    /// </summary>
    [ParseChildren(false)]
    [PersistChildren(true)]
    [ToolboxData("<{0}:JointFieldSet runat=server></{0}:JointFieldSet>")]
    public class JointFieldSet : WebControlsBase
    {
        public JointFieldSet()
            : base()
        {
            this.ControlType = ControlsType.JointFieldSet;
        }

        /// <summary>
        /// 块标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 重写控件的头部标签输出
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-elem-field");
            writer.RenderBeginTag(HtmlTextWriterTag.Fieldset); //<fieldset>
            writer.RenderBeginTag(HtmlTextWriterTag.Legend); //<legend>
            writer.Write(this.Title);
            writer.RenderEndTag(); //</legend>
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-field-box");
            writer.RenderBeginTag(HtmlTextWriterTag.Div); //<div>
        }

        /// <summary>
        /// 重写控件的尾部标签输出
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.RenderEndTag(); //<div>
            writer.RenderEndTag(); //</fieldset>
        }
    }
}

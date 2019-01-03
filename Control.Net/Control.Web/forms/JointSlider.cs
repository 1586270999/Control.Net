using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// 滑动条控件(集成Label标签控件)
    /// </summary>
    [ToolboxData("<{0}:JointSlider runat=server></{0}:JointSlider>")]
    public class JointSlider : FormsControlBase
    {
        public JointSlider()
            : base()
        {
            this.Width = 600;
            this.ControlType = ControlsType.JointSlider;
        }

        /// <summary>
        /// 标签控件的输出
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderLabelOut(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-form-label");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_Label");
            if (string.IsNullOrEmpty(this.LabelColor) && this.IsRequired)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "red");
            }
            else if (!string.IsNullOrEmpty(this.LabelColor))
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Color, this.LabelColor);
            }
            writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, this.LabelAlign);

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
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-slider");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddStyleAttribute(HtmlTextWriterStyle.MarginLeft, ((Unit)(this.LabelWidth.Value)).ToString());
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, ((Unit)(this.Width.Value - this.LabelWidth.Value)).ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-slider-way");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_way");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-slider-bar");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_bar");
            if (!string.IsNullOrEmpty(this.DefaultValue))
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.DefaultValue + "%");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-slider-wrap");
            if (this.ReadOnly)
                writer.AddAttribute("read", "true");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_wrap");
            if (!string.IsNullOrEmpty(this.DefaultValue))
                writer.AddStyleAttribute(HtmlTextWriterStyle.Left, this.DefaultValue + "%");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-slider-btn layui-tooltip");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-slider-pop");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_tip");

            writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.Write(this.DefaultValue);
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-slider-poprow");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.Write("<script type='text/javascript' language='javascript'>" + this.ClientInitScript + "</script>");
        }

        /// <summary>
        /// 控件对应的JS变量输出
        /// </summary>
        private string ClientInitScript
        {
            get
            {
                return string.Format(@"var {0}_wrap=document.getElementById('{0}_wrap');
var {0}_bar=document.getElementById('{0}_bar');
var {0}_way=document.getElementById('{0}_way');
var {0}_tip=document.getElementById('{0}_tip');
{0}_wrap.addEventListener('mousedown',function(e){1}
  document.onmousemove = function(e){1}
    e = e || window.event;fnSetSliderMoveState(e, {0}_way, {0}_wrap, {0}_bar, {0}_tip);
  {2}
{2});
{0}_way.addEventListener('mousedown', function(e){1}
  e = e || window.event;
  fnSetSliderMoveState(e, {0}_way, {0}_wrap, {0}_bar, {0}_tip);
{2});
{0}_wrap.addEventListener('mouseover', function(e){1}
  {0}_tip.style.left = {0}_wrap.style.left;
  {0}_tip.style.display = 'block';
{2});",
                    this.ClientID, "{", "}");
            }
        }
    }
}

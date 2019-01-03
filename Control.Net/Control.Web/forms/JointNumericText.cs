using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// 数字录入控件(集成Label标签控件)
    /// </summary>
    [ToolboxData("<{0}:JointNumericText runat=server></{0}:JointNumericText>")]
    public class JointNumericText : JointTextField
    {
        public JointNumericText()
            : base()
        {
            this.AutoSwithIME = false;
            this.CssClass = "layui-input";
            this.PassWord = false;
            this.MultiLine = false;
            this.ControlType = ControlsType.JointNumericText;
            this.AllowLessZero = true;
            this.IsFloat = true;
            this.Precision = 0;
            this.OnFocus = "this.select();";
            this.OnKeyUp = "JointManage.FocusToNextInput();CheckIsNumberChar(this);";
            this.OnBlur = "CheckIsNumberChar(this);";
            this.IsEnablePaste = false;
            this.ShowSpinner = false;
        }

        /// <summary>
        /// 是否允许小于零
        /// </summary>
        public bool AllowLessZero { get; set; }

        /// <summary>
        /// 精度
        /// </summary>
        public int Precision { get; set; }

        /// <summary>
        /// 是否是浮点数，即是否可以有小数点
        /// </summary>
        public bool IsFloat { get; set; }

        /// <summary>
        /// 是否启用黏贴功能
        /// </summary>
        public bool IsEnablePaste { get; set; }

        /// <summary>
        /// 是否显示Spinner，默认为false
        /// </summary>
        public bool ShowSpinner { get; set; }

        /// <summary>
        /// 重写属性添加方法
        /// </summary>
        /// <param name="writer"></param>
        protected override void AddAttributeToText(HtmlTextWriter writer)
        {
            //if (this.Precision > 0)
            //    this.OnChange += string.Format("SuccNumericText.SetValue('{0}', $('#{0}').val());", this.ClientID);

            base.AddAttributeToText(writer);
            writer.AddAttribute("lesszero", this.AllowLessZero ? "true" : "false");
            writer.AddAttribute("isfloat", this.IsFloat ? "true" : "false");
            writer.AddAttribute("precision", this.Precision.ToString());
            writer.AddAttribute("ondragenter", "return false;");
            if (!this.IsEnablePaste)
            {
                writer.AddAttribute("onpaste", "return false;"); //禁用黏贴
            }
        }

        /// <summary>
        /// 重写往CSS属性添加方法
        /// </summary>
        /// <param name="writer"></param>
        protected override void AddStyleAttributeToText(HtmlTextWriter writer)
        {
            base.AddStyleAttributeToText(writer);
            writer.AddStyleAttribute("ime-mode", "disabled");
            if (!this.IsFloat && this.ShowSpinner)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "20px;");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, ((Unit)(this.Width.Value - this.LabelWidth.Value - 35)).ToString());
            }
        }

        /// <summary>
        /// 扩展控件输出,用于显示Spinner
        /// </summary>
        /// <param name="writer"></param>
        protected override void AddControlToInput(HtmlTextWriter writer)
        {
            if (!this.IsFloat && this.ShowSpinner)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-slider-input-btn");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-icon layui-icon-up");
                writer.AddAttribute("onclick", "fnAddOneByOne(this);");
                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag();
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-icon layui-icon-down");
                writer.AddAttribute("onclick", "fnDecOneByOne(this);");
                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
        }

        /// <summary>
        /// 重写文本输入框的输出(简单模式，用于表格里编辑使用)
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderTextBoxOutSimple(HtmlTextWriter writer)
        {
            this.LabelWidth = 0;
            base.LoadControlCssStyle(writer);
            if (this.ReadOnly)
            {
                writer.AddAttribute("readonly", "readonly");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-input-readonly");
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
            }

            base.LoadCommonAttribute(writer);

            writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, "99999");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            if (!string.IsNullOrEmpty(this.DefaultValue))
            {
                writer.AddAttribute("value", this.DefaultValue);
            }

            this.AddAttributeToText(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute("clstype", ControlType.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }
    }
}

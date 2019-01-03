using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// 带单位的文本框控件(集成Label标签控件)
    /// </summary>
    [ToolboxData("<{0}:JointUnitField runat=server></{0}:JointUnitField>")]
    public class JointUnitField : JointTextField
    {
        public JointUnitField()
            : base()
        {
            this.AutoSwithIME = false;
            this.CssClass = "layui-input";
            this.PassWord = false;
            this.MultiLine = false;
            this.ControlType = ControlsType.JointUnitField;
            this.EnableNumInput = false;
            this.AllowLessZero = true;
            this.IsFloat = true;
            this.Precision = 0;
            this.IsEnablePaste = false;
            this.UnitWidth = 31;
        }

        #region 控件属性

        /// <summary>
        /// 是否启用数字录入功能
        /// </summary>
        public bool EnableNumInput { get; set; }

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
        /// 单位文本宽度
        /// </summary>
        public Unit UnitWidth { get; set; }

        /// <summary>
        /// 单位文本颜色
        /// </summary>
        public string UnitColor { get; set; }

        /// <summary>
        /// 单位文本字体大小
        /// </summary>
        public string UnitFontSize { get; set; }

        /// <summary>
        /// 单位显示文本
        /// </summary>
        public string UnitText { get; set; }

        #endregion

        /// <summary>
        /// 重写属性添加方法
        /// </summary>
        /// <param name="writer"></param>
        protected override void AddAttributeToText(HtmlTextWriter writer)
        {
            base.AddStyleAttributeToText(writer);
            if (this.EnableNumInput)
            {
                //if (this.Precision > 0)
                //    this.OnChange += string.Format("SuccNumericText.SetValue('{0}', $('#{0}').val());", this.ClientID);

                base.AddAttributeToText(writer);
                writer.AddAttribute("lesszero", this.AllowLessZero ? "true" : "false");
                writer.AddAttribute("isfloat", this.IsFloat ? "true" : "false");
                writer.AddAttribute("precision", this.Precision.ToString());
                writer.AddAttribute("ondragenter", "return false;");
                if (!this.IsEnablePaste)
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
            if (this.EnableNumInput)
            {
                writer.AddStyleAttribute("ime-mode", "disabled");
                this.OnFocus = "this.select();";
                this.OnKeyUp = "CheckIsNumberChar(this);";
                this.OnBlur = "CheckIsNumberChar(this);";
            }
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, ((Unit)(this.Width.Value - this.LabelWidth.Value - this.UnitWidth.Value - 37)).ToString());
        }

        /// <summary>
        /// 扩展控件输出,用于显示Spinner
        /// </summary>
        /// <param name="writer"></param>
        protected override void AddControlToInput(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-spinner-unit");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Top, "0px");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.UnitWidth.ToString());
            writer.AddStyleAttribute(HtmlTextWriterStyle.MarginLeft, ((Unit)(this.Width.Value - this.LabelWidth.Value - this.UnitWidth.Value - 44)).ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            string styleString = string.Empty;
            if (!string.IsNullOrEmpty(this.UnitColor))
                styleString = "color:" + this.UnitColor + ";";
            if (!string.IsNullOrEmpty(this.UnitFontSize))
                styleString += "font-size:" + this.UnitFontSize + ";";
            if (!string.IsNullOrEmpty(styleString))
            {
                writer.Write("<span class=\"layui-spinner-unit-span\" style=\":" + styleString + "\">" + this.UnitText + "</span>");
            }
            else
            {
                writer.Write("<span class=\"layui-spinner-unit-span\">" + this.UnitText + "</span>");
            }
            writer.RenderEndTag();
        }
    }
}

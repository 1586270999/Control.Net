using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// 星形评价控件(集成Label标签控件)
    /// </summary>
    [ToolboxData("<{0}:JointStar runat=server></{0}:JointStar>")]
    public class JointStar : FormsControlBase
    {
        public JointStar()
            : base()
        {
            this.Length = 5;
            this.ShowHalf = true;
            this.StarText = string.Empty;
            this.IsShowHint = true;
            this.ControlType = ControlsType.JointStar;
        }

        #region 控件属性

        /// <summary>
        /// 星星个数
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 星星颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 是否显示半星
        /// </summary>
        public bool ShowHalf { get; set; }

        /// <summary>
        /// 星星描述信息
        /// </summary>
        public string StarText { get; set; }

        /// <summary>
        /// 是否显示提示信息
        /// </summary>
        public bool IsShowHint { get; set; }

        #endregion

        /// <summary>
        /// 标签控件的输出
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderLabelOut(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-form-label");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_Label");
            if (string.IsNullOrEmpty(this.LabelColor) && this.IsRequired)
                writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "red");
            else if (!string.IsNullOrEmpty(this.LabelColor))
                writer.AddStyleAttribute(HtmlTextWriterStyle.Color, this.LabelColor);
            writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, this.LabelAlign);
            if (!this.IsShowTable)
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
            double randValue = 0;
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-input-inline");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, ((Unit)(this.Width.Value - this.LabelWidth.Value)).ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-rate");
            if (this.ReadOnly)
                writer.AddAttribute("readonly", "readonly");
            writer.RenderBeginTag(HtmlTextWriterTag.Ul); //<ul>
            for (int c = 0; c < this.Length; c++)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-inline");
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                if (!string.IsNullOrEmpty(this.DefaultValue))
                {
                    double value = double.Parse(this.DefaultValue);
                    if (value >= (c + 1))
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-icon layui-icon-rate-solid");
                        randValue++;
                    }
                    else if (value >= (c + 0.5))
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-icon layui-icon-rate-half");
                        randValue = (randValue + 0.5);
                    }
                    else
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-icon layui-icon-rate");
                    }
                }
                if (!string.IsNullOrEmpty(this.Color))
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Color, this.Color);
                writer.RenderBeginTag(HtmlTextWriterTag.I);

                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            writer.RenderEndTag(); //</ul>

            if (this.IsShowHint)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-inline");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                if (randValue > 0)
                {
                    if (!string.IsNullOrEmpty(this.StarText))
                    {
                        string[] StarList = this.StarText.Split('^');
                        if (this.ShowHalf)
                            writer.Write(StarList[int.Parse((randValue / 0.5).ToString()) - 1]);
                        else
                            writer.Write(StarList[int.Parse((randValue).ToString()) - 1]);
                    }
                    else
                    {
                        writer.Write(randValue.ToString() + "星");
                    }

                }
                writer.RenderEndTag();
            }

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
                return string.Format("var {0}_OBJ={{data:'{1}', half: '{2}', hint: '{3}', value: '{4}', readonly: '{5}'}};fnJointStarInit('{0}');",
                    this.ClientID,
                    string.IsNullOrEmpty(this.StarText) ? "" : this.StarText,
                    this.ShowHalf ? "true" : "false",
                    this.IsShowHint ? "true" : "false",
                    string.IsNullOrEmpty(this.DefaultValue) ? "0" : this.DefaultValue,
                    this.ReadOnly ? "true" : "false");
            }
        }
    }
}

using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// 文本输入框控件(集成Label标签控件)
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:JointTextField runat=server></{0}:JointTextField>")]
    public class JointTextField : FormsControlBase
    {
        public JointTextField()
            : base()
        {
            this.ControlType = ControlsType.JointTextField;
            this.AutoSwithIME = false;
            this.CssClass = "layui-input";
            this.PassWord = false;
            this.MultiLine = false;
            this.OnKeyUp = "JointManage.FocusToNextInput();";
        }

        #region 控件属性

        /// <summary>
        /// 是否自动切换输入法
        /// </summary>
        public bool AutoSwithIME { get; set; }

        /// <summary>
        /// 文本内容粗体格式
        /// </summary>
        public string FontWeight { get; set; }

        /// <summary>
        /// 文本内容字体大小
        /// </summary>
        public string FontSize { get; set; }

        /// <summary>
        /// 是否允许智能提示
        /// </summary>
        public bool IsEnableSmartHint { get; set; }

        /// <summary>
        /// 智能提示内容
        /// </summary>
        public string SmartHintContent { get; set; }

        /// <summary>
        /// 是否密码输入框
        /// </summary>
        public bool PassWord { get; set; }

        /// <summary>
        /// 多行文本效果
        /// </summary>
        public bool MultiLine { get; set; }

        #endregion

        #region 控件常用事件

        /// <summary>
        /// 客户端KeyPress/OnKeyUp/OnKeyDown事件
        /// </summary>
        [Category("Joint.Event")]
        public string OnKeyPress { get; set; }
        [Category("Joint.Event")]
        public string OnKeyUp { get; set; }
        [Category("Joint.Event")]
        public string OnKeyDown { get; set; }

        /// <summary>
        /// 客户端Blur事件
        /// </summary>
        [Category("Joint.Event")]
        public string OnBlur { get; set; }

        /// <summary>
        /// 获取焦点事件
        /// </summary>
        [Category("Joint.Event")]
        public string OnFocus { get; set; }

        /// <summary>
        /// 客户端change事件
        /// </summary>
        [Category("Joint.Event")]
        public virtual string OnChange { get; set; }

        #endregion

        /// <summary>
        /// 注册控件对应脚本资源
        /// </summary>
        protected override void RegisterControlScriptAndStyle()
        {
            //this.ResourceManager.RegisterScriptsInclude("TextBox", "/Content/Controls/js/TextBox.js");
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
                writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "red");
            else if (!string.IsNullOrEmpty(this.LabelColor))
                writer.AddStyleAttribute(HtmlTextWriterStyle.Color, this.LabelColor);
            writer.AddAttribute("for", this.ClientID);
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
            if (this.IsEnableSmartHint && !string.IsNullOrEmpty(this.SmartHintContent))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-input-inline");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "75%");
                writer.AddStyleAttribute("max-width", ((Unit)(this.Width.Value - this.LabelWidth.Value)).ToString());
            }
            else
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "75%");
                writer.AddStyleAttribute("max-width", ((Unit)(this.Width.Value - this.LabelWidth.Value - 35)).ToString());
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-input-inline");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            //添加CSS样式属性
            this.LoadControlCssStyle(writer);

            #region 添加属性

            if (this.ReadOnly)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-input-readonly");
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Type, this.PassWord ? "password" : "text");
            if (!string.IsNullOrEmpty(this.Title))
                writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Title);

            //公共属性输出(和简单模式共用)
            this.LoadCommonAttribute(writer);

            if (this.IsRequired)
                writer.AddAttribute("required", "true");

            if (!string.IsNullOrEmpty(this.VerifyType) || this.IsRequired)
                writer.AddAttribute("lay-verify", !string.IsNullOrEmpty(this.VerifyType) ? this.VerifyType : "required");

            if (!this.ReadOnly)
            {
                if (!string.IsNullOrEmpty(this.PlaceHolder))
                {
                    writer.AddAttribute("placeholder", this.PlaceHolder);
                }
                else if (this.IsRequired)
                {
                    writer.AddAttribute("placeholder", "请输入" + this.LabelText);
                }
            }

            //AutoSwithIME 尚未实现
            //if (this.AutoSwithIME)
            //{
            //    this.OnFocus += "SuccTextField.AutoSwithIME('" + this.ClientID + "',true);";
            //}

            if (this.ReadOnly)
                writer.AddAttribute("readonly", "readonly");

            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            if (!string.IsNullOrEmpty(this.DefaultValue))
            {
                if (!string.IsNullOrEmpty(this.RefDataEntityColID))
                {
                    writer.AddAttribute("value", this.GetRefDataEntityColValue(this.RefDataEntityColID, this.DefaultValue));
                }
                else
                {
                    writer.AddAttribute("value", this.DefaultValue);
                }
            }
            if (this.Radius)
                writer.AddAttribute("style", "border-radius: 3px;");

            this.AddAttributeToText(writer);

            #endregion
            writer.AddAttribute("clstype", ControlType.ToString());
            if (this.MultiLine)
            {
                if (!string.IsNullOrEmpty(this.Height.ToString()))
                    writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
                // 去掉IE内核浏览器后侧的树向滚动条
                writer.AddStyleAttribute(HtmlTextWriterStyle.Overflow, "auto");
                writer.RenderBeginTag(HtmlTextWriterTag.Textarea);
                writer.Write(this.DefaultValue);
            }
            else
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Input);
            }
            writer.RenderEndTag();
            this.AddControlToInput(writer);
            writer.RenderEndTag();
            if (this.IsEnableSmartHint && !string.IsNullOrEmpty(this.SmartHintContent))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-form-mid layui-word-aux");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(this.SmartHintContent);
                writer.RenderEndTag();
            }
        }

        /// <summary>
        /// 加载CSS样式
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void LoadControlCssStyle(HtmlTextWriter writer)
        {
            writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, this.TextAlign);
            if (this.FontWeight != "")
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.FontWeight, this.FontWeight);
            }
            if (this.FontSize != "")
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, this.FontSize);
            }

            //writer.AddStyleAttribute(HtmlTextWriterStyle.Width, ((Unit)(this.Width.Value - this.LabelWidth.Value - 35)).ToString());

            this.AddStyleAttributeToText(writer);
        }

        /// <summary>
        /// 公共属性输出(和简单模式共用)
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void LoadCommonAttribute(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.FieldName) && string.IsNullOrEmpty(this.FieldAlias))
                writer.AddAttribute("FieldName", this.FieldName);
            if (this.IsForSave && string.IsNullOrEmpty(this.FieldAlias) && string.IsNullOrEmpty(this.RefDataEntityColID))
                writer.AddAttribute("IsForSave", this.IsForSave ? "true" : "false");

            writer.AddAttribute(HtmlTextWriterAttribute.AutoComplete, "off");
            this.OnFocus += this.Attributes["onfocus"];
            this.OnBlur += this.Attributes["onblur"];
            this.OnChange += this.Attributes["onchange"];
            this.OnKeyUp += this.Attributes["onkeyup"];
            this.OnKeyDown += this.Attributes["onkeydown"];

            if (!string.IsNullOrEmpty(this.OnFocus))
            {
                writer.AddAttribute("onfocus", this.OnFocus);
            }

            if (!string.IsNullOrEmpty(this.OnBlur))
            {
                writer.AddAttribute("onblur", this.OnBlur);
            }

            if (!string.IsNullOrEmpty(this.OnChange))
            {
                writer.AddAttribute("onchange", this.OnChange);
            }

            if (!string.IsNullOrEmpty(this.OnKeyUp))
            {
                writer.AddAttribute("onkeyup", this.OnKeyUp);
            }

            if (!string.IsNullOrEmpty(this.OnKeyDown))
            {
                writer.AddAttribute("onkeydown", this.OnKeyDown);
            }
            if (!string.IsNullOrEmpty(this.OnKeyPress))
            {
                writer.AddAttribute("onkeypress", this.OnKeyPress);
            }
        }

        /// <summary>
        /// 为文本框添加属性
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void AddAttributeToText(HtmlTextWriter writer)
        {

        }

        /// <summary>
        /// 为文本框增加样式
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void AddStyleAttributeToText(HtmlTextWriter writer)
        {

        }

        /// <summary>
        /// 为文本框增加平行标签
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void AddControlToInput(HtmlTextWriter writer)
        {

        }

        /// <summary>
        /// 获取参考实体列的值
        /// </summary>
        /// <param name="_RefDataEntityColID"></param>
        /// <param name="_DefaultValue"></param>
        protected virtual string GetRefDataEntityColValue(string _RefDataEntityColID, string _DefaultValue)
        {
            return "";
        }

        /// <summary>
        /// 重写文本输入框的输出(简单模式，用于表格里编辑使用)
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderTextBoxOutSimple(HtmlTextWriter writer)
        {
            this.LabelWidth = 0;
            if (this.ReadOnly)
            {
                writer.AddAttribute("readonly", "readonly");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-input-readonly");
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
            }

            this.LoadControlCssStyle(writer);

            this.LoadCommonAttribute(writer);

            writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, "99999");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute("clstype", ControlType.ToString());
            if (!string.IsNullOrEmpty(this.DefaultValue))
                writer.AddAttribute("value", this.DefaultValue);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }
    }
}

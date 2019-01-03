using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// 单选框控件(集成Label标签控件)
    /// </summary>
    [ToolboxData("<{0}:JointCheckBox runat=server></{0}:JointCheckBox>")]
    public class JointCheckBox : FormsControlBase
    {
        public JointCheckBox()
            : base()
        {
            this.Skins = "switch";
            this.ShowText = "ON|OFF";
            this.Checked = false;
            this.ControlType = ControlsType.JointCheckBox;
            this.MinWidth = "50px";
            this.OnClientClick = "fnSwitch2State(this);";
        }

        #region 控件属性

        /// <summary>
        /// 控件呈现样式，默认值lay-skin
        /// </summary>
        public string Skins { get; set; }

        /// <summary>
        /// 选中状态。默认非选中
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// 显示文本内容(ON|OFF或开启|关闭)
        /// </summary>
        public string ShowText { get; set; }

        /// <summary>
        /// box框的最小宽度
        /// </summary>
        public string MinWidth { get; set; }

        #endregion

        #region 控件常用事件

        /// <summary>
        /// 客户端值改变时触发
        /// </summary>
        [Category("Joint.Event")]
        public string OnClientChange { get; set; }

        /// <summary>
        /// 客户端单击时触发
        /// </summary>
        [Category("Joint.Event")]
        public string OnClientClick { get; set; }

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
        /// 重写单选框控件的样式输出
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderTextBoxOut(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-input-inline");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (this.Skins == "switch")
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-unselect layui-form-switch" + (this.Checked ? " layui-form-onswitch" : ""));
                if (!string.IsNullOrEmpty(this.Title))
                    writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Title);
                if (!string.IsNullOrEmpty(this.ShowText))
                    writer.AddAttribute("lay-text", this.ShowText);
                if (!string.IsNullOrEmpty(this.FieldName))
                    writer.AddAttribute("FieldName", this.FieldName);
                if (this.IsForSave)
                    writer.AddAttribute("IsForSave", this.IsForSave ? "true" : "false");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
                writer.AddStyleAttribute("min-width", MinWidth);
                if (!string.IsNullOrEmpty(this.OnClientChange))
                    writer.AddAttribute("onchange", this.OnClientChange);
                if (!string.IsNullOrEmpty(this.OnClientClick))
                    writer.AddAttribute("onclick", this.OnClientClick);
                writer.AddAttribute(HtmlTextWriterAttribute.Align, "left");
                if (this.Checked)
                {
                    writer.AddAttribute("checked", "true");
                    writer.AddAttribute("lay-value", "1");
                }
                else
                {
                    writer.AddAttribute("lay-value", "0");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.RenderBeginTag(HtmlTextWriterTag.Em);
                if (!string.IsNullOrEmpty(this.ShowText))
                {
                    if (this.Checked)
                        writer.Write(this.ShowText.Split('|')[1]);
                    else
                        writer.Write(this.ShowText.Split('|')[0]);
                }
                writer.RenderEndTag();

                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag();

                writer.RenderEndTag();
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-unselect layui-form-checkbox" + (this.Checked ? " layui-form-checked" : ""));
                if (!string.IsNullOrEmpty(this.Title))
                    writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Title);
                if (!string.IsNullOrEmpty(this.ShowText))
                    writer.AddAttribute("lay-text", this.ShowText);
                if (!string.IsNullOrEmpty(this.FieldName))
                    writer.AddAttribute("FieldName", this.FieldName);
                if (this.IsForSave)
                    writer.AddAttribute("IsForSave", this.IsForSave ? "true" : "false");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
                writer.AddStyleAttribute("min-width", MinWidth);
                if (!string.IsNullOrEmpty(this.OnClientChange))
                    writer.AddAttribute("onchange", this.OnClientChange);
                if (!string.IsNullOrEmpty(this.OnClientClick))
                    writer.AddAttribute("onclick", this.OnClientClick);
                writer.AddAttribute(HtmlTextWriterAttribute.Align, "left");
                if (this.Checked)
                {
                    writer.AddAttribute("checked", "true");
                    writer.AddAttribute("lay-value", "1");
                }
                else
                {
                    writer.AddAttribute("lay-value", "0");
                }
                writer.AddAttribute("lay-skin", "primary");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(this.ShowText);
                writer.RenderEndTag();

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-icon layui-icon-ok");
                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag();

                writer.RenderEndTag();
            }
            writer.RenderEndTag();
        }

        /// <summary>
        /// 重写文本输入框的输出(简单模式，用于表格里编辑使用)
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderTextBoxOutSimple(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Title);
            if (!string.IsNullOrEmpty(this.FieldName))
                writer.AddAttribute("FieldName", this.FieldName);
            if (this.IsForSave)
                writer.AddAttribute("IsForSave", this.IsForSave ? "true" : "false");
            writer.AddAttribute("lay-skin", this.Skins);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, ((Unit)this.Width.Value).ToString());
            if (!string.IsNullOrEmpty(this.OnClientChange))
                writer.AddAttribute("onchange", this.OnClientChange);
            if (!string.IsNullOrEmpty(this.OnClientClick))
                writer.AddAttribute("onclick", this.OnClientClick);
            writer.AddAttribute(HtmlTextWriterAttribute.Align, "left");
            if (this.Checked)
            {
                writer.AddAttribute("checked", "true");
                writer.AddAttribute("lay-value", "1");
            }
            else
            {
                writer.AddAttribute("lay-value", "0");
            }

            if (this.ReadOnly)
            {
                writer.AddAttribute("disabled", "true");
            }

            writer.AddAttribute("lay-text", this.ShowText);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
        }
    }
}

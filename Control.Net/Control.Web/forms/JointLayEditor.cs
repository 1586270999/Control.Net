using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// layui的富文本编辑框(集成Label标签控件)
    /// </summary>
    [ToolboxData("<{0}:JointLayEditor runat=server></{0}:JointLayEditor>")]
    public class JointLayEditor : FormsControlBase
    {
        public JointLayEditor()
            : base()
        {
            this.Width = 700;
            this.Height = 300;
            this.LabelAlign = "Left";
            this.IsRequired = false;
            this.ReadOnly = false;
            this.IsForSave = false;
            this.LabelWidth = 90;
            this.ControlType = ControlsType.JointLayEditor;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-form-item");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_Main");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            //标签样式输出
            if (this.IsShowLabel)
                this.RenderLabelOut(writer);

            //文本框样式输出
            this.RenderTextBoxOut(writer);

            writer.RenderEndTag();
        }

        /// <summary>
        /// 重写HTML编辑器控件前Label输出
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
        /// 重写HTML编辑器控件的样式输出
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderTextBoxOut(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-layedit");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            if (!this.ReadOnly)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-unselect layui-layedit-tool");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write("<button title=\"加粗\" type=\"button\" class=\"layui-icon layedit-tool-b\" style=\"border:0px;background:none;\" onclick=\"fnLayEditorCommand('LAYEDIT_" + this.ClientID + "','bold', true);\"></button>");
                writer.Write("<button title=\"斜体\" type=\"button\" class=\"layui-icon layedit-tool-i\" style=\"border:0px;background:none;\" onclick=\"fnLayEditorCommand('LAYEDIT_" + this.ClientID + "','italic', true);\"></button>");
                writer.Write("<button title=\"下划线\" type=\"button\" class=\"layui-icon layedit-tool-u\" style=\"border:0px;background:none;\" onclick=\"fnLayEditorCommand('LAYEDIT_" + this.ClientID + "','underline', true);\"></button>");
                writer.Write("<button title=\"删除线\" type=\"button\" class=\"layui-icon layedit-tool-d\" style=\"border:0px;background:none;\" onclick=\"fnLayEditorCommand('LAYEDIT_" + this.ClientID + "','strikeThrough', true);\"></button>");
                writer.Write("<span class=\"layedit-tool-mid\"></span>");
                writer.Write("<button title=\"左对齐\" type=\"button\" class=\"layui-icon layedit-tool-left\" style=\"border:0px;background:none;\" onclick=\"fnLayEditorCommand('LAYEDIT_" + this.ClientID + "','left', false);\"></button>");
                writer.Write("<button title=\"居中对齐\" type=\"button\" class=\"layui-icon layedit-tool-center\" style=\"border:0px;background:none;\" onclick=\"fnLayEditorCommand('LAYEDIT_" + this.ClientID + "','center', false);\"></button>");
                writer.Write("<button title=\"右对齐\" type=\"button\" class=\"layui-icon layedit-tool-right\" style=\"border:0px;background:none;\" onclick=\"fnLayEditorCommand('LAYEDIT_" + this.ClientID + "','right', false);\"></button>");
                writer.RenderEndTag();
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-layedit-iframe");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, "LAYEDIT_" + this.ClientID);
            writer.AddAttribute("frameborder", "0");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Height, ((Unit)(this.Height.Value)).ToString());
            writer.AddAttribute("onload", "fnLayEditorInit($('#" + "LAYEDIT_" + this.ClientID + "'), '" + this.DefaultValue + "', " + (this.ReadOnly ? "true" : "false") + ");");

            writer.RenderBeginTag(HtmlTextWriterTag.Iframe);//<Iframe>
            writer.RenderEndTag();//</Iframe>
            writer.RenderEndTag();

            writer.RenderEndTag();
        }
    }
}

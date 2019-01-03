using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// 表单控件基类
    /// </summary>
    public class FormsControlBase : WebControlsBase
    {
        public FormsControlBase()
            : base()
        {
            this.Effect = true;
            this.Width = 300;
            this.IsShowLabel = true;
            this.LabelWidth = 80;
            this.LabelAlign = "Left";
            this.TextAlign = "Left";
            this.IsRequired = false;
            this.ReadOnly = false;
            this.Radius = true;
            this.IsForSave = false;
            this.IsShowTable = false;
            this.IsShowIntable = false;
            this.StyleCSS = string.Empty;
        }

        #region 通用属性

        /// <summary>
        /// Tab键序
        /// </summary>
        public new int TabIndex { get; set; }

        /// <summary>
        /// 是否启用得到焦点时的特效
        /// </summary>
        public bool Effect { get; set; }

        /// <summary>
        /// 控件的总宽度
        /// </summary>
        public override Unit Width { get; set; }

        /// <summary>
        /// 文本框的高度
        /// </summary>
        public override Unit Height { get; set; }

        /// <summary>
        /// 是否显示Label标题
        /// </summary>
        public bool IsShowLabel { get; set; }

        /// <summary>
        /// 输入框前的文本标签
        /// </summary>
        public string LabelText { get; set; }

        /// <summary>
        /// 文本标签的宽度
        /// </summary>
        public Unit LabelWidth { get; set; }

        /// <summary>
        /// Label标签颜色，没有默认值，如果值为空则根据IsRequired=true，则为红色。否则黑色。如果存在值，则以LabelColor颜色为准
        /// </summary>
        public string LabelColor { get; set; }

        /// <summary>
        /// 标签内容对齐方式(Left/Center/Right)
        /// </summary>
        public string LabelAlign { get; set; }

        /// <summary>
        /// 文本内容对齐方式(Left/Center/Right)
        /// </summary>
        public string TextAlign { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// 是否查看模式
        /// </summary>
        public bool ViewMode { get; set; }

        /// <summary>
        /// 输入框圆角效果,默认是
        /// </summary>
        public bool Radius { get; set; }

        /// <summary>
        /// 鼠标滑过提示内容
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 数据加载时绑定字段
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 数据加载时绑定字段,主要是JointTriggerField控件加载数据使用
        /// </summary>
        public string FieldAlias { get; set; }

        /// <summary>
        /// 文本内容是否用来保存。默认值为false
        /// </summary>
        public bool IsForSave { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 隐藏真值，用于存在FieldName和FieldAlias属性的功能使用
        /// </summary>
        public string HiddenTrueValue { get; set; }

        /// <summary>
        /// 参照实体ID
        /// </summary>
        public string RefDataEntityColID { get; set; }

        /// <summary>
        /// 是否呈现表格样式。默认为false。当前功能暂时先不处理了，在写布局控件时出现样式问题
        /// </summary>
        public bool IsShowTable { get; set; }

        /// <summary>
        /// 客户端校验规则。系统内置的有required/phone/email/number/date/url/identity
        /// </summary>
        public string VerifyType { get; set; }

        /// <summary>
        /// 默认文本提示内容
        /// </summary>
        public string PlaceHolder { get; set; }

        /// <summary>
        /// 是否在编辑表格中进行显示
        /// </summary>
        public bool IsShowIntable { get; set; }

        /// <summary>
        /// 所在编辑表格的ID
        /// </summary>
        public string ShowIntableID { get; set; }

        /// <summary>
        /// CSS样式
        /// </summary>
        public string StyleCSS { get; set; }

        #endregion

        /// <summary>
        /// 控件初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAfterInit(EventArgs e)
        {
            base.OnAfterInit(e);

            this.EnsureChildControls();
            this.RegisterControlScriptAndStyle();
            this.AppendControlJSDS();
        }

        /// <summary>
        /// 控件输出
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (!string.IsNullOrEmpty(this.StyleCSS))
                writer.AddAttribute(HtmlTextWriterAttribute.Style, this.StyleCSS);

            //是否在编辑表格中进行展示，如果是则去掉label等样式输出
            if (this.IsShowIntable)
            {
                //writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-inline");
                //writer.RenderBeginTag(HtmlTextWriterTag.Div);

                //(简单)文本框样式输出
                this.RenderTextBoxOutSimple(writer);

                //writer.RenderEndTag();
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-inline" + (this.IsShowTable ? " layui-form-pane" : ""));
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "95%");
                writer.AddStyleAttribute("max-width", ((Unit)(this.Width.Value)).ToString());
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_Main");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                if (this.IsShowLabel)
                {
                    //标签样式输出
                    this.RenderLabelOut(writer);
                }
                else
                {
                    this.LabelWidth = 0;
                }

                //文本框样式输出
                this.RenderTextBoxOut(writer);

                writer.RenderEndTag();
            }
        }

        /// <summary>
        /// 标签控件的输出
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void RenderLabelOut(HtmlTextWriter writer)
        {

        }

        /// <summary>
        /// 重写文本输入框的输出
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void RenderTextBoxOut(HtmlTextWriter writer)
        {

        }

        /// <summary>
        /// 重写文本输入框的输出(简单模式，用于表格里编辑使用)
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void RenderTextBoxOutSimple(HtmlTextWriter writer)
        {

        }

        /// <summary>
        /// 注册控件对应脚本资源
        /// </summary>
        protected virtual void RegisterControlScriptAndStyle()
        {

        }

        /// <summary>
        /// 注册控件对象对应脚本
        /// </summary>
        protected virtual void AppendControlJSDS()
        {

        }
    }
}

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// 日期选择控件(集成Label标签控件)
    /// </summary>
    [ToolboxData("<{0}:JointDateField runat=server></{0}:JointDateField>")]
    public class JointDateField : FormsControlBase
    {
        public JointDateField()
        {
            this.ControlType = ControlsType.JointDateField;
            this.DateType = DateType.date;
            this.IsShowRange = false;
            this.FormatString = "yyyy-MM-dd";
            this.IsAllShow = false;
            this.DatePosition = DatePosition._absolute;
            this.IsShowButton = true;
            this.ButtonOrder = "['clear', 'now', 'confirm']";
            this.DateTheme = "default";
            this.IsCalendar = false;
            this.CssClass = "layui-input";
            this.MinDaysFormNow = "";
            this.MaxDaysFormNow = "";
        }

        #region 控件属性

        /// <summary>
        /// 日期时间类型
        /// </summary>
        public DateType DateType { get; set; }

        /// <summary>
        /// 是否开启左右面板范围选择
        /// </summary>
        public bool IsShowRange { get; set; }

        /// <summary>
        /// 日期时间格式化串
        /// </summary>
        public string FormatString { get; set; }

        /// <summary>
        /// 最小日期距离今天。2表示最小日期为两天前
        /// </summary>
        public string MinDaysFormNow { get; set; }

        /// <summary>
        /// 最大日期距离今天。2表示最大日期为两天后
        /// </summary>
        public string MaxDaysFormNow { get; set; }

        /// <summary>
        /// 最小可选择日期
        /// </summary>
        public string MinDate { get; set; }

        /// <summary>
        /// 最大可选择日期
        /// </summary>
        public string MaxDate { get; set; }

        /// <summary>
        /// 如果设置: true，则控件默认显示在绑定元素的区域。通常用于外部事件调用控件
        /// </summary>
        public bool IsAllShow { get; set; }

        /// <summary>
        /// 控件布局方式
        /// </summary>
        public DatePosition DatePosition { get; set; }

        /// <summary>
        /// 是否显示控件的底部栏区域
        /// </summary>
        public bool IsShowButton { get; set; }

        /// <summary>
        /// 右下角显示的按钮，会按照数组顺序排列，内置可识别的值有：clear、now、confirm
        /// 默认值：['clear', 'now', 'confirm']
        /// </summary>
        public string ButtonOrder { get; set; }

        /// <summary>
        /// 是否显示英文
        /// </summary>
        public bool IsShowEnglish { get; set; }

        /// <summary>
        /// 显示主题
        /// </summary>
        public string DateTheme { get; set; }

        /// <summary>
        /// 是否显示公历节日，默认值false
        /// </summary>
        public bool IsCalendar { get; set; }

        /// <summary>
        /// 提示信息内容
        /// </summary>
        public string ShowHintMsg { get; set; }

        #endregion

        #region 控件事件属性

        /// <summary>
        /// 控件初始打开的回调
        /// 控件在打开时触发，回调返回一个参数：初始的日期时间对象
        /// </summary>
        [Category("Joint.Event")]
        public string OnReady { get; set; }

        /// <summary>
        /// 日期时间被切换后的回调
        /// 年月日时间被切换时都会触发。回调返回三个参数，分别代表：生成的值、日期时间对象、结束的日期时间对象
        /// </summary>
        [Category("Joint.Event")]
        public virtual string OnChange { get; set; }

        /// <summary>
        /// 控件选择完毕后的回调
        /// 点击日期、清空、现在、确定均会触发。回调返回三个参数，分别代表：生成的值、日期时间对象、结束的日期时间对象
        /// </summary>
        [Category("Joint.Event")]
        public virtual string OnDone { get; set; }

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
        /// 重写文本输入框的输出
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderTextBoxOut(HtmlTextWriter writer)
        {
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "75%");
            writer.AddStyleAttribute("max-width", ((Unit)(this.Width.Value - this.LabelWidth.Value - 35)).ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-input-inline");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (this.ReadOnly)
            {
                writer.AddAttribute("readonly", "readonly");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-input-readonly");
            }
            else
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Title);
            if (!string.IsNullOrEmpty(this.FieldName))
                writer.AddAttribute("FieldName", this.FieldName);
            if (this.IsForSave)
                writer.AddAttribute("IsForSave", this.IsForSave ? "true" : "false");

            writer.AddAttribute("required", this.IsRequired ? "true" : "false");
            if (!string.IsNullOrEmpty(this.VerifyType) || this.IsRequired)
                writer.AddAttribute("lay-verify", !string.IsNullOrEmpty(this.VerifyType) ? this.VerifyType : "required");
            writer.AddAttribute("placeholder", this.PlaceHolder);
            writer.AddAttribute(HtmlTextWriterAttribute.AutoComplete, "off");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);

            if (this.Radius)
                writer.AddAttribute("style", "border-radius: 3px;");
            //writer.AddStyleAttribute(HtmlTextWriterStyle.Width, ((Unit)(this.Width.Value - this.LabelWidth.Value - 35)).ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
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
                string _DefaultValue = "";
                if (!string.IsNullOrEmpty(this.DefaultValue))
                {
                    DateTime showDate = DateTime.Parse(this.DefaultValue);
                    _DefaultValue = showDate.ToString(this.FormatString);
                }

                return "var " + this.ClientID + "_OBJ = {" + string.Format("elem: '#{0}',type: '{1}',range: {2},format: '{3}',value: '{4}',{5},{6},showBottom: {7},btns: {8},calendar: {9},ready: '{10}',change: '{11}',done: '{12}'", this.ClientID, this.DateType.ToString(), this.IsShowRange ? "true" : "false", this.FormatString, _DefaultValue, (this.MinDaysFormNow == "" ? "mi:-1" : "min: " + this.MinDaysFormNow), (this.MaxDaysFormNow == "" ? "ma:-1" : "max: " + this.MaxDaysFormNow), this.IsShowButton ? "true" : "false", this.ButtonOrder, this.IsCalendar ? "true" : "false", this.OnReady, this.OnChange, this.OnDone) + "};laydate.render(" + this.ClientID + "_OBJ);";
            }
        }
    }
}

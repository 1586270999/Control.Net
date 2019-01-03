using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// JointDynaminCombox动态下拉框控件(集成Label标签控件)
    /// </summary>
    [ToolboxData("<{0}:JointDynaminCombox runat=server></{0}:JointDynaminCombox>")]
    public class JointDynaminCombox : FormsControlBase
    {
        public JointDynaminCombox()
            : base()
        {
            this.IsAsynDataLoad = false;
            this.IsOnlyAsynOnce = false;
            this.ControlType = ControlsType.JointDynamicCombobox;
            this.ComboboxMaxWidth = 300;
            this.ComboboxMaxHeight = 300;
            this.ShowCountOnce = 25;
            this.IsShowPager = true;
            this.ShowIntableID = "c_MainGrid";
        }

        #region 属性

        /// <summary>
        /// 是否开启异步回调处理，默认为false
        /// </summary>
        public bool IsAsynDataLoad { get; set; }

        /// <summary>
        /// 异步回调时，是否只回调一次，默认为false
        /// </summary>
        public bool IsOnlyAsynOnce { get; set; }

        /// <summary>
        /// 一次显示多少条记录,启用异步加载时有效
        /// </summary>
        public int ShowCountOnce { get; set; }

        /// <summary>
        /// 下拉框最大宽度，默认300
        /// </summary>
        public int ComboboxMaxWidth { get; set; }

        /// <summary>
        /// 下拉框最大高度，默认300
        /// </summary>
        public int ComboboxMaxHeight { get; set; }

        /// <summary>
        /// 表格查询语句
        /// </summary>
        public string QueryFFuncID { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        private DataTable DataSource { get; set; }

        /// <summary>
        /// 客户端改变选项后触发事件
        /// </summary>
        public string OnClientChange { get; set; }

        /// <summary>
        /// 是否显示分页条
        /// </summary>
        public bool IsShowPager { get; set; }

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
        /// 重写下拉选择框的输出
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderTextBoxOut(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-input-inline");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "75%");
            writer.AddStyleAttribute("max-width", ((Unit)(this.Width.Value - this.LabelWidth.Value - 35)).ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, (this.ReadOnly ? "" : "layui-input ") + "tableInput" + (this.ReadOnly ? " layui-input-readonly" : ""));
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            if (!string.IsNullOrEmpty(this.FieldName))
                writer.AddAttribute("FieldName", this.FieldName);
            if (!string.IsNullOrEmpty(this.FieldAlias))
                writer.AddAttribute("FieldAlias", this.FieldAlias);
            if (this.IsForSave)
                writer.AddAttribute("IsForSave", this.IsForSave ? "true" : "false");
            writer.AddAttribute("required", this.IsRequired ? "true" : "false");
            if (!string.IsNullOrEmpty(this.VerifyType) || this.IsRequired)
                writer.AddAttribute("lay-verify", "required");

            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            //writer.AddStyleAttribute(HtmlTextWriterStyle.Width, ((Unit)(this.Width.Value - this.LabelWidth.Value - 35)).ToString());
            writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "30px");
            if (!this.ReadOnly)
            {
                writer.AddAttribute("onfocus", "JointDynaminCombox.Init('" + this.ClientID + "');");
                writer.AddAttribute("onkeyup", "JointDynaminCombox.OnKeyUp('" + this.ClientID + "');");
                writer.AddAttribute("placeholder", string.IsNullOrEmpty(this.PlaceHolder) ? "请输入" + this.LabelText : this.PlaceHolder);
            }
            if (!string.IsNullOrEmpty(this.HiddenTrueValue))
            {
                writer.AddAttribute("lay-value", this.HiddenTrueValue);
            }
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
            writer.AddAttribute("clstype", ControlType.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-icon layui-icon-triangle-d");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "top:20%;right:7px;position:absolute;color:#c2c2c2;cursor:pointer;");
            writer.AddAttribute("onclick", "JointDynaminCombox.Show('" + this.ClientID + "', true);$('#" + this.ClientID + "').focus();");
            writer.RenderBeginTag(HtmlTextWriterTag.I);
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.Write("<script type='text/javascript' language='javascript'>" + this.ClientInitScript + "</script>");
        }

        /// <summary>
        /// 控件初始化时执行
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (this.Page != null)
            {
                this.Page.Init += new EventHandler(this.Page_Init);
            }
        }

        /// <summary>
        /// 用户处理控件数据源的加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Init(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 控件对应的JS变量输出
        /// </summary>
        private string ClientInitScript
        {
            get
            {
                string dataString = "", dataColumn = "";
                for (int c = 0; c < this.DataSource.Rows.Count; c++)
                {
                    dataColumn = "";
                    foreach (DataColumn dc in this.DataSource.Columns)
                    {
                        dataColumn += " \"" + dc.ColumnName + "\":\"" + this.DataSource.Rows[c][dc.ColumnName].ToString() + "\", ";
                    }
                    dataString += "{" + dataColumn.TrimEnd(',') + "},";
                }

                return string.Format(@"var {0}_JSDS=[{1}];
                    var {0}_OBJ={{
                        IsAsynDataLoad:'{2}', 
                        IsOnlyAsynOnce:'{3}', 
                        ShowCountOnce:'{4}', 
                        QueryFFuncID: '{5}',
                        ComboboxMaxWidth: '{6}',
                        ComboboxMaxHeight: '{7}',
                        OnClientChange: '{8}',
                        IsShowPager: '{9}',
                        IsShowIntable: '{10}',
                        ShowIntableID: '{11}'
                    }};",
                    this.ClientID + (this.IsShowIntable ? "_D" : ""),
                    dataString.TrimEnd(','),
                    this.IsAsynDataLoad ? "true" : "false",
                    this.IsOnlyAsynOnce ? "true" : "false",
                    this.ShowCountOnce,
                    this.QueryFFuncID,
                    (this.ComboboxMaxWidth < 300) ? 300 : this.ComboboxMaxWidth,
                    this.ComboboxMaxHeight,
                    this.OnClientChange,
                    this.IsShowPager ? "true" : "false",
                    this.IsShowIntable ? "true" : "false",
                    string.IsNullOrEmpty(this.ShowIntableID) ? "" : this.ShowIntableID
                    );
            }
        }

        /// <summary>
        /// 重写显示值的获取
        /// </summary>
        /// <param name="_RefDataEntityColID"></param>
        /// <param name="_DefaultValue"></param>
        /// <returns></returns>
        protected string GetRefDataEntityColValue(string _RefDataEntityColID, string _DefaultValue)
        {
            //尚未实现
            return "";// ControlSQLHelper.GetRefDataEntityColValue(_RefDataEntityColID, _DefaultValue);
        }

        /// <summary>
        /// 重写文本输入框的输出(简单模式，用于表格里编辑使用)
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderTextBoxOutSimple(HtmlTextWriter writer)
        {
            writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, "99999");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            writer.AddAttribute("clstype", ControlType.ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-input-inline");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, ((Unit)(this.Width.Value - this.LabelWidth.Value - 35)).ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute(HtmlTextWriterAttribute.Class, (this.ReadOnly ? "" : "layui-input ") + "tableInput" + (this.ReadOnly ? " layui-input-readonly" : ""));
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.ClientID + "_D");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_D");
            if (!string.IsNullOrEmpty(this.FieldName))
                writer.AddAttribute("FieldName", this.FieldName);
            if (!string.IsNullOrEmpty(this.FieldAlias))
                writer.AddAttribute("FieldAlias", this.FieldAlias);

            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "30px");
            if (!this.ReadOnly)
            {
                writer.AddAttribute("onfocus", "JointDynaminCombox.Init('" + this.ClientID + "_D" + "');");
                writer.AddAttribute("onkeyup", "JointDynaminCombox.OnKeyUp('" + this.ClientID + "_D" + "');");
                //writer.AddAttribute("onblur", "JointDynaminCombox.Blur(this);");
            }
            if (!string.IsNullOrEmpty(this.HiddenTrueValue))
            {
                writer.AddAttribute("lay-value", this.HiddenTrueValue);
            }
            if (!string.IsNullOrEmpty(this.DefaultValue))
            {
                writer.AddAttribute("value", this.DefaultValue);
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-icon layui-icon-triangle-d");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "top:20%;right:7px;position:absolute;color:#c2c2c2;cursor:pointer;");
            writer.AddAttribute("onclick", "JointDynaminCombox.Show('" + this.ClientID + "_D" + "', true);$('#" + this.ClientID + "_D" + "').focus();");
            writer.RenderBeginTag(HtmlTextWriterTag.I);
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.Write("<script type='text/javascript' language='javascript'>" + this.ClientInitScript + "</script>");
        }
    }
}

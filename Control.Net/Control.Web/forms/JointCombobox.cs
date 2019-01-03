using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// Combobox下拉框控件(集成Label标签控件)
    /// </summary>
    [ToolboxData("<{0}:JointCombobox runat=server></{0}:JointCombobox>")]
    public class JointCombobox : FormsControlBase
    {
        public JointCombobox()
            : base()
        {
            this.IsAsynDataLoad = false;
            this.IsOnlyAsynOnce = false;
            this.ShowCountOnce = 25;
            this.ControlType = ControlsType.JointCombobox;
            this.IsQuickFilter = true;
            this.ShowIntableID = "c_MainGrid";
        }

        #region 控件属性

        /// <summary>
        /// 是否允许关键词过滤
        /// </summary>
        public bool IsQuickFilter { get; set; }

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
        /// 目标控件ID，用于动态编辑表格内嵌控件输出处理
        /// </summary>
        public string TargetConrtrolID { get; set; }

        /// <summary>
        /// 表格查询语句
        /// </summary>
        public string QueryFFuncID { get; set; }

        /// <summary>
        /// Ajax回调地址,默认值
        /// </summary>
        public string AjaxUrl { get; set; }

        /// <summary>
        /// 项数据格式
        /// 1.固定项(1[#]Value1|Text1|禁用^Value2|Text2|禁用^Value3|Text3|禁用)
        /// 2.SQL语句(2[#]SELECT FID,FName FROM Com_SubMessage WHERE FItemID=103010)
        /// </summary>
        public string ValueList { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        private DataTable DataSource { get; set; }

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
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-unselect layui-form-select");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-select-title");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if ((!string.IsNullOrEmpty(this.ValueList) && this.ValueList.Split('^').Length > 0) || !(string.IsNullOrEmpty(this.QueryFFuncID)))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, this.ReadOnly ? "layui-input-readonly" : "layui-input layui-unselect");
                //writer.AddStyleAttribute(HtmlTextWriterStyle.Width, ((Unit)(this.Width.Value - this.LabelWidth.Value - 35)).ToString());
                writer.AddAttribute(HtmlTextWriterAttribute.Name, this.ClientID);
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
                if (!string.IsNullOrEmpty(this.FieldName))
                    writer.AddAttribute("FieldName", this.FieldName);
                if (this.IsForSave)
                    writer.AddAttribute("IsForSave", this.IsForSave ? "true" : "false");
                writer.AddAttribute("required", this.IsRequired ? "true" : "false");
                if (!string.IsNullOrEmpty(this.VerifyType) || this.IsRequired)
                    writer.AddAttribute("lay-verify", "required");
                if (this.IsAsynDataLoad)
                    writer.AddAttribute("AsynDataLoad", "true");
                if (this.IsOnlyAsynOnce)
                    writer.AddAttribute("OnlyAsynOnce", "true");
                writer.AddAttribute("type", "text");
                if (!this.ReadOnly)
                {
                    writer.AddAttribute("onfocus", "JointCombobox.Show(this);");
                    writer.AddAttribute("onblur", "JointCombobox.Blur(this);");
                    writer.AddAttribute("onclick", "JointCombobox.Show(this);");
                    writer.AddAttribute("onkeyup", "JointCombobox.KeyUp(event, this);");
                    writer.AddAttribute("placeholder", string.IsNullOrEmpty(this.PlaceHolder) ? "请输入" + this.LabelText : this.PlaceHolder);
                }
                if (!this.IsQuickFilter)
                    writer.AddAttribute("readonly", "readonly");
                if (!string.IsNullOrEmpty(this.HiddenTrueValue))
                    writer.AddAttribute("lay-value", this.HiddenTrueValue);
                if (!string.IsNullOrEmpty(this.TargetConrtrolID))
                {
                    writer.AddAttribute("target-control", this.TargetConrtrolID);
                }
                else
                {
                    writer.AddAttribute("target-control", this.ClientID);
                }
                writer.AddAttribute("layid", this.ClientID);

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

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-edge");
                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag();
            }
            else
            {
                throw new Exception("JointCombobox控件的ValueList属性值不能为空!");
            }
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-anim layui-anim-upbit");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_dl");
            writer.RenderBeginTag(HtmlTextWriterTag.Dl);
            writer.RenderEndTag();
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
            if (!string.IsNullOrEmpty(this.ValueList) && this.ValueList.StartsWith("1[#]"))
            {
                DataTable dtTemp = new DataTable();
                dtTemp.Columns.Add("FID", Type.GetType("System.Int16"));
                dtTemp.Columns.Add("FName", Type.GetType("System.String"));
                foreach (string ListItem in (this.ValueList.Replace("1[#]", "")).Split('^'))
                {
                    DataRow drTemp = dtTemp.NewRow();
                    drTemp["FID"] = ListItem.Split('|')[0];
                    drTemp["FName"] = ListItem.Split('|')[1];
                    dtTemp.Rows.Add(drTemp);
                }

                this.DataSource = dtTemp;
            }
            else if (!string.IsNullOrEmpty(this.ValueList) && this.ValueList.StartsWith("2[#]"))
            {
                //暂未实现
            }
            else if (string.IsNullOrEmpty(this.QueryFFuncID))
            {
                throw new Exception("JointCombobox控件的ValueList属性值不能为空,或格式不满足1[#]/2[#]!");
            }
        }

        /// <summary>
        /// 控件对应的JS变量输出
        /// </summary>
        private string ClientInitScript
        {
            get
            {
                string dataString = "", dataColumn = "";
                if (this.DataSource != null)
                {
                    if (this.DataSource.Rows.Count > 0)
                    {
                        for (int c = 0; c < this.DataSource.Rows.Count; c++)
                        {
                            dataColumn = "";
                            foreach (DataColumn dc in this.DataSource.Columns)
                            {
                                dataColumn += " \"" + dc.ColumnName + "\":\"" + this.DataSource.Rows[c][dc.ColumnName].ToString() + "\", ";
                            }
                            dataString += "{ \"Value\":\"" + this.DataSource.Rows[c]["FID"].ToString() + "\", \"Text\":\"" + this.DataSource.Rows[c]["FName"].ToString() + "\", Values: {" + dataColumn.TrimEnd(',') + "} },";
                        }
                    }
                }

                return string.Format(@"
                    var {0}_JSDS=[{1}];
                    var {0}_OBJ={{
                        IsAsynDataLoad:'{2}', 
                        IsOnlyAsynOnce:'{3}', 
                        ShowCountOnce:'{4}', 
                        QueryFFuncID: '{5}',
                        AjaxUrl: '{6}',
                        IsShowIntable: '{7}',
                        ShowIntableID: '{8}'
                    }};",
                        string.IsNullOrEmpty(this.TargetConrtrolID) ? this.ClientID + (this.IsShowIntable ? "_D" : "") : this.TargetConrtrolID,
                    dataString.TrimEnd(','),
                    this.IsAsynDataLoad ? "true" : "false",
                    this.IsOnlyAsynOnce ? "true" : "false",
                    this.ShowCountOnce,
                    this.QueryFFuncID,
                    this.AjaxUrl,
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
            return ""; //ControlSQLHelper.GetRefDataEntityColValue(_RefDataEntityColID, _DefaultValue);
        }

        /// <summary>
        /// 重写文本输入框的输出(简单模式，用于表格里编辑使用)
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderTextBoxOutSimple(HtmlTextWriter writer)
        {
            writer.AddStyleAttribute(HtmlTextWriterStyle.ZIndex, "99999");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-unselect layui-form-select");
            writer.AddAttribute("clstype", ControlType.ToString());
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, ((Unit)this.Width.Value).ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-unselect layui-form-select");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-select-title");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if ((!string.IsNullOrEmpty(this.ValueList) && this.ValueList.Split('^').Length > 0) || !(string.IsNullOrEmpty(this.QueryFFuncID)))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-input layui-unselect" + (this.ReadOnly ? " layui-input-readonly" : ""));
                if (!string.IsNullOrEmpty(this.FieldName))
                    writer.AddAttribute("FieldName", this.FieldName);
                if (!string.IsNullOrEmpty(this.FieldAlias))
                    writer.AddAttribute("FieldAlias", this.FieldAlias);
                if (this.IsForSave)
                    writer.AddAttribute("IsForSave", this.IsForSave ? "true" : "false");
                if (this.IsAsynDataLoad)
                    writer.AddAttribute("AsynDataLoad", "true");
                if (this.IsOnlyAsynOnce)
                    writer.AddAttribute("OnlyAsynOnce", "true");
                writer.AddAttribute("type", "text");
                if (!this.ReadOnly)
                {
                    //writer.AddAttribute("onfocus", "JointCombobox.Show(this);");
                    writer.AddAttribute("onblur", "JointCombobox.Blur(this);");
                    //writer.AddAttribute("onclick", "JointCombobox.Show(this);");
                    writer.AddAttribute("onkeyup", "JointCombobox.KeyUp(event, this);");
                }
                else
                {
                    writer.AddAttribute("readonly", "readonly");
                }
                if (!string.IsNullOrEmpty(this.HiddenTrueValue))
                {
                    writer.AddAttribute("lay-value", this.HiddenTrueValue);
                }
                if (!string.IsNullOrEmpty(this.TargetConrtrolID))
                {
                    writer.AddAttribute("target-control", this.TargetConrtrolID);
                }
                else
                {
                    writer.AddAttribute("target-control", this.ClientID + "_D");
                }
                writer.AddAttribute("layid", this.ClientID + "_D");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_D");
                if (!string.IsNullOrEmpty(this.DefaultValue))
                {
                    writer.AddAttribute("value", this.DefaultValue);
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Input);
                writer.RenderEndTag();

                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-edge");
                writer.AddAttribute("onclick", "JointCombobox.Show(this.parentElement.children[0], true);$('#" + this.ClientID + "_D" + "').focus();");
                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag();
            }
            else
            {
                throw new Exception("JointCombobox控件的ValueList属性值不能为空!");
            }
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-anim layui-anim-upbit");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_dl");
            writer.RenderBeginTag(HtmlTextWriterTag.Dl);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.Write("<script type='text/javascript' language='javascript'>" + this.ClientInitScript + "</script>");
        }
    }
}

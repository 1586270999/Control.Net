using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// 复选框组控件(集成Label标签控件)
    /// </summary>
    [ToolboxData("<{0}:JointCheckBoxGroup runat=server></{0}:JointCheckBoxGroup>")]
    public class JointCheckBoxGroup : FormsControlBase
    {
        public JointCheckBoxGroup()
            : base()
        {
            this.ZIndex = 1;
            this.ColumnsNumber = 4;
            this.Skins = "";
            this.ControlType = ControlsType.JointCheckBoxGroup;
        }

        #region 控件属性

        /// <summary>
        /// 每行显示几个选项
        /// </summary>
        public int ColumnsNumber { get; set; }

        /// <summary>
        /// Z-Index值
        /// </summary>
        public int ZIndex { get; set; }

        /// <summary>
        /// 控件呈现样式，默认值lay-skin
        /// </summary>
        public string Skins { get; set; }

        /// <summary>
        /// 项数据格式
        /// 1.固定项(1#Value1|Text1|禁用^Value2|Text2|禁用^Value3|Text3|禁用)
        /// 2.SQL语句(2#SELECT FID,FName FROM Com_SubMessage WHERE FItemID=103010)
        /// </summary>
        public string ValueList { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        private NoSortHashTable HashDataSource { get; set; }

        #endregion

        /// <summary>
        /// 客户端点击事件
        /// </summary>
        [Category("Joint.Event")]
        public string OnClientClick { get; set; }

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
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-input-block");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, ((Unit)(this.Width.Value - this.LabelWidth.Value)).ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Name, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute("required", this.IsRequired ? "true" : "false");
            if (!string.IsNullOrEmpty(this.VerifyType) || this.IsRequired)
            {
                writer.AddAttribute("lay-verify", "required");
            }
            if (!string.IsNullOrEmpty(this.FieldName))
                writer.AddAttribute("FieldName", this.FieldName);
            if (this.IsForSave)
                writer.AddAttribute("IsForSave", this.IsForSave ? "true" : "false");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            if (this.HashDataSource.Count > 0)
            {
                int cc = 1;
                if (this.Skins == "switch")
                {
                    foreach (string item in HashDataSource.Keys)
                    {
                        //默认勾选设置
                        if (!string.IsNullOrEmpty(this.DefaultValue) && this.DefaultValue.IndexOf(item) >= 0)
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-unselect layui-form-checkbox layui-form-checked");
                            writer.AddAttribute("checked", "true");
                            writer.AddAttribute("lay-value", "1");
                        }
                        else
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-unselect layui-form-checkbox");
                            writer.AddAttribute("lay-value", "0");
                        }

                        if (!string.IsNullOrEmpty(this.Title))
                            writer.AddAttribute(HtmlTextWriterAttribute.Title, HashDataSource[item].ToString());
                        writer.AddAttribute("lay-text", HashDataSource[item].ToString());
                        writer.AddAttribute(HtmlTextWriterAttribute.Value, item);
                        writer.AddAttribute(HtmlTextWriterAttribute.Name, this.ClientID);
                        writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + item);
                        writer.AddAttribute(HtmlTextWriterAttribute.Align, "left");
                        writer.AddAttribute("onclick", "fnSwitch2State(this);");
                        writer.RenderBeginTag(HtmlTextWriterTag.Div);

                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        writer.Write(HashDataSource[item].ToString());
                        writer.RenderEndTag();

                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-icon layui-icon-ok");
                        writer.RenderBeginTag(HtmlTextWriterTag.I);
                        writer.RenderEndTag();

                        writer.RenderEndTag();

                        cc++;
                        if (cc > this.ColumnsNumber)
                        {
                            writer.RenderBeginTag(HtmlTextWriterTag.Br);
                            cc = 1;
                        }
                    }
                }
                else
                {
                    foreach (string item in HashDataSource.Keys)
                    {
                        //默认勾选设置
                        if (!string.IsNullOrEmpty(this.DefaultValue) && this.DefaultValue.IndexOf(item) >= 0)
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-unselect layui-form-checkbox layui-form-checked");
                            writer.AddAttribute("checked", "true");
                            writer.AddAttribute("lay-value", "1");
                        }
                        else
                        {
                            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-unselect layui-form-checkbox");
                            writer.AddAttribute("lay-value", "0");
                        }

                        if (!string.IsNullOrEmpty(this.Title))
                            writer.AddAttribute(HtmlTextWriterAttribute.Title, HashDataSource[item].ToString());
                        writer.AddAttribute("lay-text", HashDataSource[item].ToString());
                        writer.AddAttribute(HtmlTextWriterAttribute.Value, item);
                        writer.AddAttribute(HtmlTextWriterAttribute.Name, this.ClientID);
                        writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + item);
                        writer.AddAttribute(HtmlTextWriterAttribute.Align, "left");
                        writer.AddAttribute("lay-skin", "primary");
                        writer.AddAttribute("onclick", "fnSwitch2State(this);");
                        writer.RenderBeginTag(HtmlTextWriterTag.Div);

                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        writer.Write(HashDataSource[item].ToString());
                        writer.RenderEndTag();

                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-icon layui-icon-ok");
                        writer.RenderBeginTag(HtmlTextWriterTag.I);
                        writer.RenderEndTag();

                        writer.RenderEndTag();

                        cc++;
                        if (cc > this.ColumnsNumber)
                        {
                            writer.RenderBeginTag(HtmlTextWriterTag.Br);
                            cc = 1;
                        }
                    }
                }
            }
            writer.RenderEndTag();
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
                NoSortHashTable _HashTable = new NoSortHashTable();
                foreach (string ListItem in (this.ValueList.Replace("1[#]", "")).Split('^'))
                {
                    _HashTable.Add(ListItem.Split('|')[0], ListItem.Split('|')[1]);
                }

                this.HashDataSource = _HashTable;
            }
            else if (!string.IsNullOrEmpty(this.ValueList) && this.ValueList.StartsWith("2[#]"))
            {
                //执行SQL语句，查询数据源，暂未实现
            }
            else
            {
                throw new Exception("JointCheckBoxGroup控件的ValueList属性值不能为空,或格式不满足1[#]/2[#]!");
            }
        }
    }
}

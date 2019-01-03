using System;
using System.Collections;
using System.Web.UI;

namespace Control.Web
{
    /// <summary>
    /// 按钮带菜单控件
    /// </summary>
    [ParseChildren(true)]
    [ToolboxData("<{0}:JointSplitButton runat=server></{0}:JointSplitButton>")]
    public class JointSplitButton : JointButton
    {
        public JointSplitButton()
            : base()
        {
            this.ButtonType = false;
            this.Radius = false;
            this.LaySkins = ButtonObject.empty;
            this.LaySize = ButtonSize.empty;
            this.ControlType = ControlsType.JointSplitButton;
        }

        /// <summary>
        /// 项数据格式(按钮名称/命令名/图标)
        /// 1.固定项(1[#]Text1|CMD1|ICON1^Text2|CMD2|ICON2^Text3|CMD3|ICON3)
        /// 2.SQL语句(2[#]SELECT FID,FName FROM Com_SubMessage WHERE FItemID=103010)
        /// </summary>
        public string ValueList { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        private Hashtable HashDataSource { get; set; }

        /// <summary>
        /// 按钮控件输出
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Width.Value > 0)
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Width.ToString());
            if (this.Height.Value > 0)
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "btn-group");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (this.HashDataSource.Count > 0)
            {
                ArrayList arrarykeys = new ArrayList(HashDataSource.Keys);
                arrarykeys.Sort();

                foreach (string item in arrarykeys)
                {
                    DefineValueList _value = (DefineValueList)HashDataSource[item];
                    if (item.ToString() == "Order_1")
                    {
                        string _AddClass = string.Empty;
                        if (this.LaySkins != ButtonObject.empty)
                            _AddClass = " layui-btn-" + this.LaySkins.ToString();
                        switch (this.LaySize)
                        {
                            case ButtonSize.mini:
                                _AddClass += " layui-btn-xs";
                                break;
                            case ButtonSize.small:
                                _AddClass += " layui-btn-sm";
                                break;
                            case ButtonSize.big:
                                _AddClass += " layui-btn-lg";
                                break;
                            default:
                                _AddClass += " layui-btn-sm";
                                break;
                        }
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-btn" + _AddClass + (this.Radius ? " layui-btn-radius" : ""));
                        writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_" + _value.Value2);
                        if (string.IsNullOrEmpty(this.OnClientClick))
                            writer.AddAttribute("onclick", "JointLibrary.fnButtonClick('" + _value.Value2 + "', '" + _value.Value1 + "');return false;");
                        else
                            writer.AddAttribute("onclick", this.OnClientClick);
                        writer.RenderBeginTag(HtmlTextWriterTag.Button);

                        if (!string.IsNullOrEmpty(_value.Value3))
                            writer.Write("<i class=\"layui-icon " + _value.Value3 + " \"></i>");
                        writer.Write(_value.Value1);
                        writer.RenderEndTag();
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "dropdown-toggle layui-btn" + _AddClass + (this.Radius ? " layui-btn-radius" : ""));
                        //writer.AddAttribute("data-toggle", "dropdown");
                        writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "0px 0px");
                        writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "26px");
                        writer.AddStyleAttribute(HtmlTextWriterStyle.MarginLeft, "0px");
                        writer.AddAttribute("onclick", "$(this).next().toggle();return false;");
                        writer.RenderBeginTag(HtmlTextWriterTag.Button);
                        writer.Write("<span class=\"caret\"></span>");
                        writer.RenderEndTag();

                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "dropdown-menu");
                        writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                    }
                    else
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Li);
                        if (!string.IsNullOrEmpty(_value.Value2))
                            writer.AddAttribute("onclick", "JointLibrary.fnButtonClick('" + _value.Value2 + "', '" + _value.Value1 + "');return false;");
                        writer.RenderBeginTag(HtmlTextWriterTag.A);

                        if (!string.IsNullOrEmpty(_value.Value3))
                            writer.Write("<i class=\"layui-icon " + _value.Value3 + " \"></i>");
                        writer.Write(_value.Value1);
                        writer.RenderEndTag();
                        writer.RenderEndTag();
                    }
                }
                writer.RenderEndTag();
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
                Hashtable _HashTable = new Hashtable();
                int cc = 0;
                foreach (string ListItem in (this.ValueList.Replace("1[#]", "")).Split('^'))
                {
                    cc++;
                    DefineValueList _value = new DefineValueList();
                    _value.Value1 = ListItem.Split('|')[0]; //按钮名称
                    _value.Value2 = ListItem.Split('|')[1]; //命令名
                    _value.Value3 = ListItem.Split('|')[2]; //图标

                    _HashTable.Add("Order_" + cc, _value);
                }

                this.HashDataSource = _HashTable;
            }
            else if (!string.IsNullOrEmpty(this.ValueList) && this.ValueList.StartsWith("2[#]"))
            {
                //执行SQL语句，查询数据源，暂未实现
            }
            else
            {
                throw new Exception("JointSplitButton控件的ValueList属性值不能为空,或格式不满足1[#]/2[#]!");
            }
        }
    }
}

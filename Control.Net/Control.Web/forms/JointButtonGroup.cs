using System;
using System.Web.UI;

namespace Control.Web
{
    /// <summary>
    /// 按钮组控件
    /// </summary>
    [ToolboxData("<{0}:JointButtonGroup runat=server></{0}:JointButtonGroup>")]
    public class JointButtonGroup : JointButton
    {
        public JointButtonGroup()
            : base()
        {
            this.ButtonType = false;
            this.Radius = false;
            this.LaySkins = ButtonObject.empty;
            this.LaySize = ButtonSize.empty;
            this.ControlType = ControlsType.JointButtonGroup;
            this.AccessString = "";
        }

        /// <summary>
        /// 项数据格式(按钮名称/命令名/图标)
        /// 1.固定项(1[#]Text1|CMD1|ICON1^Text2|CMD2|ICON2^Text3|CMD3|ICON3)
        /// 2.SQL语句(2[#]SELECT FID,FName FROM Com_SubMessage WHERE FItemID=103010)
        /// </summary>
        public string ValueList { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        public string AccessString { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        private NoSortHashTable HashDataSource { get; set; }

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

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-btn-group");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (this.HashDataSource.Count > 0)
            {
                foreach (string item in HashDataSource.Keys)
                {
                    string varString = HashDataSource[item].ToString();
                    if (this.AccessString == "")
                    {
                        this.RenderButton(writer, this.ClientID + '_' + varString.Split('^')[0], "JointLibrary.fnButtonClick('" + varString.Split('^')[0] + "', '" + item + "');return false;", item, varString.Split('^')[1]);
                    }
                    else
                    {
                        if (this.AccessString.Contains(varString.Split('^')[0]))
                            this.RenderButton(writer, this.ClientID + '_' + varString.Split('^')[0], "JointLibrary.fnButtonClick('" + varString.Split('^')[0] + "', '" + item + "');return false;", item, varString.Split('^')[1]);
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
                    _HashTable.Add(ListItem.Split('|')[0], ListItem.Split('|')[1] + '^' + (ListItem.Split('|').Length > 2 ? ListItem.Split('|')[2] : ""));
                }

                this.HashDataSource = _HashTable;
            }
            else if (!string.IsNullOrEmpty(this.ValueList) && this.ValueList.StartsWith("2[#]"))
            {
                //执行SQL语句，查询数据源，暂未实现
            }
            else
            {
                throw new Exception("JointButtonGroup控件的ValueList属性值不能为空,或格式不满足1[#]/2[#]!");
            }
        }
    }
}

using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// 按钮控件
    /// </summary>
    [ToolboxData("<{0}:JointButton runat=server></{0}:JointButton>")]
    public class JointButton : WebControlsBase
    {
        public JointButton()
            : base()
        {
            this.Width = 80;
            this.ButtonType = false;
            this.Radius = false;
            this.Text = "";
            this.LaySkins = ButtonObject.empty;
            this.LaySize = ButtonSize.empty;
            this.ControlType = ControlsType.JointButton;
            this.AlwaysShow = false;
        }

        #region 控件属性

        /// <summary>
        /// 控件的总宽度
        /// </summary>
        public override Unit Width { get; set; }

        /// <summary>
        /// 文本框的高度
        /// </summary>
        public override Unit Height { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public ButtonObject LaySkins { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public ButtonSize LaySize { get; set; }

        /// <summary>
        /// 圆角效果,默认否
        /// </summary>
        public bool Radius { get; set; }

        /// <summary>
        /// 文字
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string LayIcon { get; set; }

        /// <summary>
        /// 按钮类型：(0按钮1链接)
        /// </summary>
        public bool ButtonType { get; set; }

        /// <summary>
        /// 按钮类型为链接时，地址Url
        /// </summary>
        public string PostUrl { get; set; }

        /// <summary>
        /// 控件对应脚本路径
        /// </summary>
        public string ControlScriptPath { get; set; }

        /// <summary>
        /// 按钮执行保存命令时对应的表ID
        /// </summary>
        public string TableID { get; set; }

        /// <summary>
        /// 查看等特殊页面是否一直显示
        /// </summary>
        public bool AlwaysShow { get; set; }

        /// <summary>
        /// 按钮对应执行存储过程名
        /// </summary>
        public string ProcName { get; set; }

        #endregion

        /// <summary>
        /// 客户端点击事件
        /// </summary>
        public string OnClientClick { get; set; }

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

            this.RenderButton(writer, this.ClientID, this.OnClientClick, this.Text, this.LayIcon);
        }

        /// <summary>
        /// 单独按钮的输出
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="clientID">控件ID</param>
        /// <param name="onclick">单击事件</param>
        /// <param name="posturl">链接地址</param>
        /// <param name="text">按钮内容</param>
        /// <param name="layIcon">按钮图标</param>
        protected virtual void RenderButton(HtmlTextWriter writer, string clientID, string onclick, string text, string layIcon)
        {
            string _AddClass = string.Empty;
            if (this.LaySkins != ButtonObject.empty)
                _AddClass = " layui-btn-" + this.LaySkins.ToString();

            if (this.LaySize != ButtonSize.empty)
            {
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
                }
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-btn" + _AddClass + (this.Radius ? " layui-btn-radius" : ""));
            writer.AddAttribute(HtmlTextWriterAttribute.Id, clientID);
            if (!string.IsNullOrEmpty(this.TableID))
                writer.AddAttribute("tableID", this.TableID);
            if (!string.IsNullOrEmpty(this.ProcName))
                writer.AddAttribute("procName", this.ProcName);

            if (this.ButtonType)
            {
                writer.AddAttribute("href", this.PostUrl);
                writer.RenderBeginTag(HtmlTextWriterTag.A);
            }
            else
            {
                writer.AddAttribute("onclick", onclick);
                writer.RenderBeginTag(HtmlTextWriterTag.Button);
            }
            if (!string.IsNullOrEmpty(layIcon))
                writer.Write("<i class=\"layui-icon layui-icon-" + layIcon + " \"></i>");
            writer.Write(text);

            writer.RenderEndTag();
        }
    }
}

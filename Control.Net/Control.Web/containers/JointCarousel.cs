using System.Web.UI;

namespace Control.Web
{
    /// <summary>
    /// 轮播主控件
    /// </summary>
    [ParseChildren(false)]
    [PersistChildren(true)]
    [ToolboxData("<{0}:JointCarousel runat=server></{0}:JointCarousel>")]
    public class JointCarousel : WebControlsBase
    {
        public JointCarousel()
            : base()
        {
            this.ControlType = ControlsType.JointCarousel;
            this.Width = "400px";
            this.Height = "400px";
            this.Animate = "default";
            this.Autoplay = true;
            this.Interval = 3000;
            this.ArrowStyle = "hover";
            this.ItemCount = 1;
        }

        #region 控件属性

        /// <summary>
        /// 控件宽度(可以是400px，也可以是60%)
        /// </summary>
        public new string Width { get; set; }

        /// <summary>
        /// 控件高度(可以是400px，也可以是60%)
        /// </summary>
        public new string Height { get; set; }

        /// <summary>
        /// 轮播切换动画方式
        /// (default左右切换/updown上下切换/fade渐隐渐显切换)
        /// </summary>
        public string Animate { get; set; }

        /// <summary>
        /// 是否自动切换
        /// </summary>
        public bool Autoplay { get; set; }

        /// <summary>
        /// 自动切换的时间间隔
        /// 单位：ms, 默认3000
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// 切换箭头默认显示状态
        /// hover悬停显示/always始终显示/none始终不显示
        /// </summary>
        public string ArrowStyle { get; set; }

        /// <summary>
        /// 明细项个数
        /// </summary>
        public int ItemCount { get; set; }

        #endregion

        /// <summary>
        /// 重写控件的头部标签输出
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-carousel");
            writer.AddAttribute("interval", this.Interval.ToString());
            writer.AddAttribute("count", this.ItemCount.ToString());
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            if (!string.IsNullOrEmpty(this.ArrowStyle))
                writer.AddAttribute("lay-arrow", this.ArrowStyle);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Width);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height);
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            writer.AddAttribute("carousel-item", "");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        /// <summary>
        /// 重写控件的尾部标签输出
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
            if (this.ItemCount > 0)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-carousel-ind");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                for (int c = 0; c < this.ItemCount; c++)
                {
                    if (c == 0)
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-this");
                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
                writer.RenderEndTag();
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-icon layui-carousel-arrow");
            writer.AddAttribute("lay-type", "sub");
            writer.RenderBeginTag(HtmlTextWriterTag.Button);
            writer.Write("");
            writer.RenderEndTag();
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-icon layui-carousel-arrow");
            writer.AddAttribute("lay-type", "add");
            writer.RenderBeginTag(HtmlTextWriterTag.Button);
            writer.Write("");
            writer.RenderEndTag();

            writer.RenderEndTag();
            writer.Write("<script type='text/javascript' language='javascript'>fnCarouselInit('" + this.ClientID + "');</script>");
        }
    }
}

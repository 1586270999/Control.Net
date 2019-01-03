using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// 布局控件(内部TR/TD)
    /// </summary>
    [ParseChildren(false)]
    [PersistChildren(true)]
    [ToolboxData("<{0}:JointLayout runat=server></{0}:JointLayout>")]
    public class JointLayout : WebControlsBase
    {
        public JointLayout()
            : base()
        {
            this.Type = PositionType.Center;
            this.IsCollapse = true;
            this.IsSplit = true;
            this.ControlType = ControlsType.JointLayout;
            this.IsHorizontal = true;
            this.MarginBottom = 0;
            this.MarginTop = 0;
            this.MarginLeft = 0;
            this.MarginRight = 0;
        }

        #region 控件属性

        /// <summary>
        /// 布局类型，默认中间
        /// </summary>
        public PositionType Type { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 是否显示收缩按钮,默认true
        /// </summary>
        public bool IsCollapse { get; set; }

        /// <summary>
        /// 是否显示分隔线,默认true
        /// </summary>
        public bool IsSplit { get; set; }

        /// <summary>
        /// 分割线宽度(暂不实现，默认4px)
        /// </summary>
        public int SplitWidth { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public override Unit Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public override Unit Height { get; set; }

        /// <summary>
        /// 上边距
        /// </summary>
        public Unit MarginTop { get; set; }

        /// <summary>
        /// 下边距
        /// </summary>
        public Unit MarginBottom { get; set; }

        /// <summary>
        /// 左边距
        /// </summary>
        public Unit MarginLeft { get; set; }

        /// <summary>
        /// 右边距
        /// </summary>
        public Unit MarginRight { get; set; }

        /// <summary>
        /// 父布局控件ID，以后再完善
        /// </summary>
        public string ParentControlID { get; set; }

        /// <summary>
        /// 是否横向布局
        /// </summary>
        public bool IsHorizontal { get; set; }

        #endregion

        /// <summary>
        /// 重写控件的头部标签输出
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            switch (this.Type)
            {
                case PositionType.Top:
                    this.RenderTopBegin(writer);
                    break;
                case PositionType.Bottom:
                    this.RenderBottomBegin(writer);
                    break;
                case PositionType.Left:
                    this.RenderLeftBegin(writer);
                    break;
                case PositionType.Right:
                    this.RenderRightBegin(writer);
                    break;
                default:
                    this.RenderCenterBegin(writer);
                    break;
            }
        }

        /// <summary>
        /// 重写控件的尾部标签输出
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            switch (this.Type)
            {
                case PositionType.Top:
                    this.RenderTopEnd(writer);
                    break;
                case PositionType.Bottom:
                    this.RenderBottomEnd(writer);
                    break;
                case PositionType.Left:
                    this.RenderLeftEnd(writer);
                    break;
                case PositionType.Right:
                    this.RenderRightEnd(writer);
                    break;
                default:
                    this.RenderCenterEnd(writer);
                    break;
            }
        }

        #region Top布局输出

        /// <summary>
        /// Top布局输出BEGIN
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void RenderTopBegin(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-top");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (!string.IsNullOrEmpty(this.Title))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-header");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-text");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(this.Title);
                writer.RenderEndTag();
                if (this.IsCollapse)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-icon-d");
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "$('#" + this.ParentControlID + "').toggleClass('layout-top-hide');");
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
            }
            else
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Top, "0px");
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-body");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-auto");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        /// <summary>
        /// Top布局输出END
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void RenderTopEnd(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();

            if (this.IsSplit)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-split-row top-row");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Top, this.Height.ToString());
                writer.AddAttribute("onmousedown", "fnLayoutSplitMove(this, 'top', '#" + this.ParentControlID + "');");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderEndTag();
            }
        }

        #endregion

        #region Bottom布局输出

        /// <summary>
        /// Bottom布局输出BEGIN
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void RenderBottomBegin(HtmlTextWriter writer)
        {
            if (this.IsSplit)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-split-row bottom-row");
                writer.AddStyleAttribute("bottom", this.Height.ToString());
                writer.AddAttribute("onmousedown", "fnLayoutSplitMove(this, 'bottom', '#" + this.ParentControlID + "');");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderEndTag();
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-bottom");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (!string.IsNullOrEmpty(this.Title))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-header");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-text");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(this.Title);
                writer.RenderEndTag();
                if (this.IsCollapse)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-icon-d");
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "$('#" + this.ParentControlID + "').toggleClass('layout-bottom-hide');");
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
            }
            else
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Top, "0px");
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-body");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-auto");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        /// <summary>
        /// Bottom布局输出END
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void RenderBottomEnd(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        #endregion

        #region Center布局输出

        /// <summary>
        /// Center布局输出END
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void RenderCenterBegin(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-center");
            if (this.IsHorizontal)
            {
                writer.AddStyleAttribute("left", ((Unit)(this.MarginLeft.Value + 4)).ToString());
                writer.AddStyleAttribute("right", ((Unit)(this.MarginRight.Value + 4)).ToString());
                writer.AddStyleAttribute("height", "100%");
            }
            else
            {
                writer.AddStyleAttribute("top", ((Unit)(this.MarginTop.Value + 4)).ToString());
                writer.AddStyleAttribute("bottom", ((Unit)(this.MarginBottom.Value + 4)).ToString());
                writer.AddStyleAttribute("width", "100%");
            }
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (!string.IsNullOrEmpty(this.Title))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-header");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-text");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(this.Title);
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            else
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Top, "0px");
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-body");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-auto");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        /// <summary>
        /// Center布局输出END
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void RenderCenterEnd(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
        }

        #endregion

        #region Left布局输出

        /// <summary>
        /// Left布局输出BEGIN
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void RenderLeftBegin(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-left");
            writer.AddStyleAttribute("width", this.Width.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (!string.IsNullOrEmpty(this.Title))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-header");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-text");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(this.Title);
                writer.RenderEndTag();
                if (this.IsCollapse)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-icon-d");
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "$('#" + this.ParentControlID + "').toggleClass('layout-left-hide');");
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
            }
            else
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Top, "0px");
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-body");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-auto");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        /// <summary>
        /// Left布局输出END
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void RenderLeftEnd(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
            if (!string.IsNullOrEmpty(this.Title) && this.IsCollapse)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-left-copy");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-icon-d");
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "$('#" + this.ParentControlID + "').toggleClass('layout-left-hide');");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderEndTag();
                writer.RenderEndTag();
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-text");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(this.Title);
                writer.RenderEndTag();
                writer.RenderEndTag();
            }

            if (this.IsSplit)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-split-col left-col");
                writer.AddStyleAttribute("left", this.Width.ToString());
                writer.AddAttribute("onmousedown", "fnLayoutSplitMove(this, 'left', '#" + this.ParentControlID + "');");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderEndTag();
            }
        }

        #endregion

        #region Right布局输出

        /// <summary>
        /// Right布局输出BEGIN
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void RenderRightBegin(HtmlTextWriter writer)
        {
            if (this.IsSplit)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-split-col right-col");
                writer.AddStyleAttribute("right", this.Height.ToString());
                writer.AddAttribute("onmousedown", "fnLayoutSplitMove(this, 'right', '#" + this.ParentControlID + "');");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderEndTag();
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-right");
            writer.AddStyleAttribute("width", this.Width.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            if (!string.IsNullOrEmpty(this.Title))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-header");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-text");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(this.Title);
                writer.RenderEndTag();
                if (this.IsCollapse)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-icon-d");
                    writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "$('#" + this.ParentControlID + "').toggleClass('layout-right-hide');");
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                    writer.RenderEndTag();
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
            }
            else
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Top, "0px");
            }
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-body");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-auto");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        /// <summary>
        /// Right布局输出END
        /// </summary>
        /// <param name="writer"></param>
        protected virtual void RenderRightEnd(HtmlTextWriter writer)
        {
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
            if (!string.IsNullOrEmpty(this.Title) && this.IsCollapse)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-right-copy");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-icon-d");
                writer.AddAttribute(HtmlTextWriterAttribute.Onclick, "$('#" + this.ParentControlID + "').toggleClass('layout-right-hide');");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.RenderEndTag();
                writer.RenderEndTag();
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layout-text");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.Write(this.Title);
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
        }

        #endregion
    }
}

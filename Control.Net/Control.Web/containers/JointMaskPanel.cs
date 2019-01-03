using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// 蒙板控件
    /// </summary>
    [ParseChildren(false)]
    [PersistChildren(true)]
    [ToolboxData("<{0}:JointMaskPanel runat=server></{0}:JointMaskPanel>")]
    public class JointMaskPanel : WebControlsBase
    {
        public JointMaskPanel()
            : base()
        {
            this.LaySkin = "demo-class";
            this.WHSize = "auto";
            this.ButtonAlign = "r";
            this.CloseSkin = 1;
            this.ShadowCSS = "[0.3, '#000']";
            this.ShadowClose = false;
            this.AnimType = 0;
            this.IsCloseAnim = false;
            this.ShowMaxMin = false;
            this.Fixed = true;
            this.Resize = true;
            this.BuutonArray = "['确定', '关闭']";
            this.ScrollBar = true;
            this.ZIndex = 19891014;
            this.SetMostTop = false;
            this.ControlType = ControlsType.JointMaskPanel;
            this.Width = 800;
            this.Height = 500;
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
        /// 弹窗皮肤，默认值为demo-class。
        /// 可选：layui-layer-lan 和 layui-layer-molv
        /// </summary>
        public string LaySkin { get; set; }

        /// <summary>
        /// 窗体大小
        /// '500px'：表示宽度500px，高度仍然是自适应
        /// ['500px', '300px']：标识宽度500px，宽度300px
        /// 这里，暂时将该参数去掉功能，改为通过Width和Height属性来进行设置
        /// </summary>
        public string WHSize { get; set; }

        /// <summary>
        /// 弹出框标题。默认值：'信息'。
        /// 也可以设置 ['文本', 'font-size:18px;']
        /// 如果不想显示标题，设置值为false即可
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 坐标，默认值为空，表示“垂直水平居中”
        /// '100px'  ==> 只定义top坐标，水平保持居中
        /// ['100px', '50px'] ==>  同时定义top、left坐标
        /// 't'      ==> 快捷设置顶部坐标
        /// 'r'      ==> 快捷设置右边缘坐标
        /// 'b'      ==> 快捷设置底部坐标
        /// 'l'      ==> 快捷设置左边缘坐标
        /// 'lt'     ==> 快捷设置左上角
        /// 'lb'     ==> 快捷设置左下角
        /// 'rt'     ==> 快捷设置右上角
        /// 'rb'     ==> 快捷设置右下角
        /// </summary>
        public string Offeset { get; set; }

        /// <summary>
        /// 按钮数组。默认值为['确定', '关闭']
        /// ['按钮1', '按钮2', '按钮3', …]
        /// </summary>
        public string BuutonArray { get; set; }

        /// <summary>
        /// 按钮排列位置：默认值为 'r'：右对齐
        /// 'l'：左对齐  'c'：居中对齐
        /// </summary>
        public string ButtonAlign { get; set; }

        /// <summary>
        /// 关闭按钮风格，默认值为1
        /// 可选择值2：关闭按钮弹出模式。
        /// 值为0时。标识不显示关闭按钮。
        /// </summary>
        public int CloseSkin { get; set; }

        /// <summary>
        /// 遮罩样式，默认值[0.3, '#000']
        /// 若不显示遮罩，值为0即可
        /// </summary>
        public string ShadowCSS { get; set; }

        /// <summary>
        /// 是否点击遮罩关闭，默认值为false
        /// </summary>
        public bool ShadowClose { get; set; }

        /// <summary>
        /// 自动关闭时间，默认不自动关闭
        /// 5000：表示5秒后自动关闭
        /// </summary>
        public int AutoCloseTime { get; set; }

        /// <summary>
        /// 弹出动画类型。默认值为0，可支持的动画类型有0-6。
        /// 如果不想显示动画，设置 anim: -1 即可
        /// </summary>
        public int AnimType { get; set; }

        /// <summary>
        /// 关闭动画。默认值为true
        /// </summary>
        public bool IsCloseAnim { get; set; }

        /// <summary>
        /// 是否显示最大小化按钮。默认不显示
        /// </summary>
        public bool ShowMaxMin { get; set; }

        /// <summary>
        /// 鼠标滚动时，层是否固定在可视区域。默认值为true
        /// 如果不想，设置fixed: false即可
        /// </summary>
        public bool Fixed { get; set; }

        /// <summary>
        /// 是否允许拉伸。默认值为true
        /// </summary>
        public bool Resize { get; set; }

        /// <summary>
        /// 是否允许浏览器出现滚动条。默认值为true
        /// </summary>
        public bool ScrollBar { get; set; }

        /// <summary>
        /// 最大宽度。默认值为360。
        /// 只有当WHSize: 'auto'时，maxWidth的设定才有效。
        /// </summary>
        public string MaxWidth { get; set; }

        /// <summary>
        /// 层叠顺序。默认值为19891014
        /// </summary>
        public int ZIndex { get; set; }

        /// <summary>
        /// 是否最前。默认值为false
        /// </summary>
        public bool SetMostTop { get; set; }

        #endregion

        /// <summary>
        /// 重写控件的头部标签输出
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Width.ToString());
            writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
        }

        /// <summary>
        /// 重写控件的尾部标签输出
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
            base.RenderEndTag(writer);

            writer.Write("<script type='text/javascript' language='javascript'>" + this.ClientInitScript + "</script>");
        }

        /// <summary>
        /// 控件对应的JS变量输出
        /// </summary>
        private string ClientInitScript
        {
            get
            {
                return string.Format("var {0}_JSDS={{LaySkin:'{1}', WHSize:['{2}','{3}'], Title:'{4}', Offeset:'{5}', BuutonArray:{6}," +
                                     "ButtonAlign:'{7}', CloseSkin:{8}, ShadowCSS:{9}, ShadowClose:{10}, AutoCloseTime:{11}," +
                                     "AnimType:{12}, IsCloseAnim:{13}, ShowMaxMin:{14}, Fixed:{15}, Resize:{16}, ScrollBar:{17}," +
                                     "MaxWidth:'{18}', ZIndex:{19}, SetMostTop:{20}}};",
                    this.ClientID,
                    this.LaySkin,
                    this.Width.ToString(),
                    ((Unit)(this.Height.Value + 140)).ToString(),
                    string.IsNullOrEmpty(this.Title) ? "" : this.Title,
                    string.IsNullOrEmpty(this.Offeset) ? "" : this.Offeset,
                    this.BuutonArray,
                    this.ButtonAlign,
                    this.CloseSkin,
                    this.ShadowCSS,
                    this.ShadowClose ? "true" : "false",
                    this.AutoCloseTime,
                    this.AnimType,
                    this.IsCloseAnim ? "true" : "false",
                    this.ShowMaxMin ? "true" : "false",
                    this.Fixed ? "true" : "false",
                    this.Resize ? "true" : "false",
                    this.ScrollBar ? "true" : "false",
                    this.MaxWidth,
                    this.ZIndex,
                    this.SetMostTop ? "true" : "false"
                    );
            }
        }
    }
}

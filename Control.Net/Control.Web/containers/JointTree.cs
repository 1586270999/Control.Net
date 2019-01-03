using System;
using System.Web.UI;

namespace Control.Web
{
    /// <summary>
    /// 树控件
    /// </summary>
    [ToolboxData("<{0}:JointTree runat=server></{0}:JointTree>")]
    public class JointTree : WebControlsBase
    {
        public JointTree()
            : base()
        {
            this.ControlType = ControlsType.JointTree;
            this.MultiSelect = false;
            this.AutoSelectFirstNode = true;
            this.IsAsynDataLoad = false;
            this.AjaxUrl = "/DataHandler/AsynGetZTreeData.ashx";
            this.ParentFieldName = "FParentID";
            this.ClientNodeClickEvent = "JointLibrary.MainTreeClickEvent";
            this.ClientNodeDblClickEvent = "";
            this.ClientNodeRightClickEvent = "";
            this.ClientNodeCollapseEvent = "";
            this.ClientNodeExpandEvent = "";
            this.SelectMode = TreeSelectMode.none;
        }

        /// <summary>
        /// 控件初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAfterInit(EventArgs e)
        {
            base.OnAfterInit(e);
        }

        #region 控件属性

        /// <summary>
        /// 数据源查询语句
        /// </summary>
        public string QueryFFuncID { get; set; }

        /// <summary>
        /// 选择模式
        /// </summary>
        public TreeSelectMode SelectMode { get; set; }

        /// <summary>
        /// 是否运行多选，默认false
        /// </summary>
        public bool MultiSelect { get; set; }

        /// <summary>
        /// 是否自动选中第一个节点,默认true
        /// </summary>
        public bool AutoSelectFirstNode { get; set; }

        /// <summary>
        /// 是否异步加载树，默认值为false
        /// </summary>
        public bool IsAsynDataLoad { get; set; }

        /// <summary>
        /// 是否默认全选
        /// </summary>
        public bool IsDefaultSelectAll { get; set; }

        /// <summary>
        /// Ajax回调地址,默认值
        /// </summary>
        public string AjaxUrl { get; set; }

        /// <summary>
        /// 父关联字段,默认FParentID
        /// </summary>
        public string ParentFieldName { get; set; }

        #endregion

        #region 控件常用事件

        /// <summary>
        /// 节点的单击事件
        /// </summary>
        public string ClientNodeClickEvent { get; set; }

        /// <summary>
        /// 节点的双击事件
        /// </summary>
        public string ClientNodeDblClickEvent { get; set; }

        /// <summary>
        /// 节点的右击事件
        /// </summary>
        public string ClientNodeRightClickEvent { get; set; }

        /// <summary>
        /// 节点收缩时事件
        /// </summary>
        public string ClientNodeCollapseEvent { get; set; }

        /// <summary>
        /// 节点展开时事件
        /// </summary>
        public string ClientNodeExpandEvent { get; set; }

        #endregion

        /// <summary>
        /// 树控件输出
        /// </summary>
        /// <param name="writer"></param>
        protected override void Render(HtmlTextWriter writer)
        {
            if (this.Width.Value > 0)
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Width.ToString());
            if (this.Height.Value > 0)
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());

            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "ztree");
            writer.RenderBeginTag(HtmlTextWriterTag.Ul);

            //此处渲染前台ztree格式JS数据,树的数据由前台页面渲染时进行异步加载,后台渲染树不加载具体业务数据
            writer.Write("<script type='text/javascript'>" + this.ClientInitScript + "</script>");

            writer.RenderEndTag();
        }

        /// <summary>
        /// 控件对应的JS变量输出
        /// </summary>
        protected string ClientInitScript
        {
            get
            {
                return string.Format("var {0}_JSDS={{QueryFFuncID:'{1}', AutoSelectFirstNode:'{2}', MultiSelect:'{3}'," +
                                     "ClientNodeClickEvent:'{4}', ClientNodeDblClickEvent:'{5}', ClientNodeRightClickEvent:'{6}'," +
                                     "IsAsynDataLoad:'{7}', IsDefaultSelectAll:'{8}', AjaxUrl:'{9}', ParentFieldName:'{10}', SelectMode:'{11}'}};",
                    this.ClientID,
                    this.QueryFFuncID,
                    this.AutoSelectFirstNode ? "true" : "false",
                    this.MultiSelect ? "true" : "false",
                    this.ClientNodeClickEvent,
                    this.ClientNodeDblClickEvent,
                    this.ClientNodeRightClickEvent,
                    this.IsAsynDataLoad ? "true" : "false",
                    this.IsDefaultSelectAll ? "true" : "false",
                    this.AjaxUrl,
                    this.ParentFieldName,
                    this.SelectMode.ToString()
                    );
            }
        }
    }
}

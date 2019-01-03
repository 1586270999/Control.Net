using System;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// Web控件基类
    /// </summary>
    public class WebControlsBase : WebControl, ICompositeControlDesignerAccessor
    {
        public WebControlsBase()
            : base()
        {
        }

        /// <summary>
        /// 控件类型
        /// </summary>
        public ControlsType ControlType { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName
        {
            get { return "JointSoft.WebControls"; }
        }

        /// <summary>
        /// 版本名称
        /// </summary>
        public string VersionName
        {
            get { return "1.0"; }
        }

        void ICompositeControlDesignerAccessor.RecreateChildControls()
        {
            this.RecreateChildControls();
        }

        protected virtual void RecreateChildControls()
        {
            this.CreateChildControls();
        }

        /// <summary>
        /// 控件初始化
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            
            this.OnAfterInit(e);
        }

        /// <summary>
        /// 控件初始化之后
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnAfterInit(EventArgs e)
        {

        }
    }
}

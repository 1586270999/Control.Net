using System.Web.UI;

namespace Control.Web
{
    /// <summary>
    /// 多行文本框录入控件(集成Label标签控件)
    /// </summary>
    [ToolboxData("<{0}:JointTextArea runat=server></{0}:JointTextArea>")]
    public class JointTextArea : JointTextField
    {
        public JointTextArea()
        {
            this.MultiLine = true;
            this.CssClass = "layui-textarea";
            this.ControlType = ControlsType.JointTextArea;
            this.WantReturns = true;
        }

        /// <summary>
        /// 换行属性设置(ON/OFF)
        /// </summary>
        public string WrapAttri { get; set; }

        /// <summary>
        /// 为真时回车换行,跳出按住Ctrl，为假时回车即跳出.默认值为true
        /// </summary>
        public bool WantReturns { get; set; }

        protected override void OnPreRender(System.EventArgs e)
        {
            if (this.WantReturns)
            {
                this.OnKeyUp = "JointManage.FocusToNextInputV2(this);";
            }

            base.OnPreRender(e);
        }
    }
}

using System.Web.UI;
using System.Web.UI.WebControls;

namespace Control.Web
{
    /// <summary>
    /// 文件上传控件
    /// </summary>
    [ToolboxData("<{0}:JointFileUpload runat=server></{0}:JointFileUpload>")]
    public class JointFileUpload : FormsControlBase
    {
        public JointFileUpload()
            : base()
        {
            this.LabelAlign = "Left";
            this.ControlType = ControlsType.JointFileUpload;
            this.IsShowLabel = true;
            this.Url = string.Empty;
            this.Data = "{ FID: 1}";
            this.Accept = FileUploadType.images;
            this.Exts = "";
            this.Auto = true;
            this.Size = 0;
            this.Multiple = false;
            this.Number = 10;
            this.IsShowPreview = true;
            this.ButtonSkins = ButtonObject.primary;
            this.ButtonSize = ButtonSize.small;
            this.ButtonText = "选择文件";
            this.Radius = false;
            this.IsShowFilePath = true;
            this.ImageWidth = 500;
            this.ImageHeight = 500;
            this.ViewMode = false;
        }

        #region 控件属性

        /// <summary>
        /// 服务上传地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 请求上传接口的额外参数
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// 请求的接口头。如headers:{token:'sasaas'}
        /// </summary>
        public string Headers { get; set; }

        /// <summary>
        /// 允许上传文件类型，默认图片
        /// </summary>
        public FileUploadType Accept { get; set; }

        /// <summary>
        /// 规定打开文件选择框时，筛选出的文件类型。如：
        /// image/*:只显示图片文件
        /// image/jpg,image/png:只显示jpg和png类型的文件
        /// </summary>
        public string AcceptMime { get; set; }

        /// <summary>
        /// 允许上传的文件后缀
        /// 默认值：jpg/png/gif/bmp/jpeg
        /// </summary>
        public string Exts { get; set; }

        /// <summary>
        /// 选择文件后是否自动上传
        /// 默认值：true
        /// </summary>
        public bool Auto { get; set; }

        /// <summary>
        /// 指定一个按钮触发上传，一般配合Auto:false使用
        /// </summary>
        public string BindAction { get; set; }

        /// <summary>
        /// 设定文件域的字段名
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 设置文件最大可允许上传的大小，单位KB
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 是否允许多选
        /// </summary>
        public bool Multiple { get; set; }

        /// <summary>
        /// 设置同时可上传的文件数量，一般配合Multiple参数出现
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 是否接受拖拽的文件上传，默认值为false
        /// </summary>
        public bool Drag { get; set; }

        /// <summary>
        /// 是否显示预览图片或预览窗口
        /// </summary>
        public bool IsShowPreview { get; set; }

        /// <summary>
        /// 是否显示图片路径
        /// </summary>
        public bool IsShowFilePath { get; set; }

        /// <summary>
        /// 预览图片图片宽度
        /// </summary>
        public Unit ImageWidth { get; set; }

        /// <summary>
        /// 预览图片高度
        /// </summary>
        public Unit ImageHeight { get; set; }

        #endregion

        #region 控件常用事件

        /// <summary>
        /// 选择文件后的回调参数,返回一个Object参数
        /// </summary>
        public string Choose { get; set; }

        /// <summary>
        /// 文件提交上传前的回调,返回一个object参数
        /// </summary>
        public string Before { get; set; }

        /// <summary>
        /// 执行上传请求后的回调。返回三个参数，分别为：
        /// res（服务端响应信息）
        /// index（当前文件的索引）
        /// upload（重新上传的方法，一般在文件上传失败后使用）
        /// </summary>
        public string Done { get; set; }

        /// <summary>
        /// 执行上传请求出现异常的回调（一般为网络异常、URL 404等）。返回两个参数，分别为：
        /// index（当前文件的索引）
        /// upload（重新上传的方法）。
        /// </summary>
        public string Error { get; set; }

        #endregion

        #region 上传控件元素属性

        /// <summary>
        /// 主题
        /// </summary>
        public ButtonObject ButtonSkins { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public ButtonSize ButtonSize { get; set; }

        /// <summary>
        /// 文字
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string ButtonIcon { get; set; }

        #endregion

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
            writer.AddAttribute("for", this.ClientID);
            writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, this.LabelAlign);
            if (!this.IsShowTable)
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.LabelWidth.ToString());
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            if (!string.IsNullOrEmpty(this.LabelText))
                writer.Write(this.LabelText + "：");
            writer.RenderEndTag();
        }

        /// <summary>
        /// 重写文件上传控件的样式输出
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderTextBoxOut(HtmlTextWriter writer)
        {
            if (this.IsShowPreview)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-upload");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                DoRenderButton(writer);
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-upload-list");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_ImgShow");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-upload-img");
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, ((Unit)this.ImageWidth.Value).ToString());
                writer.AddStyleAttribute(HtmlTextWriterStyle.Height, ((Unit)this.ImageHeight.Value).ToString());
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_Img");
                if (!string.IsNullOrEmpty(this.DefaultValue))
                    writer.AddAttribute("src", this.DefaultValue);
                writer.RenderBeginTag(HtmlTextWriterTag.Img);
                writer.RenderEndTag();
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_DemoText");
                writer.RenderBeginTag(HtmlTextWriterTag.P);
                writer.RenderEndTag();
                writer.RenderEndTag();
                writer.RenderEndTag();
            }
            else
            {
                DoRenderButton(writer);
            }

            writer.Write("<script type='text/javascript' language='javascript'>" + this.ClientInitScript + "</script>");
        }

        /// <summary>
        /// 上传按钮的输出
        /// </summary>
        /// <param name="writer"></param>
        private void DoRenderButton(HtmlTextWriter writer)
        {
            if (!this.IsShowLabel)
                this.LabelWidth = 0;
            writer.AddAttribute("readonly", "readonly");
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-input-readonly");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, ((Unit)(this.Width.Value - this.LabelWidth.Value - 168)).ToString());
            if (!this.IsShowFilePath)
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
            if (!string.IsNullOrEmpty(this.FieldName))
                writer.AddAttribute("FieldName", this.FieldName);
            if (this.IsForSave)
                writer.AddAttribute("IsForSave", this.IsForSave ? "true" : "false");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
            if (!string.IsNullOrEmpty(this.DefaultValue))
                writer.AddAttribute("value", this.DefaultValue);
            writer.RenderBeginTag(HtmlTextWriterTag.Input);
            writer.RenderEndTag();

            string _AddClass = string.Empty;
            if (this.ButtonSkins != ButtonObject.empty)
                _AddClass = " layui-btn-" + this.ButtonSkins.ToString();

            if (this.ButtonSize != ButtonSize.empty)
            {
                switch (this.ButtonSize)
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
            if (!this.ViewMode)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-btn" + _AddClass + (this.Radius ? " layui-btn-radius" : ""));
                writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "0 5px");
                writer.AddStyleAttribute(HtmlTextWriterStyle.MarginTop, "0px");
                writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_Btn1");
                writer.RenderBeginTag(HtmlTextWriterTag.Button);
                if (!string.IsNullOrEmpty(this.ButtonIcon))
                    writer.Write("<i class=\"layui-icon " + this.ButtonIcon + " \"></i>");
                writer.Write(this.ButtonText);
                writer.RenderEndTag();
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "layui-btn" + _AddClass + (this.Radius ? " layui-btn-radius" : ""));
            writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "0 5px");
            writer.AddStyleAttribute(HtmlTextWriterStyle.MarginTop, "0px");
            writer.AddStyleAttribute(HtmlTextWriterStyle.MarginLeft, "1px");
            writer.AddAttribute("onclick", "layer.open({type: 1,title: '图片预览', area:['550px','580px'], content: $('#" + this.ClientID + "_ImgShow')});");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_Btn2");
            writer.RenderBeginTag(HtmlTextWriterTag.Button);
            if (!string.IsNullOrEmpty(this.ButtonIcon))
                writer.Write("<i class=\"layui-icon " + this.ButtonIcon + " \"></i>");
            writer.Write("预览");
            writer.RenderEndTag();
        }

        /// <summary>
        /// 控件对应的JS变量输出
        /// </summary>
        private string ClientInitScript
        {
            get
            {
                string __EventDone = "";
                if (!string.IsNullOrEmpty(this.Done))
                {
                    __EventDone = string.Format("{0}_Btn1_OBJ.done=function(res, index, upload){{{1}(res, index, upload);}};", this.ClientID, this.Done);
                }
                else
                {
                    __EventDone = string.Format("{0}_Btn1_OBJ.done=function(res, index, upload){{$('#{0}').val(res.src);$('#{0}_Img').attr('src', res.src);}};", this.ClientID);
                }
                return string.Format(@"var {0}_OBJ={{
                        elem:'#{0}', 
                        accept:'{1}', 
                        exts:'{2}', 
                        data: {3},
                        size: {4},
                        multiple: {5},
                        number: '{6}',
                        drag: {7},
                        auto: {8}
                    }};",
                    this.ClientID + "_Btn1",
                    this.Accept,
                    this.Exts,
                    this.Data,
                    this.Size,
                    this.Multiple ? "true" : "false",
                    this.Number,
                    this.Drag ? "true" : "false",
                    this.Auto ? "true" : "false") +
                    (!string.IsNullOrEmpty(this.BindAction) ? this.ClientID + "_Btn1_OBJ.bindAction=" + this.BindAction + ";" : "") +
                    (!string.IsNullOrEmpty(this.Choose) ? this.ClientID + "_Btn1_OBJ.choose=function(obj){" + this.Choose + "(obj);};" : "") +
                    (!string.IsNullOrEmpty(this.Before) ? this.ClientID + "_Btn1_OBJ.before=function(obj){" + this.Before + "(obj);};" : "") +
                    __EventDone +
                    (!string.IsNullOrEmpty(this.Error) ? this.ClientID + "_Btn1_OBJ.error=function(index, upload){" + this.Error + "(index, upload);};" : "") +
                    "JointFileUpload.render(" + this.ClientID + "_Btn1_OBJ);";
            }
        }
    }
}

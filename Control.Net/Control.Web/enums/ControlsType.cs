namespace Control.Web
{
    /// <summary>
    /// JointFrame所有Web控件枚举
    /// 1~~49：表示表单控件
    /// 50~~59：表示表格控件
    /// 60~~79：表示容器控件
    /// 80~~99：表示布局控件
    /// 100~~100+：留待以后控件扩展试用
    /// </summary>
    public enum ControlsType
    {
        /// <summary>
        /// 文本框控件
        /// </summary>
        JointTextField = 1,

        /// <summary>
        /// 单选框控件
        /// </summary>
        JointCheckBox = 2,

        /// <summary>
        /// 下拉框控件
        /// </summary>
        JointCombobox = 3,

        /// <summary>
        /// 日期控件
        /// </summary>
        JointDateField = 4,

        /// <summary>
        /// 多行文本框控件
        /// </summary>
        JointTextArea = 5,

        /// <summary>
        /// 复选框组控件
        /// </summary>
        JointCheckBoxGroup = 6,

        /// <summary>
        /// Html文本编辑框
        /// </summary>
        JointHtmlEditor = 7,

        /// <summary>
        /// 带选择按钮的文本框
        /// </summary>
        JointTriggerField = 8,

        /// <summary>
        /// 数字录入控件
        /// </summary>
        JointNumericText = 9,

        /// <summary>
        /// 单选框组控件
        /// </summary>
        JointRadioGroup = 10,

        /// <summary>
        /// 带单位的文本框控件
        /// </summary>
        JointUnitField = 11,

        /// <summary>
        /// Label标签
        /// </summary>
        JointLabel = 12,

        /// <summary>
        /// 进度条控件
        /// </summary>
        JointProgressBar = 13,

        /// <summary>
        /// 颜色选择器控件
        /// </summary>
        JointColorField = 14,

        /// <summary>
        /// 按钮控件
        /// </summary>
        JointButton = 15,

        /// <summary>
        /// 按钮组控件
        /// </summary>
        JointButtonGroup = 16,

        /// <summary>
        /// 按钮带菜单控件
        /// </summary>
        JointSplitButton = 17,

        /// <summary>
        /// 数据域控件
        /// </summary>
        JointHiddenData = 18,

        /// <summary>
        /// 文件上传控件
        /// </summary>
        JointFileUpload = 19,

        /// <summary>
        /// 树控件
        /// </summary>
        JointTree = 20,

        /// <summary>
        /// 隐藏域控件
        /// </summary>
        JointHiddenField = 21,

        /// <summary>
        /// 动态下拉框
        /// </summary>
        JointDynamicCombobox = 22,

        /// <summary>
        /// 星形评价控件
        /// </summary>
        JointStar = 23,

        /// <summary>
        /// 滑动条控件
        /// </summary>
        JointSlider = 24,

        /// <summary>
        /// 轮播主控件
        /// </summary>
        JointCarousel = 25,

        /// <summary>
        /// 轮播明细项控件
        /// </summary>
        JointCarouselItem = 26,

        /// <summary>
        /// 简单编辑框
        /// </summary>
        JointLayEditor = 27,

        /// <summary>
        /// 分割线控件
        /// </summary>
        JointSplitLine = 28,





        /// <summary>
        /// 普通表格控件
        /// </summary>
        JointDataTables = 50,

        /// <summary>
        /// 普通表格控件LayUi
        /// </summary>
        JointLayTables = 51,

        /// <summary>
        /// 树表格控件
        /// </summary>
        JointTreeTables = 52,

        /// <summary>
        /// 动态编辑表格
        /// </summary>
        JointEditTables = 53,





        /// <summary>
        /// 蒙板控件
        /// </summary>
        JointMaskPanel = 60,

        /// <summary>
        /// Tab组控件
        /// </summary>
        JointTabPanel = 61,

        /// <summary>
        /// Tab明细项控件
        /// </summary>
        JointTabPanelItem = 62,

        /// <summary>
        /// Iframe控件
        /// </summary>
        JointIframe = 63,

        /// <summary>
        /// 带标题的字段集区块控件
        /// </summary>
        JointFieldSet = 64,

        /// <summary>
        /// 模板控件，可内置HTML模版样式，通用数据源
        /// </summary>
        JointTemplate = 65,





        /// <summary>
        /// 布局控件(外部Table)
        /// </summary>
        JointBorderLayout = 80,

        /// <summary>
        /// 布局控件(内部TR/TD)
        /// </summary>
        JointLayout = 81,

        /// <summary>
        /// 手风琴控件/折叠面板控件
        /// </summary>
        JointAccordion = 82,

        /// <summary>
        /// 折叠面板明细项控件
        /// </summary>
        JointAccordionItem = 83
    }
}

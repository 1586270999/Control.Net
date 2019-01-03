namespace Control.Web
{
    /// <summary>
    /// 设定日期控件的定位方式
    /// </summary>
    public enum DatePosition
    {
        /// <summary>
        /// 绝对定位，始终吸附在绑定元素周围。默认值
        /// </summary>
        _absolute = 0,

        /// <summary>
        /// 固定定位，初始吸附在绑定元素周围，不随浏览器滚动条所左右。一般用于在固定定位的弹层中使用。
        /// </summary>
        _fixed = 1,

        /// <summary>
        /// 静态定位，控件将直接嵌套在指定容器中。 
        /// 注意：请勿与 show 参数的概念搞混淆。show为 true 时，控件仍然是采用绝对或固定定位。而这里是直接嵌套显示
        /// </summary>
        _static = 2
    }
}

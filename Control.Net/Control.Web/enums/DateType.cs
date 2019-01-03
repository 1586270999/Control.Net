namespace Control.Web
{
    /// <summary>
    /// 日期控件的选择器类型
    /// </summary>
    public enum DateType
    {
        /// <summary>
        /// 日期选择器,可选择：年、月、日。type默认值，一般可不填
        /// </summary>
        date = 1,

        /// <summary>
        /// 日期时间选择器,可选择：年、月、日、时、分、秒
        /// </summary>
        datetime = 2,

        /// <summary>
        /// 时间选择器,只提供时、分、秒选择
        /// </summary>
        time = 3,

        /// <summary>
        /// 年选择器,只提供年列表选择
        /// </summary>
        year = 4,

        /// <summary>
        /// 年月选择器,只提供年、月选择
        /// </summary>
        month = 5
    }
}

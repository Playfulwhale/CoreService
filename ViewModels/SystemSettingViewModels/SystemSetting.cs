namespace ApiTemplate.ViewModels.SystemSettingViewModels
{
    public class SystemSetting
    {
        /// <summary>
        /// Khóa chính tự tăng
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tên hiển thị
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tên tham số
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Giá trị tham số
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Dữ liệu để lựa chọn
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Kiểu dữ liệu
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Nhóm SystemSettingGroup
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// The URL used to retrieve the resource conforming to REST'ful JSON http://restfuljson.org/.
        /// </summary>
        public string Url { get; set; }
    }
}
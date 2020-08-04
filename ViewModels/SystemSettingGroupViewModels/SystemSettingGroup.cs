namespace ApiTemplate.ViewModels.SystemSettingGroupViewModels
{
    public class SystemSettingGroup
    {
        /// <summary>
        /// Khóa chính tự tăng
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tên nhóm
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Thứ tự hiển thị
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// The URL used to retrieve the resource conforming to REST'ful JSON http://restfuljson.org/.
        /// </summary>
        public string Url { get; set; }
    }
}
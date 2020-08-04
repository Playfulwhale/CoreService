namespace ApiTemplate.Models
{
    public class SystemSettingGroup : BaseModel
    {
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
    }
}
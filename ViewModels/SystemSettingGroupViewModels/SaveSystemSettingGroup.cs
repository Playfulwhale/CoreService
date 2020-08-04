namespace ApiTemplate.ViewModels.SystemSettingGroupViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class SaveSystemSettingGroup
    {
        /// <summary>
        /// Tên nhóm
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Thứ tự hiển thị
        /// </summary>
        [Required]
        public int Order { get; set; }
    }
}
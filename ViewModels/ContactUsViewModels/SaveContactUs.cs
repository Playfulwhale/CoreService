namespace ApiTemplate.ViewModels.ContactUsViewModels
{
    using Swashbuckle.AspNetCore.Annotations;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Name of Department.
    /// </summary>
    public class SaveContactUs
    {
        /// <summary>
        /// Quý danh của người liên hệ. VD: Mrs, Mr
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Họ
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Địa chỉ mail
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// Nội dung tin nhắn(nội dung liên hệ)
        /// </summary>
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        [Required]
        public string Status { get; set; }
    }
}
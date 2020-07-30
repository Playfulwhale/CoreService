namespace ApiTemplate.ViewModels.ContactUsViewModels
{
    using Swashbuckle.AspNetCore.Annotations;

    /// <summary>
    /// Name of Department.
    /// </summary>
    public class ContactUs
    {
        /// <summary>
        /// Khóa chính tự tăng
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Quý danh của người liên hệ. VD: Mrs, Mr
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Họ
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Địa chỉ mail
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Nội dung tin nhắn(nội dung liên hệ)
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Trạng thái
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// The URL used to retrieve the resource conforming to REST'ful JSON http://restfuljson.org/.
        /// </summary>
        public string Url { get; set; }
    }
}
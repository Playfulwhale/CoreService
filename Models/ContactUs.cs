namespace ApiTemplate.Models
{
    public class ContactUs : BaseModel
    {
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
    }

}
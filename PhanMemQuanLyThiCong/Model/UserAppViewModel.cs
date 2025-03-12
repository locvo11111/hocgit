namespace PhanMemQuanLyThiCong.Model
{
    public class UserAppViewModel
    {
        public int Id { get; set; }

        public string SerialNo { get; set; }

        public string UserId { get; set; }

        public int ServerId { get; set; }
        public int TypeAccountId { get; set; } = 1;
    }
}
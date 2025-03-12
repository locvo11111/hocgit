using System;

namespace PhanMemQuanLyThiCong.Model
{
    public class ServerInfoViewModel
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string UrlAPI { get; set; }

        public string ServerIP { get; set; }

        public string ServerName { get; set; }

        public string ServerPort { get; set; } = "1433";

        public string DatabaseName { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }
    }
}
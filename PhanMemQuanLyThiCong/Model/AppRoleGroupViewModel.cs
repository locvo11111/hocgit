namespace PhanMemQuanLyThiCong.Model
{
    public class AppRoleGroupViewModel
    {
        public int GroupId { set; get; }
        public string RoleId { set; get; }

        public virtual AppRoleViewModel AppRole { set; get; }

        public virtual AppGroupViewModel AppGroup { set; get; }
    }
}
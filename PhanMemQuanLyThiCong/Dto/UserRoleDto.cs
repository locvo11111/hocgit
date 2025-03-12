using VChatCore.Model;

namespace VChatCore.Dto
{
    public class UserRoleDto
    {
        public string RoleId { get; set; }
        public string UserId { get; set; }
        public string THDACode { get; set; }
        public UserDto user { get; set; }
    }
}

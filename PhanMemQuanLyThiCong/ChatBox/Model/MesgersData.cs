using DevExpress.DevAV.Chat.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevExpress.DevAV.Chat.Model.Contact;

namespace PhanMemQuanLyThiCong.ChatBox.Model
{
    public class MesgersData
    {
        public static List<ContactViewsModel> GetContactViewsModels()
        {
            Image photo = Image.FromFile(@"D:\New folder (2)\pm360\PM360\PhanMemQuanLyThiCong\Shared\Icon\group.png");

            List<ContactViewsModel> contactViewsModels = new List<ContactViewsModel>()
            {
                new ContactViewsModel() { ID = 1, Avatar = photo, HasUnreadMessages = true, IsInactive = true, LastActivity = DateTime.Now, LastOnline = DateTime.Now, LastOnlineText = "Đã gửi", StatusCore= ContactViewsModel.Status.Active, UnreadCount = 1, UserName = "User1" },
                new ContactViewsModel() { ID = 2, Avatar = photo, HasUnreadMessages = true, IsInactive = true, LastActivity = DateTime.Now, LastOnline = DateTime.Now, LastOnlineText = "Đã gửi", StatusCore = ContactViewsModel.Status.Active, UnreadCount = 1, UserName = "User2" },
                new ContactViewsModel() { ID = 3, Avatar = photo, HasUnreadMessages = false, IsInactive = false, LastActivity = DateTime.Now, LastOnline = DateTime.Now, LastOnlineText = "Đã nhận", StatusCore = ContactViewsModel.Status.Active, UnreadCount = 2, UserName = "User3" }
            };
            return contactViewsModels;
        }
        public static List<Contact> GetContactsAsync()
        {
            List<Contact> contacts = new List<Contact>();
            foreach (var item in GetContactViewsModels())
            {
                Contact contact = new Contact(id: item.ID, userName: item.UserName, photo: item.Avatar);
                contacts.Add(contact);
            }
            return contacts;
        }
        public static List<MessageDto> GetMessageDtosAsync()
        {
            List<Contact> contacts = GetContactsAsync();
            List<MessageDto> messageDtos = new List<MessageDto>()
            {
                new MessageDto(){ Contact = contacts[0], Content = "Anh dạy dễ hiểu ghê. c.ơn anh đã mang lại những bài học bổ ích", isOwner = true },
               new MessageDto() { Contact = contacts[0], Content = "anh dạy kỹ thật quá hay <3 Hôm nào anh làm về Responsive nữa đi ạ", isOwner = false },
               new MessageDto() { Contact = contacts[0], Content = "Cảm giác giống table, nhưng mà xịn hơn nhiều 🤣 video rất hay, anh dạy rất dễ hiểu", isOwner = true },
               new MessageDto() { Contact = contacts[0], Content = "Video làm kĩ càng với chăm chút quá ạ, cảm ơn anh vì video bổ ích này", isOwner = false },

               new MessageDto() { Contact = contacts[1], Content = "Hay quá a ơi, Cảm ơn a nhiều ạ", isOwner = true },
               new MessageDto() { Contact = contacts[1], Content = "Tuyệt vời. Cảm ơn anh đã chia sẻ", isOwner = false },

               new MessageDto() { Contact = contacts[2], Content = "cái grid-area  giờ mới biết luôn. ảo thật =))", isOwner = true},
               new MessageDto() { Contact = contacts[2], Content = "hay quá", isOwner = false },
            };
            return messageDtos;
        }
    }
}

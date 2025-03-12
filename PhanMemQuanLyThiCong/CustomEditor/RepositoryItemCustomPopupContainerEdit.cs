using System.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

namespace CustomDevExEditor
{
	[UserRepositoryItem("Register")]
	public class RepositoryItemCustomPopupContainerEdit : RepositoryItemPopupContainerEdit
	{
		static RepositoryItemCustomPopupContainerEdit()
		{
			Register();
		}

		public RepositoryItemCustomPopupContainerEdit()
		{
		}

		public const string CustomEditName = "CustomPopupContainerEdit";
		public override string EditorTypeName
		{
			get { return CustomEditName; }
		}
		
		public Image Glyph { get; set; }

		public static void Register()
		{
			EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName,
			  typeof(CustomPopupContainerEdit), typeof(RepositoryItemCustomPopupContainerEdit),
			  typeof(PopupContainerEditViewInfo), new CustomPopupContainerEditPainter(), true));
		}
	}
}
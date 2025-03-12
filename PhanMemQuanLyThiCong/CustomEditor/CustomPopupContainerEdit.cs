using System;
using System.ComponentModel;
using DevExpress.XtraEditors;

namespace CustomDevExEditor
{
	public class CustomPopupContainerEdit : PopupContainerEdit
	{
		static CustomPopupContainerEdit()
		{
			RepositoryItemCustomPopupContainerEdit.Register();
			
		}

		public CustomPopupContainerEdit()
		{
		}

		public override string EditorTypeName
		{
			get { return RepositoryItemCustomPopupContainerEdit.CustomEditName; }
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new RepositoryItemCustomPopupContainerEdit Properties
		{
			get { return base.Properties as RepositoryItemCustomPopupContainerEdit; }
		}
	}
}

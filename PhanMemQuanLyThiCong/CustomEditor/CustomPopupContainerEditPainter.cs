using System.Drawing;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.ViewInfo;

namespace CustomDevExEditor
{
	public class CustomPopupContainerEditPainter : ButtonEditPainter
	{
		protected override void DrawContent(ControlGraphicsInfoArgs info)
		{
			base.DrawContent(info);
			DrawImage(info);
		}

		private static void DrawImage(ControlGraphicsInfoArgs info)
		{
			PopupContainerEditViewInfo viewInfo = info.ViewInfo as PopupContainerEditViewInfo;
			RepositoryItemCustomPopupContainerEdit item = viewInfo.Item as RepositoryItemCustomPopupContainerEdit;

			if ( item.Glyph == null )
				return;

			Rectangle textBounds = viewInfo.GetTextBounds();
			Size imgSize = item.Glyph.Size;
			if ( imgSize.Width > textBounds.Width )
				imgSize.Width = textBounds.Width;

			Rectangle imageBounds = new Rectangle(textBounds.X + (textBounds.Width / 2 - imgSize.Width / 2), textBounds.Y, imgSize.Width, textBounds.Height);
			info.Graphics.DrawImage(item.Glyph, imageBounds);
		}
	}
}

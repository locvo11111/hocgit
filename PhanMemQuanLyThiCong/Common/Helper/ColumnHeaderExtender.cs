using DevExpress.CodeParser;
using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.Utils.Drawing;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Drawing;
using System.Windows.Forms;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public class ColumnHeaderExtender
    {
        private AdvBandedGridView parentView;
        private GridView view;
        private SkinButtonObjectPainter customButtonPainter;
        private EditorButtonObjectInfoArgs args;
        private Size buttonSize;
        private ObjectState currentState;

        public ColumnHeaderExtender(AdvBandedGridView parentView, GridView view)
        {
            this.parentView = parentView;
            this.view = view;
            buttonSize = new Size(85, 25);
            currentState = ObjectState.Normal;
        }

        public void AddCustomButton()
        {
            CreateButtonPainter();
            CreateButtonInfoArgs();
            SubscribeToEvents();
        }

        private void CreateButtonInfoArgs()
        {
            EditorButton btn = new EditorButton(ButtonPredefines.Glyph);
            args = new EditorButtonObjectInfoArgs(btn, new DevExpress.Utils.AppearanceObject());
        }

        private void CreateButtonPainter()
        {
            customButtonPainter = new SkinButtonObjectPainter(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveLookAndFeel);
        }

        private void SubscribeToEvents()
        {
            view.CustomDrawFooter += view_CustomDrawFooter;
            view.MouseDown += OnMouseDown;
            view.MouseUp += OnMouseUp;
            view.MouseMove += OnMouseMove;
        }

        private void view_CustomDrawFooter(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            e.DefaultDraw();
            DrawCustomButton(e);
            e.Handled = true;
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            GridHitInfo hitInfo = view.CalcHitInfo(e.Location);
            if (hitInfo.HitTest != GridHitTest.Footer) return;
            if (IsButtonRect(e.Location))
            {
                SetButtonState(ObjectState.Normal);
                object obj = parentView.GetRowCellValue(parentView.FocusedRowHandle, "Ten");
                bool isNew = obj == null || string.IsNullOrEmpty(obj.ToString());
                string text = view.Name == "gv_NhienLieu" ? "nhiên liệu" : "định mức";
                if (isNew)
                {
                    XtraMessageBox.Show($"Vui lòng lưu tên máy trước khi lưu {text} cho máy hiện hành","Phần mềm quản lý thi công - Thông báo");
                    return;
                }
                if(text== "định mức")
                {
                    obj = parentView.GetRowCellValue(parentView.FocusedRowHandle, "NhienLieuChinh");
                    if (obj.ToString()=="")
                    {
                        MessageShower.ShowError("Vui lòng tạo Nhiên liệu chính cho máy trước khi tạo định mức!!!!!!!");
                        return;
                    }
                }
                view.AddNewRow();
                view.RefreshData();
                DXMouseEventArgs.GetMouseArgs(e).Handled = true;
            }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            GridHitInfo hitInfo = view.CalcHitInfo(e.Location);
            if (hitInfo.HitTest != GridHitTest.Footer) return;
            if (IsButtonRect(e.Location))
                SetButtonState(ObjectState.Hot);
            else
                SetButtonState(ObjectState.Normal);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo hitInfo = view.CalcHitInfo(e.Location);
            if (hitInfo.HitTest != GridHitTest.Footer) return;
            if (IsButtonRect(e.Location))
                SetButtonState(ObjectState.Pressed);
        }

        private void SetButtonState(ObjectState state)
        {
            currentState = state;
            view.InvalidateFooter();
        }

        private bool IsButtonRect(Point point)
        {
            GraphicsInfo info = new GraphicsInfo();
            info.AddGraphics(null);
            GridViewInfo viewInfo = view.GetViewInfo() as GridViewInfo;
            Rectangle bounds = viewInfo.FooterInfo.Bounds;
            Rectangle buttonRect = CalcButtonRect(bounds, info.Graphics);
            info.ReleaseGraphics();
            return buttonRect.Contains(point);
        }

        private Rectangle CalcButtonRect(Rectangle footerBounds, Graphics gr)
        {
            //calc button bounds here as your needs dictate
            Rectangle buttonRect = new Rectangle(new Point(footerBounds.X + 20, footerBounds.Y + 5), buttonSize);
            return buttonRect;
        }

        private void DrawCustomButton(RowObjectCustomDrawEventArgs e)
        {
            SetUpButtonInfoArgs(e);
            customButtonPainter.DrawObject(args);
            StringFormat f = new StringFormat();
            //calc text bounds here  
            Rectangle textBounds = args.Bounds;
            textBounds.Inflate(-5, -5);
            int imageWidth = 16;
            int imageHeight = 16;
            customButtonPainter.DrawCaption(args, "Thêm mới", new Font(e.Appearance.Font, FontStyle.Bold), customButtonPainter.GetForeBrush(args), textBounds, f);
            e.Graphics.DrawImage(new Bitmap(Properties.Resources._103), new Rectangle(args.Bounds.Right - imageWidth - 5, args.Bounds.Y + 3, imageWidth, imageHeight));
            customButtonPainter.DrawElementInfoBitmap(args);
            customButtonPainter.DrawElementIntoBitmap(args, ObjectState.Normal);
        }

        private void SetUpButtonInfoArgs(RowObjectCustomDrawEventArgs e)
        {
            args.Cache = e.Cache;
            args.Bounds = CalcButtonRect(e.Info.Bounds, e.Graphics);
            args.State = currentState;
        }

        private static void DefaultDrawColumnHeader(RowObjectCustomDrawEventArgs e)
        {
            e.Painter.DrawObject(e.Info);
        }

        private void UnsubscribeFromEvents()
        {
            view.CustomDrawFooter -= view_CustomDrawFooter;
            view.MouseDown -= OnMouseDown;
            view.MouseUp -= OnMouseUp;
            view.MouseMove -= OnMouseMove;
        }

        public void RemoveCustomButton()
        {
            UnsubscribeFromEvents();
        }
    }
}
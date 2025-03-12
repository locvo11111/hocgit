using DevExpress.Accessibility;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DevExpress.XtraEditors.XtraInputBox;
using PhanMemQuanLyThiCong.Common.Constant.Enum;
using System.Drawing;

namespace PhanMemQuanLyThiCong.Controls
{
    [UserRepositoryItem("Registerurp_ActionButtonEditRepository")]
    public class RepositoryItemurp_ActionButtonEditRepository : RepositoryItemButtonEdit
    {
        static RepositoryItemurp_ActionButtonEditRepository()
        {
            Registerurp_ActionButtonEditRepository();
        }

        public const string CustomEditName = "urp_ActionButtonEditRepository";

        public RepositoryItemurp_ActionButtonEditRepository()
        {
            InitializeComponent();
        }

        public override string EditorTypeName => CustomEditName;

        public static void Registerurp_ActionButtonEditRepository()
        {
            Image img = null;
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(CustomEditName, typeof(urp_ActionButtonEditRepository), typeof(RepositoryItemurp_ActionButtonEditRepository), typeof(urp_ActionButtonEditRepositoryViewInfo), new urp_ActionButtonEditRepositoryPainter(), true, img, typeof(ButtonEditAccessible)));

        }

        private void InitializeComponent()
        {
            TextEditStyle = TextEditStyles.HideTextEditor;

            var btAdd = new EditorButton();
            btAdd.Kind = ButtonPredefines.Glyph;
            btAdd.Caption = "Add";
            btAdd.ToolTip = "Thêm";
            btAdd.ImageOptions.SvgImage = Resources.actions_add;
            btAdd.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            btAdd.IsLeft = true;
            btAdd.Visible = false;

            var btView = new EditorButton();
            btView.Kind = ButtonPredefines.Glyph;
            btView.Caption = "View";
            btView.ToolTip = "Chi tiết";
            btView.ImageOptions.Image = Resources.show_16x16;
            btView.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            btView.IsLeft = true;

            var btEdit = new EditorButton();
            btEdit.Kind = ButtonPredefines.Glyph;
            btEdit.Caption = "Edit";
            btEdit.ToolTip = "Chỉnh sửa";
            btEdit.ImageOptions.SvgImage = Resources.actions_edit;
            btEdit.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            btEdit.IsLeft = true;

            var btDelete = new EditorButton();
            btDelete.Kind = ButtonPredefines.Glyph;
            btDelete.Caption = "Delete";
            btDelete.ToolTip = "Xóa";
            btDelete.ImageOptions.SvgImage = Resources.actions_deletecircled;
            btDelete.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            btDelete.IsLeft = true;


            Buttons.Clear();

            Buttons.AddRange(new EditorButton[] { btDelete, btEdit, btView, btAdd });
            //Buttons.Capacity = 3;
            ButtonClick += Button_Click;
        }


        private void Button_Click(object sender, ButtonPressedEventArgs e)
        {
            var caption = e.Button.Caption;
            var val = (ButtonActionTypeEnum)Enum.Parse(typeof(ButtonActionTypeEnum), caption);
            ((ButtonClickEventHandler)base.Events[buttonClick])?.Invoke(val);
        }

        public delegate void ButtonClickEventHandler(ButtonActionTypeEnum buttonType);
        readonly object buttonClick = new object();

        public event ButtonClickEventHandler CusButtonClick
        {
            add
            {
                base.Events.AddHandler(buttonClick, value);
            }
            remove
            {
                base.Events.RemoveHandler(buttonClick, value);
            }
        }

        public override void Assign(RepositoryItem item)
        {
            BeginUpdate();
            try
            {
                base.Assign(item);
                RepositoryItemurp_ActionButtonEditRepository source = item as RepositoryItemurp_ActionButtonEditRepository;
                if (source == null) return;
                //
            }
            finally
            {
                EndUpdate();
            }
        }
    }

    [ToolboxItem(true)]
    public class urp_ActionButtonEditRepository : ButtonEdit
    {
        static urp_ActionButtonEditRepository()
        {
            RepositoryItemurp_ActionButtonEditRepository.Registerurp_ActionButtonEditRepository();
        }

        public urp_ActionButtonEditRepository()
        {
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemurp_ActionButtonEditRepository Properties => base.Properties as RepositoryItemurp_ActionButtonEditRepository;

        public override string EditorTypeName => RepositoryItemurp_ActionButtonEditRepository.CustomEditName;
    }

    public class urp_ActionButtonEditRepositoryViewInfo : ButtonEditViewInfo
    {
        public urp_ActionButtonEditRepositoryViewInfo(RepositoryItem item) : base(item)
        {
        }
    }

    public class urp_ActionButtonEditRepositoryPainter : ButtonEditPainter
    {
        public urp_ActionButtonEditRepositoryPainter()
        {
        }
    }
}

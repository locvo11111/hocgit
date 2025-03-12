using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraEditors;
using PhanMemQuanLyThiCong.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Validate;

namespace PhanMemQuanLyThiCong.Controls
{
    public class uc_CustomValidateEditor : ButtonEdit
    {
        DXValidationProvider _dxValidationProvider;
        bool initDone = false;
        public uc_CustomValidateEditor()
        {
            this.
            InitializeComponent();
            initDone = true;

        }


        private void CreateButtonCollection()
        {

        }
        private void InitializeComponent()
        {
            _dxValidationProvider = new DXValidationProvider();
            ButtonClick += Button_Click;
            var bt = new EditorButton();
            bt.Kind = ButtonPredefines.Glyph;
            bt.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            bt.IsLeft = true;



            var bteye = new EditorButton();

            bteye.Kind = ButtonPredefines.Glyph;
            bteye.ImageOptions.Image = Resources.show_16x16;
            bteye.IsLeft = false;
            bteye.Caption = "Show";
            bteye.ToolTip = "Mật khẩu";
            Properties.Buttons.AddRange(new EditorButton[] { bt, bteye });
            //Properties.Buttons.Add(bteye);



        }

        public ValidationModeEditorEnum _TBTMode;
        [Description("Mode")]
        [Category("TBT")]
        public ValidationModeEditorEnum TBTMode
        {
            get
            {
                return _TBTMode;
            }
            set
            {
                _TBTMode = value;


                Properties.Buttons[1].Visible = false;

                if (value == ValidationModeEditorEnum.EMAIL)
                {
                    Properties.Buttons[0].ImageOptions.SvgImage = Resources.at_solid;

                    _dxValidationProvider.SetValidationRule(this,
                                                            new CustomValidateEmail
                                                            {
                                                                ErrorText = "Vui lòng nhập đúng định dạng Email hợp lệ!",
                                                                ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning
                                                            });
                }
                else if (value == ValidationModeEditorEnum.PHONE)
                {
                    Properties.Buttons[0].ImageOptions.SvgImage = Resources.glyph_phone;
                    _dxValidationProvider.SetValidationRule(this,
                                         new CustomValidatePhoneNumber
                                         {
                                             ErrorText = "Vui lòng nhập đúng định dạng số điện thoại hợp lệ!",
                                             ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning
                                         });
                }
                else if (value == ValidationModeEditorEnum.USERNAME)
                {
                    Properties.Buttons[0].ImageOptions.SvgImage = Resources.actions_user;
                    _dxValidationProvider.SetValidationRule(this,
                                         new CustomValidateEmailOrUserName
                                         {
                                             ErrorText = "Vui lòng nhập đúng định dạng tên người dùng!",
                                             ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning
                                         });
                }
                else if (value == ValidationModeEditorEnum.PASSWORD)
                {
                    Properties.Buttons[0].ImageOptions.SvgImage = Resources.security_key;
                    Properties.Buttons[1].Visible = true;
                    Properties.UseSystemPasswordChar = true;
                    Properties.Buttons[1].ImageOptions.Image = Resources.show_16x16;

                    _dxValidationProvider.SetValidationRule(this,
                                         new CustomValidatePassword
                                         {
                                             ErrorText = "Vui lòng nhập đúng định dạng tên người dùng!",
                                             ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning
                                         });
                }
            }
        }

        public bool CustomValidate()
        {
            var rt = _dxValidationProvider.Validate();
            //if (rt)
            //    BackColor = Color.White;
            return rt;
        }

        public void Button_Click(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Caption == "Show")
            {
                if (Properties.UseSystemPasswordChar)
                {
                    Properties.UseSystemPasswordChar = false;
                    e.Button.ImageOptions.Image = Resources.hide_16x16;
                }
                else
                {
                    Properties.UseSystemPasswordChar = true;
                    e.Button.ImageOptions.Image = Resources.show_16x16;
                }
            }
        }

    }
}

using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VChatCore.Dto;

namespace PhanMemQuanLyThiCong
{
    public partial class DevForm_ThemCaNhanVaoTHDA : DevExpress.XtraEditors.XtraForm
    {
        public DevForm_ThemCaNhanVaoTHDA()
        {
            InitializeComponent();
        }

        /*private async Task fcn_GetListUser()
        {
            var response = await CusHttpClient.InstanceTBT.GetAsync($@"{MyConstant.SERVER_TYPE_MODEL_Users}\allusersdto");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                //var Users = JObject.Parse(JObject.Parse(content.ToString())["data"].ToString());
                var lsUser = JsonConvert.DeserializeObject<List<UserDto>>(JObject.Parse(content.ToString())["data"].ToString())
                    .Select(x => new { x.Email, x.FullName}).ToList();
                lookupUser.Properties.DataSource = lsUser;
                lookupUser.Properties.ValueMember = "Email";
                lookupUser.Properties.DisplayMember = "FullName";
                lookupUser.Properties.PopulateColumns();
                //lookupUser.data
                //foreach (var THDA in lsTHDA)
                //{
                //    accordionControl1.Elements.Where(x => x.Text == "Danh sách tổng hợp dự án").First().Elements.Add(new AccordionControlElement()
                //    {
                //        Text = THDA.TenTongHopDuAn,
                //        AccessibleName = THslke_ThongTinDuAn.EditValue,
                //        Style = ElementStyle.Item
                //    });
                //}
            }
            else
            {
                MessageShower.ShowInformation("Không thể lấy thông tin người dùng");
            }
        }*/

        private async void DevForm_ThemCaNhanVaoTHDA_Load(object sender, EventArgs e)
        {
            //await fcn_GetListUser();

        }

        private void lookupUser_AutoSearch(object sender, DevExpress.XtraEditors.Controls.LookUpEditAutoSearchEventArgs e)
        {
            string[] fields = new string[] { "Email", "FullName" };
            e.SetParameters(fields, e.Text, FindPanelParserKind.And, FilterCondition.Contains);
            //e.SetHighlightRanges(HighlightTags(e.Text));
        }

        static Func<string, string, DisplayTextHighlightRange[]> HighlightTags(string pattern)
        {
            var indexOf = IgnoreCaseComparisonFunctions.GetIndexOf(
                CultureInfo.CurrentCulture.CompareInfo, CompareOptions.IgnoreCase);
            var parts = pattern.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);
            return (displayText, fieldName) => {
                var tags = displayText.Split(new string[] { ", " },
                    StringSplitOptions.RemoveEmptyEntries);
                var ranges = new List<DisplayTextHighlightRange>();
                for (int i = 0/*skip country tag*/; i < tags.Length; i++)
                {
                    int tagStart = displayText.IndexOf(tags[i]);
                    for (int j = 0; j < parts.Length; j++)
                    {
                        int index = indexOf(tags[i], parts[j]);
                        if (index != -1)
                            ranges.Add(new DisplayTextHighlightRange(tagStart + index, parts[j].Length));
                    }
                }
                return ranges.ToArray();
            };
        }
    }
}

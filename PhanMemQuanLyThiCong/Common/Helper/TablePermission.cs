using DevExpress.DocumentServices.ServiceModel.DataContracts;
using DevExpress.Utils;
using DevExpress.Utils.Svg;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraRichEdit.Model;
using PhanMemQuanLyThiCong.Common.Constant;
using PhanMemQuanLyThiCong.Common.Enums;
using PhanMemQuanLyThiCong.Common.Helper;
using PhanMemQuanLyThiCong.Dto;
using PhanMemQuanLyThiCong.Model;
using PM360.Common.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace PermissionApp
{
    class TablePermission
    {
        private static TablePermission _defaultInstance;
        public static TablePermission Instance
        {
            get
            {
                if (_defaultInstance == null)
                {
                    _defaultInstance = new TablePermission();
                }
                return _defaultInstance;
            }
            set => _defaultInstance = value;
        }

        public TablePermission()
        {

        }

        public DataTable CreatedTablePermissionTabMenu(List<PermissionViewModel> permissions, AccountType type)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Id", typeof(string));
            tbl.Columns.Add("ParentId", typeof(string));
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("View", typeof(bool));
            tbl.Columns.Add("Add", typeof(bool));
            tbl.Columns.Add("Edit", typeof(bool));
            tbl.Columns.Add("Delete", typeof(bool));
            tbl.Columns.Add("Approve", typeof(bool));
            tbl.Columns["View"].DefaultValue = false;
            tbl.Columns["Add"].DefaultValue = false;
            tbl.Columns["Edit"].DefaultValue = false;
            tbl.Columns["Delete"].DefaultValue = false;
            tbl.Columns["Approve"].DefaultValue = false;

            Dictionary<string, string> dicParentName = new Dictionary<string, string>();    
            foreach (FunctionCode fun in Enum.GetValues(typeof(FunctionCode)))
            {
               
                string IdGoc = Enum.GetName(typeof(FunctionCode), fun);

                if (IdGoc.StartsWith("EXTERNAL"))
                    continue;

                string Id = IdGoc.Replace("__EX", "");
                if (!Id.Contains("_")) //bỏ qua mục cha
                {
                    //dicParentName.Add(IdGoc, fun.GetEnumDisplayName());
                    tbl.Rows.Add(Id, "0", fun.GetEnumDisplayName());
                    continue;
                }

                if (type != AccountType.INTERNAL && !IdGoc.EndsWith("__EX"))
                    continue;

                string ParentId = $"{Id.Substring(0, Id.IndexOf('_'))}";
                string Name = fun.GetEnumDisplayName();
                tbl.Rows.Add(Id, ParentId, Name);
                DataRow crRow = tbl.AsEnumerable().Last();

                var commandsInFunction = permissions.Where(x => x.FunctionId == Id);

                foreach (var cmd in commandsInFunction)
                {
                    string cmdId = cmd.CommandId;
                    crRow[cmdId] = true;
                }
            }

            //var prs = tbl.AsEnumerable().Select(x => (string)x["ParentId"]).Distinct().ToArray();

            //foreach (var pr in prs)
            //{

            //    tbl.Rows.Add(pr, "0", dicParentName[pr]);
            //}



            return tbl;
        }

        public DataTable CreatedTablePermissionCongTac(CongTacBriefWithRoleDetailViewModel vm, string colFk)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Id", typeof(string));
            tbl.Columns.Add("ParentId", typeof(string));
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("View", typeof(bool));
            tbl.Columns.Add("Add", typeof(bool));
            tbl.Columns.Add("Edit", typeof(bool));
            tbl.Columns.Add("Delete", typeof(bool));
            tbl.Columns.Add("Approve", typeof(bool));
            tbl.Columns["View"].DefaultValue = false;
            tbl.Columns["Add"].DefaultValue = false;
            tbl.Columns["Edit"].DefaultValue = false;
            tbl.Columns["Delete"].DefaultValue = false;
            tbl.Columns["Approve"].DefaultValue = false;

            List<CongTacBriefViewModel> CongTacBriefs = vm.CongTacBriefs;
            List<RoleDetailViewModel> RoleDetails = vm.RoleDetails;

            var grDVLs = CongTacBriefs.GroupBy(x => x.CodeCT_DVL);

            foreach (var grDVL in grDVLs)
            {
                tbl.Rows.Add(grDVL.Key, null, grDVL.First().TenCT_DVL);

                //CongTacBriefs.Add(new CongTacBriefViewModel()
                //{
                //    Id = grDVL.Key,
                //    Name = grDVL.First().TenCT_DVL,
                //});
                var grDVNs = grDVL.GroupBy(x => x.CodeHM_DVN);

                foreach (var grDVN in grDVNs)
                {
                    tbl.Rows.Add(grDVN.Key, grDVL.Key, grDVN.First().TenHM_DVN);

                    //CongTacBriefs.Add(new CongTacBriefViewModel()
                    //{
                    //    Id = grDVN.Key,
                    //    ParentId = grDVL.Key,
                    //    Name = grDVN.First().TenHM_DVN,

                    //});

                    foreach (var CT in grDVN)
                    {
                        tbl.Rows.Add(CT.Id, grDVN.Key, CT.Name);

                        DataRow crRow = tbl.AsEnumerable().Last();

                        var permissions = RoleDetails.Where(x => (string)x.GetValueByPropName(colFk) == CT.Id);
                        foreach (var per in permissions)
                        {
                            string cmdId = per.CommandId;
                            crRow[cmdId] = true;
                        }
                    }
                }
            };

            return tbl;
        }


        //public Tuple<DataTable, ImageCollection> GetAllTableMenu(RibbonControl ribbonControl)
        //{

        //    var imageCollection = new ImageCollection();
        //    DataTable table_ribbon = CreatedTablePermission();
        //    ArrayList visiblePages = ribbonControl.TotalPageCategory.GetVisiblePages();
        //    foreach (RibbonPage page in visiblePages)
        //    {
        //        var page_name = page.Name;
        //        var page_caption = page.Text;
        //        imageCollection.AddImage(Properties.Resources.root_icon, page_name);
        //        table_ribbon.Rows.Add(page_name, "0", page_caption, 1, 0, null, imageCollection.Images.Count - 1);

        //        foreach (RibbonPageGroup group in page.Groups)
        //        {
        //            var page_group_name = group.Name;
        //            var page_group_caption = group.Text;
        //            if (page_group_name.Equals("ribbonPageGroup_Giaodien"))
        //            {
        //                break;
        //            }
        //            imageCollection.AddImage(Properties.Resources.folder_icon, page_name);
        //            table_ribbon.Rows.Add(page_group_name, page_name, page_group_caption, 0, 1, null, imageCollection.Images.Count - 1);

        //            foreach (BarItemLink item in group.ItemLinks)
        //            {
        //                var item_caption = item.Caption;
        //                var item_name = item.Item.Name;

        //                if (item.ImageOptions.HasSvgImage)
        //                {
        //                    var item_image = item.ImageOptions.SvgImage;
        //                    SvgBitmap source = new SvgBitmap(item_image);
        //                    var imgFromSGV = source.Render(null, 0.5);
        //                    //var size = new Size(16, 16);
        //                    //Bitmap target = new Bitmap(size.Width, size.Height);
        //                    //using (Graphics g = Graphics.FromImage(target))
        //                    //{
        //                    //    source.RenderToGraphics(g,
        //                    //        SvgPaletteHelper.GetSvgPalette(LookAndFeel, ObjectState.Normal));
        //                    //}
        //                    imageCollection.AddImage(imgFromSGV, item_caption);
        //                    using (MemoryStream mStream = new MemoryStream())
        //                    {
        //                        item_image.Save(mStream);
        //                        table_ribbon.Rows.Add(item_name, page_group_name, item_caption, 0, 0, mStream.ToArray(), imageCollection.Images.Count - 1);
        //                    }
        //                }
        //                else if (item.ImageOptions.HasImage)
        //                {
        //                    var item_image = item.ImageOptions.Image;
        //                    imageCollection.AddImage(item_image, item_caption);
        //                    using (MemoryStream mStream = new MemoryStream())
        //                    {
        //                        item_image.Save(mStream, item_image.RawFormat);
        //                        table_ribbon.Rows.Add(item_name, page_group_name, item_caption, 0, 0, mStream.ToArray(), imageCollection.Images.Count - 1);
        //                    }
        //                }
        //                else
        //                {
        //                    table_ribbon.Rows.Add(item_name, page_group_name, item_caption, 0, 0, null);
        //                }
        //            }

        //        }
        //    }

        //    return Tuple.Create(table_ribbon, imageCollection);
        //}
    }
}
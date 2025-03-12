using DevExpress.CodeParser;
using System;
using System.Collections.Generic;

namespace PhanMemQuanLyThiCong.Model
{
    public class ResultMessage<T>
    {
        public int STATUS_CODE { get; set; } = -1;
        public string MESSAGE_CODE { get; set; }
        public string MESSAGE_CONTENT { get; set; }
        public string MESSAGE_STATUS { get; set; }
        public bool MESSAGE_TYPECODE { get; set; } = false;
        public string MESSAGE_TYPE { get; set; } = "";
        public Guid? MESSAGE_GIUD { get; set; }
        public T Dto { get; set; }
    }
}
namespace PhanMemQuanLyThiCong.Views {
    using System.Collections.Concurrent;
    using System.IO;
    using DevExpress.XtraEditors;
    using PhanMemQuanLyThiCong;

    abstract class Style {
        readonly string htmlName, cssName;
        protected Style(string htmlName = null, string cssName = null) {
            if(string.IsNullOrEmpty(htmlName)) {
                var typeName = this.GetType().Name;
                this.htmlName = typeName.Substring(0, typeName.Length - nameof(Style).Length);
            }
            else this.htmlName = htmlName;
            if(string.IsNullOrEmpty(cssName)) {
                var typeName = this.GetType().Name;
                this.cssName = typeName.Substring(0, typeName.Length - nameof(Style).Length);
            }
            else this.cssName = cssName;
        }
        string htmlCore;
        public string Html {
            get { return htmlCore ?? (htmlCore = ReadText(htmlName, nameof(Html))); }
        }
        string cssCore;
        public string Css {
            get { return cssCore ?? (cssCore = ReadText(cssName, nameof(Css))); }
        }
        #region ReadText
        readonly static ConcurrentDictionary<string, string> texts = new ConcurrentDictionary<string, string>();
        static string ReadText(string name, string type) {
            string resourceName = "PhanMemQuanLyThiCong.Resources." + $@"{type}.{name}.{type}";
            return texts.GetOrAdd(resourceName, x =>
            {
                using ( var stream = typeof(Program).Assembly.GetManifestResourceStream(x))
                    return new StreamReader(stream).ReadToEnd();
            });
            //return "Cha test";
        }
        #endregion ReadText
        #region Apply
        public void Apply(HtmlContentControl control) {
            control.HtmlImages = Program.SvgImages;
            control.HtmlTemplate.Template = Html;
            control.HtmlTemplate.Styles = Css;
        }
        public void Apply(HtmlContentPopup popup) {
            //popup.HtmlImages = ChatClient.SvgImages;
            popup.HtmlTemplate.Template = Html;
            popup.HtmlTemplate.Styles = Css;
        }
        public void Apply(DevExpress.Utils.Html.HtmlTemplate template) {
            template.Template = Html;
            template.Styles = Css;
        }
        #endregion Apply
    }
}

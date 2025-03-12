using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Portable;

namespace DevExpress.XtraGantt.Demos.Utils {
    class EventLogger {
        XtraRichEdit.RichEditControl logView;
        public EventLogger(XtraRichEdit.RichEditControl logView) {
            this.logView = logView;
            logView.Views.SimpleView.Padding = new PortablePadding(2);
            logView.Document.AppendHtmlText(string.Empty);
        }
        public void Append(string value) {
            if(!logView.Enabled) return;
            logView.Document.AppendHtmlText(string.Format("<b>{0}</b><br/>", value));
            AppendCore();
        }
        void AppendCore() {
            if(logView.Visible)
                logView.ScrollToCaret(logView.HtmlText.Length);
        }
        public void AppendWithIndent(string value) {
            if(!logView.Enabled) return;
            logView.Document.AppendHtmlText(string.Format("{0}<br/>", value));
            AppendCore();
        }
    }
}

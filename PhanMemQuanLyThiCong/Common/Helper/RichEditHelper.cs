using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraSpreadsheet.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong.Common.Helper
{
    public static class RichEditHelper
    {
        public static void UpdateBasicFormatRange(this DocumentRange rangeDoc, bool? isBold = null, bool? isItalic = null, UnderlineType? isUnderline = null, Color? foreColor = null, Color? BackColor = null)
        {
            // Start the update
            SubDocument subDoc = rangeDoc.BeginUpdateDocument();

            // Obtain character properties
            CharacterProperties cp = subDoc.BeginUpdateCharacters(rangeDoc);

            if (isBold != null)
                cp.Bold = isBold;
            if (isItalic != null)
                cp.Italic = isItalic;
            if (isUnderline != null)
                cp.Underline = isUnderline;

            if (foreColor != null)
                cp.ForeColor = foreColor;

            if (BackColor != null)
                cp.BackColor = BackColor;

            // Finalize modifications
            subDoc.EndUpdateCharacters(cp);
            rangeDoc.EndUpdateDocument(subDoc);
        }

        public static void fcn_InsertTextWithAlignment(this Document doc, DocumentPosition position, string text, ParagraphAlignment align)
        {
            DocumentRange range = doc.InsertText(position, text);
            fcn_AlignmentParagrap(doc, range, align);
        }

        private static void fcn_AlignmentParagrap(Document doc, DocumentRange range, ParagraphAlignment alignment)
        {
            ParagraphProperties pp = doc.BeginUpdateParagraphs(range);
            pp.Alignment = alignment;
            doc.EndUpdateParagraphs(pp);
        }
    }
}

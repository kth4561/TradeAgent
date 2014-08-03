using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace TradeAgent.Control
{
    public class RichTextBoxEx : RichTextBox
    {
        public RichTextBoxEx Write(string str)
        {
            return this.Write(str, this.ForeColor);
        }

        public RichTextBoxEx Write(string str, Color color)
        {
            this.SelectionStart = this.TextLength;
            this.SelectionLength = 0;
            this.SelectionColor = color;
            this.AppendText(str);
            this.SelectionColor = this.ForeColor;
            return this;
        }

        public RichTextBoxEx WriteLine(string str)
        {
            Write(str);
            this.AppendText(Environment.NewLine);
            return this;
        }

        public RichTextBoxEx WriteLine(string str, Color color)
        {
            Write(str, color);
            this.AppendText(Environment.NewLine);
            return this;
        }
    }
}

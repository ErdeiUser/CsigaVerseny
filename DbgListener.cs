using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsigaVerseny
{
    class DbgListener : TraceListener
    {
        private TextBox tBox;
        public bool Enabled=true;
        public DbgListener(TextBox box)
        {
            this.tBox = box;
        }

        public override void Write(string msg)
        {
            if (!Enabled) return;
            Console.Write(msg);
            tBox.Parent.Invoke(new MethodInvoker(delegate ()
            {
                tBox.AppendText(msg);
                Application.DoEvents();
            }));
        }

        public override void WriteLine(string msg)
        {
            if (!Enabled) return;
            Write(msg + "\r\n");
        }
    }
}

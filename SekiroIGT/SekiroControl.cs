using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SekiroIGT {
    class SekiroControl : UserControl  {

        public SekiroControl() { }

        public XmlNode GetSettings(XmlDocument doc) => doc.CreateElement("Settings");
        public void SetSettings(XmlNode node) { }

        private void InitializeComponent() {
            this.SuspendLayout();
            // 
            // SekiroControl
            // 
            this.Name = "SekiroControl";
            this.Size = new System.Drawing.Size(554, 327);
            this.ResumeLayout(false);

        }
    }
}

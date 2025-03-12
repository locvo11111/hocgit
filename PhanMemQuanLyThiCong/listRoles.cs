using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanMemQuanLyThiCong
{
    public partial class listRoles : Component
    {
        public listRoles()
        {
            InitializeComponent();
        }

        public listRoles(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}

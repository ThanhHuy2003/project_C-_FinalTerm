﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject.App.Admin.ThongBao
{
    public partial class CardTBAdmin : UserControl
    {
        Form frm = new TBDetailAdmin();
        public CardTBAdmin()
        {
            InitializeComponent();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frm.ShowDialog();
        }
    }
}

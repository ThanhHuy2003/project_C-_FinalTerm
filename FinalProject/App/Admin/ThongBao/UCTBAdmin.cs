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
    public partial class UCTBAdmin : UserControl
    {
        public UCTBAdmin()
        {
            InitializeComponent();
        }
        Form frm = new TBDetailAdmin();
        private void btnAddTB_Click(object sender, EventArgs e)
        {
            frm.ShowDialog();
        }
    }
}
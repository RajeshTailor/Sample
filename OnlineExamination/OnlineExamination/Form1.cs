using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineExamination
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //is_validate();
        }

        private bool is_validate()
        {

            bool no_error = true;

            //if (txtFName.Text == string.Empty)
            //{

            //    errorProvider1.SetError(txtFName, "Please Enter First Name");
            //    no_error = false;
            //}

            return no_error;
        }
    }
}

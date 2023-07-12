using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormBeta
{
    public partial class Main : Form
    {
        ContactEntities db;
        public Main(Contact obj)
        {
            InitializeComponent();
            db = new ContactEntities();
            if (obj == null)
            {
                contactBindingSource.DataSource = new Contact();
                db.Contacts.Add(contactBindingSource.Current as Contact);
            }
            else
            {
                contactBindingSource.DataSource = obj;
                db.Contacts.Attach(contactBindingSource.Current as Contact);


            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(UsernameBox.Text))
                {
                    MessageBox.Show("Please enter your contact name", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UsernameBox.Focus();
                    e.Cancel = true;
                    return;
                }

                db.SaveChanges();
                e.Cancel = false;
            }
            e.Cancel = false;
        }
    }
}

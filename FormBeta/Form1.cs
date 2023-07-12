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
    public partial class Form1 : Form
    {
        ContactEntities db;
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (contactBindingSource.Current == null)
                return;

            using (Main frm = new Main (contactBindingSource.Current as Contact))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    contactBindingSource.DataSource = db.Contacts.ToList();

            }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                dataGridView.DataSource = contactBindingSource;
            }
            else
            {
                var searchQuery = txtSearch.Text;
                var query = contactBindingSource.DataSource as List<Contact>;
                query = query.Where(o => o.Username.StartsWith(searchQuery) || (o.Email ?? "").StartsWith(searchQuery)).ToList();

                dataGridView.DataSource = query;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (contactBindingSource.Current != null)
            {

                if (MessageBox.Show("Are you sure you want to delete this record?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    db.Contacts.Remove(contactBindingSource.Current as Contact);
                    contactBindingSource.RemoveCurrent();
                    db.SaveChanges();

                }

            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            using (Main frm = new Main (null))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    contactBindingSource.DataSource = db.Contacts.ToList();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            db = new ContactEntities();
            contactBindingSource.DataSource = db.Contacts.ToList();
        }
    }
}

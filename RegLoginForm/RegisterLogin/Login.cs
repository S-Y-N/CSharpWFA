using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace RegisterLogin
{
    public partial class Login : Form
    {
       
        public Login()
        {
            InitializeComponent();
            this.txtPass.AutoSize = false;
            this.txtPass.Size = new Size(this.txtPass.Width, 40); 
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DB db = new DB();

            String username = txtLogin.Text;
            String password = txtPass.Text;

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM Admin WHERE Login = @usn and Password = @pass", db.getConnection());

            command.Parameters.Add("@usn", SqlDbType.VarChar).Value = username;
            command.Parameters.Add("@pass", SqlDbType.VarChar).Value = password;

            adapter.SelectCommand = command;

            adapter.Fill(table);
            //check if the user exist or not
            if (table.Rows.Count > 0)
            {
                this.Hide();
                MainForm mainForm = new MainForm();
                mainForm.Show();
            }
            else if(username.Equals("admin")&&password.Equals("admin"))
                {
                this.Hide();
                AdminPanel adminForm = new AdminPanel();
                adminForm.Show();
                }
            else
            {
                if(username.Trim().Equals(""))
                {
                    MessageBox.Show("Введите ваш логин в поле логин","Пустое поле логина", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (password.Trim().Equals(""))
                {
                    MessageBox.Show("Введите ваш пароль в поле пароль", "Пустое поле пароля", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль", "Неверные данные", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lblGoToRegisterForm_Click(object sender, EventArgs e)
        {
            RegisterForm rf = new RegisterForm();
            rf.Show();
            Hide();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void txtLogin_Leave(object sender, EventArgs e)
        {
            string fLog = txtLogin.Text;
            if(fLog.Equals(""))
            {
                txtLogin.Text = "введите логин";
                txtLogin.ForeColor = Color.Gray;
            }
        }
        private void txtLogin_Enter(object sender, EventArgs e)
        {
            string flog = txtLogin.Text;
            if (flog.Equals("введите логин"))
            {
                txtLogin.Text = "";
                txtLogin.ForeColor = Color.Black;
            }  
        }
        private void txtPass_Leave(object sender, EventArgs e)
        {
            string fpass = txtPass.Text;
            if (fpass.Equals(""))
            {
                txtPass.Text = "введите пароль";
                txtPass.ForeColor = Color.Gray;
                txtPass.UseSystemPasswordChar = false;
            }
        }
        private void txtPass_Enter(object sender, EventArgs e)
        {
            string fpass = txtPass.Text;
            if (fpass.Equals("введите пароль"))
            {
                txtPass.Text = "";
                txtPass.ForeColor = Color.Black;
                txtPass.UseSystemPasswordChar = true;
            }
        }
    }
}

using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RegisterLogin
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            this.txtPass.AutoSize = false;
            this.txtConfirmPass.AutoSize = false;
            this.txtPass.Size = new Size(txtPass.Width, 40);
            this.txtConfirmPass.Size = new Size(txtConfirmPass.Width, 40);
        }

        private void lblGoToLoginForm_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            Hide();
        }

        private void txtFirstName_Enter(object sender, EventArgs e)
        {
            string fname = txtFirstName.Text;
            if (fname.Equals("Имя"))
            {
                txtFirstName.Text = "";
                txtFirstName.ForeColor = Color.Black;
            }
        }

        private void txtFirstName_Leave(object sender, EventArgs e)
        {
            string fname = txtFirstName.Text;
            if (fname.Equals(""))
            {
                txtFirstName.Text = "Имя";
                txtFirstName.ForeColor = Color.Gray;
            }
        }

        private void txtLastName_Leave(object sender, EventArgs e)
        {
            string lName = txtLastName.Text;
            if (lName.Equals(""))
            {
                txtLastName.Text = "Фамилия";
                txtLastName.ForeColor = Color.Gray;
            }
        }

        private void txtLastName_Enter(object sender, EventArgs e)
        {
            string lName = txtLastName.Text;
            if (lName.Equals("Фамилия"))
            {
                txtLastName.Text = "";
                txtLastName.ForeColor = Color.Black;
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            if (email.Equals(""))
            {
                txtEmail.Text = "Email";
                txtEmail.ForeColor = Color.Gray;
            }
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            if (email.Equals("Email"))
            {
                txtEmail.Text = "";
                txtEmail.ForeColor = Color.Black;
            }
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            string uname = txtUserName.Text;
            if (uname.Equals(""))
            {
                txtUserName.Text = "Логин";
                txtUserName.ForeColor = Color.Gray;
            }
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            string uname = txtUserName.Text;
            if (uname.Equals("Логин"))
            {
                txtUserName.Text = "";
                txtUserName.ForeColor = Color.Black;
            }
        }

        private void txtPass_Leave(object sender, EventArgs e)
        {
            string pass = txtPass.Text;
            if (pass.Equals(""))
            {
                txtPass.Text = "Пароль";
                txtPass.UseSystemPasswordChar = false;
                txtPass.ForeColor = Color.Gray;
            }
        }

        private void txtPass_Enter(object sender, EventArgs e)
        {
            string pass = txtPass.Text;
            if (pass.Equals("Пароль"))
            {
                txtPass.Text = "";
                txtPass.UseSystemPasswordChar = true;
                txtPass.ForeColor = Color.Black;
            }
        }

        private void txtConfirmPass_Enter(object sender, EventArgs e)
        {
            string conPass = txtConfirmPass.Text;
            if (conPass.Equals("Повторите пароль"))
            {
                txtConfirmPass.Text = "";
                txtConfirmPass.UseSystemPasswordChar = true;
                txtConfirmPass.ForeColor = Color.Black;
            }
        }

        private void txtConfirmPass_Leave(object sender, EventArgs e)
        {
            string conPass = txtConfirmPass.Text;
            if (conPass.Equals(""))
            {
                txtConfirmPass.Text = "Повторите пароль";
                txtConfirmPass.UseSystemPasswordChar = false;
                txtConfirmPass.ForeColor = Color.Gray;
            }
        }

        private void btnCreateAcc_Click(object sender, EventArgs e)
        {
            //add a new user

            DB db = new DB();
            SqlCommand command = new SqlCommand("INSERT INTO Admin( Name, Surname, Phone, Login, Password) VALUES (@fn, @ln, @email, @usn, @pass)", db.getConnection());
            SqlCommand command1 = new SqlCommand("INSERT INTO Clients( Name, Surname, Phone) VALUES (@fn1, @ln1, @phone)", db.getConnection());
            command.Parameters.Add("@fn", SqlDbType.VarChar).Value = txtFirstName.Text;
            command.Parameters.Add("@ln", SqlDbType.VarChar).Value = txtLastName.Text;
            command.Parameters.Add("@email", SqlDbType.VarChar).Value = txtEmail.Text;
            command.Parameters.Add("@usn", SqlDbType.VarChar).Value = txtUserName.Text;
            command.Parameters.Add("@pass", SqlDbType.VarChar).Value = txtPass.Text;


            command1.Parameters.Add("@fn1", SqlDbType.VarChar).Value = txtFirstName.Text;
            command1.Parameters.Add("@ln1", SqlDbType.VarChar).Value = txtLastName.Text;
            command1.Parameters.Add("@phone", SqlDbType.VarChar).Value = txtEmail.Text;
            //open the connection 

            db.openConnection();

            //check of the textboxes contains the default values
            if (!checkTextBozesValues())
            {
                //check ot hte pass equal the confirm pass
                if (txtPass.Text.Equals(txtConfirmPass.Text))
                {
                    if (checkUsername())
                    {
                        MessageBox.Show("Данный логин уже существует. Выбирите другой","Ошибка",MessageBoxButtons.OKCancel,MessageBoxIcon.Error);
                    }
                    else
                    {
                        //execute the query
                        if (command.ExecuteNonQuery() == 1 &&command1.ExecuteNonQuery()==1)
                        {
                            MessageBox.Show("Аккаунт создан","Регистрация",MessageBoxButtons.OK);
                            this.Hide();
                            Login login = new Login();
                            login.Show();
                        }
                        else
                        {
                            MessageBox.Show("Обратитесь в тех поддержку","Ошибка", MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Проверочный пароль не совпадает","Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                //check if the username already exists 
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
  
            //close the connection
            db.closeConnection();
        }
        //check if the username already exists
        public Boolean checkUsername()
        {
            DB db = new DB();
            String username = txtUserName.Text;
         

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM Admin WHERE Name = @usn", db.getConnection());

            command.Parameters.Add("@usn", SqlDbType.VarChar).Value = username;

            adapter.SelectCommand = command;

            adapter.Fill(table);
            //check if this username already exists in the database 
            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean checkTextBozesValues()
        {

            String fname = txtFirstName.Text;
            String lname = txtLastName.Text;
            String email = txtEmail.Text;
            String uname = txtUserName.Text;
            String pass = txtPass.Text;
            if (fname.Equals("Имя") || lname.Equals("Фамилия") || email.Equals("Email") || uname.Equals("Логин") || pass.Equals("Пароль"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

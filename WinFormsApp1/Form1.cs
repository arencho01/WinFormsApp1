namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            APISettingsBtn.Click += APISettingsBtn_Click;
            EditBtn.Click += EditBtn_Click;
            AddBtn.Click += AddBtn_Click;
            DeleteBtn.Click += DeleteBtn_Click;
            ClientComboBox.SelectedIndexChanged += Client—omboBox_SelectedIndexChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void APISettingsBtn_Click(Object? sender, EventArgs e)
        {
            APISettingsFrom APISettingsFrom = new APISettingsFrom();
            APISettingsFrom.Show();
        }

        private void EditBtn_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Edit");
        }

        private void AddBtn_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Add");
        }

        private void DeleteBtn_Click(object? sender, EventArgs e)
        {
            MessageBox.Show("Delete");
        }

        private void Client—omboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {

        }
    }
}

namespace WinFormsApp1
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();

            APISettings.Click += APISettings_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void APISettings_Click(Object? sender, EventArgs e)
        {
            MessageBox.Show("Hello");
        }
    }
}

namespace ChartApp
{
    public partial class ChartApp : Form
    {
        private string _filePath = "";
        private string _rawFile = "";
        private List<string> _lines =new List<string>();
        public ChartApp()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            // open a file dialog
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // set properties on file
                openFileDialog.InitialDirectory = Path.Combine(Environment.CurrentDirectory, "Data");
                openFileDialog.Filter = "csc files (*.csv)|*.csv|All files (*.*)|*.*";

                // open file dialog and so something
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // process the file information
                    _filePath = openFileDialog.FileName;

                    var fileStream = openFileDialog.OpenFile();

                    using(StreamReader reader = new StreamReader(fileStream))
                    {
                        _rawFile = reader.ReadToEnd();
                        
                        while(reader.Peek() >= 0)
                        {
                            _lines.Add(reader.ReadLine());
                        }
                    }
                    // MessageBox.Show(_filePath);
                }
               
            }

        }

    }
}

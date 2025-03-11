using CsvHelper;
using System.Drawing.Text;
using System.Globalization;

namespace ChartApp
{
    public partial class ChartApp : Form
    {
        private string _filePath = "";
        private string _rawFile = "";
        private List<WeatherEvent> _events = new List<WeatherEvent>();
        public ChartApp()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            // loads the data in the lable to display
            lblDisplay.Text = "";

            for(int i = 0; i < _events.Count;i++)
            {
                lblDisplay.Text += _events[i].ToString() + "\n";

            }

            DrawGraph();
        }

        private void DrawGraph()
        {
            // horizontal - id - 1 to N
            // vertical temp - 50 - 150
            // add dynamic ranges to the app
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            Pen blackPen = new Pen(blackBrush);
            Graphics g = this.CreateGraphics();

            int startX = 100;
            int startY = 10;
            int sizeX = 300;
            int sizeY = 300;


            Point topLeft = new Point(startX, startY);
            Point bottomRight = new Point(startX + sizeX, startY +sizeY);

            g.DrawLine(blackPen, topLeft, bottomRight);
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

                    using (StreamReader reader = new StreamReader(fileStream))
                    using (var csv = new CsvReader(
                        reader, CultureInfo.InvariantCulture))
                    {
                        _events = csv.GetRecords<WeatherEvent>().ToList();
                        LoadData();
                    }



                }



            }

        }
    }
}

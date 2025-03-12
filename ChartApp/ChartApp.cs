using CsvHelper;
using System.Drawing;
using System.Drawing.Text;
using System.Globalization;
using System.Security.Cryptography.Pkcs;

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

            for (int i = 0; i < _events.Count; i++)
            {
                lblDisplay.Text += _events[i].ToString() + "\n";

            }


        }

        private void DrawGraph()
        {
            // horizontal - id - 1 to N
            // vertical temp - 50 - 150
            // add dynamic ranges to the app
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            Pen blackPen = new Pen(blackBrush);
            Graphics g = this.CreateGraphics();

            int startX = 150;
            int startY = 10;
            int sizeX = 300;
            int sizeY = 300;


            Point topLeft = new Point(startX, startY);
            Point topRight = new Point(startX + sizeX, startY );
            Point bottomRight = new Point(startX + sizeX, startY + sizeY);
            Point bottomLeft = new Point(startX, startY + sizeY);

            // draw graph boundrys
            g.DrawLine(blackPen, topLeft, bottomLeft);
            g.DrawLine(blackPen, bottomLeft, bottomRight);

            // draw x hashes
            int xStep = 10;

            // draw y hashes
            int numHashY = 10;
            int yStep = (sizeY - startY) / numHashY;

            int tempStep =(150 - 50) / numHashY ;

           

            // draw vertical hash marks
            for(int i =0; i<10; i++)
            {
                Point pt1 = new Point(startX,startY+ (yStep *i));
                Point pt2 = new Point(startX -10, startY + (yStep * i));

                Point textPt = new Point(startX - 40, startY + (yStep * i));

                string label = (150 -(tempStep * i)) .ToString();

                g.DrawLine(blackPen, pt1, pt2);
                g.DrawString(label, new Font(FontFamily.GenericMonospace, 10.0f), blackBrush, textPt );
            }
           
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
        
     

        private void ChartApp_Paint(object sender, PaintEventArgs e)
        {
            DrawGraph();
        }
    }
}

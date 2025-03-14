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

        // graphic objects
        private SolidBrush blackBrush;
        private Pen blackPen;
        private Graphics g;

        // graph positioning values
        private int startX = 150;
        private int startY = 10;
        private int sizeX = 300;
        private int sizeY = 300;


        private Point topLeft;
        private Point topRight;
        private Point bottomRight;
        private Point bottomLeft;

        private int numHashesX = 14;
        private int xStep;

        private int numHashesY = 10;
        private int yStep;

        public ChartApp()
        {
            InitializeComponent();
            blackBrush = new SolidBrush(Color.Black);
            blackPen = new Pen(blackBrush);
            g = this.CreateGraphics();

            topLeft = new Point(startX, startY);
            topRight = new Point(startX + sizeX, startY);
            bottomRight = new Point(startX + sizeX, startY + sizeY);
            bottomLeft = new Point(startX, startY + sizeY);

            xStep= (sizeX / numHashesX);
            yStep = (sizeY - startY) / numHashesY;




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

        private void DrawData()
        {
            // for each weather event in our list of weather events
              // draw a circle at the day and temp
              foreach(WeatherEvent evt in _events)
              {
                //MessageBox.Show("X: " + evt.id + ", Y: " + evt.temp);
                    int x = evt.id;
                    int y = evt.temp;

                float posXStart = startX + (xStep * x);
                float width = 5;
                float posYStart = sizeY - ((y - 50) * 3);
                float height = 5;

                g.DrawEllipse(blackPen, posXStart , posYStart, width, height);
              }
        }

        private void DrawGraph()
        {
            // horizontal - id - 1 to N
            // vertical temp - 50 - 150
            // add dynamic ranges to the app
           

          

            // draw graph boundrys
            g.DrawLine(blackPen, topLeft, bottomLeft);
            g.DrawLine(blackPen, bottomLeft, bottomRight);

           // draw x hashes
          


            // draw verical hash marks
            for (int i = 0; i < numHashesX; i++)
            {
                Point pt1 = new Point(startX + (xStep * (i + 1)), startY + startY + 280);
                Point pt2 = new Point(startX + (xStep * (i + 1)), startY + sizeY);

                    Point textPt = new Point(startX + (xStep * (i + 1)), startY + sizeY + 20);

                   string label = (i + 1).ToString();

                g.DrawLine(blackPen, pt1, pt2);
                g.DrawString(label, new Font(FontFamily.GenericMonospace, 8.0f), blackBrush, textPt);
            }

                // draw y hashes
               

                int tempStep = (150 - 50) / numHashesY;


            
            // draw horizontal hash marks
            for(int i =0; i <numHashesY; i++)
            {
                Point pt1 = new Point(startX - 10, startY + (yStep * i));
                Point pt2 = new Point(startX, startY + (yStep * i));

                Point textPt = new Point(startX - 40, startY + (yStep * i));

                string label = (150 - (tempStep * i)).ToString();

                g.DrawLine(blackPen, pt1, pt2);
                g.DrawString(label, new Font(FontFamily.GenericMonospace, 10.0f), blackBrush, textPt);
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
            DrawData();
            
        }


        
        
    }
}

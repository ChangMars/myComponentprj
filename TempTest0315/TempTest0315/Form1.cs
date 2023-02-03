using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using PubLK =  SqlFrameworkLibrary.General;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Windows.Forms.DataVisualization.Charting;
//load ini
using System.Runtime.InteropServices;
using SqlFrameworkLibrary.Utility;

namespace TempTest0315
{
    public partial class Form1 : Form
    {
        private Class1 Cust= new Class1();
        private string sql = "";
        private DataTable Dt = new DataTable();

        //456
        delegate void SetPos(int ipos, string vinfo);
        //
        //value
        private int _value= 0;
        //max value
        private int _valueMax = 0;
        //progressBar1 title
        private string _title;
        //
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();//引用stopwatch物件

        public Form1()
        {
            InitializeComponent();
			Link();
			InitialListView();
			bindingListView();
			testLambda();


        }

        public static double[] Butterworth(double[] indata, double deltaTimeinsec, double CutOff)
        {
            if (indata == null) return null;
            if (CutOff == 0) return indata;

            double Samplingrate = 1 / deltaTimeinsec;
            long dF2 = indata.Length - 1;        // The data range is set with dF2
            double[] Dat2 = new double[dF2 + 4]; // Array with 4 extra points front and back
            double[] data = indata; // Ptr., changes passed data

            // Copy indata to Dat2
            for (long r = 0; r < dF2; r++)
            {
                Dat2[2 + r] = indata[r];
            }
            Dat2[1] = Dat2[0] = indata[0];
            Dat2[dF2 + 3] = Dat2[dF2 + 2] = indata[dF2];

            const double pi = 3.14159265358979;
            double wc = Math.Tan(CutOff * pi / Samplingrate);
            double k1 = 1.414213562 * wc; // Sqrt(2) * wc
            double k2 = wc * wc;
            double a = k2 / (1 + k1 + k2);
            double b = 2 * a;
            double c = a;
            double k3 = b / k2;
            double d = -2 * a + k3;
            double e = 1 - (2 * a) - k3;

            // RECURSIVE TRIGGERS - ENABLE filter is performed (first, last points constant)
            double[] DatYt = new double[dF2 + 4];
            DatYt[1] = DatYt[0] = indata[0];
            for (long s = 2; s < dF2 + 2; s++)
            {
                DatYt[s] = a * Dat2[s] + b * Dat2[s - 1] + c * Dat2[s - 2]
                           + d * DatYt[s - 1] + e * DatYt[s - 2];
            }
            DatYt[dF2 + 3] = DatYt[dF2 + 2] = DatYt[dF2 + 1];

            // FORWARD filter
            double[] DatZt = new double[dF2 + 2];
            DatZt[dF2] = DatYt[dF2 + 2];
            DatZt[dF2 + 1] = DatYt[dF2 + 3];
            for (long t = -dF2 + 1; t <= 0; t++)
            {
                DatZt[-t] = a * DatYt[-t + 2] + b * DatYt[-t + 3] + c * DatYt[-t + 4]
                            + d * DatZt[-t + 1] + e * DatZt[-t + 2];
            }

            // Calculated points copied for return
            for (long p = 0; p < dF2; p++)
            {
                data[p] = DatZt[p];
            }

            return data;
        }
        private void Link()
        {

            sql = "select * from TEST0315";
            //// sql="exec GetOeeDAY_RECORD_BE '新能場','2022/03/18','2022/03/18','MODULE 組裝線','A-1 設備故障','A-2 人工按停','A-3 回堵','A-4 其它','Error_1','Error_2','Error_3','Error_4','Error_5','P-1 空格損失','P-2 線速損失','Error_6','Error_7','Error_8','Error_9','Error_10','Error_11','Error_12','Q-1 外觀不良','Q-2 電性不良','Q-3 結構不良','Q-4 機能不良','Error_13','Error_14','Error_15','Error_16','Error_17'";
            //StringBuilder str = new StringBuilder();
            //str.AppendLine(@" EXEC [APDB03].[dbo].[APDB03_GetSIGNAL] '2022/02/24 10:10:00',
            //    '2022/02/24 12:00:00'");
 

            Dt = new DataTable();
            Dt= PubLK.DataTableQuery(PubLK.Getcon("connAp01_Test"), sql);
            
        }
        private void testLambda()
        {
            //lambda return
            Func<string> funTest = ()=>"return test";
            funTest();

        }
        private void BTN_SUBMIT_Click(object sender, EventArgs e)
        {


            DialogResult result;
            result = MessageBox.Show("are you sure ?" , "確認", MessageBoxButtons.YesNo);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
				//Start DB Link
				PubLK.YLLKStart(PubLK.Getcon("connAp01_Test"));
				//BeginTransaction
				PubLK.BeginTransaction();
				//Query
				PubLK.YLQuery(Cust.SaveData(CRTNO.Text, ACTNO.Text, CARDTE.Text, NAME.Text, UPOP.Text));
				if (!PubLK.YLConnectState) MessageBox.Show(PubLK.YLConnectError);
				PubLK.YLQuery(Cust.SaveData0316(CRTNO.Text, "123"));
				if (!PubLK.YLConnectState) MessageBox.Show(PubLK.YLConnectError);

				//Query State
				if (PubLK.YLConnectState)
				{
					//Successful Commit
					PubLK.Commit();
					MessageBox.Show("Done");
				}
				else
				{
					//Fail RollBack
					PubLK.RollBack();
					//Error Msg
					MessageBox.Show(PubLK.YLConnectError);
				}
				//Close DB Link
				PubLK.YLLKClose();

				listView1.Items.Clear();
                bindingListView();
            }
        }
        private void InitialListView()
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.LabelEdit = false;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("CRTNO", 100);
            listView1.Columns.Add("ACTNO", 100);
            listView1.Columns.Add("NAME", 100);
        }
        private void bindingListView()
        {
            Dt = new DataTable();
            Dt = PubLK.DataTableQuery(PubLK.Getcon("connAp01"), Cust.GetTest0315());
            foreach(DataRow row in Dt.Rows)
            {
                var item = new ListViewItem(row["CRTNO"].ToString());
                item.SubItems.Add(row["ACTNO"].ToString());
                item.SubItems.Add(row["CARDTE"].ToString());
                item.SubItems.Add(row["NAME"].ToString());
                item.SubItems.Add(row["UPOP"].ToString());
                listView1.Items.Add(item);
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection breakfast = listView1.SelectedItems;

            foreach (ListViewItem item in breakfast)
            {
                CRTNO.Text =item.SubItems[0].Text;
                ACTNO.Text = item.SubItems[1].Text;
                CARDTE.Text = item.SubItems[2].Text;
                NAME.Text = item.SubItems[3].Text;
                UPOP.Text = item.SubItems[4].Text;
            }

        }

        private void SetTextMesssage(int ipos, string vinfo)
        {


            if (this.InvokeRequired)
            {
                SetPos setpos = new SetPos(SetTextMesssage);
                Invoke(setpos, new object[] { ipos, vinfo });
            }
            else
            {
                label7.Text = ipos.ToString()+"/"+ _valueMax.ToString();
                progressBar1.Value = Convert.ToInt32(ipos);
                richTextBox1.AppendText(vinfo+"\n");
                richTextBox1.ScrollToCaret();
            }
        }
        private void SleepT()
        {



            //Parallel.For(0, _valueMax, (i, loopState) =>
            //{
            //    System.Threading.Thread.Sleep(10);

            //    if (i == _valueMax)
            //    {
            //        碼錶停止
            //        sw.Stop();
            //        印出所花費的總秒數
            //        string result1 = sw.Elapsed.TotalSeconds.ToString();
            //        分子,分母,當下狀況
            //        SetTextMesssage(i, i.ToString() + "\n------\n結束\n" + "花費" + result1 + "秒");

            //    }
            //    else
            //    {
            //        分子,分母,當下狀況
            //        SetTextMesssage(i, i.ToString());
            //    }
            //});
            for (int i = 0; i <= _valueMax; i++)
            {
                System.Threading.Thread.Sleep(10);

                if (i == _valueMax)
                {
                    //碼錶停止
                    sw.Stop();
                    //印出所花費的總秒數
                    string result1 = sw.Elapsed.TotalSeconds.ToString();
                    //分子,分母,當下狀況
                    SetTextMesssage(i, i.ToString() + "\n------\n結束\n" + "花費" + result1 + "秒");

                }
                else
                {
                    //分子,分母,當下狀況
                    SetTextMesssage(i, i.ToString());
                }



                }
            }

        public void Progress_Set(int value, int valueMax, string title)
        {


            //value
            _value = value;
            //max value
            _valueMax = valueMax;
            //progressBar1 title
            _title = title;

            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = _valueMax;
            this.label10.Text = _title;


            this.progressBar1.Value = Convert.ToInt32(_value);

            Thread fThread = new Thread(new ThreadStart(SleepT));
            fThread.Start();
        }


        private void BTN_DELETE_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            Progress_Set(0, 100, "test");
            sw.Reset();//碼表歸零

            sw.Start();//碼表開始計時

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox3.Text = "StringBuilder " + textBox4.Text + " = new StringBuilder();";
       
            foreach (var str in textBox2.Text.Replace("\r\n","^").Split('^'))
            {
                if (str !="")
                {
                    richTextBox3.Text += " \r\n ";
                    richTextBox3.Text += textBox4.Text + ".AppendLine(\"" + " " + str + "\");";
                }
            }

            richTextBox3.Text += " \r\n String " + textBox3.Text+" = "+ textBox4.Text + ".ToString();";
      







        }





        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox3.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string date = textBox5.Text;
            //試跑油漆廠 的 監測點位
            StringBuilder Str = new StringBuilder();
            Str.AppendLine(" SELECT  *");
            Str.AppendLine(" FROM [APDB01].[dbo].[YULON_SI_NYKB]");
            Str.AppendLine(" where ");
            Str.AppendLine(" sj between '08:00:00' and '16:45:00' and ");
            //Str.AppendLine(" rqsj between '" + date + " 08:00:00.000' and '" + date + " 16:45:00.000'  and ");
            Str.AppendLine("  rq between '"+ date + "' and '"+date+"'");
            String Sql = Str.ToString();


            Dt = new DataTable();
            Dt = PubLK.DataTableQuery(PubLK.Getcon("connAp01"), Sql);
            //要查看的欄位

            string col_ED研輸送帶_RUN = "ED研輸送帶_RUN";
            string col_ED研輸送帶_ERROR = "ED研輸送帶_ERROR";
            string col_U33輸送帶輸送帶_RUN = "U3.3輸送帶輸送帶_RUN";
            string col_U33輸送帶輸送帶_ERROR = "U3.3輸送帶輸送帶_ERROR";
            string col_U34滾筒及昇降_RUN = "U3.4滾筒及昇降_RUN";
            string col_U34滾筒及昇降_ERROR = "U3.4滾筒及昇降_ERROR";
            string col_噴房輸送帶_RUN = "噴房輸送帶_RUN";
            string col_噴房輸送帶_Error = "噴房輸送帶_Error";
            string col_送風機and水幕_RUN = "送風機and水幕_RUN";
            string col_送風機and水幕_ERROR = "送風機and水幕_ERROR";
            string col_排風機and烤爐_RUN = "排風機and烤爐_RUN";
            string col_排風機and烤爐_ERROR = "排風機and烤爐_ERROR";
            string col_噴房人工按停 = "噴房人工按停";
            string col_打膠烤爐人工按停 = "打膠烤爐人工按停";
            string col_ED研人工按停 = "ED研人工按停";
            string col_中研人工按停 = "中研人工按停";
            string col_中塗停止進車 = "中塗停止進車";
            string col_中研輸送帶_RUN = "中研輸送帶_RUN";
            string col_中研輸送帶_ERROR = "中研輸送帶_ERROR";
            string col_sj = "sj";
            string col_rq = "rq";



            //取一欄 填column
            string 監測點位 = Dt.Rows[0]["監測點位"].ToString();
            監測點位 = 監測點位.Replace("True", "1");
            監測點位 = 監測點位.Replace("False", "0");
            //轉型
            var 監測點位Obj = JObject.Parse(監測點位);
            //構成datatable
            DataTable 監測點位DT = new DataTable();
            //形成Colnum
            foreach (var item in 監測點位Obj)
            {
                string Colnum = item.Key;
                監測點位DT.Columns.Add(Colnum);

            }
            //額外補時間欄位
            監測點位DT.Columns.Add(col_rq);
            監測點位DT.Columns.Add(col_sj);
            //----

            //填值
            foreach (DataRow SI_row in Dt.Rows)
            {
                string _監測點位 = "";
                _監測點位 = SI_row["監測點位"].ToString();
                _監測點位 = _監測點位.Replace("True", "1");
                _監測點位 = _監測點位.Replace("False", "0");
                //轉型
                var _監測點位Obj = JObject.Parse(_監測點位);

                //new datarow
                DataRow 監測點位Row = 監測點位DT.NewRow();
                foreach (var item in _監測點位Obj)
                {
                    if (item.Key == col_噴房人工按停) 監測點位Row[col_噴房人工按停] = item.Value.ToString();
                    if (item.Key == col_打膠烤爐人工按停) 監測點位Row[col_打膠烤爐人工按停] = item.Value.ToString();
                    if (item.Key == col_ED研人工按停) 監測點位Row[col_ED研人工按停] = item.Value.ToString();
                    if (item.Key == col_中研人工按停) 監測點位Row[col_中研人工按停] = item.Value.ToString();
                    if (item.Key == col_中塗停止進車) 監測點位Row[col_中塗停止進車] = item.Value.ToString();
                    if (item.Key == col_噴房輸送帶_RUN) 監測點位Row[col_噴房輸送帶_RUN] = item.Value.ToString();
                    if (item.Key == col_ED研輸送帶_RUN) 監測點位Row[col_ED研輸送帶_RUN] = item.Value.ToString();
                    if (item.Key == col_ED研輸送帶_ERROR) 監測點位Row[col_ED研輸送帶_ERROR] = item.Value.ToString();
                    if (item.Key == col_U33輸送帶輸送帶_RUN) 監測點位Row[col_U33輸送帶輸送帶_RUN] = item.Value.ToString();
                    if (item.Key == col_U33輸送帶輸送帶_ERROR) 監測點位Row[col_U33輸送帶輸送帶_ERROR] = item.Value.ToString();
                    if (item.Key == col_U34滾筒及昇降_RUN) 監測點位Row[col_U34滾筒及昇降_RUN] = item.Value.ToString();
                    if (item.Key == col_U34滾筒及昇降_ERROR) 監測點位Row[col_U34滾筒及昇降_ERROR] = item.Value.ToString();
                    if (item.Key == col_噴房輸送帶_RUN) 監測點位Row[col_噴房輸送帶_RUN] = item.Value.ToString();
                    if (item.Key == col_噴房輸送帶_Error) 監測點位Row[col_噴房輸送帶_Error] = item.Value.ToString();
                    if (item.Key == col_送風機and水幕_RUN) 監測點位Row[col_送風機and水幕_RUN] = item.Value.ToString();
                    if (item.Key == col_中研輸送帶_RUN) 監測點位Row[col_中研輸送帶_RUN] = item.Value.ToString();
                    if (item.Key == col_中研輸送帶_ERROR) 監測點位Row[col_中研輸送帶_ERROR] = item.Value.ToString();
                    if (item.Key == col_送風機and水幕_ERROR) 監測點位Row[col_送風機and水幕_ERROR] = item.Value.ToString();
                    if (item.Key == col_排風機and烤爐_RUN) 監測點位Row[col_排風機and烤爐_RUN] = item.Value.ToString();
                    if (item.Key == col_排風機and烤爐_ERROR) 監測點位Row[col_排風機and烤爐_ERROR] = item.Value.ToString();

                }
                //RQ value
                監測點位Row[col_sj] = SI_row[col_sj];
                監測點位Row[col_rq] = SI_row[col_rq];
                監測點位DT.Rows.Add(監測點位Row);
            }


            //first 中途停車 true 
            var query_Rule1 = from rows in 監測點位DT.AsEnumerable()
                              where rows[col_中塗停止進車].ToString() == "1"
                              select rows;

            //second 中途停車 true && col_噴房輸送帶_RUN false && col_噴房人工按停 true
            //var query_Rule2 = from rows in 監測點位DT.AsEnumerable()
            //                  where rows[col_中塗停止進車].ToString() == "1" &&
            //                  rows[col_噴房輸送帶_RUN].ToString() == "0" &&
            //                   rows[col_噴房人工按停].ToString() == "1"
            //                  select rows;
            //third 
            var query_Rule3 = from rows in 監測點位DT.AsEnumerable()
                              where rows[col_噴房輸送帶_RUN].ToString() == "0" &&
                                rows[col_中塗停止進車].ToString() == "0" &&
                                rows[col_噴房人工按停].ToString() == "0" &&
                              (
                                  rows[col_ED研輸送帶_ERROR].ToString() == "1" ||
                                  rows[col_U33輸送帶輸送帶_ERROR].ToString() == "1" ||
                                  rows[col_U34滾筒及昇降_ERROR].ToString() == "1" ||
                                  rows[col_噴房輸送帶_Error].ToString() == "1" ||
                                  rows[col_送風機and水幕_ERROR].ToString() == "1" ||
                                  rows[col_排風機and烤爐_ERROR].ToString() == "1" ||
                                  rows[col_中研輸送帶_ERROR].ToString() == "1"
                                )
                              select rows;
            //fourth
            var query_Rule4 = from rows in 監測點位DT.AsEnumerable()
                              where rows[col_噴房輸送帶_RUN].ToString() == "0" &&
                                  rows[col_ED研輸送帶_ERROR].ToString() == "0" &&
                                  rows[col_U33輸送帶輸送帶_ERROR].ToString() == "0" &&
                                  rows[col_U34滾筒及昇降_ERROR].ToString() == "0" &&
                                  rows[col_噴房輸送帶_Error].ToString() == "0" &&
                                  rows[col_送風機and水幕_ERROR].ToString() == "0" &&
                                  rows[col_排風機and烤爐_ERROR].ToString() == "0" &&
                                  rows[col_中研輸送帶_ERROR].ToString() == "0" &&
                                    rows[col_中塗停止進車].ToString() == "0" &&
                                rows[col_噴房人工按停].ToString() == "0" &&
                              (
                                        rows[col_噴房人工按停].ToString() == "1" ||
                                        rows[col_打膠烤爐人工按停].ToString() == "1" ||
                                        rows[col_ED研人工按停].ToString() == "1" ||
                                        rows[col_中研人工按停].ToString() == "1"
                              ) 
                              select rows;

            //fifth
            var query_Rule5 = from rows in 監測點位DT.AsEnumerable()
                              where rows[col_噴房輸送帶_RUN].ToString() == "0" &&
                                  rows[col_ED研輸送帶_ERROR].ToString() == "0" &&
                                  rows[col_U33輸送帶輸送帶_ERROR].ToString() == "0" &&
                                  rows[col_U34滾筒及昇降_ERROR].ToString() == "0" &&
                                  rows[col_噴房輸送帶_Error].ToString() == "0" &&
                                  rows[col_送風機and水幕_ERROR].ToString() == "0" &&
                                  rows[col_排風機and烤爐_ERROR].ToString() == "0" &&
                                  rows[col_中研輸送帶_ERROR].ToString() == "0" &&
                                  rows[col_噴房人工按停].ToString() == "0" &&
                                  rows[col_打膠烤爐人工按停].ToString() == "0" &&
                                  rows[col_ED研人工按停].ToString() == "0" &&
                                  rows[col_中研人工按停].ToString() == "0" &&
                                rows[col_中塗停止進車].ToString() == "0" &&
                                rows[col_噴房人工按停].ToString() == "0"
                              select rows;


            int 累積工單 = 0;

            //抓條件
            //先抓中塗=true之案件
            //if (query_Rule1.Count() > 0)
            //{
            //    foreach (var EachData in query_Rule1)
            //    {
            //        Console.WriteLine("觸發條件1:"+EachData["rq"].ToString()+ EachData["sj"].ToString());
            //        累積工單++;
            //    }
            //}
            if (query_Rule3.Count() > 0 )
            {
                foreach (var EachData in query_Rule3)
                {
                    Console.WriteLine("觸發條件3:" + EachData["rq"].ToString() +" "+ EachData["sj"].ToString());
                    累積工單++;
                }
            }
            if (query_Rule4.Count() > 0  && query_Rule3.Count() == 0)
            {
                foreach (var EachData in query_Rule4)
                {
                    Console.WriteLine("觸發條件4:" + EachData["rq"].ToString() + " " + EachData["sj"].ToString());
                    累積工單++;
                }
            }

            if (query_Rule5.Count() > 0 && query_Rule3.Count() == 0 && query_Rule4.Count() == 0)
            {
                foreach (var EachData in query_Rule5)
                {
                    Console.WriteLine("觸發條件5:" + EachData["rq"].ToString() + " " + EachData["sj"].ToString());
                    累積工單++;
                }
            }
            Console.WriteLine("累積工單:" + 累積工單.ToString());

        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>  
        /// Adds a week of data with values between 20 and 35.  
        /// </summary>  
        private void AddChartData()
        {
            // Declare new random variable  
            Random rand = new Random();

            // Add a week of data  
            for (int i = 0; i < 7; i++)
            {
                chart1.Series[0].Points.AddXY(DateTime.Now.AddDays(i), rand.Next(20, 35));
            }
        }

        /// <summary>  
        /// Adds repeating horizontal strip lines at intervals of 5.  
        /// </summary>  
        private void AddHorizRepeatingStripLines()
        {
            // Instantiate new strip line  
            StripLine stripLine1 = new StripLine();
            stripLine1.StripWidth = 0;
            stripLine1.BorderColor = Color.Black;
            stripLine1.BorderWidth = 3;
            stripLine1.Interval = 5;

            // Consider adding transparency so that the strip lines are lighter  
            stripLine1.BackColor = Color.FromArgb(120, Color.Red);

            stripLine1.BackSecondaryColor = Color.Black;
            stripLine1.BackGradientStyle = GradientStyle.LeftRight;

            // Add the strip line to the chart  
            chart1.ChartAreas[0].AxisY.StripLines.Add(stripLine1);
        }

        /// <summary>  
        /// Adds strip lines to highlight weekend values.  
        /// </summary>  
        private void HighlightWeekendsWithStripLines()
        {
            // Set strip line to highlight weekends  
            StripLine stripLine2 = new StripLine();
            stripLine2.BackColor = Color.FromArgb(120, Color.Gold);
            stripLine2.IntervalOffset = -1.5;
            stripLine2.IntervalOffsetType = DateTimeIntervalType.Days;
            stripLine2.Interval = 1;
            stripLine2.IntervalType = DateTimeIntervalType.Weeks;
            stripLine2.StripWidth = 2;
            stripLine2.StripWidthType = DateTimeIntervalType.Days;

            // Add strip line to the chart  
            chart1.ChartAreas[0].AxisX.StripLines.Add(stripLine2);

            // Set the axis label to show the name of the day  
            // This is done in order to demonstrate that weekends are highlighted  
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "ddd";
        }

        /// <summary>  
        /// Adds a horizontal threshold strip line at the calculated mean   
        /// value of all data points in the first series of the chart.  
        /// </summary>  
        private void AddThresholdStripLine()
        {
            StripLine stripLine3 = new StripLine();

            // Set threshold line so that it is only shown once  
            stripLine3.Interval = 0;

            // Set the threshold line to be drawn at the calculated mean of the first series  
            stripLine3.IntervalOffset = chart1.DataManipulator.Statistics.Mean(chart1.Series[0].Name);

            stripLine3.BackColor = Color.DarkGreen;
            stripLine3.StripWidth = 0.25;

            // Set text properties for the threshold line  
            stripLine3.Text = "Mean";
            stripLine3.ForeColor = Color.Black;

            // Add strip line to the chart  
            chart1.ChartAreas[0].AxisY.StripLines.Add(stripLine3);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ip = "";
            string db = "";
            string account = "";
            string password = "";
            string title = "";
            IniManager iniManager = new IniManager("D:/YLCC.ini");




            if (comboBox1.Text.Split('-')[1] == "01")
            {
                iniManager.WriteIniFile("LINK_TYPE", "TYPE", "正式");

                ip = iniManager.ReadIniFile("APDB01", "IP");
                db = iniManager.ReadIniFile("APDB01", "DB");
                account = iniManager.ReadIniFile("APDB01", "ACCOUNT");
                password = iniManager.ReadIniFile("APDB01", "PASSWORD");

                title = iniManager.ReadIniFile("LINK_TYPE", "TYPE");
            }
            else if (comboBox1.Text.Split('-')[1] == "02")
            {
                iniManager.WriteIniFile("LINK_TYPE", "TYPE", "測試");

                ip = iniManager.ReadIniFile("APDB01_TEST", "IP");
                db = iniManager.ReadIniFile("APDB01_TEST", "DB");
                account = iniManager.ReadIniFile("APDB01_TEST", "ACCOUNT");
                password = iniManager.ReadIniFile("APDB01_TEST", "PASSWORD");

                title = iniManager.ReadIniFile("LINK_TYPE", "TYPE");
            }


            label9.Text = ip;
            label11.Text = db;
            label12.Text = account;
            label13.Text = password;
            label14.Text = title;




        }
    }

    public class IniManager
    {
        private string filePath;
        private StringBuilder lpReturnedString;
        private int bufferSize;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string lpString, string lpFileName);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        public IniManager(string iniPath)
        {
            filePath = iniPath;
            bufferSize = 512;
            lpReturnedString = new StringBuilder(bufferSize);
        }

        // read ini date depend on section and key
        public string ReadIniFile(string section, string key, string defaultValue = "尋找不到該值")
        {
            lpReturnedString.Clear();
            GetPrivateProfileString(section, key, defaultValue, lpReturnedString, bufferSize, filePath);
            return lpReturnedString.ToString();
        }

        // write ini data depend on section and key
        public void WriteIniFile(string section, string key, Object value)
        {
            WritePrivateProfileString(section, key, value.ToString(), filePath);
        }
    }


    //設計許多的介面
    //然後每個介面有它專精的抽象方法
    public interface IProgrammer
    {
        void coding();
    }

    public interface IPianoPlayer
    {
        void playpiano();
    }

    public class GameProgrammer : IProgrammer, IPianoPlayer
    {
        public void coding()
        {
            Console.WriteLine("I could used Unity to develop game");
        }
        public void playpiano()
        {
            Console.WriteLine("I could played piano");
        }
    }

    //public class Programmer
    //{
    //    private IProgrammer iprogrammer;

    //    public Programmer(IProgrammer programmer)
    //    {
    //        iprogrammer = programmer;
    //    }

    //    public void coding()
    //    {
    //        iprogrammer.coding();
    //    }

    //    public void debug()
    //    {
    //        iprogrammer.debug();
    //    }
    //}

    //class MainProgram
    //{
    //    static void Main()
    //    {
    //        IProgrammer i_gameProgrammer = new GameProgrammer();

    //        Programmer gameprogrammer = new Programmer(i_gameProgrammer);

    //        gameprogrammer.coding();

    //        gameprogrammer.debug();
    //    }
    //}
   
}

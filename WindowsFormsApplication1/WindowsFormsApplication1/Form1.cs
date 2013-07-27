using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        List<bro> warbrostopurge = new List<bro>();
        int purgedate;
        int curbrocount;
        public Form1()
        {
            InitializeComponent();
            purgedate = 1;
        }

        public void Init()
        {
            warbrostopurge.Clear();
            string[] lines = System.IO.File.ReadAllLines(@"Warbros.csv");
            curbrocount = lines.Count();
            String[] chrs;
            String[] chrs2;
            //char[] dchrs = new char[3];
            char[] mchrs = new char[3];
            //char[] nchrs = new char[2];
            for (int i = 0; i < curbrocount; i++)
            {
                bro curbro = new bro();
                int a = lines[i].Count();


                String[] line = lines[i].Split(',');
                String date = line[0];
                curbro.name = line[1];

                string m = date.Substring(4, 3);
                //string d = new string(nchrs);

                switch (m)
                {
                    case ("Jan"):
                        curbro.months = 1;
                        break;
                    case ("Feb"):
                        curbro.months = 2;
                        break;
                    case ("Mar"):
                        curbro.months = 3;
                        break;
                    case ("Apr"):
                        curbro.months = 4;
                        break;
                    case ("May"):
                        curbro.months = 5;
                        break;
                    case ("Jun"):
                        curbro.months = 6;
                        break;
                    case ("Jul"):
                        curbro.months = 7;
                        break;
                    case ("Aug"):
                        curbro.months = 8;
                        break;
                    case ("Sep"):
                        curbro.months = 9;
                        break;
                    case ("Oct"):
                        curbro.months = 10;
                        break;
                    case ("Nov"):
                        curbro.months = 11;
                        break;
                    case ("Dec"):
                        curbro.months = 12;
                        break;
                    default:
                        break;
                }

                if ((DateTime.Today.Month - curbro.months) >= purgedate)
                {
                    list.Items.Add((DateTime.Today.Month - curbro.months).ToString()).SubItems.Add(curbro.name);
                    warbrostopurge.Add(curbro);
                }

                label1.Text = "Processed " + curbrocount.ToString() + " Members And Found " + list.Items.Count.ToString() + " Of Them Suitable For Purge.";
            }
            button2.Enabled = true;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            list.Items.Clear();
            list.Columns.Add("Months AFK");
            list.Columns.Add("Name");
            Init();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> print = new List<string>();
            print.Add("Purge List With Members Afk For " + purgedate.ToString() + " Or More Months.");
            print.Add(warbrostopurge.Count.ToString() + " Members Listed Out Total " + curbrocount.ToString() + " Are Found Wanting.\r\n");
            print.Add("Months AFK - Name\r\n");


            warbrostopurge.Sort((x, y) => x.months.CompareTo(y.months));

            for (int i = 0; i < warbrostopurge.Count(); i++)
			{
                print.Add((DateTime.Today.Month - warbrostopurge[i].months)+ " - " + warbrostopurge[i].name);
			}
            print.Add("\r\n\r\nGenerated Using Purge Watch By KONAir");
            print.Add("\r\n\r\nEnd Of File - Purge The AFK - ");
            System.IO.File.WriteAllLines(@"WarbrosPurgeList.txt", print);

            System.Diagnostics.Process.Start(@"WarbrosPurgeList.txt");
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            purgedate = Convert.ToInt32(numericUpDown1.Value);
        }


    }
}

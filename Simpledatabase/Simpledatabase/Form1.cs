using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Simpledatabase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public delegate void Chancedecider(object sender, EventArgs e);
        public event Chancedecider Chance;
        int[] n = new int[11];
        
        Random r = new Random();
        int player1 = 0, player2 = 0, player3 = 0, player4 = 0;
        int flag1 = 1, flag2 = 1, flag3 = 1, flag4 = 1; int id = 1;
        int count = 1; long pot_money = 0; long player1mon = 5000, player2mon = 5000, player3mon = 5000, player4mon = 5000;
        int no_of_cards = 0; int i, g;
        bool click = false; int h;
        int master = 0; int lastcard = 0;int j = 0;

        SqlConnection cn = new SqlConnection();
        SqlCommand cmd;
        SqlDataReader dr;
        MemoryStream ms;
        private void Form1_Load(object sender, EventArgs e)
        {
            cn.ConnectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Akshay\Documents\mycards.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            bet.Visible = false;
            textBox1.Visible = false;
            progressBar1.Maximum = 10;
            progressBar2.Maximum = 10;
            progressBar3.Maximum = 10;
            progressBar4.Maximum = 10;
            progressBar2.Visible = false;
            progressBar3.Visible = false;
            progressBar4.Visible = false;
            label1.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label13.Text = pot_money.ToString();
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 11; i++)
            {
                if (n[i] == 19||n[i]==27)
                {
                    i = i - 1;
                    continue;
                }
                else
                {
                    n[i] = r.Next(53) + 1;
                }
            }
                  
                 cn.Open();
               

                 for (i = 0; i < 3; i++)
                 {
                     cmd = new SqlCommand("select * from tblImage where imgId = '" + n[i] + "'", cn);
                     dr = cmd.ExecuteReader();
                     if (dr.Read())
                     {
                         byte[] picarr = (byte[])dr["imgImage"];
                         ms = new MemoryStream(picarr);
                         ms.Seek(0, SeekOrigin.Begin);
                     }
                     if (i == 0)
                         pictureBox1.Image = Image.FromStream(ms);

                     else if (i == 1)
                         pictureBox2.Image = Image.FromStream(ms);

                     else if (i == 2)
                         pictureBox3.Image = Image.FromStream(ms);

                     cmd.Dispose();
                     dr.Close();
                 }
                         
                         
                         timer1.Start();
                         label1.Visible = true;
                         label3.Visible = true;
                         label6.Text = "Player1!! Your turn";
                         no_of_cards = 3;
                 }
        

       
        private void timer1_Tick(object sender, EventArgs e)
        {
            id = 1;

            label13.Text = pot_money.ToString();
            if (flag1 == 1)
            {
                if (flag2 == -1 && flag3 == -1 && flag4 == -1)
                {
                    label11.Visible = true;
                    label11.Text = "Player 1 is the winner with " + (player1mon+pot_money) + "chips!!!";
                    timer1.Stop();
                }
                count++;
                //p1.Text = card1;
                label3.Text = player1mon.ToString();
                progressBar1.Value += 1;
                cmd = new SqlCommand("select * from tblImage where imgId = '" + n[3] + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["imgImage"];
                    ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                }    
                pictureBox6.Image = Image.FromStream(ms);
                cmd.Dispose();
                dr.Close();
                

                cmd = new SqlCommand("select * from tblImage where imgId = '" + n[4] + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["imgImage"];
                    ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                }
                pictureBox7.Image = Image.FromStream(ms);
                cmd.Dispose();
                dr.Close();
                

                if (player4 == 1)    //the preceding player checks
                {
                    //3 options to display
                    Call.Visible = false;
                    Check.Visible = true;
                }

                else if (player4 == 2) //the preceding player calls
                {
                    if (id == master)   // if the control comes back to the person who raised the money.
                    {
                        Check.Visible = true;
                        Call.Visible = false;
                    }
                    else
                    {
                        Check.Visible = false;  //the next player cant check
                        Call.Visible = true;
                    }
                }

                else if (player4 == 3)  //the player raises the pot money
                {
                   
                        Check.Visible = false;  //the next player cant check
                        Call.Visible = true;
                }


                //checking the time count so that control can be passed to the next player
                if (click == true)
                {
                    click = false;
                    timer1.Stop();
                    label1.Visible = false;
                    label3.Visible = false;
                    progressBar1.Visible = false;
                    
                    //p1.Text = "";
                    cmd = new SqlCommand("select * from tblImage where imgId = '" + 56 + "'", cn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        byte[] picarr = (byte[])dr["imgImage"];
                        ms = new MemoryStream(picarr);
                        ms.Seek(0, SeekOrigin.Begin);
                    }
                    pictureBox6.Image = Image.FromStream(ms);
                    cmd.Dispose();
                    dr.Close();


                    cmd = new SqlCommand("select * from tblImage where imgId = '" + 56 + "'", cn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        byte[] picarr = (byte[])dr["imgImage"];
                        ms = new MemoryStream(picarr);
                        ms.Seek(0, SeekOrigin.Begin);
                    }
                    pictureBox7.Image = Image.FromStream(ms);
                    cmd.Dispose();
                    dr.Close();


                    label6.Text = "";
                    count = 1;
                    progressBar2.Visible = true;
                    progressBar2.Value = 0;
                    timer2.Start();
                    label4.Visible = true;
                    label5.Visible = true;
                    label6.Text = "Player 2!! Your turn";
                    this.Chance += new Chancedecider(timer2_Tick);
                }
            }

            else
            {
                click = false;
                timer1.Stop();
                progressBar1.Visible = false;
                label1.Visible = false;
                label3.Visible = false;
                //p1.Text = "";
                cmd = new SqlCommand("select * from tblImage where imgId = '" + 56 + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["imgImage"];
                    ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                }
                pictureBox6.Image = Image.FromStream(ms);
                cmd.Dispose();
                dr.Close();


                cmd = new SqlCommand("select * from tblImage where imgId = '" + 56 + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["imgImage"];
                    ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                }
                pictureBox7.Image = Image.FromStream(ms);
                cmd.Dispose();
                dr.Close();

                
                label6.Text = "";
                count = 1;
                progressBar2.Visible = true;
                progressBar2.Value = 0;
                label4.Visible = true;
                label5.Visible = true;
                timer2.Start();
                label6.Text = "Player 2!! Your turn";
                this.Chance += new Chancedecider(timer2_Tick);
            }

            if (count == 10)
            {
                timer1.Stop();
                progressBar1.Visible = false;
                label1.Visible = false;
                label3.Visible = false;
                count = 1;
                flag1 = -1;
                //card1 = "";
                //p1.Visible = false;
                pictureBox6.Visible = false;
                pictureBox7.Visible = false;
                player1 = 1;
                lastcard++;
                progressBar2.Visible = true;
                progressBar2.Value = 0;
                label4.Visible = false;
                label5.Visible = false;
                timer2.Start();
                label6.Text = "Player 2!! Your turn";
                this.Chance += new Chancedecider(timer2_Tick);
            }
            timer1.Interval = 1000;
        }






        private void timer2_Tick(object sender, EventArgs e)
        {
            id = 2;
            label13.Text = pot_money.ToString();
            if (flag2 == 1)
            {
                if (flag1 == -1 && flag3 == -1 && flag4 == -1)
                {
                    label11.Visible = true;
                    label11.Text = "Player 2 is the winner with " + (player2mon+pot_money) + "chips!!!";
                    timer2.Stop();
                }
                count++;
                //p2.Text = card2;
                label5.Text = player2mon.ToString();
                progressBar2.Value += 1;
                cmd = new SqlCommand("select * from tblImage where imgId = '" + n[5] + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["imgImage"];
                    ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                }
                pictureBox8.Image = Image.FromStream(ms);
                cmd.Dispose();
                dr.Close();

                cmd = new SqlCommand("select * from tblImage where imgId = '" + n[6] + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["imgImage"];
                    ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                }
                pictureBox9.Image = Image.FromStream(ms);
                cmd.Dispose();
                dr.Close();

                if (player1 == 1)
                {
                    Call.Visible = false;
                    Check.Visible = true;
                }

                else if (player1 == 2)
                {
                    if (id == master)
                    {
                        Check.Visible = true;
                        Call.Visible = false;
                    }
                    else
                    {
                        Check.Visible = false;
                        Call.Visible = true;
                    }
                }

                else if (player1 == 3)
                {
                    Check.Visible = false;
                    Call.Visible = true;
                }
                if (click == true)
                {
                    click = false;
                    timer2.Stop();
                    progressBar2.Visible = false;
                    label4.Visible = false;
                    label5.Visible = false;
                    //p2.Text = "";
                    cmd = new SqlCommand("select * from tblImage where imgId = '" + 56 + "'", cn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        byte[] picarr = (byte[])dr["imgImage"];
                        ms = new MemoryStream(picarr);
                        ms.Seek(0, SeekOrigin.Begin);
                    }
                    pictureBox8.Image = Image.FromStream(ms);
                    cmd.Dispose();
                    dr.Close();

                    cmd = new SqlCommand("select * from tblImage where imgId = '" + 56 + "'", cn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        byte[] picarr = (byte[])dr["imgImage"];
                        ms = new MemoryStream(picarr);
                        ms.Seek(0, SeekOrigin.Begin);
                    }
                    pictureBox9.Image = Image.FromStream(ms);
                    cmd.Dispose();
                    dr.Close();
                    

                    label6.Text = "";
                    count = 1;
                    progressBar3.Visible = true;
                    progressBar3.Value = 0;
                    timer3.Start();
                    label7.Visible = true;
                    label8.Visible = true;
                    label6.Text = "Player 3!! Your turn";
                    this.Chance += new Chancedecider(timer3_Tick);
                }
            }
            
            else
            {
                click = false;
                timer2.Stop();
                progressBar2.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                //p2.Text = "";
                cmd = new SqlCommand("select * from tblImage where imgId = '" + 56 + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["imgImage"];
                    ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                }
                pictureBox8.Image = Image.FromStream(ms);
                cmd.Dispose();
                dr.Close();


                cmd = new SqlCommand("select * from tblImage where imgId = '" + 56 + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["imgImage"];
                    ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                }
                pictureBox9.Image = Image.FromStream(ms);
                cmd.Dispose();
                dr.Close();


                label6.Text = "";
                count = 1;
                progressBar3.Visible = true;
                progressBar3.Value = 0;
                timer3.Start();
                label7.Visible = true;
                label8.Visible = true;
                label6.Text = "Player 3!! Your turn";
                this.Chance += new Chancedecider(timer3_Tick);
            }

            if (count == 10)
            {
                timer2.Stop();
                progressBar2.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                count = 1;
                flag2 = -1;
                //card2 = "";
                //p2.Visible = false;
                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
                player2 = 1;
                lastcard++;
                progressBar3.Visible = true;
                progressBar3.Value = 0;
                timer3.Start();
                label7.Visible = true;
                label8.Visible = true;
                label6.Text = "Player 3!! Your turn";
                this.Chance += new Chancedecider(timer3_Tick);
            }
            timer2.Interval = 1000;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            id = 3;
            label13.Text = pot_money.ToString();  
            if (flag3 == 1)
            {
                if (flag2 == -1 && flag1 == -1 && flag4 == -1)
                {
                    label11.Visible = true;
                    label11.Text = "Player 3 is the Winner with" + (player3mon+pot_money) + "chips!!!";
                    timer3.Stop();
                }
                count++;
                //p3.Text = card3;
                label8.Text = player3mon.ToString();
                progressBar3.Value += 1;
                cmd = new SqlCommand("select * from tblImage where imgId = '" + n[7] + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["imgImage"];
                    ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                }
                pictureBox10.Image = Image.FromStream(ms);
                cmd.Dispose();
                dr.Close();

                cmd = new SqlCommand("select * from tblImage where imgId = '" + n[8] + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["imgImage"];
                    ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                }
                pictureBox11.Image = Image.FromStream(ms);
                cmd.Dispose();
                dr.Close();


                if (player2 == 1)
                {
                    Call.Visible = false;
                    Check.Visible = true;
                }

                else if (player2 == 2)
                {
                    if (id == master)
                    {
                        Check.Visible = true;
                        Call.Visible = false;
                    }
                    else
                    {
                        Check.Visible = false;
                        Call.Visible = true;
                    }
                }

                else if (player2 == 3)
                {
                    Check.Visible = false;
                    Call.Visible = true;
                }
                if (click == true)
                {
                    click = false;
                    timer3.Stop();
                    label7.Visible = false;
                    label8.Visible = false;
                    progressBar3.Visible = false;
                    //p3.Text = "";
                    cmd = new SqlCommand("select * from tblImage where imgId = '" + 56 + "'", cn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        byte[] picarr = (byte[])dr["imgImage"];
                        ms = new MemoryStream(picarr);
                        ms.Seek(0, SeekOrigin.Begin);
                    }
                    pictureBox10.Image = Image.FromStream(ms);
                    cmd.Dispose();
                    dr.Close();

                    cmd = new SqlCommand("select * from tblImage where imgId = '" + 56 + "'", cn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        byte[] picarr = (byte[])dr["imgImage"];
                        ms = new MemoryStream(picarr);
                        ms.Seek(0, SeekOrigin.Begin);
                    }
                    pictureBox11.Image = Image.FromStream(ms);
                    cmd.Dispose();
                    dr.Close();


                    label6.Text = "";
                    count = 1;
                    progressBar4.Visible = true;
                    progressBar4.Value = 0;
                    timer4.Start();
                    label9.Visible = true;
                    label10.Visible = true;
                    label6.Text = "Player 4!! Your turn";
                    this.Chance += new Chancedecider(timer4_Tick);
                }
            }
            

            else
            {
                click = false;
                timer3.Stop();
                label7.Visible = false;
                label8.Visible = false;
                progressBar3.Visible = false;
                //p3.Text = "";
                cmd = new SqlCommand("select * from tblImage where imgId = '" + 56 + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["imgImage"];
                    ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                }
                pictureBox10.Image = Image.FromStream(ms);
                cmd.Dispose();
                dr.Close();

                cmd = new SqlCommand("select * from tblImage where imgId = '" + 56 + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["imgImage"];
                    ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                }
                pictureBox11.Image = Image.FromStream(ms);
                cmd.Dispose();
                dr.Close();


                label6.Text = "";
                count = 1;
                progressBar4.Visible = true;
                progressBar4.Value = 0;
                timer4.Start();
                label9.Visible = true;
                label10.Visible = true;
                label6.Text = "Player 4!! Your turn";
                this.Chance += new Chancedecider(timer4_Tick);
            }

            if (count == 10)
            {
                timer3.Stop();
                progressBar3.Visible = false;
                label7.Visible = false;
                label8.Visible = false;
                count = 1;
                flag3 = -1;
                //card3 = "";
                //p3.Visible = false;
                pictureBox10.Visible = false;
                pictureBox11.Visible = false;
                player3 = 1;
                lastcard++;
                progressBar4.Visible = true;
                progressBar4.Value = 0;
                timer4.Start();
                label9.Visible = true;
                label10.Visible = true;
                label6.Text = "Player 4!! Your turn";
                this.Chance +=new Chancedecider(timer4_Tick);
            }
            timer3.Interval = 1000;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            id = 4;
            label13.Text = pot_money.ToString();
            if (flag4 == 1)
            {
                if (flag2 == -1 && flag3 == -1 && flag1 == -1)
                {
                    label11.Visible = true;
                    label11.Text = "Player4 is the Winner with '" + (player4mon+pot_money) + "'chips!!!";
                    timer4.Stop();
                }
                count++;
                //p4.Text = card4;
                label10.Text = player4mon.ToString();
                progressBar4.Value += 1;
                cmd = new SqlCommand("select * from tblImage where imgId = '" + n[9] + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["imgImage"];
                    ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                }
                pictureBox12.Image = Image.FromStream(ms);
                cmd.Dispose();
                dr.Close();

                cmd = new SqlCommand("select * from tblImage where imgId = '" + n[10] + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["imgImage"];
                    ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                }
                pictureBox13.Image = Image.FromStream(ms);
                cmd.Dispose();
                dr.Close();


                if (player3 == 1)
                {
                    Call.Visible = false;
                    Check.Visible = true;
                }
                else if (player3 == 2)
                {
                    if (id == master)
                    {
                        Check.Visible = true;
                        Call.Visible = false;
                    }

                    else
                    {
                        Check.Visible = false;
                        Call.Visible = true;
                    }
                }

                else if (player3 == 3)
                {
                    Check.Visible = false;
                    Call.Visible = true;
                }
                if (click == true)
                {
                    click = false;
                    timer4.Stop();
                    label9.Visible = false;
                    label10.Visible = false;
                    progressBar4.Visible = false;
                    //p4.Text = "";
                    cmd = new SqlCommand("select * from tblImage where imgId = '" + 56 + "'", cn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        byte[] picarr = (byte[])dr["imgImage"];
                        ms = new MemoryStream(picarr);
                        ms.Seek(0, SeekOrigin.Begin);
                    }
                    pictureBox12.Image = Image.FromStream(ms);
                    cmd.Dispose();
                    dr.Close();

                    cmd = new SqlCommand("select * from tblImage where imgId = '" + 56 + "'", cn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        byte[] picarr = (byte[])dr["imgImage"];
                        ms = new MemoryStream(picarr);
                        ms.Seek(0, SeekOrigin.Begin);
                    }
                    pictureBox13.Image = Image.FromStream(ms);
                    cmd.Dispose();
                    dr.Close();


                    label6.Text = "";
                    count = 1;
                    progressBar1.Visible = true;
                    progressBar1.Value = 0;
                    timer1.Start();
                    label1.Visible = true;
                    label3.Visible = true;
                    label6.Text = "Player 1!! Your turn";
                    this.Chance += new Chancedecider(timer1_Tick);
                }
            }


            else
            {
                click = false;
                timer4.Stop();
                label9.Visible = false;
                label10.Visible = false;
                progressBar4.Visible = false;
                //p4.Text = "";
                cmd = new SqlCommand("select * from tblImage where imgId = '" + 56 + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["imgImage"];
                    ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                }
                pictureBox12.Image = Image.FromStream(ms);
                cmd.Dispose();
                dr.Close();


                cmd = new SqlCommand("select * from tblImage where imgId = '" + 56 + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    byte[] picarr = (byte[])dr["imgImage"];
                    ms = new MemoryStream(picarr);
                    ms.Seek(0, SeekOrigin.Begin);
                }
                pictureBox13.Image = Image.FromStream(ms);
                cmd.Dispose();
                dr.Close();


                label6.Text = "";
                count = 1;
                progressBar1.Visible = true;
                progressBar1.Value = 0;
                timer1.Start();
                label1.Visible = true;
                label3.Visible = true;
                label6.Text = "Player 1!! Your turn";
                this.Chance += new Chancedecider(timer1_Tick);
            }

            if (count == 10)
            {
                timer4.Stop();
                progressBar4.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                count = 1;
                flag4 = -1;
                //card4 = "";
                //p4.Visible = false;
                pictureBox12.Visible = false;
                pictureBox13.Visible = false;
                player4 = 1;
                lastcard++;
                progressBar1.Visible = true;
                progressBar1.Value = 0;
                timer1.Start();
                label1.Visible = true;
                label3.Visible = true;
                label6.Text = "Player 1!! Your turn";
                this.Chance += new Chancedecider(timer1_Tick);
            }
            timer4.Interval = 1000;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Check_Click(object sender, EventArgs e)
        {
            click = true;
            if (no_of_cards == 4)
                lastcard++;

            if (id == 1)
                player1 = 1;
            else if (id == 2)
                player2 = 1;
            else if (id == 3)
                player3 = 1;
            else
                player4 = 1;
            
            //if all the players agree to check
            if ( player1 == 1 && player2 == 1 && player3 == 1 && player4 == 1 && no_of_cards == 3)
            {
                for (i = 0; i < i + 1; i++)
                {
                    g = r.Next(53) + 1;
                    if (g == 19 || g == 27)
                    {
                        continue;
                    }
                    else
                    {
                        if (no_of_cards == 3)
                        {
                            no_of_cards++;
                            pictureBox4.Visible = true;
                            cmd = new SqlCommand("select * from tblImage where imgId = '" + g + "'", cn);
                            dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                byte[] picarr = (byte[])dr["imgImage"];
                                ms = new MemoryStream(picarr);
                                ms.Seek(0, SeekOrigin.Begin);
                            }
                            pictureBox4.Image = Image.FromStream(ms);
                            cmd.Dispose();
                            dr.Close();
                            break;
                        }
                    }
                }
            }

            if (lastcard == 4 && no_of_cards == 4 &&(player1 == 1 && player2 == 1 && player3 == 1 && player4 == 1))
             {      
                 for (i = 0; i < i + 1; i++)
                {
                    g = r.Next(53) + 1;     
                    no_of_cards++;
                    pictureBox5.Visible = true;
                     cmd = new SqlCommand("select * from tblImage where imgId = '" + n[3] + "'", cn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                     {
                      byte[] picarr = (byte[])dr["imgImage"];
                      ms = new MemoryStream(picarr);
                      ms.Seek(0, SeekOrigin.Begin);
                     }    
                    pictureBox5.Image = Image.FromStream(ms);
                    cmd.Dispose();
                    dr.Close();
                     break;
                }
            }

                
            
            
            //else if (no_of_cards == 5 && player1 == 1 && player2 == 1 && player3 == 1 && player4 == 1)
            //{
  

            
            
        }

        private void Call_Click(object sender, EventArgs e)
        {
            click = true;

            if (id == 1)
            {
                player1 = 2;
                player1mon -= h;
                pot_money += h;
            }
            else if (id == 2)
            {
                player2 = 2;
                player2mon -= h;
                pot_money += h;
            }
            else if (id == 3)
            {
                player3 = 2;
                player3mon -= h;
                pot_money += h;
            }
            else
            {
                player4 = 2;
                player4mon -= h;
                pot_money += h;
            }
            
        }

        private void Raise_Click(object sender, EventArgs e)
        {
            click = true;
            bet.Visible = true;
            textBox1.Visible = true;
          
            if (id == 1)
            {
                player1 = 3;
                master = 1;
                timer1.Stop();
            }
            else if (id == 2)
            {
                player2 = 3;
                master = 2;
                timer2.Stop();
            }
            else if (id == 3)
            {
                player3 = 3;
                master = 3;
                timer3.Stop();
            }
            else
            {
                player4 = 3;
                master = 4;
                timer4.Stop();
            }
              
        }

        private void Fold_Click(object sender, EventArgs e)
        {    
             click = true;
            if (id == 1)
            {
                flag1 = -1;
                //card1 = "";
                //p1.Visible = false;
                pictureBox6.Visible = false;
                pictureBox7.Visible = false;
                player1 = 1;
                lastcard++;
            }
            else if (id == 2)
            {
                flag2 = -1;
                //card2 = "";
                //p2.Visible = false;
                pictureBox8.Visible = false;
                pictureBox9.Visible = false;
                player2 = 1;
                lastcard++;
            }
            else if (id == 3)
            {
                flag3 = -1;
                //card3 = "";
                //p3.Visible = false;
                pictureBox10.Visible = false;
                pictureBox11.Visible = false;
                player3 = 1;
                lastcard++;
            }
            else
            {
                flag4 = -1;
                //card4 = "";
                //p4.Visible = false;
                pictureBox12.Visible = false;
                pictureBox13.Visible = false;
                player4 = 1;
                lastcard++;
            }
            
        }

        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (id == 1)
            {
                if (Convert.ToInt32(textBox1.Text) > player1mon)
                {
                    MessageBox.Show("Your current balance is--->" + player1mon + " The bet amount should be less than " + player1mon);
                    textBox1.Text = "";
                }

                else
                {
                    h = Convert.ToInt32(textBox1.Text);
                    pot_money += h;
                    player1mon -= h;
                    textBox1.Text = "";
                    timer1.Start();
                }
                
            }

            else if (id == 2)
            {
                if (Convert.ToInt32(textBox1.Text) > player2mon)
                {
                    MessageBox.Show("Your current balance is--->" + player2mon + " The bet amount should be less than " + player2mon);
                    textBox1.Text = "";
                }

                else
                {
                    h = Convert.ToInt32(textBox1.Text);
                    pot_money += h;
                    player2mon -= h;
                    textBox1.Text = "";
                    timer2.Start();
                }
            }

            else if (id == 3)
            {
                if (Convert.ToInt32(textBox1.Text) > player3mon)
                {
                    MessageBox.Show("Your current balance is--->" + player3mon + "The bet amount should be less than" + player3mon);
                    textBox1.Text = "";
                }

                else
                {
                    h = Convert.ToInt32(textBox1.Text);
                    pot_money += h;
                    player3mon -= h;
                    textBox1.Text = "";
                    timer3.Start();
                }
            }

            else
            {
                if (Convert.ToInt32(textBox1.Text) > player4mon)
                {
                    MessageBox.Show("Your current balance is--->" + player4mon + " The bet amount should be less than " + player4mon);
                    textBox1.Text = "";
                }

                else
                {
                    h = Convert.ToInt32(textBox1.Text);
                    pot_money += h;
                    player4mon -= h;
                    textBox1.Text = "";
                    timer4.Start();
                }
             }

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }

        private void progressBar3_Click(object sender, EventArgs e)
        {

        }

        private void progressBar4_Click(object sender, EventArgs e)
        {

        }

        private void bet_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

      
    }
}

       
        

        
    


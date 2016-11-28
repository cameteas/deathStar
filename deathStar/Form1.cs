using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace deathStar
{
    public partial class Form1 : Form
    {
        //change for first commit
        //Declaring Global Variables

        bool leftArrowDown;
        bool downArrowDown;
        bool rightArrowDown;
        bool upArrowDown;
        bool spaceDown;

        bool start;
        bool launch;
        bool fall;
        bool reloading;
        bool approach;

        bool hit;
        bool miss;

        bool bounce = false;
        bool hitfall = false;
        bool stopbouncing = false;

        bool reactor = false;
        bool bottomTouch = false;
        bool floorBreak = false;
        bool detonate;

        int drawX = 900;
        int drawY = 300;

        int bombX;
        int bombY;
        int stage = 0;
        int bombsLeft = 5;

        int entranceHoleX = 0;
        int entranceHoleY = 400;
        int entranceHoleBottom = 600;
        int trenchBottomY = 400;

        int bounceNum = 0;
        int slowFall = 5;
        int bombwait = 0;


        int ventSize = 25;
        int speed = 5;
        int fallTime = 0;
        string words;

        Stopwatch incoming = new Stopwatch();
        public Form1()
        {
            InitializeComponent();
            start = true;
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Space:
                    spaceDown = true;
                    break;
                default:
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Space:
                    spaceDown = false;
                    break;
                default:
                    break;
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            #region key_controls
            if (leftArrowDown)
            {
                drawX--;
            }
            if (downArrowDown)
            {
                drawY = drawY + 2;
            }
            if (rightArrowDown)
            {
                drawX = drawX + 5;
            }
            if (upArrowDown)
            {
                drawY = drawY - 2;
            }
            if (spaceDown)
            {
                if (bombsLeft > 0)
                {
                    
                    launch = true;
                }
                if (hit)
                {
                    launch = false;
                }
            }
            #endregion
            if (start)
            {
                words = "You Luke Skywalker are preparing for your attack on the death star, Use this simulator to practice your bomb dropping skills";
                incoming.Start();
                label2.Text = incoming.ElapsedMilliseconds + " ";
                if (incoming.ElapsedMilliseconds > 5000)
                {
                    approach = true;
                }
                
            }
            if (approach)
            {
                entranceHoleX = entranceHoleX + speed;
                if (entranceHoleX > 1020)
                {
                    approach = false;
                }
            }
            #region Bomb_Dropping

            if (launch)
            {
                
                bombX = drawX - 15;
                bombY = drawY + 20;
                stage = 0;
                launch = false;
                fall = true;
                reloading = true;
                
            }
            else if (fall)
            {
                if (stage == 1)
                {
                    bombsLeft--;
                    bombX = bombX - 10;
                    bombY = bombY + 2;
                    stage++;
                    label1.Text = "Bombs: " + bombsLeft;
                }
                else if (stage < 10)
                {
                    
                    bombX = bombX - 10;
                    bombY = bombY + 2;
                    stage++;
                    
                }
                else if (stage < 20)
                {
                    bombX = bombX - 8;
                    bombY = bombY + 4;
                    stage++;
                }
                else if (stage < 30)
                {
                    bombX = bombX - 6;
                    bombY = bombY + 6;
                    stage++;
                }
                else if (stage < 40)
                {
                    bombX = bombX - 4;
                    bombY = bombY + 8;
                    stage++;
                }
                else if (stage < 50)
                {
                    bombX = bombX - 2;
                    bombY = bombY + 10;
                    stage++;
                }
                else if (stage < 60)
                {
                    bombY = bombY + 10;
                    stage++;
                }
                else if (stage < 70)
                {
                    bombX = bombX + 2;
                    bombY = bombY + 10;
                        stage++;
                    reloading = false;
                }
                if (bombY > 600) 
                {
                    fall = false;
                }
            }
            #endregion
            #region check Hit

            if (bombY + 5 > 400 && bombX <= entranceHoleX && bombX >= entranceHoleX - ventSize)
            {
                fall = false;
                hit = true;
            }
            else if (bombY + 5 > 400 && bombX < entranceHoleX - ventSize)
            {
                fall = false;
            }
            else if (bombY + 5 > 400 && bombX > entranceHoleX)
            {
                fall = false;
            }
            if (bounceNum > 4)
            {
                
                if(bombX > entranceHoleX + ventSize/2)
                {
                    fallTime++;
                }
                else
                {
                    bombX++;
                }
                if (fallTime > 30)
                {
                    reactor = true;
                    hit = false;
                }
            }

            if (hit)
            {
                start = false;
                approach = false;
                trenchBottomY = trenchBottomY - 10;
                entranceHoleY = entranceHoleY - 10;
                drawY = drawY - 10;
                drawX = drawX + 2;
                //    if (stopbouncing != true)
                //    {
                //        if (bounce == false && hitfall == false)
                //        {
                //            hitfall = true;
                //        }
                //        if (bombX < entranceHoleX - ventSize)
                //        {
                //            hitfall = false;
                //            bounce = true;
                //            bounceNum++;
                //            slowFall--;
                //        }
                //        else if (bombX > entranceHoleX - 10)
                //        {
                //            hitfall = true;
                //            bounce = false;
                //            bounceNum++;
                //            slowFall--;
                //        }

                //        if (bounce){
                //            entranceHoleX = entranceHoleX - slowFall;


                //        }
                //         if (hitfall)
                //        {
                //            entranceHoleX = entranceHoleX + slowFall;
                //        }
                //    }

            }
            #endregion
            #region reactor
            if (reactor)
            {
                
                if (bottomTouch == false)
                {
                    entranceHoleBottom = entranceHoleBottom - 10;
                    bombX++;
                        if (bombY + 15 > entranceHoleBottom)
                    {
                        bottomTouch = true;
                        floorBreak = true;
                    }
                }
                if (bottomTouch)
                {
                    if (floorBreak)
                    {
                        for(int i = 0; i < 10; i++)
                        {
                            entranceHoleBottom = entranceHoleBottom - i;
                            Thread.Sleep(5);
                            Refresh();
                            floorBreak = false;
                        }
                    }
                    
                    bombwait++;
                    if(bombwait == 30)
                    {
                        reactor = false;
                    }
                }
                
            }
            #endregion
            Refresh();
        }
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            Pen thickPen = new Pen(Color.Black, 4);
            Pen medPen = new Pen(Color.Black, 3);
            Pen thinPen = new Pen(Color.Black, 2);
            Pen holePen = new Pen(Color.White, 3);
            Font wordFont = new Font("Arial", 10);

            //print words
            e.Graphics.DrawString(words, wordFont, drawBrush, 0, 0);

            //drawing the scene
            e.Graphics.DrawLine(thinPen, 0, trenchBottomY - 310, 1000, trenchBottomY - 310);
            e.Graphics.DrawLine(medPen, 0, trenchBottomY - 300, 1000, trenchBottomY - 300);
            e.Graphics.DrawLine(medPen, 0, trenchBottomY, 1000, trenchBottomY);

            //drawing the ship 
            e.Graphics.DrawLine(medPen, drawX, drawY, drawX - 40, drawY + 15);
            e.Graphics.DrawLine(medPen, drawX , drawY + 30, drawX - 40, drawY + 15);
            e.Graphics.DrawLine(medPen, drawX, drawY + 30, drawX , drawY);

            //drawing the bomb
            if (fall || hit || reactor)
            {
                e.Graphics.DrawEllipse(medPen, bombX, bombY, 10, 10);
            }

            //drawing aim help
            e.Graphics.DrawEllipse(thinPen, drawX - 315, drawY + 322, 1, 1);
            

            //drawing vent to core
            e.Graphics.DrawLine(holePen, entranceHoleX, entranceHoleY, entranceHoleX - ventSize, entranceHoleY);

            //drawing vent wall
            e.Graphics.DrawLine(medPen, entranceHoleX, entranceHoleY, entranceHoleX, entranceHoleBottom);
            e.Graphics.DrawLine(medPen, entranceHoleX - ventSize, entranceHoleY, entranceHoleX - ventSize, entranceHoleBottom);

            //reactor
                e.Graphics.DrawLine(medPen, entranceHoleX, entranceHoleBottom, entranceHoleX - ventSize, entranceHoleBottom);

            DoubleBuffered = true;
        }


    }
}

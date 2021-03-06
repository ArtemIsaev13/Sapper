﻿#region Usings

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Bryozoa[] _bryozoaList;
        private Graphics _gameFieldGraph;
        private int _startX = -10;
        private int _startY = -10;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GameField.Height = 650;
            GameField.Width = 1120;
            cursorSelector.Left = 1150;
            cursorSelector.Top = 50;
            cursorType.SelectedItem = cursorType.Items[0];
            _gameFieldGraph = Graphics.FromHwnd(GameField.Handle);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var userBlack = new Pen(Color.Black, 2);
            _gameFieldGraph.DrawRectangle(userBlack, _startX, _startY, 50, 50);
            _startX += 50;
            _startY += 50;
        }

        private void GameField_Click(object sender, EventArgs e)
        {
            var cursorCoordination = new Point(Cursor.Position.X - GameField.Location.X - Left - 8, Cursor.Position.Y - GameField.Location.Y - Top - 30);
            button1.Text = Left.ToString();

            if (cursorType.SelectedItem == cursorType.Items[1])
            {
                if (_bryozoaList == null) //Create first object of this class
                {
                    button2.Text = "null";
                    _bryozoaList = new Bryozoa[1];
                    _bryozoaList[0] = new Bryozoa(cursorCoordination);
                }
                else //Create another (not first) object
                {
                    var tempArray = new Bryozoa[_bryozoaList.Length + 1];
                    for (var i = 0; i < _bryozoaList.Length; i++) tempArray[i] = _bryozoaList[i];
                    tempArray[tempArray.Length - 1] = new Bryozoa(cursorCoordination);
                    _bryozoaList = tempArray;
                }

                _bryozoaList[_bryozoaList.Length - 1].DrawMyself(_gameFieldGraph);
                button2.Text = _bryozoaList.Length.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_bryozoaList == null) return;

            foreach (var i in _bryozoaList)
            {
                i.DrawMyself(_gameFieldGraph);
                i.Activate(_gameFieldGraph);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var newDiffForm3 = new Form3();
            newDiffForm3.Show();
        }
    }

    internal class Bryozoa
    {
        private readonly Point _myCoordinate;
        public int Radius = 25;
        public int RadiusAct = 10;

        public Bryozoa(Point myCoordinateCon)
        {
            _myCoordinate = myCoordinateCon;
        }

        public void DrawMyself(Graphics grField)
        {
            var userBlack = new Pen(Color.Black, 2);
            grField.FillEllipse(Brushes.DarkOliveGreen, _myCoordinate.X - Radius, _myCoordinate.Y - Radius, 2 * Radius, 2 * Radius);
            grField.DrawEllipse(userBlack, _myCoordinate.X - Radius, _myCoordinate.Y - Radius, 2 * Radius, 2 * Radius);
        }

        public void Activate(Graphics grField)
        {
            var userBlack = new Pen(Color.Black, 2);
            grField.FillEllipse(Brushes.Crimson, _myCoordinate.X - RadiusAct, _myCoordinate.Y - RadiusAct, 2 * RadiusAct, 2 * RadiusAct);
            grField.DrawEllipse(userBlack, _myCoordinate.X - RadiusAct, _myCoordinate.Y - RadiusAct, 2 * RadiusAct, 2 * RadiusAct);
        }
    }
}
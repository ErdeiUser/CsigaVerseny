using CsigaVerseny.Controller;
using CsigaVerseny.Interface;
using CsigaVerseny.Modell;
using CsigaVerseny.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsigaVerseny
{
    /// <summary>
    /// Main form of the application
    /// </summary>
    public partial class Form1 : Form
    {

       /// <summary>
       /// Game Logic Controller
       /// </summary>       
        public IGameController gameController { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Start the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            //create game controller
            var gameController = new GameController();
            //different view type
            if (rbConsole.Checked)
            {
                //start game with console view
                gameController.StartGame(1,txtDebug);
            }
            else
            {
                //start game with text view
                gameController.StartGame(2, tbText);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }


        private void rbConsole_CheckedChanged(object sender, EventArgs e)
        {
             
        }
    }
}

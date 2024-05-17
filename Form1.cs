using CsigaVerseny.Controller;
using CsigaVerseny.Interface;
using CsigaVerseny.Modell;
using CsigaVerseny.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsigaVerseny
{
    public partial class Form1 : Form
    {
        private Game game;
        
        private ICsigaView View;
        private RaceController controller;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(rbConsole.Checked)
            {
                View = new ConsoleView();
            }
            else
            {
                View = new TextView();
            }
            controller = new RaceController(View, game);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game();
            game.StartGame();
        }
    }
}

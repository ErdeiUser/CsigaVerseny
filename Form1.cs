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
    public partial class Form1 : Form
    {
        private Game game;
        
        private ICsigaView View;
        private RaceController controller;
        private DbgListener dbgListener;

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
                View = new TextView(tbText);
            }
            controller = new RaceController(View, game);
            game.Controller=controller;
            game.StartGame();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dbgListener = new DbgListener(txtDebug);
            Debug.Listeners.Add(dbgListener);
            
            game = new Game();
        }


        private void rbConsole_CheckedChanged(object sender, EventArgs e)
        {
                dbgListener.Enabled = rbConsole.Checked;
        }
    }
}

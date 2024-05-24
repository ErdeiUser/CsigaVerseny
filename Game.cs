using CsigaVerseny.DAL;
using CsigaVerseny.Interface;
using CsigaVerseny.Modell;
using CsigaVerseny.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsigaVerseny
{
    public class Game:IGame
    {
        private int maxRound=5;

        public bool IsRunning { get; set; }
        public bool IsPaused { get; set; }
        public bool IsPlayEnd { get; set; }
        public List<IRacerDepot> Depos{ get; set; }

        private IRacerDepot racerDepot;

        public List<IRacer> Racers { get; set ; }
        public List<IPlayer> Players { get; set; }
        public IRaceController Controller { get; set; }

        IRace CurrentRace;
        private IRacerDepot snailDepot;

        public Game() 
        {

            Depos = new List<IRacerDepot>();
            racerDepot = new RacerDepot();
            Depos.Add(racerDepot);
            snailDepot = new SnailDepot();
            Depos.Add(snailDepot);

            //Reset();

            Racers = new List<IRacer>();
            Players = new List<IPlayer>();

            IPlayer player = new Player("player1", 100);
            Players.Add(player);

            foreach (IRacerDepot depot in Depos)
            {
                var filter=new Racer(null,null,0,null);
                if (depot is SnailDepot)
                {
                    filter.Type = 2;
                }

                foreach (IRacer Racer in depot.LoadAll(filter))
                {
                    Racers.Add(Racer);
                }
            }

            if (Racers.Count == 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    IRacer racer = racerDepot.Create((i + 1) + " racer", i.ToString(), 1, "white");
                    racerDepot.Save(racer);
                    Racers.Add(racer);
                }

                for (int i = 0; i < 3; i++)
                {
                    IRacer racer = snailDepot.Create((i + 1) + " snail", i.ToString(), 1, "slime");
                    snailDepot.Save(racer); 
                    Racers.Add(racer);
                }
            }

        }
        public void StartGame()
        {
             IsRunning = true;
            IsPaused = false;
            IsPlayEnd = false;


            CurrentRace = new Race(Racers, Players, maxRound);
            
            Random rnd = new Random();
            IPlayer player= Players.First();
            Controller.view.show("Palyer :" + player.ToString());

            foreach (IRacer racer in CurrentRace.Racers)
            {
                racer.backToStart();
                Controller.view.show("Racer:" + racer.ToString());
            }

            IBet bet1 = new Bet(player, Racers[rnd.Next(Racers.Count())], 10, 2);
            Controller.view.show(string.Format("Player bet:{0}",bet1.ToString()));
            CurrentRace.Bets.Add(bet1);


            Controller.view.show("Game start");

            int round = 1;
            while(round<= CurrentRace.MaxRound)
            {
                Controller.view.show("Round: "+round);

                int boosted= rnd.Next(0, CurrentRace.Racers.Count());
                CurrentRace.Racers[boosted].Boost = 2;
                Controller.view.show("Boosted racer: " + CurrentRace.Racers[boosted]);
                foreach (IRacer racer in Racers)
                {
                    racer.Move(round);
                    Controller.view.show(racer.ToString());
                }
                round++;
            }
            Controller.view.show("Race end");
            IRacer winner = CurrentRace.Racers.OrderByDescending(r=>r.Distance).First();
            CurrentRace.Winner = winner;
            Controller.view.show("Winner:"+winner.ToString());
            IPlayer PlayerWin = null;

            List<IBet> playBets= CurrentRace.Bets.Where(b=>b.player.Name==player.Name).ToList();

            foreach (IBet bet in CurrentRace.Bets)
            {
                if (bet.Racer == winner && bet.player.Name==player.Name)
                {
                    PlayerWin = bet.player;
                    player.Money+= bet.Amount * bet.IncomeRate;
                    Controller.view.show("Winner bet:" + bet.ToString());
                }
                else
                {
                    player.Money -= bet.Amount;
                    Controller.view.show("Looser bet:" + bet.ToString());
                }
            }

            IsPlayEnd = true;   
        }

        public void StopGame()
        {
            throw new NotImplementedException();
        }

        public void PauseGame()
        {
            throw new NotImplementedException();
        }

        public void ContinueGame()
        {
            throw new NotImplementedException();
        }

        public void LoadGame()
        {
            
        }

        public void SaveGame()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            string retval = "";
            retval+= "Game status: "+IsRunning;

            return retval;
        }

        internal void Reset()
        {
            foreach (IRacerDepot depot in Depos)
            {
                depot.DeleteAll();
                break;
            }   
        }
    }   
}

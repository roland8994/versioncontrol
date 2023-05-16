using WorldsHardestGame;

namespace week10
{
    public partial class Form1 : Form
    {
        int populationSize = 100;
        int nbrOfSteps = 10;
        int nbrOfStepsIncrement = 10;
        int generation = 1;
        GameController gc = new GameController();
        Brain winnerBrain = null;
        GameArea ga;
        public Form1()
        {
            InitializeComponent();

            ga = gc.ActivateDisplay();
            this.Controls.Add(ga);
            //gc.AddPlayer();
            //gc.Start(true);
            gc.GameOver += Gc_GameOver;

            for (int i = 0; i < populationSize; i++)
            {
                gc.AddPlayer(nbrOfSteps);
            }
            gc.Start();
        }

        private void Gc_GameOver(object sender)
        {
            generation++;
            label1.Text = string.Format(
                "{0}. gener�ci�",
                generation);
            var playerList = from p in gc.GetCurrentPlayers()
                             orderby p.GetFitness() descending
                             select p;
            var topPerformers = playerList.Take(populationSize / 2).ToList();

            gc.ResetCurrentLevel();
            foreach (var p in topPerformers)
            {
                var b = p.Brain.Clone();
                if (generation % 3 == 0)
                    gc.AddPlayer(b.ExpandBrain(nbrOfStepsIncrement));
                else
                    gc.AddPlayer(b);

                if (generation % 3 == 0)
                    gc.AddPlayer(b.Mutate().ExpandBrain(nbrOfStepsIncrement));
                else
                    gc.AddPlayer(b.Mutate());
            }
            gc.Start();

            var winners = from p in topPerformers
                          where p.IsWinner
                          select p;
            if (winners.Count() > 0)
            {
                winnerBrain = winners.FirstOrDefault().Brain.Clone();
                gc.GameOver -= Gc_GameOver;
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gc.ResetCurrentLevel();
            gc.AddPlayer(winnerBrain.Clone());
            gc.AddPlayer();
            ga.Focus();
            gc.Start(true);
        }
    }
}
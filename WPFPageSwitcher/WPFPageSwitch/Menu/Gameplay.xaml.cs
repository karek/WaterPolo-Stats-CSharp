using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;

namespace WPFPageSwitch
{
	public partial class Gameplay : UserControl, ISwitchable
	{
        private void NewClick(object sender, RoutedEventArgs e)
        {
            DataAccessor.Instance.newClick(); 
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
             DataAccessor.Instance.saveClick(); 
        }

		public Gameplay()
		{
			InitializeComponent();

            foreach (Team t in DataAccessor.Instance.teams)
            {
                TeamsSelect.Items.Add(t.ToString());
            }

            foreach (string value in Positions.GetNames(typeof(Positions)))
            {
                PositionSelect.Items.Add(value);
            }
		}

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void genPlayers(object sender, System.Windows.RoutedEventArgs e)
        {
            Team t = null;
            PlayerSelect.Items.Clear();

            try
            {
                t = DataAccessor.Instance.teams.Find(x => x.name == TeamsSelect.SelectedValue.ToString());
                var results = from item in DataAccessor.Instance.players where item.team.ToString() == t.ToString() select item; 
                foreach(Player p in results)
                {
                    PlayerSelect.Items.Add(p.ToString());
                }
            }
            catch(Exception)
            { }
        }

        private void createGoal(bool hit)
        {
            Team t = null;
            Player p = null;
            Goal g = null;
            Positions pos;

            try
            {
                t = DataAccessor.Instance.teams.Find(x => x.name == TeamsSelect.SelectedValue.ToString());
                p = DataAccessor.Instance.players.Find(x => x.name + " " + x.surname == PlayerSelect.SelectedValue.ToString());
                pos =  (Positions) Enum.Parse(typeof(Positions), PositionSelect.SelectedValue.ToString()); 

                if (t == null || p == null)
                    throw new Exception();
                g = new Goal(pos, p, hit);
                
                DataAccessor.Instance.goals.Add(g);
                MessageBox.Show(string.Format("Shoot created (false if miss, true if goal) -> {0}, position: {1}", hit, pos.ToString()), "Shoot");
            }
            catch (Exception)
            {
                MessageBox.Show("Not enough information to create a goal or miss", "Error!");
            }
        }

        private void Goal_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            createGoal(true);
        }

        private void Miss_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            createGoal(false);
        }      

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	Switcher.Switch(new MainMenu());
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFPageSwitch
{
    public partial class StatsExplorer : UserControl, ISwitchable
    {
        private Positions pos;
        private Team t;
        private Player p;
        private int inGoal, outGoal;

        public StatsExplorer()
        {
            InitializeComponent();
            inGoal = outGoal = 0;

            foreach (Team tt in DataAccessor.Instance.teams)
            {
                TeamsSelect.Items.Add(tt.ToString());
            }

            foreach (string value in Positions.GetNames(typeof(Positions)))
            {
                PositionSelect.Items.Add(value);
            }
        }

        private void NewClick(object sender, RoutedEventArgs e)
        {
            DataAccessor.Instance.newClick();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            DataAccessor.Instance.saveClick();
        }

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void typeSet(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                switch (TypeSelect.SelectedValue.ToString())
                {
                    case "Individual Stats":
                        TeamsSelect.Visibility = System.Windows.Visibility.Visible;
                        PlayerSelect.Visibility = System.Windows.Visibility.Visible;
                        selTeam.Visibility = System.Windows.Visibility.Visible;
                        selPla.Visibility = System.Windows.Visibility.Visible;
                        break;

                    case "Team Stats":
                        TeamsSelect.Visibility = System.Windows.Visibility.Visible;
                        PlayerSelect.Visibility = System.Windows.Visibility.Hidden;
                        selTeam.Visibility = System.Windows.Visibility.Visible;
                        selPla.Visibility = System.Windows.Visibility.Hidden;
                        break;

                    case "All Stats":
                        TeamsSelect.Visibility = System.Windows.Visibility.Hidden;
                        PlayerSelect.Visibility = System.Windows.Visibility.Hidden;
                        selTeam.Visibility = System.Windows.Visibility.Hidden;
                        selPla.Visibility = System.Windows.Visibility.Hidden;
                        break;

                    default: break;
                }
            }
            catch (Exception)
            { }
        }

        private void setText()
        {
            stats.Text = string.Format("Current stats: {0}/{1}", inGoal, inGoal + outGoal);
        }

        private void setStats()
        {
            switch (TypeSelect.SelectedValue.ToString())
            {
                case "Individual Stats":
                    var results = from item in DataAccessor.Instance.goals where item.hit == true && item.position == pos && item.player.ToString() == p.ToString() select item;
                        inGoal = results.Count();
                        results = from item in DataAccessor.Instance.goals where item.hit == false && item.position == pos && item.player.ToString() == p.ToString() select item;
                        outGoal = results.Count();                     
                        break;

                case "Team Stats":
                    var results1 = from item in DataAccessor.Instance.goals where item.hit == true && item.position == pos && item.player.team == t select item;
                    inGoal = results1.Count();
                    results1 = from item in DataAccessor.Instance.goals where item.hit == false && item.position == pos && item.player.team== t select item;
                    outGoal = results1.Count();             
                    break;

                case "All Stats":
                    var results2 = from item in DataAccessor.Instance.goals where item.hit == true && item.position == pos select item;
                    inGoal = results2.Count();
                    results2 = from item in DataAccessor.Instance.goals where item.hit == false && item.position == pos select item;
                    outGoal = results2.Count();                    
                    break;

                 default: break;
            }

            setText();
        }

        private void posSet(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                pos = (Positions)Enum.Parse(typeof(Positions), PositionSelect.SelectedValue.ToString());
                setStats();
            }
            catch (Exception ee)
            {
                //throw ee;
                //MessageBox.Show("Team or position or player not selected", "Error!");
            }
        }

        private void genPlayers()
        {
            t = null;
            PlayerSelect.Items.Clear();

            try
            {
                t = DataAccessor.Instance.teams.Find(x => x.name == TeamsSelect.SelectedValue.ToString());
                var results = from item in DataAccessor.Instance.players where item.team.ToString() == t.ToString() select item;
                foreach (Player p in results)
                {
                    PlayerSelect.Items.Add(p.ToString());
                }
            }
            catch (Exception)
            { }
        }

        private void teamSet(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                genPlayers();
                t = DataAccessor.Instance.teams.Find(x => x.name == TeamsSelect.SelectedValue.ToString());

                if (t == null)
                    throw new Exception();

                setStats();
            }
            catch (Exception)
            {
               // MessageBox.Show("Team or position or player not selected", "Error!");
            }
        }

        private void playerSet(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                p = DataAccessor.Instance.players.Find(x => x.name + " " + x.surname == PlayerSelect.SelectedValue.ToString());

                if (t == null || p == null)
                    throw new Exception();

                setStats();
            }
            catch (Exception)
            {
              //  MessageBox.Show("Team or position or player not selected", "Error!");
            }
        }

        private void Back_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }
    }
}

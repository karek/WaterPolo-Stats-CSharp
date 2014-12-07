using System;

namespace WPFPageSwitch
{
    public sealed class DataAccessor
    {
        private static volatile DataAccessor instance;
        private static object syncRoot = new Object();

        private DataAccessor() { }
        private string file { set; get; }

        public void newClick()
        {

        }

        public void saveClick()
        {

        }

        public void saveAsClick()
        {

        }

        public void openClick()
        {

        }

        private List<Goal> goals { get; }
        private List<Player> players { get; }
        private List<Team> teams { get; }

        public static DataAccessor Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DataAccessor();
                    }
                }

                return instance;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Windows;
using System.Reflection;
using System.Xml.Serialization;
using System.IO;

public sealed class DataAccessor
{
    private static volatile DataAccessor instance;
    private static object syncRoot = new Object();
    private string fileGoals, filePlayers, fileTeams;

    public List<Goal> goals;
    public List<Player> players;
    public List<Team> teams;

    private DataAccessor()
    {
        goals = new List<Goal>();
        players = new List<Player>();
        teams = new List<Team>();
        fileGoals = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        filePlayers = fileTeams = fileGoals;

        fileGoals += "\\goals.xml";
        filePlayers += "\\players.xml";
        fileTeams += "\\teams.xml";
        //  MessageBox.Show(string.Format("{0} - > {1} -> {2}", fileGoals, filePlayers, fileTeams), "Results");

        if (File.Exists(fileTeams)) teams = DeserializeTeamsFromXML();
        if (File.Exists(filePlayers)) players = DeserializePlayersFromXML();
        if (File.Exists(fileGoals)) goals = DeserializeGoalsFromXML();
    }

    private List<Goal> DeserializeGoalsFromXML()
    {
        XmlSerializer deserializer = new XmlSerializer(typeof(List<Goal>));
        TextReader textReader = new StreamReader(@fileGoals);
        List<Goal> _goals;
        _goals = (List<Goal>)deserializer.Deserialize(textReader);
        textReader.Close();
        return _goals;
    }

    private List<Player> DeserializePlayersFromXML()
    {
        XmlSerializer deserializer = new XmlSerializer(typeof(List<Player>));
        TextReader textReader = new StreamReader(@filePlayers);
        List<Player> _players;
        _players = (List<Player>)deserializer.Deserialize(textReader);
        textReader.Close();
        return _players;
    }

    private List<Team> DeserializeTeamsFromXML()
    {
        XmlSerializer deserializer = new XmlSerializer(typeof(List<Team>));
        TextReader textReader = new StreamReader(@fileTeams);
        List<Team> _teams;
        _teams = (List<Team>)deserializer.Deserialize(textReader);
        textReader.Close();
        return _teams;
    }

    private void SerializeGoalsToXML()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Goal>));
        TextWriter textWriter = new StreamWriter(@fileGoals);
        serializer.Serialize(textWriter, goals);
        textWriter.Close();
    }

    private void SerializePlayersToXML()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Player>));
        TextWriter textWriter = new StreamWriter(@filePlayers);
        serializer.Serialize(textWriter, players);
        textWriter.Close();
    }

    private void SerializeTeamsToXML()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Team>));
        TextWriter textWriter = new StreamWriter(@fileTeams);
        serializer.Serialize(textWriter, teams);
        textWriter.Close();
    }

    public void newClick()
    {
        goals.Clear();
        players.Clear();
        teams.Clear();
    }

    //Not yet
    public void saveClick()
    {
        try
        {
            SerializeGoalsToXML();
            SerializeTeamsToXML();
            SerializePlayersToXML();
            MessageBox.Show("Saved!!!", "Saved!");
        }
        catch (Exception)
        {
            MessageBox.Show("Save Failed", "Error");
        }
    }

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

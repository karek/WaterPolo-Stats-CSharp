using System;

[Serializable]
public class Player
{
    private string name { get; set; }
    private string surname { get; set; }
    private Team team { get; set; }
    public Player(string _name, string _surname, Team _team)
    {
        this.name = _name;
        this.surname = _surname;
        this.team = _team;
    }

    static string SerializeToXml(Player p)
    {
        StringWriter writer = new StringWriter();

        XmlSerializer serializer = new XmlSerializer(typeof(Player));
        serializer.Serialize(writer, p);

        return writer.ToString();
    }
}

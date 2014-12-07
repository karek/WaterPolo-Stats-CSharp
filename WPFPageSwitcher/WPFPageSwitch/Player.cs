using System;
using System.IO;
using System.Xml.Serialization;
[Serializable]
public class Player
{

//Care, name + surname = primary key, there can't be players with same name and surname
    public string name { get; set; }
    public string surname { get; set; }
    public Team team { get; set; }

    public Player(string _name, string _surname, Team _team)
    {
        this.name = _name;
        this.surname = _surname;
        this.team = _team;
    }

    public Player() { }


    static string SerializeToXml(Player p)
    {
        StringWriter writer = new StringWriter();

        XmlSerializer serializer = new XmlSerializer(typeof(Player));
        serializer.Serialize(writer, p);

        return writer.ToString();
    }

    public override string ToString()
    {
        return name + " " + surname;
    }

    public bool Equals(Team other)
    {
        if (other == null) return false;
        return (this.name.Equals(other.name));
    }
}

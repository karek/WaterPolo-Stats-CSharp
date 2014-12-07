using System;
using System.IO;
using System.Xml.Serialization;
[Serializable]
public class Team
{
    //I know, using ToString() as primary Key is a bad idea.
    public string name { get; set; }
	public Team(string _name)
	{
        name = _name;
	}

    public Team() { }

    static string SerializeToXml(Team p)
    {
        StringWriter writer = new StringWriter();

        XmlSerializer serializer = new XmlSerializer(typeof(Team));
        serializer.Serialize(writer, p);

        return writer.ToString();
    }

    public override string ToString()
    {
        return name;
    }

    public bool Equals(Team other)
    {
        if (other == null) return false;
        return (this.name.Equals(other.name));
    }
}

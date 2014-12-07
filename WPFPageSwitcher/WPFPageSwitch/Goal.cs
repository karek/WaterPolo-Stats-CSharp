using System;
using System.IO;
using System.Xml.Serialization;
public enum Positions { leftWing, middle, rightWing, leftHalf, midHalf, rightHalf, ownHalf };
[Serializable]
public class Goal
{
    public Player player;
    public Positions position;
    public bool hit;
    public Goal(Positions _pos, Player _player, bool _hit)
    {
        hit = _hit;
        position = _pos;
        player = _player;
    }

    Goal() { }

    static string SerializeToXml(Goal p)
    {
        StringWriter writer = new StringWriter();

        XmlSerializer serializer = new XmlSerializer(typeof(Goal));
        serializer.Serialize(writer, p);

        return writer.ToString();
    }
}

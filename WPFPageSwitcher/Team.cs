using System;

[Serializable]
public class Team
{
    private string name { get; set; }
	public Team(string _name)
	{
        name = _name;
	}
}

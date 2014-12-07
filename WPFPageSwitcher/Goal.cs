using System;

enum Positions { leftWing, middle, rightWing, leftHalf, midHalf, rightHalf, ownHalf };
[Serializable]
public class Goal
{
    private Player player { get; set; }
    private Positions position { get; set; }
    bool hit { get; set; }
	public Goal(Positions _pos, Player _player, bool _hit)
	{
        hit = _hit;
        position = _pos;
        player = _player;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{

    private static Dictionary<GameObject, Player> players = new Dictionary<GameObject, Player>();
    private static Player activePlayer;

    public static Player CreatePlayer(GameObject _obj)
    {
        Player player = new Player(_obj);
        players.Add(_obj, player);
        activePlayer = player;
        return player;
    }

    public static Player GetPlayer(GameObject _obj)
    {
        return players[_obj];
    }

    public static Player ActivePlayer
    {
        get { return activePlayer; }
        set { activePlayer = value; }
    }

    public static void Reset()
    {
        players.Clear();
        activePlayer = null;
    }
}

public class Player
{
    GameObject obj;
    float power;
    int powerPerBattery;
    int countBattery;

    public Player(GameObject _obj)
    {
        obj = _obj;
    }

    public int PowerPerBattery
    {
        get { return powerPerBattery; }
        set { powerPerBattery = value; }
    }

    public float Power
    {
        get { return power; }
        set { power = value > (countBattery * powerPerBattery) ? (countBattery * powerPerBattery) : value; }
    }

    public int BatteryCount
    {
        get { return countBattery; }
        set { countBattery = value; }
    }

}

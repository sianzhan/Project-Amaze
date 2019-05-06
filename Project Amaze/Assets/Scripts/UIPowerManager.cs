using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIPowerManager : MonoBehaviour {

    public GameObject prefabBattery;

    public const int cellCount = 5;
    public float spacingToNextBattery = 25;

    RectTransform rect;

    float power;
    int powerPerBattery;
    int countBattery;

    List<Battery> batteries;
    // Use this for initialization

	void Start () {
        batteries = new List<Battery>();
        rect = GetComponent<RectTransform>();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateBatteryLevel();

	}

    void UpdateBatteryLevel()
    {
        Player player = PlayerManager.ActivePlayer;
        power = player.Power;
        powerPerBattery = player.PowerPerBattery;
        countBattery = player.BatteryCount;
        float powerPerCell = powerPerBattery / cellCount;

        if (countBattery > batteries.Count) AddBattery(countBattery - batteries.Count);
        
        for(int i = 0; i < batteries.Count; ++i)
        {

            if (i < countBattery)
            {
                batteries[i].SetBatteryLevel(Mathf.CeilToInt(power / powerPerCell));
                power -= powerPerBattery;
            }
            else
            {
                batteries[i].SetActive(false);
                break;
            }

        }
    }

    public int GetBatteryCount()
    {
        return batteries.Count;
    }

    void AddBattery(int _count = 1)
    {
        while(--_count >= 0)
        {
            GameObject obj = Instantiate(prefabBattery);
            obj.transform.SetParent(rect, false);
            batteries.Add(new Battery(obj));
        }

    }



    class Battery
    {
        GameObject battery;
        GameObject[] cells;
        public Battery(GameObject _obj)
        {
            battery = _obj;
            cells = new GameObject[cellCount];
            for (int i = 0; i < cellCount; ++i)
            {
                cells[i] = battery.transform.GetChild(i).gameObject;
            }
        }

       public void SetBatteryLevel(int _level)
        {
            for(int i = 0; i < cellCount; ++i)
            {
                cells[i].SetActive(i < _level);
            }
        }

        public void SetActive(bool _active)
        {
            battery.SetActive(_active);
        }
    }
}

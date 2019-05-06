using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour {

    List<GameObject> powersInactivated;
    List<GameObject> powersActivated;
    public int maxCountActivePower = 1;

	// Use this for initialization
	void Start () {
        powersInactivated = new List<GameObject>();
        powersActivated = new List<GameObject>();

        for (int i = 0; i < transform.childCount; ++i)
        {
            GameObject child = transform.GetChild(i).gameObject;
            if(child.CompareTag("Power"))
            {
                child.SetActive(false);
                powersInactivated.Add(child);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < powersActivated.Count;)
        {
            if(!powersActivated[i].activeSelf)
            {
                DeactivePower(powersActivated[i]);
                continue;
            }
            ++i;
        }

        int countToActive = maxCountActivePower - powersActivated.Count;
        if (countToActive > powersInactivated.Count) countToActive = powersInactivated.Count;

        while (countToActive-- > 0)
        {
            GameObject chosen = powersInactivated[Random.Range(0, powersInactivated.Count - 1)];
            ActivePower(chosen);

        }
    }

    void ActivePower(GameObject _power)
    {
        _power.SetActive(true);
        powersInactivated.Remove(_power);
        powersActivated.Add(_power);
    }

    void DeactivePower(GameObject _power)
    {
        _power.SetActive(false);
        powersActivated.Remove(_power);
        powersInactivated.Add(_power);
    }

}

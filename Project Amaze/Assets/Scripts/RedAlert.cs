using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedAlert : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Player player = PlayerManager.ActivePlayer;
        Image img = GetComponent<Image>();
        if (player.Power < 0)
        {
            img.color = new Color(1, 0, 0, player.Power < -player.PowerPerBattery  ? 0.6f: (-player.Power) / player.PowerPerBattery * 0.6f);
        }else
        {
            img.color = new Color(1, 0, 0, 0);
        }
    }
}

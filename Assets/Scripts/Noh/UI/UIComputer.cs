using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComputer : UIBase {
    public GameObject ComputerBG;
    
    public override void OpenUI()
    {
        Player.instance.AnimationState = 3;
        ComputerBG.GetComponent<Animator>().SetBool("Switch", true);
        AudioManager.instance.PlayEffect("computerBooting");
        base.OpenUI();
    }
    public override void CloseUI()
    {
        Player.instance.AnimationState = Player.instance.TempAnimState;
        ComputerBG.GetComponent<Animator>().SetBool("Switch", false);
        base.CloseUI();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

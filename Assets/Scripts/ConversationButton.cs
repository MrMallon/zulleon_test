using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationButton : MonoBehaviour {

    public Canvas Parent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CloseCanvas()
    {
        Parent.enabled = false;
    }

    public void NextPhrase()
    {
        CloseCanvas();
    }
}

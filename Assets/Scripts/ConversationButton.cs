using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationButton : MonoBehaviour {

    public GameObject Parent;

    public void CloseCanvas()
    {
        DestroyObject(Parent);
    }

    public void NextPhrase()
    {
        CloseCanvas();
    }
}

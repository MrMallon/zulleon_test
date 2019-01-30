using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimateText : MonoBehaviour {

    public float _textSpeed;
    public string _defaultMessage;
    public Text _messageUI;

	// Use this for initialization
	void Start () {
        _messageUI.text = "";

        StartCoroutine(AnimateString(_defaultMessage));
    }

    private IEnumerator AnimateString(string message)
    {
        foreach(char letter in message.ToCharArray())
        {
            _messageUI.text += letter;
            yield return 0;
            yield return new WaitForSeconds(_textSpeed);
        }
    }
}

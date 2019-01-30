using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Animator _animator;
    public Canvas _dialog;

    // Use this for initialization
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        TouchInput(GetComponent<BoxCollider2D>());
    }

    public void TouchInput(BoxCollider2D collider)
    {
        if (Input.touchCount > 0)
        {
            if (collider == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)))
            {
                switch (Input.GetTouch(0).phase)
                {
                    case TouchPhase.Began:
                        ShowDialog();
                        CheckIsRunning();
                        break;
                    case TouchPhase.Moved:
                        DragCharacter();
                        break;
                    case TouchPhase.Stationary:
                        DragCharacter();
                        break;
                    case TouchPhase.Ended:
                        CheckIsRunning();
                        break;
                }
            }
        }
    }

    private void DragCharacter()
    {
        HideDialog();
        SetRunningAnimation(true);
        Vector3 pos;

        pos = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y, transform.position.z);
        transform.position = pos;
    }

    private void SetRunningAnimation(bool isRunning)
    {
        _animator.SetBool("IsPickedUp", isRunning);
    }

    private void CheckIsRunning()
    {
        if (_animator.GetBool("IsPickedUp"))
            SetRunningAnimation(false);
    }

    private void ShowDialog()
    {
        if (_animator.GetBool("IsPickedUp") == false)
            _dialog.enabled = true;
    }

    private void HideDialog()
    {
        if (_animator.GetBool("IsPickedUp"))
            _dialog.enabled = false;
    }
}

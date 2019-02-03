using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour, IBeginDragHandler, IEndDragHandler, ISelectHandler {

    private Animator _animator;
    private bool _playerPickedUp;
    private bool _dragging;
    public GameObject dialogPrefab;
    public string _playerDialog;

    // Use this for initialization
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_dragging)
            TouchInput(GetComponent<BoxCollider2D>());
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _dragging = false;
    }


    public void OnSelect(BaseEventData eventData)
    {
        if (_dragging == false)
            ShowDialog();
    }

    private void OnMouseDrag()
    {
        if (_playerPickedUp && _dragging)
        {
            DragCharacter(Input.mousePosition);
        }
    }

    private void OnMouseDown()
    {
        var collider = GetComponent<BoxCollider2D>();
        if (collider == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            _playerPickedUp = true;
        }
    }

    private void OnMouseUp()
    {
        var collider = GetComponent<BoxCollider2D>();
        if (_dragging == false && collider == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            ShowDialog();
        }

        _playerPickedUp = false;
        SetRunningAnimation(false);
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
                        SetRunningAnimation(false);
                        break;
                    case TouchPhase.Moved:
                        DragCharacter(Input.GetTouch(0).position);
                        break;
                    case TouchPhase.Stationary:
                        SetRunningAnimation(false);
                        break;
                    case TouchPhase.Ended:
                        SetRunningAnimation(false);
                        break;
                }
            }
        }
    }

    private void DragCharacter(Vector2 position)
    {
        if (GameObject.Find(gameObject.name + "dialog(Clone)"))
            DestroyObject(GameObject.Find(gameObject.name + "dialog(Clone)"));

        SetRunningAnimation(true);
        Vector3 pos;
        pos = new Vector3(Camera.main.ScreenToWorldPoint(position).x, Camera.main.ScreenToWorldPoint(position).y, transform.position.z);
        transform.position = pos;
    }

    private void SetRunningAnimation(bool isRunning)
    {
        _animator.SetBool("IsPickedUp", isRunning);
    }

    public void ShowDialog()
    {
        if (GameObject.Find(gameObject.name + "dialog(Clone)"))
            return;

        Vector3 playerPos = transform.position;
        Vector3 playerDirection = transform.right;
        float spawnDistance = 1;
    
        Vector3 spawnPos = playerPos + playerDirection * spawnDistance;

        var child = dialogPrefab.GetComponentInChildren<AnimateText>();
        child._defaultMessage = _playerDialog;

        dialogPrefab.name = gameObject.name + "dialog";
        GameObject dialog = Instantiate(dialogPrefab, spawnPos, Quaternion.identity) as GameObject;

        dialog.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        dialog.transform.position = spawnPos;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private Vector3 touchPosition;
    private bool _isDragActive = false;
    private Draggable _lastDragged;
    public PauseMenu pauseMenu;

    private void Update()
    {
        if(pauseMenu.isPaused == true)
        {
            return;
        }
        if(_isDragActive && (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            _isDragActive = false;
            _lastDragged = null;
            return;
        }
        if (Input.touchCount > 0)
        {
            touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
        else { return; }
        if (_isDragActive && _lastDragged != null)
        {
            _lastDragged.transform.position = new Vector2(touchPosition.x, touchPosition.y);
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
            if (hit.collider != null)
            {
                Draggable draggable = hit.transform.gameObject.GetComponent<Draggable>();
                if(draggable != null)
                {
                    Debug.Log("init");
                    _lastDragged = draggable;
                    _isDragActive = true;
                }
            }
        }
    }
}
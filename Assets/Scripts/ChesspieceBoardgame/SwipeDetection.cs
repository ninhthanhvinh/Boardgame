using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipeDetection : MonoBehaviour
{
    Chesspiece piece;
    public UnityEvent<Vector2, float> OnSwipe;
    private InputManager inputManager;


    [SerializeField]
    private float minimumDistance = .2f;
    [SerializeField]
    private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)]
    private float directionThreshold = .9f;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;
    private Camera mainCamera;
    private void Awake()
    {
        inputManager = InputManager.Instance;
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time, Chesspiece _piece)
    {
        startPosition = position;
        startTime = time;
        piece = _piece;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        //var distance = Vector3.Distance(startPosition, endPosition);
        //if (distance >= minimumDistance && 
        //    (endTime - startTime) <= maximumTime)
        //{
        //    Debug.DrawLine(startPosition, endPosition, Color.cyan, 5f);
        //    Vector3 direction = endPosition - startPosition;
        //    Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
        //    SwipeDirection(direction2D, distance *  20000f);
        //}
        var pos = mainCamera.ScreenToWorldPoint(endPosition);
        pos.y = piece.transform.position.y;
        Debug.Log(pos);
        Debug.Log("vi tri" + piece.transform.position);
        piece.Move(pos, endTime - startTime);
    }

    private void SwipeDirection(Vector2 direction, float force)
    {
        //if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        //{
        //    Debug.Log("Swipe Up");
        //}

        //else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        //{
        //    Debug.Log("Swipe Down");
        //}

        //else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        //{
        //    Debug.Log("Swipe Left");
        //}

        //else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        //{
        //    Debug.Log("Swipe Right");
        //}
        //OnSwipe?.Invoke(direction, force);
        piece.Move(direction, force);
    }
}

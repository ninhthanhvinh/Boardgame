using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RopeScript : MonoBehaviour
{
    [Header("Rope Settings")]

    [SerializeField] private Transform Transpoint1;
    [SerializeField] private Transform Transpoint2;
    [SerializeField] private Transform TranspointCenter;
    [SerializeField] private float emforce;

    private LineRenderer _lineRenderer;

    Vector3 originPos;
    Vector3 startPoint;

    // Start is called before the first frame update
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        originPos = TranspointCenter.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Transpoint1 && Transpoint2)
        {
            _lineRenderer.positionCount = 3;
            _lineRenderer.SetPosition(0, Transpoint1.position);
            _lineRenderer.SetPosition(1, TranspointCenter.position);
            _lineRenderer.SetPosition(2, Transpoint2.position);
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        collision.transform.TryGetComponent<DragDropable>(out var dragdropable);
        if (dragdropable != null && dragdropable.isDragging)
        {
            startPoint = collision.transform.position;
            //collision.transform.GetChild(0).position = collision.contacts[0].point;
            //startPoint = collision.contacts[0].point;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        var force = 0f;
        collision.transform.TryGetComponent<DragDropable>(out var dragdropable);
        if(dragdropable != null && dragdropable.isDragging)
        {
            TranspointCenter.transform.position = collision.transform.GetChild(0).position;
            force = emforce;
        }

        if (dragdropable != null && !dragdropable.isDragging &&dragdropable.isDragged)
        {
            var endPoint = TranspointCenter.transform.position;
            var direction = startPoint - endPoint;
            collision.transform.GetComponent<Chesspiece>().MoveByForce(direction, /*Mathf.Abs(direction.sqrMagnitude) **/ emforce);
            dragdropable.isDragged = false;
        }
    }
}

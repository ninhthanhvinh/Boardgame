using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetPieceChose : MonoBehaviour
{
    // Start is called before the first frame update
    private InputManager inputManager;
    Camera mainCamera;
    Ray ray;

    void Awake()
    {
        inputManager = InputManager.Instance;
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        inputManager.OnSwipe += GetPiece;
    }

    private void OnDisable()
    {
        inputManager.OnSwipe -= GetPiece;
    }
    
    private Chesspiece GetPiece(Vector2 position)
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(position);
        bool hasHit = Physics.Raycast(ray, out hit);
        Debug.Log(hit.collider.name);
        if (hasHit && hit.transform.GetComponent<Chesspiece>() != null)
        {
            var piece = hit.transform.GetComponent<Chesspiece>();
            /*SwipeDetection.OnSwipe.AddListener(piece.Move);*/
            return piece;
        }
        else return null;
        //else
            //SwipeDetection.OnSwipe.RemoveAllListeners();
    }
}

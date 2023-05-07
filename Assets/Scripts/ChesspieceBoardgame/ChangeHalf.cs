using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHalf : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] bool isPlayersHalf;

    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        var piece = other.GetComponent<Chesspiece>();
        if (piece != null && piece.isMovableByPlayer != isPlayersHalf)
        {
            piece.isMovableByPlayer = isPlayersHalf;
            piece.GetComponent<DragDropable>().UpdateDragable();

            int valueChange;
            if (isPlayersHalf)
                valueChange = 1;
            else valueChange = -1;
            gameManager.GetPieceChangeHalf(valueChange);
        }
    }


}

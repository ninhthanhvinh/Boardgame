using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] GameObject[] piecesOnBoard;
    public GameObject target;
    [SerializeField] float cooldown = 2f;
    [SerializeField] float force = 2000f;
    [SerializeField] Vector2 offset;
    float originCooldown;
    // Start is called before the first frame update
    void Start()
    {
        originCooldown = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            MoveAPiece(GetPiecesAICanMove());;
            cooldown = originCooldown;
        }
    }

    int GetPiecesAICanMove()
    {
        int i = 0;
        do
        {
            i = Random.Range(0, piecesOnBoard.Length);
        } while (piecesOnBoard[i].GetComponent<Chesspiece>().isMovableByPlayer);

        return i;
    }

    Vector2 GetDirection(GameObject piece)
    {
        var direction = target.transform.position - piece.transform.position;
        var direction2D = new Vector2(direction.x, direction.z);
        return (direction2D + Offset());
    }

    void MoveAPiece(int i)
    {
        var piece = piecesOnBoard[i].GetComponent<Chesspiece>();
        piece.AIMove(GetDirection(piece.gameObject), force);
    }

    Vector2 Offset()
    {
        var random = Random.Range(0, 10);
        if (random < 4)
            return Vector2.zero;
        else return offset * random;
    }
}

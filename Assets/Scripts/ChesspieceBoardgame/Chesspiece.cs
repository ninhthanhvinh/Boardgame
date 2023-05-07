using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chesspiece : MonoBehaviour
{
    [SerializeField] public bool isMovableByPlayer;
    public GameObject hitVFX; 
    private Rigidbody rb;
    AudioManager audioManager;

    Vector3 velocity;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioManager = AudioManager.Instance;
    }
    public void MoveByForce(Vector3 direction, float force)
    {
        if (isMovableByPlayer)
        {
            rb.AddForce(direction * force, ForceMode.Impulse);
            Debug.Log(force);
        }
    }

    public void Move(Vector3 position, float time)
    {
        transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, time);
    }

    public void AIMove(Vector2 direction2D, float force)
    {
        if (!isMovableByPlayer)
        {
            var direction = new Vector3(direction2D.x, 0, direction2D.y);
            rb.AddForce(direction * force, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        Instantiate(hitVFX, contactPoint.point, Quaternion.identity);
        audioManager.PlaySFX("Hit");
    }

}

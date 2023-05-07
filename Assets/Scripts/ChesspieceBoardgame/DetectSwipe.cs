using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DetectSwipe : MonoBehaviour
{
    public UnityEvent<Vector2> OnSwipe;

    [SerializeField] InputAction position;
    [SerializeField] InputAction press;
    [SerializeField] float swipeResistance = 50;

    Vector2 initialPos;
    Vector2 currentPos => position.ReadValue<Vector2>();

    private void Awake()
    {
        position.Enable();
        press.Enable();
        press.performed += _ => { initialPos = currentPos; };
    }
    
    void SwipeDetect()
    {
        Vector2 delta = currentPos - initialPos;
        Vector2 direction = Vector2.zero;

        if(Mathf.Abs(delta.x) > swipeResistance)
        {
            direction.x = Mathf.Clamp(delta.x, -1, 1);
        }

        if (Mathf.Abs(delta.y) > swipeResistance)
        {
            direction.y = Mathf.Clamp(delta.y, -1, 1);
        }

        if (direction != Vector2.zero)
        {
            OnSwipe?.Invoke(direction);
        }
    }

    public void Logging(Vector2 pos)
    {
        Debug.Log(pos);
    }
}

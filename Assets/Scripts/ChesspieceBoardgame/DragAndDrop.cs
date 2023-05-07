using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private InputAction mouseClick;

    [SerializeField] float mouseDragPhysicsSpeed = 10;
    [SerializeField] float mouseDragSpeed = .1f;

    private Vector3 velocity;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void OnEnable()
    {
        mouseClick.Enable();
        mouseClick.performed += MousePressed;
    }

    private void OnDisable()
    {
        mouseClick.Disable();
        mouseClick.performed -= MousePressed;
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        
        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.name);
            if(hit.collider != null)
            {
                StartCoroutine(DragUpdate(hit.collider.gameObject));
            }
        }

    }
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private IEnumerator DragUpdate(GameObject gameObject)
    {
        Debug.Log("Drag");
        float initialDistance = Vector3.Distance(gameObject.transform.position, mainCamera.transform.position);
        gameObject.TryGetComponent<Rigidbody>(out var rb);
        while (mouseClick.ReadValue<float>() != 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (rb != null)
            {
                Vector3 direction = ray.GetPoint(initialDistance) - gameObject.transform.position;
                rb.velocity = direction * mouseDragPhysicsSpeed;
                yield return waitForFixedUpdate;
            }
            else
            {
                gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, ray.GetPoint(initialDistance), ref velocity, mouseDragSpeed);
                yield return null;
            }
        }
    }
}

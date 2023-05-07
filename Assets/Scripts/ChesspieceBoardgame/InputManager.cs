
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time, Chesspiece piece);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    public delegate Chesspiece SwipePosition(Vector2 position);
    public event SwipePosition OnSwipe;
    #endregion
    private PlayerController inputActions;
    Camera mainCamera;
    private void Awake()
    {
        inputActions = new PlayerController();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        inputActions.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        inputActions.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private async void StartTouchPrimary(InputAction.CallbackContext context)
    {
        await Task.Delay(50);
        var pos = Utils.ScreenToWorld(mainCamera, inputActions.Touch.PrimaryPosition.ReadValue<Vector2>());
        Chesspiece piece = OnSwipe?.Invoke(inputActions.Touch.PrimaryPosition.ReadValue<Vector2>());
        if (OnStartTouch != null && piece != null) OnStartTouch(new Vector2(pos.x, pos.z), (float)context.startTime, piece);
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {

        var pos = Utils.ScreenToWorld(mainCamera, inputActions.Touch.PrimaryPosition.ReadValue<Vector2>());
        if (OnEndTouch != null) OnEndTouch(new Vector2(pos.x, pos.z), (float)context.time);
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera, inputActions.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}

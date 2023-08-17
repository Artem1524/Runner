using static UnityEngine.InputSystem.InputAction;

public class NewPlayerController : BasePlayerController
{
    private RunnerControls _controls;

    private void Awake()
    {
        _controls = new RunnerControls();
    }

    private void FixedUpdate()
    {
        float direction = _controls.Player.SideMove.ReadValue<float>();

        MoveSide(direction);
    }

    private void OnEnable()
    {
        _controls.Player.Enable();

        _controls.Player.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        _controls.Player.Jump.performed -= OnJump;

        _controls.Player.Disable();
    }

    private void OnDestroy()
    {
        _controls.Dispose();
    }

    private void OnJump(CallbackContext context)
    {
        Jump();
    }
}

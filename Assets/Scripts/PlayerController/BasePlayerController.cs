using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class BasePlayerController : MonoBehaviour
{
    private bool _canJump = true;
    protected Rigidbody _body;

    [SerializeField, Range(5f, 100f)]
    protected float _acceleration = 25f; // Увеличение velocity

    [SerializeField, Range(5f, 200f)]
    protected float _baseMaxSpeed = 10f; // Базовая скорость (с учётом ускорения)
    [SerializeField, Range(1f, 15f)]
    protected float _maxSpeedIncrease = 1f; // Увеличение базовой скорости (в сек.)

    [SerializeField, Range(10f, 100f)]
    [Tooltip("Горизонтальная скорость персонажа")]
    protected float _sideSpeed = 30f;

    [SerializeField, Range(50f, 500f)]
    protected float _jumpForce = 250f;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        StartCoroutine(MoveForward());
        StartCoroutine(IncreaseMaxSpeed());
    }

    protected void Jump()
    {
        if (!_canJump)
            return;

        _canJump = false;
        _body.AddForce(transform.up * _jumpForce);
    }

    protected void MoveSide(float direction)
    {
        if (direction == 0f)
            return;

        _body.velocity += transform.right * direction * _sideSpeed * Time.fixedDeltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        _canJump = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        _canJump = false;
    }

    private IEnumerator MoveForward()
    {
        while (true)
        {
            Vector3 velocity = _body.velocity;

            velocity.z = Mathf.Clamp(velocity.z + _acceleration * Time.fixedDeltaTime, 1f, _baseMaxSpeed);

            _body.velocity = velocity;

            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator IncreaseMaxSpeed()
    {
        while (true)
        {
            float increaseInterval = 0.5f; // Периодичность увеличения скорости (в сек.)
            _baseMaxSpeed += _maxSpeedIncrease * increaseInterval;
            yield return new WaitForSeconds(increaseInterval);
        }
    }

    public void SlowPlayer()
    {
        Vector3 velocity = _body.velocity;
        velocity.z *= 0.1f; // Устанавливает скорость в 10% от текущей
        _body.velocity = velocity;
    }
}

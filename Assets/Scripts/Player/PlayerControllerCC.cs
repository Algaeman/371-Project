using UnityEngine;

/* Based on Unity documentation for CharacterController.Move */

public class PlayerControllerCC : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _jumpSpeed = 7f;
    [SerializeField] float _gravity = 2f;
    [SerializeField] float _gravityJumpModifier = 2f;
    
    CharacterController _characterController;
    Vector3 _moveDirection = Vector3.zero;
    float _gravityDownBoost = 2;

    void Start() => _characterController = GetComponent<CharacterController>();

    void Update()
    {
        if (_characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            _moveDirection *= _speed;

            // Face in the right direction
            if (_moveDirection != Vector3.zero)
            {
                transform.forward = _moveDirection;
            }

            // Handle jumping
            if (Input.GetButton("Jump"))
            {
                _moveDirection.y = _jumpSpeed;
            }
        }

        // Need to continually apply gravity to player
        _moveDirection.y -= _gravityJumpModifier * _gravity * Time.deltaTime;

        // If jumping, apply more gravity on the way down to make jump "feel" better
        if (_moveDirection.y < 0)
        {
            _moveDirection.y -= _gravityDownBoost * _gravityJumpModifier * _gravity * Time.deltaTime;
        }

        // Move the controller
        _characterController.Move(_moveDirection * Time.deltaTime);
    }
}
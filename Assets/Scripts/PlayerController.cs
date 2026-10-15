using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float boostMultiplier = 20f;
    [SerializeField] private float baseSpeed = 15f;
    [SerializeField] private ScoreManager scoreManager;
    Rigidbody2D rb;
    SurfaceEffector2D surfaceEffector2D;
    InputAction moveAction;
    Vector2 moveValue;
    float maxTorque = 500f;
    int flipcount;
    float totalRotation;
    float previousRotation;
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindAnyObjectByType<SurfaceEffector2D>();
    }

    void Update()
    {
        RotatePlayer();
        BoostPlayer();
        CalculateFlips();
    }
    // Method to boost the player's speed based on movement input
    private void BoostPlayer()
    {
        // if the player is moving  (up), increase the speed of the SurfaceEffector2D
        if (moveValue.y > 0)
        {
            surfaceEffector2D.speed = boostMultiplier; // Increase speed when moving forward
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed; // Reset speed when not moving forward
        }

    }
    // Method to rotate the player based on movement input
    private void RotatePlayer()
    {
        // Read the value of the Move action and store it in moveValue
        moveValue = moveAction.ReadValue<Vector2>();

        if (moveValue.x > 0)
        {
            rb.AddTorque(-2f); // Apply torque to rotate the player clockwise
            if (rb.angularVelocity < -maxTorque)
            {
                rb.angularVelocity = -maxTorque; // Limit the maximum clockwise rotation speed
            }
        }
        else if (moveValue.x < 0)
        {
            rb.AddTorque(2f); // Apply torque to rotate the player counterclockwise
            if (rb.angularVelocity > maxTorque)
            {
                rb.angularVelocity = maxTorque; // Limit the maximum counterclockwise rotation speed
            }
        }
    }
    // Method to calculate the number of flips the player has performed
    private void CalculateFlips()
    {
        float currentRotation = transform.rotation.eulerAngles.z;
        totalRotation += Mathf.DeltaAngle(previousRotation, currentRotation);
        
        if (Mathf.Abs(totalRotation) >= 340f)
        {
            flipcount++;
            totalRotation = 0f;
            scoreManager.UpdateScore(flipcount*100);            
        }
        previousRotation = currentRotation;
    }
    
}

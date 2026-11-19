using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private float boostMultiplier = 20f;
    [SerializeField] private float baseSpeed = 15f;
    [Header("Score Manager")]
    [SerializeField] private ScoreManager scoreManager;
    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenuPanel;
    
    Rigidbody2D rb;
    SurfaceEffector2D surfaceEffector2D;
    InputAction moveAction;
    Vector2 moveValue;
    float maxTorque = 500f;
    int flipcount;
    float totalRotation;
    float previousRotation;
    private bool isPaused = false;
    void Start()
    {
        // Ensure the game is not paused when starting
        Time.timeScale = 1f;
        //Active the selected character based on the index stored in PlayerPrefs
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacter", 0);
        //Activate the selected character sprite based on the index
        transform.GetChild(0).GetChild(selectedCharacterIndex).gameObject.SetActive(true);

        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindAnyObjectByType<SurfaceEffector2D>();
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            TogglePause();
        }
        RotatePlayer();
        BoostPlayer();
        CalculateFlips();
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuPanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f; // Pause or resume the game
    }
    private void ResumeGame()
    {
        isPaused = false;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
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

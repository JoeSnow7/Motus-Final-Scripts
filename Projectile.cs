using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] Cannon cannon;
    [SerializeField] Solver solver;
    [SerializeField] SceneLoader sceneLoader;
    public TrailRenderer trail; 

    public int tries;

    internal void Start()
    {
        trail = FindObjectOfType<TrailRenderer>();
    }

    internal void SetVelocity(float xPush, float yPush)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // We dononly want to see the trajectory until we impact.
        trail.emitting = false;

        // Make sure it's not only the collision
        // but that they also input a close enough answer because many values
        // may cause the collision and we want "perfect" answers.
        string curr_scene = SceneManager.GetActiveScene().name;

        // User's response
        float response = 0f;
        // Actual correct answer
        var answer = solver.FindSolution();

        // This is because PM Angle will usually have 2 solutions
        float alt_answer = Mathf.Infinity;

        if (curr_scene == "PM Distance")
        {
            response = float.Parse(inputManager.distanceInput.text);
        }
        else if (curr_scene == "PM IV")
        {
            response = float.Parse(inputManager.ivInput.text);
        }
        else if (curr_scene == "PM Angle")
        {
            response = float.Parse(inputManager.angleInput.text);
            alt_answer = 90 - answer;
        }

        if ((Math.Abs(response - answer) < 0.01f) | (Math.Abs(response - alt_answer) < 0.01f))
        { 
            sceneLoader.LoadScene("Win Screen");
        }
        else
        {
            if (tries >= 3)
            {
                sceneLoader.LoadScene("Improve Accuracy");
            }
        }
        inputManager.Restart();
    }

    internal void LockBallToCannon()
    {
        Vector2 cannonPos = new Vector2(cannon.transform.position.x, cannon.transform.position.y);

        // This way, we're only moving one of the objects while the other one
        // sticks to it. 
        transform.position = cannonPos;
        SetVelocity(0f, 0f);
    }

    public void ShootOnButtonClick()
    {
        if (!inputManager.hasStarted)
        {
            FixInitialVelocity();
            inputManager.hasStarted = true;
            tries++;

            trail.emitting = true;
        }
    }

    public void FixInitialVelocity()
    {
        if (!inputManager.hasStarted)
        {
            // Retrieve User input 
            float iv = float.Parse(inputManager.ivInput.textComponent.text);

            // We get the angle from the object in case there's no input
            float shootAngle = cannon.transform.eulerAngles.z;

            // Get x and y components for acceleration
            float xPush = Mathf.Cos(shootAngle * Mathf.Deg2Rad) * iv;
            float yPush = Mathf.Sin(shootAngle * Mathf.Deg2Rad) * iv;

            SetVelocity(xPush, yPush);
        }
    }
}

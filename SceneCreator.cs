using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneCreator : MonoBehaviour
{

	// variables
    [SerializeField] float targetPos;
    [SerializeField] Solver solver;


    // modifiable by the user
    [SerializeField] InputManager inputManager;

    Target target;
    Projectile projectile;

    GameObject instructions;
    GameObject help;

    // Start is called before the first frame update.
    void Start()
    {
        SceneGen();
        instructions = GameObject.Find("Instructions");
        help = GameObject.Find("Help");
        help.SetActive(false);
    }

    // Update is called once per frame.
    void Update()
    {
        if (!inputManager.hasStarted)
        {
            projectile.LockBallToCannon();
        }
    }

    public void SceneGen()
    {
        inputManager.hasStarted = false;

        // Calculate measurements of the cam to position everything.
        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = cam.aspect * halfHeight;

        // Position the target.
        target = FindObjectOfType<Target>();
        float targetWidth = target.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        if (SceneManager.GetActiveScene().name =="PM Distance")
        {
            targetPos = Random.Range(halfWidth, halfWidth * 2 - targetWidth);
        }
        else
        {
            targetPos = halfWidth * 2 - targetWidth;
        }
        target.transform.position = new Vector2(targetPos, 0);

        // Find the projectile.
        projectile = FindObjectOfType<Projectile>();

        // Get new values for everything.
        inputManager.ResetValues();

        // Remake scene if it's not solvable.
        if (!solver.IsSolvable())
        {
            Debug.Log("Let's try again.");
            SceneGen();
        }
    }

    public void InstructionsOff()
    {
        instructions.SetActive(false);
    }

    public void ToggleHelp()
    {
        help.SetActive(!help.activeSelf);
    }
}
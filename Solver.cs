using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Solver : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] Target target;

    private void Start()
    {

    }


    // Calculate the solution for distance given the values generated.
    public float FindSolution()
    {
        string curr_scene = SceneManager.GetActiveScene().name;

        if (curr_scene == "PM Distance")
        {
            return FindSolutionDist();
        }
        else if (curr_scene == "PM IV")
        {
            return FindSolutionIV();
        }
        else if (curr_scene == "PM Angle")
        {
            return FindSolutionAngle();
        }
        else
        {
            return 0;
        }
    }


    public float FindSolutionDist()
    {
        float angle = inputManager.GetAngleDeg();
        float iv = float.Parse(inputManager.ivInput.text);

        // Get initial velocity in x and y axis
        float xpush = Mathf.Cos(angle * Mathf.Deg2Rad) * iv;
        float ypush = Mathf.Sin(angle * Mathf.Deg2Rad) * iv;


        float time = -ypush/Physics.gravity.y;
        float distance = xpush * 2 * time;
        Debug.Log("Solution: " + distance);
        return distance;
    }

    public float FindSolutionIV()
    {
        float angle = inputManager.GetAngleDeg();
        float distance = float.Parse(inputManager.distanceInput.text);

        float iv = Mathf.Sqrt((-distance * Physics.gravity.y) / (Mathf.Sin(angle * Mathf.Deg2Rad) * 2 * Mathf.Cos(angle * Mathf.Deg2Rad)));
        Debug.Log("Solution: " + iv);
        return iv;
    }

    public float FindSolutionAngle()
    {
        float iv = float.Parse(inputManager.ivInput.text);
        float distance = float.Parse(inputManager.distanceInput.text);

        float sin_angle = -distance * Physics.gravity.y / Mathf.Pow(iv, 2);
        float angle = Mathf.Asin(sin_angle) * Mathf.Rad2Deg / 2f;
        Debug.Log("Solution: " + angle);
        return angle;
    }

    public float FindMaxHeight()
    {
        string curr_scene = SceneManager.GetActiveScene().name;

        float iv;
        if (curr_scene == "PM IV")
        {
            iv = FindSolutionIV();
        }
        else
        {
            iv = float.Parse(inputManager.ivInput.text);
        }
        float angle;
        if (curr_scene == "PM Angle")
        {
            angle = FindSolutionAngle();
        }
        else
        {
            angle = inputManager.GetAngleDeg();
        }


        float height = Mathf.Pow(iv * Mathf.Sin(angle * Mathf.Deg2Rad), 2) / (-2 * Physics.gravity.y);
        //Debug.Log("Max Height" + height);
        return height;
    }

    // Determine if the cannon can actually be that far away from the target.
    public bool IsSolvable()
    {
        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = cam.aspect * halfHeight;

        string curr_scene = SceneManager.GetActiveScene().name;

        if (curr_scene == "PM Distance")
        {
            var distance = FindSolutionDist();

            if ((distance > (target.transform.position.x - halfWidth)) && (distance < (2 * halfWidth - target.transform.position.x-.25f)))
            {
                return true;
            }
        else
            {
                return false;
            }
        }
        else if (curr_scene == "PM IV")
        {
            var height = FindMaxHeight();
            // Here we check that the projectile doesn't have to leave the screen.
            if (height<halfHeight*2-2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (curr_scene == "PM Angle")
        {
            var angle = FindSolutionAngle();
            if (float.IsNaN(angle))
            {
                Debug.Log("Not a Number");
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            Debug.Log("This scene isn't supposed to be solved.");
            return false;
        }
    }
}

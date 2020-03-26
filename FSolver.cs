using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FSolver : MonoBehaviour
{
    FrictionIM fim;
    string curr_scene;

    private void Start()
    {

    }

    public float FindSolution()
    {
        fim = FindObjectOfType<FrictionIM>();
        curr_scene = SceneManager.GetActiveScene().name;

        if (curr_scene == "Friction")
        {
            return FindSolutionFriction();
        }
        else if (curr_scene == "FIV")
        {
            return FindSolutionFIV();
        }
        else if (curr_scene == "FDistance")
        {
            return FindSolutionFDistance();
        }
        else
        {
            return 0;
        }
    }

    public float FindSolutionFriction()
    {
        var gravity = Physics.gravity.y;
        
        float distance = float.Parse(fim.distanceInput.text);
        float iv = float.Parse(fim.ivInput.text);

        float acceleration = iv * iv / (2 * distance);
        float fcoeff = -acceleration / gravity;
        

        Debug.Log("Solution: " + fcoeff);
        return fcoeff;
    }

    public float FindSolutionFDistance()
    {
        var gravity = Physics.gravity.y;

        float fcoeff = float.Parse(fim.frictionInput.text);
        float iv = float.Parse(fim.ivInput.text);

        float acceleration = fcoeff * gravity;
        float distance = -iv * iv / (2 * acceleration);

        Debug.Log("Solution: " + distance);
        return distance;
    }

    public float FindSolutionFIV()
    {
        var gravity = Physics.gravity.y;

        float distance = float.Parse(fim.distanceInput.text);
        float fcoeff = float.Parse(fim.frictionInput.text);

        float acceleration = fcoeff * gravity;
        float iv = Mathf.Sqrt(Math.Abs(acceleration) * distance * 2.0f);

        Debug.Log("Solution: " + iv);
        return iv;
    }

    public bool IsSolvable()
    {
        var sol = FindSolution();
        if (curr_scene == "FDistance")
        {
            if (sol > 15.5f)
            {
                Debug.Log("Not solvable");
                return false;
            }
        }
        return true;
    }
}

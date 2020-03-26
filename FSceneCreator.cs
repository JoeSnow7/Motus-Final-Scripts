using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSceneCreator : MonoBehaviour
{

    FSolver fsolver;
    FrictionIM fim;
    Disk disk;

    Target target;
    
    GameObject instructions;
    GameObject help;

    // Start is called before the first frame update.
    void Start()
    {
        fim = FindObjectOfType<FrictionIM>();
        fsolver = FindObjectOfType<FSolver>();
        disk = FindObjectOfType<Disk>();
        target = FindObjectOfType<Target>();

        SceneGen();
        instructions = GameObject.Find("Instructions");
        help = GameObject.Find("Help");
        help.SetActive(false);
    }

    // Update is called once per frame.
    

    public void SceneGen()
    {
        
        // Get new values for everything.
        fim.ResetValues();

        // Report what the solution is.
        // fsolver.FindSolution();

        // Remake scene if it's not solvable.
        if (!fsolver.IsSolvable())
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

    public void StartOnButtonClick()
    {
        fim.hasStarted = true;
        disk.Reset();
        var iv = float.Parse(fim.ivInput.text);
        var friction = float.Parse(fim.frictionInput.text);

        disk.ChangeFriction(friction);
        disk.acceleration = Physics.gravity.y * float.Parse(fim.frictionInput.text);
        target.FixPosition();

        //Debug.Log(disk.acceleration);
        // speed comes last because this is where simulation starts
        disk.SetVelocity(iv, 0);
    }
}

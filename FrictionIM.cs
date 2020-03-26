using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class FrictionIM : MonoBehaviour
{
    [SerializeField] public InputField frictionInput;
    [SerializeField] public InputField distanceInput;
    [SerializeField] public InputField ivInput;
    [SerializeField] public Button startButton;

    Target target;

    public bool hasStarted = false;

    private void Start()
    {
        target = FindObjectOfType<Target>();
    }

    public void ResetValues()
    {
        var curr_scene = SceneManager.GetActiveScene().name;

        // Camera cam = Camera.main;
        // float halfHeight = cam.orthographicSize;
        // float halfWidth = cam.aspect * halfHeight;

        if (curr_scene == "Friction")
        {
            frictionInput.interactable = true;
            distanceInput.interactable = true;
            ivInput.interactable = true;

            distanceInput.text = Math.Round(UnityEngine.Random.Range(4f, 14f),2).ToString();
            distanceInput.interactable = false;
            target.FixPosition();


            ivInput.text = Math.Round(UnityEngine.Random.Range(5f, 14f),2).ToString();
            ivInput.interactable = false;
        }

        // Not created yet
        else if (curr_scene == "FDistance")
        {
            frictionInput.interactable = true;
            distanceInput.interactable = true;
            ivInput.interactable = true;

            frictionInput.text = Math.Round(UnityEngine.Random.Range(0f, 2f), 2).ToString();
            frictionInput.interactable = false;
            //target.FixPosition();


            ivInput.text = Math.Round(UnityEngine.Random.Range(5f, 14f), 2).ToString();
            ivInput.interactable = false;
        }

        // Not created yet
        else if (curr_scene == "FIV")
        {
            frictionInput.interactable = true;
            distanceInput.interactable = true;
            ivInput.interactable = true;

            frictionInput.text = Math.Round(UnityEngine.Random.Range(0f, 2f), 2).ToString();
            frictionInput.interactable = false;


            distanceInput.text = Math.Round(UnityEngine.Random.Range(4f, 14f), 2).ToString();
            distanceInput.interactable = false;
            target.FixPosition();
        }
    }
}

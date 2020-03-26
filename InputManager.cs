using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [SerializeField] public InputField angleInput;
    [SerializeField] public InputField distanceInput;
    [SerializeField] public InputField ivInput;
    [SerializeField] public Button shootButton;
    public Projectile projectile;
    public bool hasStarted = false;

    string curr_scene;

    public void Start()
    {
        projectile = FindObjectOfType<Projectile>();
    }

    public void ResetValues()
    {
        curr_scene = SceneManager.GetActiveScene().name;
        Cannon cannon = FindObjectOfType<Cannon>();

        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = cam.aspect * halfHeight;

        if (curr_scene == "PM Distance")
        {
            angleInput.interactable = true;
            ivInput.interactable = true;

            angleInput.text = GenAngle(10,80);
            angleInput.interactable = false;
            cannon.FixAngle();

            ivInput.text = Random.Range(5, 14).ToString();
            ivInput.interactable = false;
        }

        else if (curr_scene == "PM IV")
        {
            distanceInput.interactable = true;
            angleInput.interactable = true;

            angleInput.text = GenAngle(10,80);
            angleInput.interactable = false;
            cannon.FixAngle();

            distanceInput.text = Random.Range(halfWidth-0.64f, halfWidth*2-1f).ToString();
            distanceInput.interactable = false;
            cannon.FixPosition();
        }

        else if (curr_scene == "PM Angle")
        {
            distanceInput.interactable = true;
            ivInput.interactable = true;

            // The -.3f part is so that the whole projectile stays in. 
            // Should make a reference to the projectile's width.
            distanceInput.text = Random.Range((halfWidth - 0.64f), (halfWidth * 2-1f)).ToString();
            distanceInput.interactable = false;
            cannon.FixPosition();

            ivInput.text = Random.Range(5, 14).ToString();
            ivInput.interactable = false;
        }
    }

    public void Restart()
    {
        projectile.trail.emitting = false;
        hasStarted = false;
    }

    public float GetAngleDeg()
    {
        var angle = float.Parse(angleInput.text);
        if (SceneLoader.radians == true)
        {
            return angle * Mathf.Rad2Deg;
        }
        else
        {
            return angle;
        }
    }

    private string GenAngle(float minimum, float maximum)
    {
        var angle = Random.Range(minimum, maximum).ToString();

        if (SceneLoader.radians == true)
        {
            return (float.Parse(angle)*Mathf.Deg2Rad).ToString();
        }

        else
        {
            return angle;
        }
    }
}
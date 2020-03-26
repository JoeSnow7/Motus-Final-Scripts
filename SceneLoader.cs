using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    // This will keep track of the previous scene
    [SerializeField] static List<string> previousScene = new List<string>();
    public static bool radians = false;
    Toggle radianToggle;

    private void Awake()
    {
        // Screen.SetResolution(2880, 1800, false, 60);
    }

    private void Start()
    {

        // This is to make sure the toggle stays consistent with the radians.
        radianToggle = FindObjectOfType<Toggle>();
        if (radianToggle)
        {
            var radian_holder = radians;
            radianToggle.isOn = radians;
            radians = radian_holder;
            Debug.Log(radians);
        }
    }

    public void RadianChange()
    {
        radians = !radians;
        Debug.Log(radians);
    }

    
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(previousScene[previousScene.Count-1]);
    }

    public void LoadStartScene()
    {
        
        SceneManager.LoadScene(0);
    }

    public void LoadPMDist()
    {
        LoadScene("PM Distance");
    }

    public void LoadPMAngle()
    {
        
        LoadScene("PM Angle");
    }

    public void LoadPMIV()
    {
        
        LoadScene("PM IV");
    }

    public void LoadPMChooseExercise()
    {
        
        LoadScene("PM Choose Exercise");
    }

    public void LoadLosingScreen()
    {
        
        LoadScene("Missed Screen");
    }

    public void LoadFrictionMenu()
    {
        LoadScene("F Choose Exercise");
    }

    public void LoadFriction()
    {
        LoadScene("Friction");
    }

    public void LoadFDistance()
    {
        LoadScene("FDistance");
    }

    public void LoadFIV()
    {
        LoadScene("FIV");
    }

    public void LoadScene(string scene_name)
    {
        RecordScene();
        SceneManager.LoadScene(scene_name);
    }

    private static void RecordScene()
    {
        previousScene.Add(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Disk : MonoBehaviour
{
    //bool hasStarted = false;
    FrictionIM fim;
    FSolver fsolver;
    Target target;
    float velocity;
    SceneLoader sceneLoader;

    public float timeLastFrame;
    public float acceleration;


    // Start is called before the first frame update
    void Start()
    {
        timeLastFrame = Time.realtimeSinceStartup;

        GetComponent<Collider2D>().sharedMaterial.friction = 0.4f;
        fim = FindObjectOfType<FrictionIM>();
        target = FindObjectOfType<Target>();
        sceneLoader = FindObjectOfType<SceneLoader>();
        fsolver = FindObjectOfType<FSolver>();
    }

    // Update is called once per frame
    void Update()
    {
        float realDeltaTime = Time.realtimeSinceStartup - timeLastFrame;
        float startTime = realDeltaTime;
        timeLastFrame = Time.realtimeSinceStartup;

        var deltaVelocity = acceleration * realDeltaTime;

        // The absolute value is so that friction can apply both directions
        // We update position and velocity only if the velocity is larger
        // than the change. This way, the object won't move backwards.
        if (Math.Abs(velocity) > Math.Abs(deltaVelocity))
        {
            SetPosition(transform.position.x + (velocity+acceleration*startTime) * realDeltaTime, 0.75f);
            velocity = velocity + deltaVelocity;
        }
        else
        {
            // If force of friction would make it move backwards
            // the force would've stopped acting and movement would stop. 
            acceleration = 0;
            velocity = 0;
        }

        if (fim.hasStarted == true & velocity==0)
        {
            StartCoroutine(ShowResults());
            // To stop the coroutine from running every frame
            fim.hasStarted = false;
        }
    }

    // This coroutine checks once the disk stops moving
    // the results and determines whether the problem was solved successfully.
    IEnumerator ShowResults()
    {
        var solution = fsolver.FindSolution();
        float response= 0f;
        string curr_scene = SceneManager.GetActiveScene().name;

        if (curr_scene == "FDistance")
        {
            response = float.Parse(fim.distanceInput.text);
        }
        else if (curr_scene == "FIV")
        {
            response = float.Parse(fim.ivInput.text);
        }
        else if (curr_scene == "Friction")
        {
            response = float.Parse(fim.frictionInput.text);
        }

        // We wait 2 seconds so user can see it stopped.
        yield return new WaitForSeconds(1);

        var wideTarget = GameObject.Find("Target/Outer Circle");
        float targetWidth = wideTarget.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        var diskPos = transform.position.x;
        var targetPos = target.transform.position.x;

        Debug.Log((Math.Abs(response - solution)));
        if ((Math.Abs(response - solution) < 0.01f))
        {
            sceneLoader.LoadScene("Win Screen");
        }

        else if (targetPos - targetWidth / 2 < diskPos & targetPos + targetWidth / 2> diskPos) 
        {
            sceneLoader.LoadScene("Improve Accuracy");
        }
        else
        {
            sceneLoader.LoadScene("Missed Screen");
        }
    }

    internal void SetVelocity(float xPush, float yPush)
    {
        //GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
        velocity = xPush;
    }

    internal void SetPosition(float x, float y)
    {
        transform.position = new Vector3(x, y, 0);
    }

    public void ChangeFriction(float friction)
    {
        var myCollider = GetComponent<Collider2D>();

        myCollider.sharedMaterial.friction = friction;

        // for some reason, the collider needs to be disabled before
        // friction is noticed in it. 
        myCollider.enabled = false;
        myCollider.enabled = true;
    }

    public void Reset()
    {
        transform.rotation= new Quaternion(0f, 0f, 0f,0f);
        SetPosition(0, .75f);
        SetVelocity(0, 0); //121.644f
    }

    // public void StartOnButtonClick()
    // {
    //     Reset();
    //     var iv = float.Parse(fim.ivInput.text);
    //     var friction = float.Parse(fim.frictionInput.text);
    // 
    //     ChangeFriction(friction);
    //     // target.FixPosition();
    // 
    //     // speed comes last because this is where simulation starts
    //     SetVelocity(iv, 0); 
    // }
}

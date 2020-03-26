using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour

{
    [SerializeField] internal Target target;

    [SerializeField] InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
		transform.position = new Vector2(1, 1.28f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixPosition()
    {
        if (!inputManager.hasStarted)
        {
            Camera cam = Camera.main;
            float halfHeight = cam.orthographicSize;
            float halfWidth = cam.aspect * halfHeight;

            // Cannon must be on first half of screen. That's why we use clamp
            float distanceInput = float.Parse(inputManager.distanceInput.textComponent.text);
            transform.position = new Vector2(Mathf.Clamp(target.transform.position.x - distanceInput, .25f, halfWidth), 1.28f);
        }
    }

    public void FixAngle()
    {
        if (!inputManager.hasStarted)
        {
            transform.eulerAngles = new Vector3(0.0f, 0.0f, inputManager.GetAngleDeg());
        }
    }

    public void FixIV()
    {

    }
}
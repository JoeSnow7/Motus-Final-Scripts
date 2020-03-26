using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    FrictionIM fim;

    // Start is called before the first frame update
    void Start()
    {
        fim = FindObjectOfType<FrictionIM>();
    }


    internal void SetPosition(float x, float y)
    {
        transform.position = new Vector3(x, y, 0);
    }

    public void FixPosition()
    {
        var distance = float.Parse(fim.distanceInput.text);
        SetPosition(.25f+distance, 0.5f);
    }
}

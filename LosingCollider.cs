using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LosingCollider : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    Projectile projectile;
    InputManager inputManager;

    private void Start()
    {
        projectile = FindObjectOfType<Projectile>();
        inputManager = FindObjectOfType<InputManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inputManager.Restart();

        if (projectile.tries >= 3)
        {
            sceneLoader.LoadLosingScreen();
        }
    }
}

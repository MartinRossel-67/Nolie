using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background;


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void UpdateBackground(Sprite backgroundSprite)
    {
        background.sprite = backgroundSprite;
    }
}

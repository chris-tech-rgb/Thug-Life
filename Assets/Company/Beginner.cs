using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml.Serialization;

public class Beginner : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator transition;

    public float transitionTime = 2f;

    void Awake()
    {
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Start()
    {
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}

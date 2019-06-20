using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToGame : MonoBehaviour {

    // Use this for initialization
    private void Start()
    {
        StartCoroutine(WaitAndLoad(6f, "1"));
    }

    private IEnumerator WaitAndLoad(float value, string scene)
    {
        yield return new WaitForSeconds(value);
        SceneManager.LoadScene(scene);
    }

    // Update is called once per frame
    void Update () {
		
	}
}

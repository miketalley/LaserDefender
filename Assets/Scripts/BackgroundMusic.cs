using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {
    public static BackgroundMusic instance = null;

    void Awake () {
        if (instance == null)
        {
            Instantiate();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Instantiate()
    {
        instance = this;
        GameObject.DontDestroyOnLoad(gameObject);
    }
}

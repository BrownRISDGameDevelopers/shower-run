using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }

    public float TimeElapsed { get; private set; }

    [Header("Sanity")]
    [SerializeField] float maxSanity;
    public float Sanity { get; private set; }

    [Header("Player")]
    [SerializeField] string playerGameObjectString;

    public GameObject Player { get; private set; }

    public Quaternion? HideSpotRotation { get; set; } = null;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance is not null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Player = GameObject.Find(playerGameObjectString).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        TimeElapsed += Time.deltaTime;
    }

    public void ChangeSanity(float value)
    {
        Sanity += value;
    }

    public void SetSanity(float value)
    {
        Sanity = value;
    }

    public void ResetSanity()
    {
        Sanity = 0f;
    }

    public void SetMaxSanity()
    {
        Sanity = maxSanity;
    }
}
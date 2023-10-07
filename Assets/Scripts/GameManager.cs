using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public float TimeElapsed { get; private set; }

    [Header("Sanity")]
    [SerializeField] float maxSanity;
    public float Sanity { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
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

    public void ResetSanity()
    {
        Sanity = 0f;
    }

    public void MaxSanity()
    {
        Sanity = maxSanity;
    }
}
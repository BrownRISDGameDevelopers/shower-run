using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class InteractableObjectGlow : MonoBehaviour
{
    [SerializeField] private List<GameObject> renderObj;
    
    private int _glow;
    private int _noGlow;

    private void Start()
    {
        _glow = LayerMask.NameToLayer("Glow");
        _noGlow = LayerMask.NameToLayer("NoGlow");
    }

    void Update()
    {
        if (PlayerIsClose())
        {
            foreach (var o in renderObj)
            {
                o.layer = _glow;
            }
        }
        else
        {
            foreach (var o in renderObj)
            {
                o.layer = _noGlow;
            }
        }
    }

    private bool PlayerIsClose()
    {
        return Vector3.Distance(gameObject.transform.position, GameManager.Instance.Player.transform.position) < 2.5f;
    }
}

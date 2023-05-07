using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float timeCircle;
    private void Awake()
    {
        Invoke(nameof(DestroyGameObject), timeCircle);
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}

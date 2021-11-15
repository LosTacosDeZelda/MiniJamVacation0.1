using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyAfter : MonoBehaviour
{
    public float secondsUntilDestruct;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", secondsUntilDestruct);
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}

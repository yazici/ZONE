using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityVisability : MonoBehaviour
{
    public Transform inProxTo;
    public float maxDistance;
    public bool visableWithinDistance;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = !visableWithinDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, inProxTo.position) < maxDistance)
        {
           
            meshRenderer.enabled = visableWithinDistance;
        }
       

    }
}

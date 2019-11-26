using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitOnAltitude : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -4.48){
            Application.Quit();
        }
    }
}

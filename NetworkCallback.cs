using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour]
public class NetworkCallback : Bolt.GlobalEventListener
{

    public override void SceneLoadLocalDone(string scene)
    {
       GameObject gameObject = BoltNetwork.Instantiate(BoltPrefabs.Player, new Vector3(0, 10, 0), Quaternion.identity);
     

    }
  
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCooldown : MonoBehaviour
{

    //Refference for t https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/

    public bool onCooldown = false;
    public Renderer cube;
    float timeElapsed;
    float lerpDuration = 10.0f;
    
    void Start() 
    {
        cube = this.gameObject.GetComponent<Renderer>();
    }

 
    void Update()
    { 
        if(onCooldown)
        {
            if(timeElapsed <= lerpDuration - 1)
            {
                cube.material.color = Color.Lerp(Color.red, Color.white, timeElapsed/lerpDuration);
                timeElapsed += Time.deltaTime;
            }
            else
            {
                timeElapsed = 0;
                cube.material.color = Color.white;
                onCooldown = false;
            }
        }
    }
}

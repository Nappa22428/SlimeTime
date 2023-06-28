using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeMove : MonoBehaviour
{
    public bool flip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flip)
        {
            this.transform.position = new Vector3((this.transform.position.x - 2), (this.transform.position.y), 0);
            //Debug.Log(transform.position.x);
            if (this.transform.position.x <= 190)
            {
               
                flip = false;
            }
        }
        if (!flip)
        {
            this.transform.position = new Vector3((this.transform.position.x + 2), (this.transform.position.y), 0);
            //Debug.Log(transform.position.x);
            if (this.transform.position.x >= 730)
            {
                //Debug.Log(transform.position.x);
                flip = true;
            }
        }
       
       // Debug.Log(transform.position.x);

    }
}

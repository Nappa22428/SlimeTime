using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public GameObject slimeGO;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fMouse();
    }

    void fMouse()
    {
        Vector3 mP = Input.mousePosition;
        mP = Camera.main.ScreenToWorldPoint(mP);

        Vector3 direction = new Vector3(mP.x - transform.position.x, mP.y - transform.position.y, 0);

        slimeGO.transform.up = direction;
        //transform.up = direction;


    }
}

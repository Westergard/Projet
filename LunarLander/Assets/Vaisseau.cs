using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vaisseau : MonoBehaviour
{
    private SpriteRenderer rend;
    public Sprite VAF, VSF;
    public Text altitude;

    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float alt = (gameObject.transform.position.y + 5) * 10;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            vaisseauAvecFeu();
            

        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            vaisseauSansFeu();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(gameObject.transform.rotation.z < 90)
            {
                transform.Rotate(new Vector3(0, 0, (float)0.2));   
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (gameObject.transform.rotation.z > -90)
            {
                transform.Rotate(new Vector3(0, 0, (float)-0.2));
            }
        }

        altitude.text = alt.ToString();
    }

    void vaisseauAvecFeu()
    {
        rend.sprite = VAF;
    }
    void vaisseauSansFeu()
    {
        rend.sprite = VSF;
    }
}

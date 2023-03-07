using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowDeliveryScript : MonoBehaviour
{
    public bool accessible = false;
    public Rigidbody2D player;
    public Rigidbody2D package;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && accessible)
        {
            deliverPackage();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        accessible = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        accessible = false;
    }

    private void deliverPackage()
    {
        float time = 2f;
        float v_x = ((transform.position.x - player.transform.position.x) / time) - player.velocity.x;
        float v_y = ((transform.position.y - (player.transform.position.y + (0.5f * time * 3.72076f))) / time) - player.velocity.y;

        Rigidbody2D packageInstance = Instantiate(package, player.transform.position, player.transform.rotation);
        packageInstance.velocity = new Vector2(v_x, v_y);
    }
}
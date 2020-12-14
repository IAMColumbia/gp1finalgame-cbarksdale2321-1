using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
   [SerializeField] private float cameraSpeed = 0;
    private float xMaxLimit;
    private float yMinLimit;
    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, xMaxLimit), Mathf.Clamp(transform.position.y, yMinLimit, 0 ), -10);
        
    }
    public void SetBounds(Vector3 maxTile)
    {
        Vector3 b = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));

        xMaxLimit = maxTile.x - b.x;
        yMinLimit = maxTile.y - b.y;
    }
}

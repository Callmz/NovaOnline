using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public GameObject destinyHud;
    public Texture2D cursorUpTexture;
    public Texture2D cursorDownTexture;
    public Texture2D cursorGrabTexture;
    public GameObject player;
    public List<int> rayIgnore;

    private Vector2 cursorHotSpot;

    private void Awake()
    {
        cursorHotSpot = new Vector2(0, 0);
        Cursor.SetCursor(cursorUpTexture, cursorHotSpot, CursorMode.Auto);
        destinyHud.transform.position = player.transform.position;
        Physics.IgnoreLayerCollision(3, 9);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(cursorDownTexture, cursorHotSpot, CursorMode.Auto);
        }
        else if (Input.GetMouseButton(1))
        {
            Cursor.SetCursor(cursorGrabTexture, cursorHotSpot, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(cursorUpTexture, cursorHotSpot, CursorMode.Auto);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycast))
        {
            GameObject obj = raycast.collider.gameObject;
            transform.position = raycast.point;
            
            if (Input.GetMouseButton(0) && !(Input.GetMouseButton(1)|| Input.GetMouseButton(2)))
            {
                if (!rayIgnore.Contains(obj.layer))
                {
                    destinyHud.SetActive(true);
                    destinyHud.transform.position = raycast.point;
                    destinyHud.transform.Translate(Vector3.forward * -0.2f);
                    destinyHud.GetComponent<Animator>().SetBool("IsMoving", true);
                }
            }
            else
            {
                destinyHud.GetComponent<Animator>().SetBool("IsMoving", false);
            }
           
        }
    }
}

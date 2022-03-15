using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Node firstNode;
    [SerializeField]
    private Node secondNode;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                Node nodeHit = hit.collider.GetComponent<Node>();
                firstNode = GameManagerScript.instance.GetNode(nodeHit.positionX, nodeHit.positionY);
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (firstNode != null)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null)
                {
                    Node nodeHit = hit.collider.GetComponent<Node>();
                    secondNode = GameManagerScript.instance.GetNode(nodeHit.positionX, nodeHit.positionY);
                    GameManagerScript.instance.CheckLink(firstNode, secondNode);
                }
            }

            ResetNodes();
        }
    }

    private void ResetNodes()
    {
        firstNode = null;
        secondNode = null;
    }
}

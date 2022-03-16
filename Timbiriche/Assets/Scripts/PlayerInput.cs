using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;

    [SerializeField]
    private Node firstNode;
    [SerializeField]
    private Node secondNode;

    private bool canPlay = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    void Update()
    {
        if (canPlay)
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
    }

    public bool ChangeTurn()
    {
        if (canPlay)
        {
            canPlay = false;
        }
        else
        {
            canPlay = true;
        }

        return canPlay;
    }

    private void ResetNodes()
    {
        firstNode = null;
        secondNode = null;
    }
}

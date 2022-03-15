using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    [SerializeField]
    private int gridSizeX;
    [SerializeField]
    private int gridSizeY;
    [SerializeField]
    private GameObject gridParent;
    [SerializeField]
    private GameObject linesParent;
    [SerializeField]
    private Material lineMaterial;
    [SerializeField]
    private Gradient unclaimedColor;
    [SerializeField]
    private Gradient playerColor;
    [SerializeField]
    private Gradient AIColor;
    [SerializeField]
    private GameObject nodePrefab;

    private Node[,] grid;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    void Start()
    {
        GridSetup();
        LineSetup();
    }

    private void GridSetup()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Transform nodeAnchor = gridParent.transform.GetChild(0);
        GameObject currentNode;

        for (int i = 0; i < gridSizeY; i++)
        {
            for (int j = 0; j < gridSizeX; j++)
            {
                currentNode = Instantiate(nodePrefab, gridParent.transform);
                currentNode.transform.position = new Vector3(nodeAnchor.position.x + (j * 0.6f), nodeAnchor.position.y - (i * 0.6f), 0);
                currentNode.GetComponent<Node>().positionX = j;
                currentNode.GetComponent<Node>().positionY = i;
                grid[j, i] = currentNode.GetComponent<Node>();
            }
        }

        NodeSetup();
    }

    private void NodeSetup()
    {
        grid[0, 0].GetComponent<Node>().SouthNode = grid[0, 1];
        grid[0, 0].GetComponent<Node>().EastNode = grid[1, 0];

        grid[gridSizeX - 1, 0].GetComponent<Node>().SouthNode = grid[gridSizeX - 1, 1];
        grid[gridSizeX - 1, 0].GetComponent<Node>().WestNode = grid[gridSizeX - 2, 0];

        grid[0, gridSizeY - 1].GetComponent<Node>().NorthNode = grid[0, gridSizeY - 2];
        grid[0, gridSizeY - 1].GetComponent<Node>().EastNode = grid[1, gridSizeY - 1];

        grid[gridSizeX - 1, gridSizeY - 1].GetComponent<Node>().NorthNode = grid[gridSizeX - 1, gridSizeY - 2];
        grid[gridSizeX - 1, gridSizeY - 1].GetComponent<Node>().WestNode = grid[gridSizeX - 2, gridSizeY - 1];

        for (int i = 1; i < gridSizeX - 1; i++)
        {
            grid[i, 0].GetComponent<Node>().WestNode = grid[i - 1, 0];
            grid[i, 0].GetComponent<Node>().SouthNode = grid[i, 1];
            grid[i, 0].GetComponent<Node>().EastNode = grid[i + 1, 0];

            grid[i, gridSizeY - 1].GetComponent<Node>().WestNode = grid[i - 1, gridSizeY - 1];
            grid[i, gridSizeY - 1].GetComponent<Node>().NorthNode = grid[i, gridSizeY - 2];
            grid[i, gridSizeY - 1].GetComponent<Node>().EastNode = grid[i + 1, gridSizeY - 1];
        }

        for (int i = 1; i < gridSizeY - 1; i++)
        {
            grid[0, i].GetComponent<Node>().NorthNode = grid[0, i - 1];
            grid[0, i].GetComponent<Node>().SouthNode = grid[0, i + 1];
            grid[0, i].GetComponent<Node>().EastNode = grid[1, i];

            grid[gridSizeX - 1, i].GetComponent<Node>().NorthNode = grid[gridSizeX - 1, i - 1];
            grid[gridSizeX - 1, i].GetComponent<Node>().SouthNode = grid[gridSizeX - 1, i + 1];
            grid[gridSizeX - 1, i].GetComponent<Node>().WestNode = grid[gridSizeX - 2, i];
        }

        for (int i = 1; i < gridSizeY - 1; i++)
        {
            for (int j = 1; j < gridSizeX - 1; j++)
            {
                grid[j, i].GetComponent<Node>().NorthNode = grid[j, i - 1]; 
                grid[j, i].GetComponent<Node>().SouthNode = grid[j, i + 1]; 
                grid[j, i].GetComponent<Node>().EastNode = grid[j + 1, i]; 
                grid[j, i].GetComponent<Node>().WestNode = grid[j - 1, i]; 
            }
        }

        for (int i = 0; i < gridSizeY; i++)
        {
            for (int j = 0; j < gridSizeX; j++)
            {
                grid[j, i].GetComponent<Node>().SetNodeLinks();
            }
        }
    }

    private void LineSetup()
    {
        for (int i = 0;i < gridSizeX; i++)
        {
            for (var j = 0;j < gridSizeY; j++)
            {
                if (grid[j, i].SouthNode != null)
                {
                    GameObject newLine = new GameObject("Line");
                    newLine.transform.parent = linesParent.transform;
                    newLine.AddComponent<LineRenderer>();
                    newLine.GetComponent<LineRenderer>().SetPosition(0, grid[j, i].transform.position);
                    newLine.GetComponent<LineRenderer>().SetPosition(1, grid[j, i].SouthNode.transform.position);
                    newLine.GetComponent<LineRenderer>().startWidth = .1f;
                    newLine.GetComponent<LineRenderer>().endWidth = .1f;
                    newLine.GetComponent<LineRenderer>().material = lineMaterial;
                    newLine.GetComponent<LineRenderer>().colorGradient = unclaimedColor;
                    newLine.GetComponent<LineRenderer>().sortingOrder = -1;
                    grid[j, i].southLine = newLine;
                    grid[j, i].SouthNode.northLine = newLine;
                }

                if (grid[j, i].EastNode != null)
                {
                    GameObject newLine = new GameObject("Line");
                    newLine.transform.parent = linesParent.transform;
                    newLine.AddComponent<LineRenderer>();
                    newLine.GetComponent<LineRenderer>().SetPosition(0, grid[j, i].transform.position);
                    newLine.GetComponent<LineRenderer>().SetPosition(1, grid[j, i].EastNode.transform.position);
                    newLine.GetComponent<LineRenderer>().startWidth = .1f;
                    newLine.GetComponent<LineRenderer>().endWidth = .1f;
                    newLine.GetComponent<LineRenderer>().material = lineMaterial;
                    newLine.GetComponent<LineRenderer>().colorGradient = unclaimedColor;
                    newLine.GetComponent<LineRenderer>().sortingOrder = -1;
                    grid[j, i].eastLine = newLine;
                    grid[j, i].EastNode.westLine = newLine;
                }
            }
        }
    }

    public Node GetNode(int positionX, int positionY)
    {
        return grid[positionX, positionY];
    }

    public void CheckLink(Node firstNode, Node secondNode)
    {
        if (firstNode != secondNode)
        {
            if (firstNode.NorthNode == secondNode)
            {
                if (firstNode.nodeLinks[secondNode] == 0)
                {
                    Debug.Log("Link");
                    firstNode.LockNode(secondNode);
                    secondNode.LockNode(firstNode);
                    firstNode.northLine.GetComponent<LineRenderer>().colorGradient = playerColor;
                }
            }
            else if(firstNode.SouthNode == secondNode)
            {
                if (firstNode.nodeLinks[secondNode] == 0)
                {
                    Debug.Log("Link");
                    firstNode.LockNode(secondNode);
                    secondNode.LockNode(firstNode);
                    firstNode.southLine.GetComponent<LineRenderer>().colorGradient = playerColor;
                }
            }
            else if (firstNode.EastNode == secondNode)
            {
                if (firstNode.nodeLinks[secondNode] == 0)
                {
                    Debug.Log("Link");
                    firstNode.LockNode(secondNode);
                    secondNode.LockNode(firstNode);
                    firstNode.eastLine.GetComponent<LineRenderer>().colorGradient = playerColor;
                }
            }
            else if (firstNode.WestNode == secondNode)
            {
                if (firstNode.nodeLinks[secondNode] == 0)
                {
                    Debug.Log("Link");
                    firstNode.LockNode(secondNode);
                    secondNode.LockNode(firstNode);
                    firstNode.westLine.GetComponent<LineRenderer>().colorGradient = playerColor;
                }
            }
        }
    }
}

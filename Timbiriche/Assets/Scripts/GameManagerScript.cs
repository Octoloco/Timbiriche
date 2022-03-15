using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    private int gridSizeX;
    [SerializeField]
    private int gridSizeY;
    [SerializeField]
    private GameObject gridParent;
    [SerializeField]
    private GameObject nodePrefab;

    private GameObject[,] grid;

    void Start()
    {
        GridSetup();
    }

    void Update()
    {
        
    }

    private void GridSetup()
    {
        grid = new GameObject[gridSizeX, gridSizeY];
        Transform nodeAnchor = gridParent.transform.GetChild(0);
        GameObject currentNode;

        for (int i = 0; i < gridSizeY; i++)
        {
            for (int j = 0; j < gridSizeX; j++)
            {
                currentNode = Instantiate(nodePrefab, gridParent.transform);
                currentNode.transform.position = new Vector3(nodeAnchor.position.x + (j * 0.6f), nodeAnchor.position.y - (i * 0.6f), 0);
                grid[j, i] = currentNode;
            }
        }

        NodeSetup();
    }

    private void NodeSetup()
    {
        grid[0, 0].GetComponent<Node>().SouthNode = grid[0, 1].transform;
        grid[0, 0].GetComponent<Node>().EastNode = grid[1, 0].transform;

        grid[gridSizeX - 1, 0].GetComponent<Node>().SouthNode = grid[gridSizeX - 1, 1].transform;
        grid[gridSizeX - 1, 0].GetComponent<Node>().WestNode = grid[gridSizeX - 2, 0].transform;

        grid[0, gridSizeY - 1].GetComponent<Node>().NorthNode = grid[0, gridSizeY - 2].transform;
        grid[0, gridSizeY - 1].GetComponent<Node>().EastNode = grid[1, gridSizeY - 1].transform;

        grid[gridSizeX - 1, gridSizeY - 1].GetComponent<Node>().NorthNode = grid[gridSizeX - 1, gridSizeY - 2].transform;
        grid[gridSizeX - 1, gridSizeY - 1].GetComponent<Node>().WestNode = grid[gridSizeX - 2, gridSizeY - 1].transform;

        for (int i = 1; i < gridSizeX - 1; i++)
        {
            grid[i, 0].GetComponent<Node>().WestNode = grid[i - 1, 0].transform;
            grid[i, 0].GetComponent<Node>().SouthNode = grid[i, 1].transform;
            grid[i, 0].GetComponent<Node>().EastNode = grid[i + 1, 0].transform;

            grid[i, gridSizeY - 1].GetComponent<Node>().WestNode = grid[i - 1, gridSizeY - 1].transform;
            grid[i, gridSizeY - 1].GetComponent<Node>().NorthNode = grid[i, gridSizeY - 2].transform;
            grid[i, gridSizeY - 1].GetComponent<Node>().EastNode = grid[i + 1, gridSizeY - 1].transform;
        }

        for (int i = 1; i < gridSizeY - 1; i++)
        {
            grid[0, i].GetComponent<Node>().NorthNode = grid[0, i - 1].transform;
            grid[0, i].GetComponent<Node>().SouthNode = grid[0, i + 1].transform;
            grid[0, i].GetComponent<Node>().EastNode = grid[1, i].transform;

            grid[gridSizeX - 1, i].GetComponent<Node>().NorthNode = grid[gridSizeX - 1, i - 1].transform;
            grid[gridSizeX - 1, i].GetComponent<Node>().SouthNode = grid[gridSizeX - 1, i + 1].transform;
            grid[gridSizeX - 1, i].GetComponent<Node>().WestNode = grid[gridSizeX - 2, i].transform;
        }

        for (int i = 1; i < gridSizeY - 1; i++)
        {
            for (int j = 1; j < gridSizeX - 1; j++)
            {
                grid[j, i].GetComponent<Node>().NorthNode = grid[j, i - 1].transform; 
                grid[j, i].GetComponent<Node>().SouthNode = grid[j, i + 1].transform; 
                grid[j, i].GetComponent<Node>().EastNode = grid[j + 1, i].transform; 
                grid[j, i].GetComponent<Node>().WestNode = grid[j - 1, i].transform; 
            }
        }
    }
}

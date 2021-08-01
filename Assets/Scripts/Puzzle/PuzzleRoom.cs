using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoom : MonoBehaviour
{
    Element[] elements = new Element[5];
    int[,] board = new int[4,5];
    public Dictionary<int, Element> listElements = new Dictionary<int, Element>();
    List<int> password = new List<int>();
    [SerializeField] List<Material> materials = new List<Material>(); //0 IS FIRE, 1 IS TREE, 2 IS MOUNTAIN
    public Material material1;
    public enum Element
    {
        water,
        mountain,
        tree,
    }

    private void Start()
    {
        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                board[row, col] = 0;
            }
        }
        listElements.Add(0,Element.water);
        listElements.Add(1,Element.mountain);
        listElements.Add(2,Element.tree);
        AssignElements();
    }

    void AssignElements()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            if (listElements.TryGetValue(Random.Range(0, elements.Length), out Element element1))
            {
                elements[i] = element1;
            }
        }
        DisplayElements();
        GetPoints();
    }

    void DisplayElements()
    {
        foreach(Element element in elements)
        {

        }
    }

    void GetPoints()
    {
        for (int col = 0; col < board.GetLength(1); col++)
        {
            Element currentElement = elements[col];
            Debug.Log(elements[col]);
            switch(currentElement) 
            {
                case Element.water:
                    Water(col);
                    break;
                case Element.mountain:
                    Mountain(col);
                    break;
                case Element.tree:
                    Tree(col);
                    break;
            }
        }
        CheckForIntersect();
    }

    void Water(int col) 
    { 
        for (int row = 0; row < board.GetLength(0); row++)
        {
            board[row, col]++;
        }
    }

    void Mountain(int col) 
    {
        int offset = 0;
        for (int row = 0; row < board.GetLength(0); row++)
        {
            if (offset == 0)
            {
                board[row, col]++;
            }
            else
            {
                if (col - offset < board.GetUpperBound(0)) board[row, col - offset]++;
                if (col + offset < board.GetUpperBound(0)) board[row, col + offset]++;
            }
            offset++;
        }
    }

    void Tree(int col)
    {
        int offset = 0;
        for (int row = 0; row < 3; row++)
        {
            board[row , col]++;
        }
        do
        {
            if (col - offset < board.GetLowerBound(0)) { offset = 0; break; }
            else
            {
                board[3, col - offset]++;
            }
            offset++;
        }
        while (true);
        do
        {
            if (col + offset < board.GetUpperBound(0)) break; 
            else
            {
                board[3, col + offset]++;
            }
            offset++;
        }
        while (true);
    }

    void CheckForIntersect()
    {
        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
               if (board[row,col] == 2)
                {
                    AddToPassword(col + (4 * row)); // i am ordering the upperleft corner is 0 on the board and downright corner is 
                }
            }
        }
        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                Debug.Log(board[row, col]);
            }
        }
    }

    void AddToPassword(int num)
    {
        password.Add(num);
        foreach(int num1 in password)
        {
            Debug.Log(num1);
        }
    }
}

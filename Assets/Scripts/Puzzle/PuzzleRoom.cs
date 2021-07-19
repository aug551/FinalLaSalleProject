using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRoom : MonoBehaviour
{
    Element[] elements = new Element[4];
    int[,] board = new int[4,5];
    public Dictionary<int, Element> listElements = new Dictionary<int, Element>();
    List<int> password = new List<int>();
    public enum Element
    {
        water,
        mountain,
        tree,
    }

    private void Start()
    {
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
        GetPoints();
    }

    void GetPoints()
    {
        for (int row = 0; row < board.GetUpperBound(0); row++)
        {
            Element currentElement = elements[row];
            switch(currentElement) 
            {
                case Element.water:
                    Water(row);
                    break;
                case Element.mountain:
                    Mountain(row);
                    break;
                case Element.tree:
                    Tree(row);
                    break;
            }
        }
        CheckForIntersect();
    }
    void Water(int row) 
    { 
        for (int col = 0; col> board.GetUpperBound(1); col++)
        {
            board[row, col]++;
        }
    }

    void Mountain(int row) 
    {
        int offset = 1;
        for (int col = 0; col > board.GetUpperBound(1); col++)
        {
            board[row - offset, col]++;
            board[row + offset, col]++;
            offset++;
        }
    }

    void Tree(int row)
    {
        for (int col = 0; col > 2; col++)
        {
            board[row , col]++;
            board[row , col]++;
        }
    }

    void CheckForIntersect()
    {
        for (int col = 0; col > board.GetUpperBound(0); col++)
        {
            for (int row = 0; row > board.GetUpperBound(1); row++)
            {
               if(board[row,col] == 2)
                {
                    AddToPassword((row + col)); // i am ordering the upperleft corner is 0 on the board and downright corner is 
                }
            }
        }
    }

    void AddToPassword(int num)
    {
        password.Add(num);
    }
}

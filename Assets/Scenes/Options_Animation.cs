using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options_Animation : MonoBehaviour
{
    [SerializeField] float speed;     
    int categoryOption; 
    [SerializeField] Animator animator;
    
    void Awake()
    {
        categoryOption = 0;
    }

    public void NextOptionLeft()  
    {
        categoryOption++;
        if (categoryOption > 2)
        {
            categoryOption = 0;
        }
        animator.SetInteger("Category", categoryOption);
    }

    public void NextOptionRight()   
    {
        categoryOption--;
        if (categoryOption < 0)
        {
            categoryOption = 2;
        }
        animator.SetInteger("Category", categoryOption);
    }
}

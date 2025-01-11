using UnityEngine;
using System.Collections.Generic;

public class AnimationManager : MonoBehaviour
{
    public List<Animator> curtainAnimators;  // List of Animator components

    public void ToggleCurtains()
    {
        foreach (Animator animator in curtainAnimators)
        {
            if (animator != null)
            {
                // Get the current value of the "Open" parameter
                bool isOpen = animator.GetBool("Open");

                // Toggle the value of the "Open" parameter
                animator.SetBool("Open", !isOpen);

                Debug.Log(animator.gameObject.name + " toggled 'Open' to: " + !isOpen);
            }
        }
    }

    public void OpenCurtains()
    {
        foreach (Animator animator in curtainAnimators)
        {
            animator.SetBool("Open", true);
        }
    }
    public void CloseCurtains()
    {
        foreach (Animator animator in curtainAnimators)
        {
            animator.SetBool("Open", false);
        }
    }



    public void AddAnimator(Animator animator)
    {
        if (!curtainAnimators.Contains(animator))
        {
            curtainAnimators.Add(animator);
        }
    }

    public void RemoveAnimator(Animator animator)
    {
        if (curtainAnimators.Contains(animator))
        {
            curtainAnimators.Remove(animator);
        }
    }
}

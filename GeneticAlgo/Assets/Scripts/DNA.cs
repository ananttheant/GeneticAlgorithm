using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    //Gene for Color
    public float r;
    public float g;
    public float b;

    //Is this generation being taken for the next generation
    bool isDead = false;

    //Time it took to die for the current level
    /// <summary>
    /// On this basis the DNA that survived the most,
    /// in the particular generation gets to go further
    /// </summary>
    public float timeToDie = 0;

    private SpriteRenderer sRenderer;
    private Collider2D sCollider;
    

    void Start()
    {
        sRenderer = GetComponent<SpriteRenderer>();

        sCollider = GetComponent<Collider2D>();

        sRenderer.color = new Color(r, g, b);

    }

    /// <summary>
    /// To check the click on the collider and do stuff
    /// </summary>
    private void OnMouseDown()
    {
        isDead = true;
        timeToDie = PopulationManager.elapsed;
        Debug.Log("Dead At:" + timeToDie);
        sRenderer.enabled = false;
        sCollider.enabled = false;
    }
}

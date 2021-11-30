using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenRay : MonoBehaviour
{
    LineRenderer rend;
    EdgeCollider2D col;

    //sprite anim
    [SerializeField]
    private Texture[] textures;

    private int animationStep;

    [SerializeField]
    private float fps = 30f;

    private float fpsCounter;

    public List<Vector2> linePoints = new List<Vector2>();

    private void Start()
    {
        rend = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        linePoints[0] = rend.GetPosition(0);
        linePoints[1] = rend.GetPosition(1);
        col.SetPoints(linePoints);

        //sprite anim
        fpsCounter += Time.deltaTime;
        if(fpsCounter >= 1f / fps)
        {
            animationStep++;

            if (animationStep == textures.Length)
                animationStep = 0;

            rend.material.SetTexture("_MainTex", textures[animationStep]);

            fpsCounter = 0;


        }
    }


}

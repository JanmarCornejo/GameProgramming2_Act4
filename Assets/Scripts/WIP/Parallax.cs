using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    /**
    private float length, height, startposx, startposy;
    public GameObject cam;
    public float parrallaxEffect;

    void Start()
    {
        startposx = transform.position.x;
        startposy = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float tempx = (cam.transform.position.x * (1 - parrallaxEffect));
        float tempy = (cam.transform.position.y * (1 - parrallaxEffect));
        float distx = (cam.transform.position.x * parrallaxEffect);
        float disty = (cam.transform.position.y * parrallaxEffect);

        transform.position = new Vector3(startposx + distx, startposy + disty, transform.position.z);

        if (tempx > startposx + length) startposx += length;
        else if (tempx < startposx - length) startposx -= length;

        if (tempx > startposy + height) startposy += height;
        else if (tempx < startposy - height) startposy -= height;
    }
    **/

    public Transform[] Backgrounds;
    private float[] ParallaxScales;
    public float Smoothing = 1f;

    [SerializeField] private Transform cam;
    private Vector3 PreCamPo;

    // Use this for initialization
    void Start()
    {
        PreCamPo = cam.position;

        ParallaxScales = new float[Backgrounds.Length];

        for (int i = 0; i < Backgrounds.Length; i++)
        {
            ParallaxScales[i] = Backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
         for (int i = 0; i < Backgrounds.Length; i++)
         {
            float parallax_x = (PreCamPo.x - cam.position.x) * ParallaxScales[i];
            float parallax_y = (PreCamPo.y - cam.position.y) * ParallaxScales[i];

            float BackgroundTargetPosX = Backgrounds[i].position.x + parallax_x;
            float BackgroundTargetPosY = Backgrounds[i].position.y + parallax_y;

            Vector3 BackgroundTargetPos = new Vector3(BackgroundTargetPosX, BackgroundTargetPosY, Backgrounds[i].position.z);

            Backgrounds[i].position = Vector3.Lerp(Backgrounds[i].position, BackgroundTargetPos, Smoothing * Time.deltaTime);


         }

         PreCamPo = cam.position;
    }
}

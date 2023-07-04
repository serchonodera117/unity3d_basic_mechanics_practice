using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controll : MonoBehaviour
{
    public Vector3 offset; //distancia entre camara y jugador
    public Transform player;
    [Range(0, 1)] public float smooth;

    public bool mirandoBragas = false;
    public playerMovement options;
    public bool rotacionActive = true;
    public float velocidadCamara = 1f;

    public bool lookAtPlayer = false;

    private short limiteMin = 2;
    private short limiteMax = 100;
    private float myTimer = 0;
    private bool activarTimer = false;
    private Camera myCamara;
    void Start()
    {
        offset = transform.position - player.position;
        myCamara = GetComponent<Camera>();
    }
    void Update()
    {
        controlCamara();
        if (activarTimer) myTimer += Time.deltaTime;
        Debug.Log("segundos = "+ myTimer);
    }
    void controlCamara()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && myCamara.fieldOfView > limiteMin)
            myCamara.fieldOfView--;
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && myCamara.fieldOfView < limiteMax)
            myCamara.fieldOfView++;

        if (rotacionActive)
        {
            Quaternion angulo = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * velocidadCamara, Vector3.up) * Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * velocidadCamara, Vector3.right);
            offset = angulo * offset;
        }

        Vector3 newPos = player.position + offset;
        transform.position = Vector3.Slerp(transform.position, newPos, smooth);

        if (lookAtPlayer || rotacionActive)
        {
            transform.LookAt(player);
        }

        if (offset.y < -1.5)
        {
            myCamara.fieldOfView = 13;
            mirandoBragas = true;
            options.looking = true;
            activarTimer = true;
        }
        else if (offset.y > -1.5 && mirandoBragas)
        {
            myCamara.fieldOfView = 46;
            mirandoBragas = false;
            options.looking = false;
            resetTimer();
        }else
            resetTimer();

        if (myTimer > 1.5)
       {
            offset.y = 3;
            myCamara.fieldOfView = 46;
            Debug.Log("¡No mires ahí, Biejo cochino!");
            resetTimer();
        }
    }
    void resetTimer(){
        myTimer = 0;
        activarTimer = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float Speed = 1f;
    public bool grounded;
    public bool caminando;
    private bool dance = false;
    private bool up_down = true;

    public Animator animaciones;

    public float cameraSpeed = 1f;
    public float salto = 1f;
    private Rigidbody fisicas;

    public bool looking; 
   
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        fisicas = GetComponent<Rigidbody>();
        animaciones = GetComponent<Animator>();
       
    }
    // Update is called once per frame
    void Update()
    {
        animaciones.SetBool("Grounded", grounded);
        animaciones.SetBool("Caminar", caminando);
        animaciones.SetBool("Dancing", dance);
        animaciones.SetBool("Look_down", looking);
        Debug.Log("mirando = "+looking);
        
   
        movimiento();
        rotacionCamara();

       
    }


    //- - - - Movimiento del jugador y movimiento de la camara
    void movimiento(){
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
    
        if (horizontal != 0 || vertical != 0)
        {
            vertical = RotacionMovimientoPlayer(horizontal, vertical); 
         
            Vector3 direccion = transform.forward * vertical + transform.right * horizontal; 
            fisicas.MovePosition(transform.position + direccion * Speed);
            caminando = true;
            Debug.Log("2.- Horizontal : "+horizontal+ " Vertical : "+ vertical);
        }
        else caminando = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            fisicas.AddForce(new Vector3(0, salto, 0), ForceMode.Impulse);
            grounded = false;
        }
        else grounded = true;

        if (Input.GetKey(KeyCode.E)) dance = true;
        else dance = false;

    }
    float RotacionMovimientoPlayer(float horizontal, float vertical)
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (up_down == true) transform.Rotate(new Vector3(0, 180, 0));
            up_down = false;
            return 1;
        }else if(Input.GetKeyDown(KeyCode.W)){
            if (up_down == false) transform.Rotate(new Vector3(0, 180, 0));
            up_down = true;
            return 1;
        }else return 1;
     }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Suelo") grounded = true; Debug.Log("tocando suelo");
    }

   

    void rotacionCamara(){
        float rotationX = Input.GetAxis("Mouse X");
        transform.Rotate(new Vector3(0, rotationX * Time.deltaTime * cameraSpeed, 0));
    }

}

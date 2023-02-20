using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementAdvanced : MonoBehaviour
{
    public SoundsInGame sounds3;                    //u�yte aby by� dzwiek(SoundsInGame = skrypt w k�trym zawarte s� instrukjce)

    [Header("Movement")]                            //U�ycie atrybutu Unity o nazwie Header
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;         //KeyCode.Space == spacja na klawiaturze
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;                  //warstwa
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;                   //RaycastHit struktura w Unity, kt�ra informuje o kolizji 
                                                   //RacastHit informacja o punkcie trafienia w przestrzeni 3D
    private bool exitingSlope;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;                         //Vector3 = reprezentowanie punktu lub kierunku w 3D
                                                   //moveDirection zmienna potrzebna do przechowywania danych wprowadzanych przez gracza, kierunku w kt�rym obiekt powinnien sie porusza�

    Rigidbody rb;                                  //rb to nazwa zmiennej typu Rigidbody => odniesienie do komponentu Rigidbody, kt�ry jest do��czony do GameObjectu co skrypt

    public MovementState state;
    public enum MovementState                      //uzycie wylicze� do reprezentowania stanu obiektu
    {
        walking,
        sprinting,
        crouching,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();             //Przypisuje komponent Rigidbody obiektu, do kt�rego jest do��czony skrypt do zmiennej rb
                                                    //umo�liwienie dost�pu do w�a�ciwo�ci i funckji komponentu Rigidbody poprzez zmienn� rb 
                                                    //Pozwala to ustawi� si�y, mas�, i inne fizyczne czynniki
        rb.freezeRotation = true;                   //obr�t obieku jest "sta�y", co oznacza, �e nie b�dzie si� obraca� (przez si�y fizyczne)

        readyToJump = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);     //sprawdzanie pod�o�a

        MyInput();                                                 //funkcje
        SpeedControl();
        StateHandler();

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");           //odczytanie poziom� o� wejscia i przypisuje jej warto�� do zmiennej horizontalInput
        verticalInput = Input.GetAxisRaw("Vertical");               //odczytanie pioneowej osi wejscia i przypisanie jej do warto�ci zmiennej verticalInput

        if (Input.GetKey(jumpKey) && readyToJump && grounded)       //kiedy skaka�
        {
            readyToJump = false;

            Jump();
            //
            sounds3.jumpSound();                                    //dzwiek skaknia
            //
            Invoke(nameof(ResetJump), jumpCooldown);                //wykonanie funckji po okre�lonym czasie (invoke)
                                                                    //nameof zwraca nazw� funkcji przekazanej jako argument
                                                                    //jumpCooldown zmienna zawieraj�ca czas w sekundach do wywo�ania funkcji resetjump
        }

        if (Input.GetKeyDown(crouchKey))                            //kucanie
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);       //ustawienie w�a�ciwo�ci localScale obiektu na nowy wektor, przy czym x jest ustawiony na aktualny x
                                                                                                                    // y jest ustawione na warto�� crouchYScale, z na aktualne z / zmienia skal� obiektu tylko na osi y
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);      //dodanie si�y/ szybsze spadanie podczas kucania (wi�ksza si�a w kierunku do�u do komponentu Rigidbody)
        }

        
        if (Input.GetKeyUp(crouchKey))                              //wstawanie == przestwanie kucania
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);        //tworzy nowy obiekt Vector3, tr�jwymiarowy wektor, ustawia skal� na osi Y na zmienn� startYScale
                                                                                                                    //X i Z na aktualn�
        }
    }

    private void StateHandler()
    {
        if (Input.GetKey(crouchKey))                            //je�li gracz kliknie przycisk kucania to stan gracza zmieni si� na kucnaie i zmieni sie predkos� gracza na t� podan� w crouchSpeed
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        else if (grounded && Input.GetKey(sprintKey))            //je�li gracz kliknie przycisk spirtowania to stan gracza zmieni si� na sprint i zmieni sie predkos� gracza na t� podan� w sprintSpeed z t� r�nic� ze gracz musi by� na grounded
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        else if (grounded)                                       //jesli gracz jest na grounded to chodzi
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
                                                                //powietrze
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;      //chodzenie w kierunku w kt�rym patrzysz

        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);                    //dodanie si�y do komponentu Rigidbody, co powduje ruch

            if (rb.velocity.y > 0)                                                                      //sprawdzanie pr�dko�ci komponenntu Rigidbody(rb) wzd�u� osi y, je�li pr�dko�� jest wi�ksza niz 0
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);                                       //przyk�ada si�� skierowan� w d� do komponentu Rigidbody, wielko�� si�y jest okre�lona jako 80f
        }

        else if (grounded)                                                                              //na grounded
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);                   //powoduje ze ruch obiektu ze skryptem b�dzie porusza� si� w kierunku move Direction z pr�dko��ia r�wn� moveSpeed *10f
        }

        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);


        rb.useGravity = !OnSlope();                                                                    //ustawienie useGravity na warto�� przeciwn� do tego co zwraca funkcjo OnSlope
    }

    private void SpeedControl()                                                                        //kontroluje pr�dko�� w zale�no�ci czy jest OnSloop czy nie
    {
        if (OnSlope() && !exitingSlope)                                                                //czy obiekt jest OnSloop i czy nie jest w takcie wychodzenia ze Sloop
        {
            if (rb.velocity.magnitude > moveSpeed)                                                     //sprawdza czy wielko�� pr�dko�ci jest wi�ksza ni� zmienna moveSpeed
                rb.velocity = rb.velocity.normalized * moveSpeed;                                      //je�li wielko�� pr�dko�ci jest wi�ksza niz moveSpeed, normalizuje wektor i mno�y go przez moveSpeed
        }

        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);                           //tworzy nowy obiekt vector3, kt�ry jest kopi� pr�dko�ci ale z warto�ci� r�wn� y=0

            if (flatVel.magnitude > moveSpeed)                                                         //sprawdza czy aktaulna pr�dko�� jest wi�ksza ni� moveSpeed
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;                                   
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);                  //zachowanie aktualn� pr�dko�� pionow� i ograniczon� pr�dko�� na osiach x i z
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);                                        //ustawienie pr�dko�ci Rigidbody na osi y na 0 / nowy wektor z warto�ciami aktualnej pr�dko�ci  x i z ale z y = 0 

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);                                           //dodawanie si�y skierowanej do g�ry
    }
    private void ResetJump()                                                                                //reset stanu skoku
    {
        readyToJump = true;                                                                                 //czy obiekt jest w stanie skoczy� czy nie

        exitingSlope = false;
    }

    private bool OnSlope()                                                                                   //funkcja kt�ra wskazuje czy gracz jest OnSlope czy nie
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))     //wysy�a promie� w d� w pozycji obeiktu i sprawdza czy promie� na czym� si� zatrzyma�
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);                                        //oblicza k�t mi�dzy kierunkiem w g�r� a nachyleniem w kt�re uderza promie�
            return angle < maxSlopeAngle && angle != 0;                                                      //czy k�t jest mniejszy niz maxSlopeAngle i nie jest rowny 0
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;       //zwracany wektor jest rzutem wektora "moveDirection" na p�aszczyzn� zdefiniowan� przez nachylenie, zwracany wektor b�dzie r�wnoleg�y do nachylenia
    }
}
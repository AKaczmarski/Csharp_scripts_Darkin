using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementAdvanced : MonoBehaviour
{
    public SoundsInGame sounds3;                    //u¿yte aby by³ dzwiek(SoundsInGame = skrypt w kótrym zawarte s¹ instrukjce)

    [Header("Movement")]                            //U¿ycie atrybutu Unity o nazwie Header
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
    private RaycastHit slopeHit;                   //RaycastHit struktura w Unity, która informuje o kolizji 
                                                   //RacastHit informacja o punkcie trafienia w przestrzeni 3D
    private bool exitingSlope;


    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;                         //Vector3 = reprezentowanie punktu lub kierunku w 3D
                                                   //moveDirection zmienna potrzebna do przechowywania danych wprowadzanych przez gracza, kierunku w którym obiekt powinnien sie poruszaæ

    Rigidbody rb;                                  //rb to nazwa zmiennej typu Rigidbody => odniesienie do komponentu Rigidbody, który jest do³¹czony do GameObjectu co skrypt

    public MovementState state;
    public enum MovementState                      //uzycie wyliczeñ do reprezentowania stanu obiektu
    {
        walking,
        sprinting,
        crouching,
        air
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();             //Przypisuje komponent Rigidbody obiektu, do którego jest do³¹czony skrypt do zmiennej rb
                                                    //umo¿liwienie dostêpu do w³aœciwoœci i funckji komponentu Rigidbody poprzez zmienn¹ rb 
                                                    //Pozwala to ustawiæ si³y, masê, i inne fizyczne czynniki
        rb.freezeRotation = true;                   //obrót obieku jest "sta³y", co oznacza, ¿e nie bêdzie siê obraca³ (przez si³y fizyczne)

        readyToJump = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);     //sprawdzanie pod³o¿a

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
        horizontalInput = Input.GetAxisRaw("Horizontal");           //odczytanie poziom¹ oœ wejscia i przypisuje jej wartoœæ do zmiennej horizontalInput
        verticalInput = Input.GetAxisRaw("Vertical");               //odczytanie pioneowej osi wejscia i przypisanie jej do wartoœci zmiennej verticalInput

        if (Input.GetKey(jumpKey) && readyToJump && grounded)       //kiedy skakaæ
        {
            readyToJump = false;

            Jump();
            //
            sounds3.jumpSound();                                    //dzwiek skaknia
            //
            Invoke(nameof(ResetJump), jumpCooldown);                //wykonanie funckji po okreœlonym czasie (invoke)
                                                                    //nameof zwraca nazwê funkcji przekazanej jako argument
                                                                    //jumpCooldown zmienna zawieraj¹ca czas w sekundach do wywo³ania funkcji resetjump
        }

        if (Input.GetKeyDown(crouchKey))                            //kucanie
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);       //ustawienie w³aœciwoœci localScale obiektu na nowy wektor, przy czym x jest ustawiony na aktualny x
                                                                                                                    // y jest ustawione na wartoœæ crouchYScale, z na aktualne z / zmienia skalê obiektu tylko na osi y
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);      //dodanie si³y/ szybsze spadanie podczas kucania (wiêksza si³a w kierunku do³u do komponentu Rigidbody)
        }

        
        if (Input.GetKeyUp(crouchKey))                              //wstawanie == przestwanie kucania
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);        //tworzy nowy obiekt Vector3, trójwymiarowy wektor, ustawia skalê na osi Y na zmienn¹ startYScale
                                                                                                                    //X i Z na aktualn¹
        }
    }

    private void StateHandler()
    {
        if (Input.GetKey(crouchKey))                            //jeœli gracz kliknie przycisk kucania to stan gracza zmieni siê na kucnaie i zmieni sie predkosæ gracza na t¹ podan¹ w crouchSpeed
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        else if (grounded && Input.GetKey(sprintKey))            //jeœli gracz kliknie przycisk spirtowania to stan gracza zmieni siê na sprint i zmieni sie predkosæ gracza na t¹ podan¹ w sprintSpeed z t¹ ró¿nic¹ ze gracz musi byæ na grounded
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
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;      //chodzenie w kierunku w którym patrzysz

        if (OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);                    //dodanie si³y do komponentu Rigidbody, co powduje ruch

            if (rb.velocity.y > 0)                                                                      //sprawdzanie prêdkoœci komponenntu Rigidbody(rb) wzd³u¿ osi y, jeœli prêdkoœæ jest wiêksza niz 0
                rb.AddForce(Vector3.down * 80f, ForceMode.Force);                                       //przyk³ada si³ê skierowan¹ w dó³ do komponentu Rigidbody, wielkoœæ si³y jest okreœlona jako 80f
        }

        else if (grounded)                                                                              //na grounded
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);                   //powoduje ze ruch obiektu ze skryptem bêdzie porusza³ siê w kierunku move Direction z prêdkoœæia równ¹ moveSpeed *10f
        }

        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);


        rb.useGravity = !OnSlope();                                                                    //ustawienie useGravity na wartoœæ przeciwn¹ do tego co zwraca funkcjo OnSlope
    }

    private void SpeedControl()                                                                        //kontroluje prêdkoœæ w zale¿noœci czy jest OnSloop czy nie
    {
        if (OnSlope() && !exitingSlope)                                                                //czy obiekt jest OnSloop i czy nie jest w takcie wychodzenia ze Sloop
        {
            if (rb.velocity.magnitude > moveSpeed)                                                     //sprawdza czy wielkoœæ prêdkoœci jest wiêksza ni¿ zmienna moveSpeed
                rb.velocity = rb.velocity.normalized * moveSpeed;                                      //jeœli wielkoœæ prêdkoœci jest wiêksza niz moveSpeed, normalizuje wektor i mno¿y go przez moveSpeed
        }

        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);                           //tworzy nowy obiekt vector3, który jest kopi¹ prêdkoœci ale z wartoœci¹ równ¹ y=0

            if (flatVel.magnitude > moveSpeed)                                                         //sprawdza czy aktaulna prêdkoœæ jest wiêksza ni¿ moveSpeed
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;                                   
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);                  //zachowanie aktualn¹ prêdkoœæ pionow¹ i ograniczon¹ prêdkoœæ na osiach x i z
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);                                        //ustawienie prêdkoœci Rigidbody na osi y na 0 / nowy wektor z wartoœciami aktualnej prêdkoœci  x i z ale z y = 0 

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);                                           //dodawanie si³y skierowanej do góry
    }
    private void ResetJump()                                                                                //reset stanu skoku
    {
        readyToJump = true;                                                                                 //czy obiekt jest w stanie skoczyæ czy nie

        exitingSlope = false;
    }

    private bool OnSlope()                                                                                   //funkcja która wskazuje czy gracz jest OnSlope czy nie
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))     //wysy³a promieñ w dó³ w pozycji obeiktu i sprawdza czy promieñ na czymœ siê zatrzyma³
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);                                        //oblicza k¹t miêdzy kierunkiem w górê a nachyleniem w które uderza promieñ
            return angle < maxSlopeAngle && angle != 0;                                                      //czy k¹t jest mniejszy niz maxSlopeAngle i nie jest rowny 0
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;       //zwracany wektor jest rzutem wektora "moveDirection" na p³aszczyznê zdefiniowan¹ przez nachylenie, zwracany wektor bêdzie równoleg³y do nachylenia
    }
}
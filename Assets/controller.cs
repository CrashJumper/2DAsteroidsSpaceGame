using UnityEngine;

public class controller : MonoBehaviour
{
    [SerializeField] float thrust = 5f;
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] Rigidbody2D rigbody;
    [SerializeField] GameObject mainThruster;
    [SerializeField] GameObject leftThruster;
    [SerializeField] GameObject rightThruster;

    bool stabilizers = false;
    bool thrusting = false;
    bool thrustingRight = false;
    bool thrustingLeft = false;
    bool fire = false;
    int fireRate = 1;
    float lastFire = 0f;
    bool fireMissile = false;
    int missileRate = 10;
    float lastMissile = 0f;
    bool fireRocket = false;
    float lastRocket = 0f;
    int rocketRate = 5;
    float rotation = 0f;
    float angularDrag;
    float linearDrag;
    
    void Start()
    {
        if(rigbody == null)
        {
            rigbody = GetComponent<Rigidbody2D>();
            angularDrag = rigbody.angularDrag;
            linearDrag = rigbody.drag;
        }
        
    }

    void Update()
    {
        PlayerInput();
        Thrusting();
    }

    void FixedUpdate()
    {
        Movement();
        Fire();
        FireMissile();
        FireRocket();
    }

    void PlayerInput()
    {
        thrusting = Input.GetKey(KeyCode.W);
        if (Input.GetKey(KeyCode.A))
        {
            rotation = 1f;
            thrustingLeft = true;
            thrustingRight = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rotation = -1f;
            thrustingRight = true;
            thrustingLeft = false;
        }
        else
        {
            rotation = 0f;
            thrustingLeft = false;
            thrustingRight = false;
        }
        stabilizers = Input.GetKey(KeyCode.S);
        fire = Input.GetKey(KeyCode.Mouse0);
        fireMissile = Input.GetKey(KeyCode.Space);
        fireRocket = Input.GetKey(KeyCode.Mouse1);
    }

    void Movement()
    {
        if(thrusting) 
        {
            rigbody.AddForce(this.transform.up * thrust);
        }

        rigbody.AddTorque(rotation * rotateSpeed);
        if(stabilizers)
        {
            rigbody.angularDrag = 10f;
            rigbody.drag = 10f;
        } else
        {
            rigbody.angularDrag = angularDrag;
            rigbody.drag = linearDrag;
        }
    }

    void Thrusting()
    {
        if (thrusting)
        {
            mainThruster.GetComponent<ParticleSystem>().Play();
        }
        if (thrustingLeft)
        {
            leftThruster.GetComponent<ParticleSystem>().Play();
        }
        if (thrustingRight)
        {
            rightThruster.GetComponent<ParticleSystem>().Play();
        }
    }

    void Fire()
    {
        if (fire && lastFire <= 0f)
        {
            lastFire = fireRate;
            CreateProjectile();
        }
    }

    void FireMissile()
    {
        if (fireMissile && lastMissile <= 0f)
        {
            lastMissile = missileRate;
            CreateProjectile();
        }
    }

    void FireRocket()
    {
        if (fireRocket && lastRocket <= 0f)
        {
            lastRocket = rocketRate;
            CreateProjectile();
        }
    }

    void CreateProjectile()
    {

    } 

}

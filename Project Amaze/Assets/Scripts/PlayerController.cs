using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Player player;

    public ParticleSystem ps;

    public float accel = 1;
    public float maxSpeed = 10; //Speed exceeding this wont be accelerated
    public float forceJumping = 10;

    public int countBatteryInit = 1;
    public int powerPerBattery = 100;
    public float rewardPowerFalling = 10;
    public float powerConsumption = 1;

    public AudioSource srcEffect;
    public AudioClip clipFlashJump;
    public AudioClip clipPowerUp;
    public AudioClip clipBounce;
    public AudioClip clipJump;
    public AudioClip clipWaterSplash;

    public AudioSource srcSpeed;

    Rigidbody rb;


    Vector3 ptLastTouching;

    float timeLastTouch = 0;
    float timeLastJump = 0;
    int countTouching = 0;
    


    float powerPerFlashJump = 20;

    Vector3 posLast;
	// Use this for initialization
	void Awake () {
        player = PlayerManager.CreatePlayer(gameObject);
        player.BatteryCount = countBatteryInit;
        player.PowerPerBattery = powerPerBattery;
        player.Power = powerPerBattery;

        rb = transform.GetComponent<Rigidbody>();

    }


    // Update is called once per frame
    void Update () {
        if (MainController.CurrentGameState() != MainController.GameState.PLAYING)
        {
            rb.Sleep();
            srcSpeed.pitch = 0;
            return;
        }else if(CameraController.ActiveCamera() == 2)
        {
            return;
        }

        Vector3 dirMovLocal = new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical"));

        Vector3 dirMovGlobal = Camera.main.transform.TransformDirection(dirMovLocal);
        dirMovGlobal.y = 0;
        dirMovGlobal.Normalize();

        Vector3 vecVelocity = rb.velocity;

        if (!(Mathf.Abs(vecVelocity.x) > maxSpeed && vecVelocity.x / dirMovGlobal.x > 0)) vecVelocity.x += dirMovGlobal.x * accel;
        if (!(Mathf.Abs(vecVelocity.z) > maxSpeed && vecVelocity.z / dirMovGlobal.z > 0)) vecVelocity.z += dirMovGlobal.z * accel;

        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            if (countTouching == 0)//Means Flying, Accel Down
            {
                vecVelocity.y -= accel * 2;
            }
        }

        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if ((Time.time - timeLastTouch) < 0.2f && ((Time.time - timeLastJump) > 0.5f))
            {
                timeLastJump = Time.time;

                Vector3 dirJump = (transform.position - ptLastTouching).normalized;
                dirJump.y += 0.5f;
                dirJump.Normalize();
                vecVelocity += dirJump * forceJumping;

                srcEffect.PlayOneShot(clipJump, srcEffect.volume * 0.5f);
            }
        }

        else if(Input.GetKeyDown(KeyCode.F) && ((Time.time - timeLastJump) > 0.2f))
        {
            // Do Flash Jump
            if (player.Power > powerPerFlashJump)
            {
                timeLastJump = Time.time;
                Vector3 dirJump = Quaternion.Euler(CameraController.ViewRotation()) * Vector3.forward;
                dirJump.Normalize();
                dirJump.y += 0.5f;
                dirJump.Normalize();

                vecVelocity += dirJump * forceJumping;
                player.Power -= powerPerFlashJump;


                Vector3 vecPlayerPs = -(ps.transform.rotation * Vector3.forward) * 0.5f;
                Vector3 posPs = transform.position + vecPlayerPs;

                ps.transform.position = posPs;
                ps.transform.eulerAngles = CameraController.ViewRotation();

                ps.GetComponent<ParticleSystem>().Emit(10);
                srcEffect.PlayOneShot(clipFlashJump);
            }

        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            player.Power += powerPerBattery;
        }



        rb.velocity = vecVelocity;

        srcSpeed.pitch = Mathf.Clamp(Mathf.Sqrt(Mathf.Abs(rb.velocity.magnitude)/10), 0.5f, 3);

        //Gravitational Force Reward
        player.Power += posLast.y > transform.position.y ? 
            (((posLast.y - transform.position.y)/ 10.0f) * rewardPowerFalling) : 0;

        //Timely Power Consumption (in second)
        player.Power -= Time.deltaTime * powerConsumption;
        if(player.Power < -powerPerBattery)
        {
            MainController.CurrentGame().GameOver();
        }


        posLast = transform.position;

    }

    private void OnCollisionEnter(Collision collision)
    {
       
        ptLastTouching = collision.contacts[0].point;
        //rb.AddForce(collision.impulse * 10);
        if(collision.collider.name == "Cube")
        {
            rb.AddForce(collision.impulse * 100);
        }
        timeLastTouch = Time.time;
        countTouching++;
//        Debug.Log(((Mathf.Clamp(collision.impulse.sqrMagnitude / 100, 0, 1000)) / 1000) * srcEffect.volume);
        srcEffect.PlayOneShot(clipBounce, ((Mathf.Clamp(collision.impulse.sqrMagnitude/100, 0, 1000)) / 1000) * srcEffect.volume);



    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Power"))
        {
            player.Power += powerPerBattery;
            srcEffect.PlayOneShot(clipPowerUp);
        }
        else if(other.CompareTag("Battery"))
        {
            player.BatteryCount++;
            player.Power += powerPerBattery;
            srcEffect.PlayOneShot(clipPowerUp);
        }
        else if (other.CompareTag("Water"))
        {
            srcEffect.PlayOneShot(clipWaterSplash);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            player.Power -= 0.5f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            srcEffect.PlayOneShot(clipWaterSplash);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ptLastTouching = collision.contacts[0].point;
        timeLastTouch = Time.time;
    }

    private void OnCollisionExit()
    {
        countTouching--;
    }



}

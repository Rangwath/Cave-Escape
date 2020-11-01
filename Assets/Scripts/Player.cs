using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] private float runSpeed = 3f;
    [SerializeField] private float jumpSpeed = 7.5f;
    [SerializeField] private float climbSpeed = 3f;
    [SerializeField] private float deathDelay = 2f;
    [SerializeField] private AudioClip waterSplashSFX = null;
    [SerializeField] private float waterSplashVolume = 1f;
    [SerializeField] private AudioClip dieSFX = null;
    [SerializeField] private float dieVolume = 0.5f;


    // State
    private bool isAlive = true;

    // Cached component references
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private CapsuleCollider2D myBodyCollider;
    private BoxCollider2D myFeetCollider;
    private float gravityScaleAtStart;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        Walk();
        FlipCharacter();
        Jump();
        ClimbLadder();
        Die();
    }

    private void Walk()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // value is between -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Walking", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            // don't jump when not touching ground
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd; 
        }
    }

    private void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            // don't climb when not touching ladder
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical"); // value is between -1 to +1
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }

    private void FlipCharacter()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), transform.localScale.y);
        }
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards", "Water")))
        {
            isAlive = false;

            Vector2 playerVelocity = new Vector2(0, 0);
            myRigidBody.velocity = playerVelocity;

            if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Water")))
            {
                AudioSource.PlayClipAtPoint(waterSplashSFX, Camera.main.transform.position, waterSplashVolume);
                myAnimator.SetTrigger("Die Water");
            }
            else
            {
                AudioSource.PlayClipAtPoint(dieSFX, Camera.main.transform.position, dieVolume);
                myAnimator.SetTrigger("Die Enemy");
            }
            
            StartCoroutine(ProcessPlayerDeath());
        }
    }

    IEnumerator ProcessPlayerDeath()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession == null)
        {
            Debug.LogError("GameSession is null, add GameSession to the scene.");
            yield break;
        }

        yield return new WaitForSeconds(deathDelay);

        gameSession.ProcessPlayerDeath();
    }
}

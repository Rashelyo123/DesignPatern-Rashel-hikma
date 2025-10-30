using System;
using UnityEngine;

public enum SIDE { Left = -2, Mid = 0, Right = 2 }
public enum HitX { Left, Mid, Right, None }
public enum HitY { Up, Mid, Down, None }
public enum HitZ { Forward, Mid, Backward, None }

public class PlayerEndlessRunner : MonoBehaviour
{
    public SIDE m_Side = SIDE.Mid;
    float NewXPos = 0f;
    [HideInInspector]
    public bool SwipeLeft, SwipeRight, SwipeUp, SwipeDown;
    public float XValue;

    private CharacterController m_char;
    private float x;
    public float SpeedDodge;
    public float JumpPower = 7f;
    private float y;
    public bool inJump;
    public bool InSlide;
    private float ColHeight;
    private float ColCenterY;
    public float FwdSpeed = 7f;
    public HitX hitX = HitX.None;
    public HitY hitY = HitY.None;
    public HitZ hitZ = HitZ.None;
    // public Animator anim; // 
    [SerializeField] private UI_Handle GameUIManager;

    void Start()
    {
        m_char = GetComponent<CharacterController>();
        ColHeight = m_char.height;
        ColCenterY = m_char.center.y;
        transform.position = Vector3.zero;
    }

    void Update()
    {
        SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
        SwipeUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        SwipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        if (!InSlide)
            if (SwipeLeft && !InSlide)
            {
                if (m_Side == SIDE.Mid)
                {
                    NewXPos = -XValue;
                    m_Side = SIDE.Left;
                }
                else if (m_Side == SIDE.Right)
                {
                    NewXPos = 0f;
                    m_Side = SIDE.Mid;
                }
            }

        if (SwipeRight && !InSlide)
        {
            if (m_Side == SIDE.Mid)
            {
                NewXPos = XValue;
                m_Side = SIDE.Right;
            }
            else if (m_Side == SIDE.Left)
            {
                NewXPos = 0f;
                m_Side = SIDE.Mid;
            }
        }

        Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, FwdSpeed * Time.deltaTime);
        x = Mathf.Lerp(x, NewXPos, Time.deltaTime * SpeedDodge);
        m_char.Move(moveVector);

        Jump();
        Slide();
    }

    public void Jump()
    {
        if (m_char.isGrounded)
        {
            if (SwipeUp)
            {
                y = JumpPower;
                inJump = true;
                // anim.SetBool("IsJump", true);
                AudioEventSystem.PlayAudio("Jump");
            }
            else
            {
                // anim.SetBool("IsJump", false);
            }
        }
        else
        {
            y -= JumpPower * 2 * Time.deltaTime;
        }
    }

    internal float SlideCounter = 0f;
    public void Slide()
    {
        if (SwipeDown && !InSlide)
        {
            SlideCounter = 0.2f;
            y -= 10f;
            m_char.center = new Vector3(0, ColCenterY / 2f, 0);
            m_char.height = ColHeight / 2f;
            InSlide = true;
            inJump = false;
            // anim.SetBool("IsSlide", true);
            AudioEventSystem.PlayAudio("Slide");
        }

        if (InSlide)
        {
            SlideCounter -= Time.deltaTime;

            if (SlideCounter <= 0f)
            {
                m_char.center = new Vector3(0, ColCenterY, 0);
                m_char.height = ColHeight;
                InSlide = false;
                // anim.SetBool("IsSlide", false);
            }
        }
    }

    public void OnCharacterColliderHit(Collider col)
    {
        hitX = GetHitX(col);
        hitY = GetHitY(col);
        hitZ = GetHitZ(col);
    }

    public HitX GetHitX(Collider col)
    {
        Bounds char_bounds = m_char.bounds;
        Bounds col_bounds = col.bounds;
        float min_x = Mathf.Max(col_bounds.min.x, char_bounds.min.x);
        float max_x = Mathf.Min(col_bounds.max.x, char_bounds.max.x);
        float average = (min_x + max_x) / 2f - col_bounds.min.x;
        HitX hit;
        if (average > col_bounds.size.x - 0.33f)
            hit = HitX.Right;
        else if (average < 0.33f)
            hit = HitX.Left;
        else hit = HitX.Mid;
        return hit;
    }

    public HitY GetHitY(Collider col)
    {
        Bounds char_bounds = m_char.bounds;
        Bounds col_bounds = col.bounds;
        float min_y = Mathf.Max(col_bounds.min.y, char_bounds.min.y);
        float max_y = Mathf.Min(col_bounds.max.y, char_bounds.max.y);
        float average = ((min_y + max_y) / 2f - char_bounds.min.y) / char_bounds.size.y;
        HitY hit;
        if (average < 0.33f)
            hit = HitY.Down;
        else if (average < 0.66f)
            hit = HitY.Mid;
        else hit = HitY.Up;
        return hit;
    }

    public HitZ GetHitZ(Collider col)
    {
        Bounds char_bounds = m_char.bounds;
        Bounds col_bounds = col.bounds;
        float min_z = Mathf.Max(col_bounds.min.z, char_bounds.min.z);
        float max_z = Mathf.Min(col_bounds.max.z, char_bounds.max.z);
        float average = ((min_z + max_z) / 2f - char_bounds.min.y) / char_bounds.size.z;
        HitZ hit;
        if (average < 0.33f)
            hit = HitZ.Backward;
        else if (average < 0.66f)
            hit = HitZ.Mid;
        else hit = HitZ.Forward;
        return hit;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Die();
            Debug.Log("Player Hit");
        }
    }

    public void Die()
    {
        AudioEventSystem.PlayAudio("Die");
        DifficultyManager.Instance.baseSpeed = 0f;
        FindObjectOfType<MoveSection>().NotMove();
        Debug.Log("Game Over!");
        FindObjectOfType<CameraEffects>().TriggerShake(0.3f, 0.4f);

            
        GameUIManager.ShowGameOver();
    }


    public void Life()
    {
        // anim.SetBool("IsDead", false);
    }
}

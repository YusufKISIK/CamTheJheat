using System.Collections;
using UnityEngine;
using TMPro;


public class Guard : MonoBehaviour
{
    public float speed;
    public float distance;
    public Animator anim;
    public Transform Chieldvisual;
    public AudioSource AudioSource;
    public AudioClip bagag; 
    public AudioClip Cash; 
    public AudioClip Death;

    public GameObject GuardBuuble;
    public TextMeshProUGUI GuardBubleText;

    private bool GuardFreeze = false;
    
    private bool _moveRight = true;
    
    private Player _player;
    private LevelManager _levelManager;

    public Transform groundDetect;
    private static readonly int Walk = Animator.StringToHash("walk");

    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _player = FindObjectOfType<Player>();
        gameObject.SetActive(false);
        FindObjectOfType<LevelManager>().OnNightStart += () => { gameObject.SetActive(true); };
    }

    void Update()
    {
        if (GuardFreeze == false)
        {
            Move();
        }
    }


    void Move()
    {
        transform.Translate((_moveRight?Vector2.right:Vector2.left) * (speed * Time.deltaTime));

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, distance);
        anim.SetBool(Walk,true);

        if (groundInfo.collider != false) return;
        if (_moveRight)
        {
            Chieldvisual.Rotate(0, -180, 0);
            _moveRight = false;
        }
        else
        {
            Chieldvisual.Rotate(0,  180, 0);
            _moveRight = true;
        }
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _player.Freeze = true;
        GuardFreeze = true;
        anim.SetBool(Walk,false);
        AudioSource.PlayOneShot(bagag);
        
        if (_levelManager.Money >= _levelManager.penalty)
        {
            _levelManager.Money -= _levelManager.penalty;
            StartCoroutine(Wait3SecondsWin());
        }
        else
        {
            StartCoroutine( Wait3SecondsLose());
           
        }
    }

    IEnumerator Wait3SecondsWin()
    {
        GuardBuuble.SetActive(true);
        yield return new WaitForSeconds(3f);
        transform.position = _player.Front.transform.position;
        GuardBubleText.SetText("Ver Bakam 10 kayme!!!");
        AudioSource.PlayOneShot(Cash);
        
        
        yield return new WaitForSeconds(3f);
        GuardBuuble.SetActive(false);
        _player.Freeze = false;
        GetComponentInChildren<BoxCollider2D>().enabled = false;
        GuardFreeze = false;

    }
    IEnumerator Wait3SecondsLose()
    {
        GuardBuuble.SetActive(true);
        yield return new WaitForSeconds(3f);
        transform.position = _player.Front.transform.position;
        GuardBubleText.SetText("Ver Bakam 10 kayme!!!");
        yield return new WaitForSeconds(1f);
        GuardBubleText.SetText("Demek Paran Yok Ha :)))");
        yield return new WaitForSeconds(1f);
        AudioSource.PlayOneShot(Death);
        
        _levelManager.LoseLevel();
    }
}

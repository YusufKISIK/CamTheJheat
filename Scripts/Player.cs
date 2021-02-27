using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject visualMan;
    public float moveSpeed = 5f;
    public Animator anim;
    public Scrollbar _scrollbar;
    public GameObject HackingPanel;
    public GameObject Front;
    public Vector3 StartPoint;

    private bool _triggered;
    private int _direction = 1;

    public bool Freeze = false;
    
    private Canvas _canvas; 
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");

    private void Start()
    {
        StartPoint = transform.position;
        FindObjectOfType<LevelManager>().OnNightStart += () => transform.position = StartPoint;
        GetComponent<Rigidbody2D>();
        _canvas = GetComponentInChildren<Canvas>();
        _scrollbar = GetComponentInChildren<Scrollbar>(true);
    }
    
    private void Update()
    {
        if (Freeze == false)
        {
            PlayerMove();
        }
    }

    public void EnableCanvas()
    {
        _canvas.enabled = true;
    }
    
    public void DisableCanvas()
    {
        _canvas.enabled = false;
    }

    void PlayerMove()
    {
        var h = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(h, 0, 0);
        
        if (h * _direction < 0)
        {
            visualMan.transform.Rotate(0, 180, 0);
            _direction = -_direction;
        }
        
        transform.position += move * (Time.deltaTime * moveSpeed);
        anim.SetFloat(Horizontal, Mathf.Abs(h));
    }

    public void StartSabotage()
    {
        Freeze = true;
        _scrollbar.size = 0f;
        HackingPanel.SetActive(true);
        StartCoroutine(waitForHack());

    }

    IEnumerator waitForHack()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _scrollbar.size += 0.01f;
        }

        Freeze = false;
        HackingPanel.SetActive(false);
    }
}


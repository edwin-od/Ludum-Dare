using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float _speedMove;
    public float _speedJump;

    [HideInInspector] public bool _isJumped;

    private Rigidbody2D _myRg;
    private SpriteRenderer _mySpriteRend;
    [SerializeField] private Transform position;
    [SerializeField] private Transform position2;

    public GameManager gameManager;

    [HideInInspector] public AnimPlayer anim;

    [SerializeField] private string menuScene;
    [SerializeField] private string nextScene;

    [HideInInspector] public Projectile actualProjectile;
    [HideInInspector] public Projectile releasedProjectile;
    public Projectile[] projectiles;
    private int indexProjectile;

    public int ammoProjectile1, ammoProjectile2, ammoProjectile3;

    private Vector3 scaleCoefficients;

    private bool mouseLeftClick;
    private bool isInHand;

    public Transform groundDetector;
    public float groundDetectionRadius;


    private void Awake()
    {
        _myRg = GetComponent<Rigidbody2D>();
        _mySpriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<AnimPlayer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        actualProjectile = projectiles[0];
        releasedProjectile = null;
        indexProjectile = 0;

        mouseLeftClick = false;
        isInHand = false;

        scaleCoefficients = new Vector3(1 / gameObject.transform.localScale.x, 1 / gameObject.transform.localScale.y, 1 / gameObject.transform.localScale.z);

        gameManager.unselectedAmmoUI[0].SetActive(false);
        gameManager.selectedAmmoUI[0].SetActive(true);
        gameManager.unselectedAmmoUI[0].GetComponentInChildren<Text>().text = ammoProjectile1.ToString();
        gameManager.selectedAmmoUI[0].GetComponentInChildren<Text>().text = ammoProjectile1.ToString();

        gameManager.unselectedAmmoUI[1].SetActive(true);
        gameManager.selectedAmmoUI[1].SetActive(false);
        gameManager.unselectedAmmoUI[1].GetComponentInChildren<Text>().text = ammoProjectile2.ToString();
        gameManager.selectedAmmoUI[1].GetComponentInChildren<Text>().text = ammoProjectile2.ToString();

        gameManager.unselectedAmmoUI[2].SetActive(true);
        gameManager.selectedAmmoUI[2].SetActive(false);
        gameManager.unselectedAmmoUI[2].GetComponentInChildren<Text>().text = ammoProjectile3.ToString();
        gameManager.selectedAmmoUI[2].GetComponentInChildren<Text>().text = ammoProjectile3.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Mouvement();
        Jump();
        ChooseProjectile();

        if((ammoProjectile1 > 0 && indexProjectile == 0) || (ammoProjectile2 > 0 && indexProjectile == 1) || (ammoProjectile3 > 0 && indexProjectile == 2))
        {
            ShootProjectile();
        }

    }

    private void Mouvement()
    {
        if(Input.GetButtonDown("Horizontal"))
            anim.audioManager.PlayEffectOnLoop(anim.walkAudioSource, anim.walkAudioSource.clip);
        else if (Input.GetButton("Horizontal"))
        {
            float directionX = 0;
            directionX = Input.GetAxis("Horizontal") * Time.deltaTime * _speedMove;

            if (Input.GetAxis("Horizontal") < 0 && !_mySpriteRend.flipX)
            {
                _mySpriteRend.flipX = true;
                actualProjectile.transform.localPosition = new Vector3(-actualProjectile.transform.localPosition.x, actualProjectile.transform.localPosition.y, actualProjectile.transform.localPosition.z);
            }
            else if (Input.GetAxis("Horizontal") > 0 && _mySpriteRend.flipX)
            {
                _mySpriteRend.flipX = false;
                actualProjectile.transform.localPosition = new Vector3(-actualProjectile.transform.localPosition.x, actualProjectile.transform.localPosition.y, actualProjectile.transform.localPosition.z);
            }

            transform.Translate(directionX, 0, 0);

            if(!_isJumped)
                anim.WalkAnim();
        }
        else if(Input.GetButtonUp("Horizontal"))
            anim.audioManager.StopEffectOnLoop(anim.walkAudioSource, anim.walkAudioSource.clip);
        else if(!_isJumped)
            anim.IdleAnim();

        
    }

    private void Jump()
    {
        Collider2D collider = null;
        Collider2D[] cols = Physics2D.OverlapCircleAll(new Vector2(groundDetector.position.x, groundDetector.position.y), groundDetectionRadius);
        foreach (Collider2D col in cols)
        {
            if (col.gameObject.CompareTag("Platform") || col.gameObject.CompareTag("Structure1") || col.gameObject.CompareTag("Structure2") || col.gameObject.CompareTag("Structure3"))
            {
                collider = col;
            }
        }

        if (Input.GetButtonDown("Jump") && !_isJumped && collider)
        {
            _myRg.velocity = new Vector2(0, Input.GetAxis("Jump") * _speedJump);
            _isJumped = true;
            anim.JumpAnim();
        }
        else if (_isJumped && collider)
        {
            _isJumped = false;
            anim.IdleAnim();
        }

    }

    private void ChooseProjectile()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1) && indexProjectile != 0)
        {
            indexProjectile = 0;
            if (mouseLeftClick && ammoProjectile1 != 0)
            {
                if(actualProjectile && !IsPrefab(actualProjectile.gameObject))
                    Destroy(actualProjectile.gameObject);
                actualProjectile = projectiles[0];
                indexProjectile = 0;
                InstantiateProjectile();
            }
            else if(mouseLeftClick)
            {
                if (actualProjectile && !IsPrefab(actualProjectile.gameObject))
                    Destroy(actualProjectile.gameObject);
            }
            else
            {
                actualProjectile = projectiles[0];
            }
            gameManager.unselectedAmmoUI[0].SetActive(false);
            gameManager.selectedAmmoUI[0].SetActive(true);

            gameManager.unselectedAmmoUI[1].SetActive(true);
            gameManager.selectedAmmoUI[1].SetActive(false);

            gameManager.unselectedAmmoUI[2].SetActive(true);
            gameManager.selectedAmmoUI[2].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && indexProjectile != 1)
        {
            indexProjectile = 1;
            if (mouseLeftClick && ammoProjectile2 != 0)
            {
                if (actualProjectile && !IsPrefab(actualProjectile.gameObject))
                    Destroy(actualProjectile.gameObject);
                actualProjectile = projectiles[1];
                indexProjectile = 1;
                InstantiateProjectile();
            }
            else if (mouseLeftClick)
            {
                if (actualProjectile && !IsPrefab(actualProjectile.gameObject))
                    Destroy(actualProjectile.gameObject);
            }
            else
            {
                actualProjectile = projectiles[1];
            }
            gameManager.unselectedAmmoUI[0].SetActive(true);
            gameManager.selectedAmmoUI[0].SetActive(false);

            gameManager.unselectedAmmoUI[1].SetActive(false);
            gameManager.selectedAmmoUI[1].SetActive(true);

            gameManager.unselectedAmmoUI[2].SetActive(true);
            gameManager.selectedAmmoUI[2].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && indexProjectile != 2)
        {
            indexProjectile = 2;
            if (mouseLeftClick && ammoProjectile3 != 0)
            {
                if (actualProjectile && !IsPrefab(actualProjectile.gameObject))
                    Destroy(actualProjectile.gameObject);
                actualProjectile = projectiles[2];
                indexProjectile = 2;
                InstantiateProjectile();
            }
            else if (mouseLeftClick)
            {
                if (actualProjectile && !IsPrefab(actualProjectile.gameObject))
                    Destroy(actualProjectile.gameObject);
            }
            else
            {
                actualProjectile = projectiles[2];
            }
            gameManager.unselectedAmmoUI[0].SetActive(true);
            gameManager.selectedAmmoUI[0].SetActive(false);

            gameManager.unselectedAmmoUI[1].SetActive(true);
            gameManager.selectedAmmoUI[1].SetActive(false);

            gameManager.unselectedAmmoUI[2].SetActive(false);
            gameManager.selectedAmmoUI[2].SetActive(true);
        }

    }

    private void ShootProjectile()
    {
        if (Input.GetMouseButtonDown(0) && !gameManager._isPause)
        {

            mouseLeftClick = true;

            if(!releasedProjectile && !isInHand)
                InstantiateProjectile();

        }
        else if (Input.GetMouseButtonUp(0) && mouseLeftClick)
        {
            mouseLeftClick = false;

            if (!releasedProjectile && isInHand)
                ReleaseProjectile();

        }

        if (mouseLeftClick)
        {
            actualProjectile.trajectory.UpdateDots(_mySpriteRend.flipX);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("VictoryArea"))
        {
            gameManager.MyLoadScene(nextScene);
        }

        if (collision.gameObject.CompareTag("DeathZone"))
        {
            gameManager.ReloadScene();
        }


    }

    private void InstantiateProjectile()
    {
        
        if (!_mySpriteRend.flipX)
            actualProjectile = Instantiate(actualProjectile, position);
        else
            actualProjectile = Instantiate(actualProjectile, position2);

        isInHand = true;
        actualProjectile.transform.parent = this.transform;

        actualProjectile.transform.localScale = new Vector3(actualProjectile.scale.x * scaleCoefficients.x, actualProjectile.scale.y * scaleCoefficients.y, 1 * scaleCoefficients.z);

        actualProjectile.trajectory.projectile = actualProjectile;
        actualProjectile.trajectory.Hide();
        actualProjectile.trajectory.DotsInit();

        
        actualProjectile.DesactivateRb();
        actualProjectile.trajectory.Show();
    }

    private void ReleaseProjectile()
    {

        actualProjectile.ActivateRb();
        actualProjectile.Push(_mySpriteRend.flipX);
        actualProjectile.trajectory.Hide();

        actualProjectile.transform.parent = null;

        releasedProjectile = actualProjectile;
        releasedProjectile.GetComponent<CircleCollider2D>().isTrigger = true;

        if (indexProjectile == 0)
        {
            ammoProjectile1--;
            releasedProjectile.tag = "Projectile1";
            gameManager.unselectedAmmoUI[0].GetComponentInChildren<Text>().text = ammoProjectile1.ToString();
            gameManager.selectedAmmoUI[0].GetComponentInChildren<Text>().text = ammoProjectile1.ToString();
        }
        else if (indexProjectile == 1)
        {
            ammoProjectile2--;
            releasedProjectile.tag = "Projectile2";
            gameManager.unselectedAmmoUI[1].GetComponentInChildren<Text>().text = ammoProjectile2.ToString();
            gameManager.selectedAmmoUI[1].GetComponentInChildren<Text>().text = ammoProjectile2.ToString();
        }
        else if (indexProjectile == 2)
        {
            ammoProjectile3--;
            releasedProjectile.tag = "Projectile3";
            gameManager.unselectedAmmoUI[2].GetComponentInChildren<Text>().text = ammoProjectile3.ToString();
            gameManager.selectedAmmoUI[2].GetComponentInChildren<Text>().text = ammoProjectile3.ToString();
        }

        anim.ThrowProjectileAnim();

        actualProjectile = projectiles[indexProjectile];

        isInHand = false;
    }

    private bool IsPrefab(GameObject go)
    {
        return go.scene.rootCount == 0;
    }
}

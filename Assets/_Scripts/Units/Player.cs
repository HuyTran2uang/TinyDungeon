using System.Collections;
using UnityEngine;

public class Player : MonoBehaviourSingleton<Player>
{
    float scopeAttack;
    [Header("Info")]
    [SerializeField] private int _health;
    [SerializeField] private int _mana;
    [SerializeField] private int _attack;
    [SerializeField] private int _armor;
    [SerializeField] private float _speed;
    [SerializeField] private PlayerType _type;
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _currentMana;

    [field: SerializeField]
    public int AttackItem { get; set; }

    [field: SerializeField]
    public int ArmorItem { get; set; }

    [field: SerializeField]
    public int SpeedItem { get; set; }


    public int Health
    {
        get { return _health; }
    }
    public int Mana
    {
        get { return _mana; }
    }
    public float Speed
    {
        get { return _speed; }
    }
    public int Damage
    {
        get { return _attack; }
    }
    public PlayerType Type
    {
        get { return _type; }
        set { _type = value; }
    }

    public int CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;
            UIPlayerBar.Instance.SetHealthBar(CurrentHealth, Health);
        }
    }
    public int CurrentMana
    {
        get { return _currentMana; }
        set
        {
            _currentHealth = value;
            UIPlayerBar.Instance.SetHealthBar(CurrentMana, Mana);
        }
    }

    Grid grid;
    Pathfinding pathfinding;
    public Transform Shield;

    private void Awake()
    {
        grid = GetComponent<Grid>();
        pathfinding = GetComponent<Pathfinding>();
    }

    private void Start()
    {
        targetPosition = grid.GetNodeFromPos(transform.position).centerPos;
    }

    private void Update()
    {
        Attack();
        SetSkin();
        SetShield();
    }

    private void FixedUpdate()
    {
        if (timer > 0) timer -= Time.deltaTime;
        Movement();
    }

    //movement
    private void Movement()
    {
        switch (Type)
        {
            case PlayerType.Melee:
                MeleeMovement();
                break;
            case PlayerType.Distance:
                RangeMovement();
                break;
            case PlayerType.Magic:
                RangeMovement();
                break;
        }
    }
    private Vector2 targetPosition;
    private Transform targetTransform;
    [Header("Movement")]
    public LayerMask layerTargets;
    public GameObject targetPositionPrefabs;
    public GameObject targetEnemyPrefabs;

    private void MeleeMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DestroyAllTargetEnemyPrefab();
            DestroyAllTargetPositionPrefab();

            Collider2D hit = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0, layerTargets);
            if (hit == null)
            {
                targetTransform = null;
                targetPosition = grid.GetNodeFromPos(Camera.main.ScreenToWorldPoint(Input.mousePosition)).centerPos;
                Instantiate(targetPositionPrefabs, targetPosition, Quaternion.identity);
            }
            if (hit != null)
            {
                targetTransform = hit.transform;
                Instantiate(targetEnemyPrefabs, targetTransform);
            }
        }

        if (targetTransform != null)
        {
            targetPosition = targetTransform.position;
            pathfinding.FindPath(transform.position, targetTransform.position);
        }

        if (targetTransform == null)
        {
            DestroyAllTargetEnemyPrefab();
            pathfinding.FindPath(transform.position, targetPosition);
        }

        if ((Vector2)transform.position == targetPosition)
        {
            DestroyAllTargetPositionPrefab();
        }

        MoveFollowPathFound();
    }
    bool isFollowing = false;
    private void RangeMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DestroyAllTargetPositionPrefab();

            Collider2D hit = Physics2D.OverlapCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0, layerTargets);
            if (hit == null)
            {
                targetPosition = grid.GetNodeFromPos(Camera.main.ScreenToWorldPoint(Input.mousePosition)).centerPos;
                Instantiate(targetPositionPrefabs, targetPosition, Quaternion.identity);
                if (targetTransform == null) return;
                isFollowing = false;
                grid.path.Clear();
            }
            if (hit != null)
            {
                DestroyAllTargetEnemyPrefab();
                isFollowing = true;
                targetTransform = hit.transform;
                Instantiate(targetEnemyPrefabs, targetTransform);
            }
        }

        if (targetTransform != null)
        {
            if (isFollowing)
            {
                targetPosition = grid.GetNodeFromPos(transform.position).centerPos;
                pathfinding.FindPath(transform.position, targetTransform.position);
            }
            else
                pathfinding.FindPath(transform.position, targetPosition);

        }

        if (targetTransform == null)
        {
            DestroyAllTargetEnemyPrefab();
            pathfinding.FindPath(transform.position, targetPosition);
        }

        if ((Vector2)transform.position == targetPosition)
        {
            DestroyAllTargetPositionPrefab();
        }


        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, scopeAttack, layerTargets);
        foreach (var hit in hits)
            if (hit != null)
                if (hit.transform == targetTransform && isFollowing)
                    MoveToCellCenter(transform.position);
        MoveFollowPathFound();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, scopeAttack);
    }

    void MoveFollowPathFound()
    {
        if (grid.path == null || grid.path.Count <= 0)
        {
            MoveToCellCenter(transform.position);
            return;
        }

        if ((transform.position.x - grid.path[0].centerPos.x) < 0)
            transform.localScale = Vector3.one;
        if ((transform.position.x - grid.path[0].centerPos.x) > 0)
            transform.localScale = new Vector3(-1, 1, 1);

        transform.position = Vector2.MoveTowards(transform.position, grid.path[0].centerPos, Speed * Time.deltaTime);
    }

    void MoveToCellCenter(Vector3 pos)
    {
        Vector3Int cell = grid.map.WorldToCell(transform.position);
        Vector3 center = grid.map.GetCellCenterWorld(cell);
        transform.position = Vector2.MoveTowards(transform.position, center, Speed * Time.deltaTime);
    }

    IEnumerator Moving(Vector3 nextPos)
    {
        yield return transform.position = Vector2.MoveTowards(transform.position, nextPos, Speed * Time.deltaTime);
    }

    private void DestroyAllTargetPositionPrefab()
    {
        var objsTargetPosition = GameObject.FindGameObjectsWithTag("TargetPosition");
        foreach (var i in objsTargetPosition)
            Destroy(i.gameObject);
    }

    private void DestroyAllTargetEnemyPrefab()
    {
        var objsTargetEnemy = GameObject.FindGameObjectsWithTag("TargetEnemy");
        foreach (var i in objsTargetEnemy)
            Destroy(i.gameObject);
    }

    //attack
    private void Attack()
    {
        switch (Type)
        {
            case PlayerType.Melee:
                scopeAttack = 1f;
                MeleeAttack();
                break;
            case PlayerType.Distance:
                scopeAttack = 5f;
                DistanceAttack();
                break;
            case PlayerType.Magic:
                scopeAttack = 5f;
                MagicAttack();
                break;
        }
    }
    float cooldown = 2f, timer = 0;
    bool isAttacking = false;
    [Header("Attack")]
    public WeaponCtrl weaponCtrl;
    public GameObject effectSword;
    private void MeleeAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, scopeAttack, layerTargets);
        foreach (var hit in hits)
            if (hit != null && hit.transform == targetTransform)
            {
                if (isAttacking) return;
                if (timer > 0) return;
                isAttacking = true;
                timer = cooldown;
                weaponCtrl.Attack();
                GameObject effect = Instantiate(effectSword, targetTransform);
                Destroy(effect, 0.5f);
                Invoke(nameof(CompleteAttack), 0.5f);
            }
    }

    public GameObject arrow;
    private void DistanceAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, scopeAttack, layerTargets);
        foreach (var hit in hits)
            if (hit != null && hit.transform == targetTransform)
            {
                if (isAttacking) return;
                if (timer > 0) return;
                isAttacking = true;
                timer = cooldown;
                weaponCtrl.Attack();
                GameObject bulletPhysic = Instantiate(arrow, transform.position, Quaternion.identity);
                var bullet = bulletPhysic.GetComponent<Bullet>();
                bullet.Damage = Damage;
                bullet.Target = targetTransform;
                Invoke(nameof(CompleteAttack), 0.5f);
            }
    }

    public GameObject magicBullet;
    private void MagicAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, scopeAttack, layerTargets);
        foreach (var hit in hits)
            if (hit != null && hit.transform == targetTransform)
            {
                if (isAttacking) return;
                if (timer > 0) return;
                isAttacking = true;
                timer = cooldown;
                weaponCtrl.Attack();
                GameObject bulletMagic = Instantiate(magicBullet, transform.position, Quaternion.identity);
                var bullet = bulletMagic.GetComponent<Bullet>();
                bullet.Damage = Damage;
                bullet.Target = targetTransform;
                Invoke(nameof(CompleteAttack), 0.5f);
            }
    }

    void CompleteAttack()
    {
        isAttacking = false;
    }

    SpriteRenderer renderSkinCharacter;
    private void SetSkin()
    {
        if (renderSkinCharacter == null || renderSkinCharacter != transform.Find("Model").GetComponent<SpriteRenderer>())
            renderSkinCharacter = transform.Find("Model").GetComponent<SpriteRenderer>();

        switch (Type)
        {
            case PlayerType.Melee:
                renderSkinCharacter.sprite = Equipment.Instance.skinMelee;
                weaponCtrl.ChangeWeapon(Equipment.Instance.WeaponMelee);
                break;
            case PlayerType.Distance:
                renderSkinCharacter.sprite = Equipment.Instance.skinDistance;
                weaponCtrl.ChangeWeapon(Equipment.Instance.WeaponDistance);
                break;
            case PlayerType.Magic:
                renderSkinCharacter.sprite = Equipment.Instance.skinMagic;
                weaponCtrl.ChangeWeapon(Equipment.Instance.WeaponMagic);
                break;
        }
    }

    void SetShield()
    {
        switch (Type)
        {
            case PlayerType.Melee:
                Shield.gameObject.SetActive(true);
                break;
            case PlayerType.Distance:
                Shield.gameObject.SetActive(false);
                break;
            case PlayerType.Magic:
                Shield.gameObject.SetActive(false);
                break;
        }
    }
}

public enum PlayerType
{
    Melee,
    Distance,
    Magic
}

using System;
using UnityEngine;

public enum BulletType
{
    Physic,
    Magic
}
public class Bullet : MonoBehaviour
{
    public int Damage { private get; set; }
    public Transform Target { private get; set; }
    [SerializeField] private BulletType _type;
    public float speed;
    public float hitBox;

    private void Update()
    {
        DamageSender();
    }

    private void MoveToTarget()
    {
        //move forward to target
        transform.position = Vector2.MoveTowards(transform.position, Target.position, speed * Time.deltaTime);
        if (_type == BulletType.Magic) return;
        //rotation for arrow straight to target
        Vector3 dir = (transform.position - Target.position).normalized;
        float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(dir, Vector3.up));
        Vector3 cross = Vector3.Cross(dir, Vector3.up);
        angle = -Mathf.Sign(cross.z) * angle - 90f;
        //
        transform.localEulerAngles = Vector3.forward * angle;
    }

    public void DamageSender()
    {
        if (Damage == 0) return;
        if (Target == null) return;
        MoveToTarget();
        Collider2D hit = Physics2D.OverlapCircle(transform.position, hitBox, LayerMask.GetMask("Enemy"));
        if (hit == null) return;
        if (hit.transform != Target) return;
        hit.GetComponent<IDamageable>().TakeDamage(Damage);
        Destroy(gameObject);
    }
}

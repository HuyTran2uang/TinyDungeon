using UnityEngine;

public class UIAnimationDamageable : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void FixedUpdate()
    {
        Animation();
    }

    private void Animation()
    {
        transform.position += Vector3.up * _speed * Time.deltaTime;
        Destroy(gameObject, 1f);
    }
}

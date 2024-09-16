using UnityEngine;

public class Target : MonoBehaviour
{
  public float health = 150f;
  public void TakeDamage(float amount)
  {
    health -= amount;
    if (health <= 0)
    {
        die();
    }
  }
  void die()
  {
    Destroy(gameObject);
  }
}

using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public AudioClip coinSound;  // ← TAMBAHKAN INI
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Play coin sound
            if (coinSound != null)
            {
                AudioSource.PlayClipAtPoint(coinSound, transform.position, 0.5f);
            }
            
            FindObjectOfType<GameManager>().AddCoin(coinValue);
            Destroy(gameObject);
        }
    }
}
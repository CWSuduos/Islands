using UnityEngine;

public class ReverseLvl : MonoBehaviour
{
    public void DeleteObject()
    {
        Destroy(gameObject);
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
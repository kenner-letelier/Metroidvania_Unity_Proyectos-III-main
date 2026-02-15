using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScene : MonoBehaviour
{
    public GameObject destroy;
    // [SerializeField] SceneId levelFrom;
    [SerializeField] SceneId levelToLoad;
    [SerializeField] Transform spawnPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(TagId.Player.ToString()))
        {
            SceneHelper.instance.LoadScene(levelToLoad);
            //Destroy(collision.gameObject);
            DontDestroyOnLoad(destroy);
        }
    }
    public SceneId SceneToLoad()
    {
        return levelToLoad;
    }

    public Vector2 GetSpawnPosition()
    {
        return spawnPosition.position;
    }





}

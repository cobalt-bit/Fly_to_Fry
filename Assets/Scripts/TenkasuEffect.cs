using UnityEngine;

public class TenkasuEffect : MonoBehaviour
{
    [SerializeField] TenkasuObjectListScript tenkasuObjectData;
    Rigidbody2D playerRigid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        setEvent();
    }

    void setEvent()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("PlayerSearch").GetComponent<Player.PlayerSearcher>().player;
        Player.PlayerMover mover = playerObject.GetComponent<Player.PlayerMover>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

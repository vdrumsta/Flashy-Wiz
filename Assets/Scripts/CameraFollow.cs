using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    private Vector2 velocity;
    public float smoothTimeY;
    public float smoothTimeX;
	public float yDistance;

    public GameObject player;

    public bool bounds;

    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
	}

    void FixedUpdate()
    {

        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp(transform.position.y - yDistance, player.transform.position.y - yDistance, ref velocity.y, smoothTimeY);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}

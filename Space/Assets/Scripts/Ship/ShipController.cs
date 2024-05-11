using UnityEngine;

public class ShipController : MonoBehaviour
{
	[SerializeField]
    ShipControllerValues _shipValues;

    [SerializeField]
    ShipCargo _cargo;

    [SerializeField]
    ShipCargo _shipInReach;

	Vector3 velocity;

    [SerializeField]
    bool _baseInReach = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something is in range");

        if (other.tag == "Ship")
        {
            _shipInReach = other.GetComponent<ShipCargo>();
            Debug.Log("Other ship is in reach!");
        }

        if (other.tag == "Base")
        {
            _baseInReach = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ship")
        {
            _shipInReach = null;
        }

        if (other.tag == "Base")
        {
            _baseInReach = false;
        }
    }

    void Move()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxisRaw("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        Vector3 desiredVelocity = transform.forward * playerInput.y * _shipValues.maxSpeed;

        if (playerInput.y < 0)
        {
            desiredVelocity = new Vector3();
        }

        float maxSpeedChange = _shipValues.maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);

        float angle = 0;

        float desiredAngle = 90 * playerInput.x;

        angle = Mathf.MoveTowardsAngle(angle, desiredAngle, _shipValues.maxTurnSpeed);

        transform.Rotate(new Vector3(0f, 1f, 0f), angle);

        Vector3 displacement = velocity * Time.deltaTime;
        Vector3 newPosition = transform.localPosition + displacement;

        //TODO remove this
        if (newPosition.x < _shipValues.allowedArea.xMin)
        {
            newPosition.x = _shipValues.allowedArea.xMin;
            velocity.x = -velocity.x * _shipValues.bounciness;
        }
        else if (newPosition.x > _shipValues.allowedArea.xMax)
        {
            newPosition.x = _shipValues.allowedArea.xMax;
            velocity.x = -velocity.x * _shipValues.bounciness;
        }
        if (newPosition.z < _shipValues.allowedArea.yMin)
        {
            newPosition.z = _shipValues.allowedArea.yMin;
            velocity.z = -velocity.z * _shipValues.bounciness;
        }
        else if (newPosition.z > _shipValues.allowedArea.yMax)
        {
            newPosition.z = _shipValues.allowedArea.yMax;
            velocity.z = -velocity.z * _shipValues.bounciness;
        }
        transform.localPosition = newPosition;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _shipInReach)
        {
            _cargo.Scan(_shipInReach);
            _cargo.LootShip(_shipInReach);
        }

        if (Input.GetKeyDown(KeyCode.E) && _baseInReach)
        {
            _cargo.UnloadPersons();
        }
        if (Input.GetKeyDown(KeyCode.U) && _baseInReach)
        {
            _cargo.Upgrade();
        }
    }

	void FixedUpdate()
	{
		Move();
	}
}
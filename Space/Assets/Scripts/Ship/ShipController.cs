using TMPro;
using UnityEngine;
using static System.Collections.Specialized.BitVector32;

public class ShipController : MonoBehaviour
{
	[SerializeField]
    ShipControllerValues _shipValues;

    [SerializeField]
    ShipCargo _cargo;

    [SerializeField]
    ShipCargo _shipInReach;

    [SerializeField]
    private GameObject _ShipRoof;

    [SerializeField]
    private GameObject _CommandbridgeRoof;

    [SerializeField]
    CrewQuarter _quarter;

    Vector3 velocity;

    [SerializeField]
    bool _baseInReach = false;

    GameObject _notificationUI;
    private void Start()
    {
        _notificationUI = GameObject.FindGameObjectWithTag("GameUI").transform.GetChild(0).GetChild(2).gameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ship")
        {
            _shipInReach = other.GetComponent<ShipCargo>();
            Debug.Log("Other ship is in reach!");
            _notificationUI.SetActive(true);
            _notificationUI.transform.GetChild(0).GetComponent<TMP_Text>().text = "Reached another ship! Press E to collect their cargo.";
        }

        if (other.tag == "Base")
        {
            _baseInReach = true;

            _notificationUI.SetActive(true);
            _notificationUI.transform.GetChild(0).GetComponent<TMP_Text>().text = "Reached the station.Press E to unload the passengers. Press U to upgrade your ship.";
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
            _notificationUI.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.E) && _shipInReach && (_shipInReach.GetCargo().Persons > 0 || _shipInReach.GetCargo().Material > 0))
        {
            _cargo.Scan(_shipInReach);
            _cargo.LootShip(_shipInReach);

            _notificationUI.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && _baseInReach)
        {
            _cargo.UnloadPersons();
            GameObject.FindGameObjectWithTag("Loot").GetComponent<TMP_Text>().text = $"{_cargo.GetCargo().Persons}/{_cargo.GetMaxCargo()}\n{_cargo.GetCargo().Material}/{_cargo.GetMaxCargo()}";
        }
        if (Input.GetKeyDown(KeyCode.U) && _baseInReach && _cargo.GetCargo().Material >= 2 * ((_cargo.GetMaxCargo() + 2 - 8) / 2))
        {
            _cargo.Upgrade(2 * ((_cargo.GetMaxCargo() + 2 - 8) / 2));
            Debug.Log("Upgraded ship! You now need: " + 2 * ((_cargo.GetMaxCargo() + 2 - 8) / 2) + " materials to upgrade again.");
            GameObject.FindGameObjectWithTag("Loot").GetComponent<TMP_Text>().text = $"{_cargo.GetCargo().Persons}/{_cargo.GetMaxCargo()}\n{_cargo.GetCargo().Material}/{_cargo.GetMaxCargo()}";
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            _ShipRoof.SetActive(!_ShipRoof.activeSelf);
            _CommandbridgeRoof.SetActive(!_CommandbridgeRoof.activeSelf);
        }
    }

	void FixedUpdate()
	{
        if (!_quarter.GetIsLoading())
        {
            Move();
        }
	}
}
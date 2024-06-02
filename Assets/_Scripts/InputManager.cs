using UnityEngine;

public class InputManager : MonoBehaviour
{
	public static InputManager Instance { get; private set; }

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}
		Instance = this;

	}

	public Vector2 GetMouseScreenPosition()
	{
		return Input.mousePosition;
	}

	public bool GetLeftMouseButtonDown()
	{
		return Input.GetMouseButtonDown(0);
	}

	public bool GetRightMouseButtonDown()
	{
		return Input.GetMouseButtonDown(1);
	}

	public bool GetMiddleMouseButtonDown()
	{
		return Input.GetMouseButtonDown(2);
	}

	public Vector2 GetCameraMoveVector()
	{
		Vector2 inputMoveDir = new Vector2(0, 0);

		if (Input.GetKey(KeyCode.W))
		{
			inputMoveDir.y = +1f;
		}
		if (Input.GetKey(KeyCode.S))
		{
			inputMoveDir.y = -1f;
		}
		if (Input.GetKey(KeyCode.A))
		{
			inputMoveDir.x = -1f;
		}
		if (Input.GetKey(KeyCode.D))
		{
			inputMoveDir.x = +1f;
		}

		return inputMoveDir;
	}

	public float GetCameraRotationDir()
	{
		float rotationVector = 0f;

		if (Input.GetKey(KeyCode.Q))
		{
			rotationVector = -1f;
		}
		if (Input.GetKey(KeyCode.E))
		{
			rotationVector = +1f;
		}

		return rotationVector;
	}

	public float GetMouseScrollDelta()
	{
		float zoomIncrement = 0f;

		if (Input.mouseScrollDelta.y > 0)
		{
			zoomIncrement -= 1f;
		}
		if (Input.mouseScrollDelta.y < 0)
		{
			zoomIncrement += 1f;
		}

		return zoomIncrement;
	}
}

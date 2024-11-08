using UnityEngine;
using UnityEngine.InputSystem;
/*
 �ش� �ڵ�� github push/pull �������� Ȥ�ó� �浹�� �߻��ϴ� �� �׽�Ʈ
��Ʈ�ѷ� �۵� ��Ŀ� ���� ���ʸ� �����ϰ� Ȯ���ϱ� ���� �ڵ�
���� �� ����� Getkeydown ���� ����� �ƴ϶� �ٸ� ����� ����ϴµ�,
�� ���¿� �����ص� ��Ʈ�ѷ��� ���
\XR Interaction Toolkit\3.0.3\XR Device Simulator���� Prefab ���ʿ� �ִ� sheet�� ���� ǥ�� �ִ� ���̵� �� controller��� ���� ������ Ű���ε��� Ȯ���� �� ����.

Play ���¿��� ��Ʈ�ѷ� �� �ϳ��� Ȱ��ȭ �ؼ� ���콺 ���� Ŭ���� ���� �� ���鿡 ť�갡 ����� �浹�� ���� ������ Ȯ��.
-241109 ����
 */
public class CreateCubeOnTriggerPress : MonoBehaviour
{
    public InputActionAsset actionAsset; // Reference to the Input Action Asset
    public float cubeDistance = 1.5f; // Distance in front of the camera to place the cube

    private InputAction triggerAction;

    void Start()
    {
        // Find the action map and the Trigger action
        var actionMap = actionAsset.FindActionMap("Controller");
        triggerAction = actionMap.FindAction("Trigger");

        // Enable the Trigger action
        triggerAction.Enable();

        // Register a callback for when the action is performed
        triggerAction.performed += OnTriggerPressed;
    }

    private void OnDestroy()
    {
        // Unregister the callback when the script is destroyed
        triggerAction.performed -= OnTriggerPressed;
    }

    private void OnTriggerPressed(InputAction.CallbackContext context)
    {
        // Find the main camera's position and forward direction
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            // Calculate the position in front of the camera
            Vector3 spawnPosition = mainCamera.transform.position + mainCamera.transform.forward * cubeDistance;

            // Create a cube at the calculated position
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = spawnPosition;

            // Optionally, set a parent for better organization in the hierarchy
            cube.transform.SetParent(mainCamera.transform);
        }
    }
}

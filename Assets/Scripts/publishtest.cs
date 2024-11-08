using UnityEngine;
using UnityEngine.InputSystem;
/*
 해당 코드는 github push/pull 과정에서 혹시나 충돌이 발생하는 지 테스트
컨트롤러 작동 방식에 대한 기초를 간단하게 확인하기 위한 코드
수업 때 사용한 Getkeydown 등의 방식이 아니라 다른 방식을 사용하는데,
본 에셋에 적용해둔 컨트롤러의 경우
\XR Interaction Toolkit\3.0.3\XR Device Simulator에서 Prefab 왼쪽에 있는 sheet에 번개 표시 있는 아이들 중 controller라고 명명된 곳에서 키바인딩을 확인할 수 있음.

Play 상태에서 컨트롤러 중 하나를 활성화 해서 마우스 왼쪽 클릭을 했을 때 정면에 큐브가 생기면 충돌이 없는 것으로 확인.
-241109 오전
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

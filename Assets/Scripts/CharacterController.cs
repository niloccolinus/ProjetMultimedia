using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float boostSpeed;
    [SerializeField] private InputActionReference moveActionReference;
    [SerializeField] private InputActionReference boostActionReference;

    private void Start()
    {
        moveActionReference.action.Enable();
        boostActionReference.action.Enable();
    }

    private void Update()
    {
        float boost = 1;

        // V�rifie si le boost est activ�
        if (boostActionReference.action.phase == InputActionPhase.Performed)
        {
            boost = boostSpeed;
        }

        // Lecture des inputs de mouvement
        Vector2 frameMovement = moveActionReference.action.ReadValue<Vector2>();
        Vector3 frameMovement3D = new Vector3(frameMovement.x, 0, frameMovement.y);

        // D�placement du personnage
        Vector3 newPos = transform.position + frameMovement3D * movementSpeed * boost * Time.deltaTime;
        transform.position = newPos;

        // Calcul de la direction et rotation
        if (frameMovement3D.sqrMagnitude > 0.01f) // V�rifie un mouvement significatif
        {
            Quaternion targetRotation = Quaternion.LookRotation(frameMovement3D.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}

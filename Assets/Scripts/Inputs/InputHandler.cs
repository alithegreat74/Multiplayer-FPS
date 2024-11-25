using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InputHandling
{
    public class InputHandler : MonoBehaviour
    {
        public static Vector2 Movement;
        public static bool Shoot;
        public static bool Run;
        //We're using the Button Trigger class because we want the jump trigger only be true when pressed not held
        //So we set it's value to true for one frame and then set it back to false to make the player press the jump button everytime
        public static ButtonTrigger Jump;


        private Inputs input;

        private void OnEnable()
        {
            input = new Inputs();
            input.Enable();

            input.Player.Movement.performed += OnMovementPerformed;

            input.Player.Shoot.performed += OnShootPerformed;

            input.Player.Jump.started += OnJumpStarted;

            input.Player.Run.started += OnRunStarted;
        }

        private void OnRunStarted(InputAction.CallbackContext context)
        {
            Run = context.ReadValueAsButton();
        }

        private void OnJumpStarted(InputAction.CallbackContext context)
        {
            StartCoroutine(TriggerButton_Cor(Jump));
        }

        private void OnShootPerformed(InputAction.CallbackContext context)
        {
            Shoot = context.ReadValueAsButton();
        }
        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            Movement = context.ReadValue<Vector2>();
        }

        private IEnumerator TriggerButton_Cor(ButtonTrigger trigger)
        {
            trigger.SetValue(true);
            yield return new WaitForEndOfFrame();
            trigger.SetValue(false);
        }
        
        private void OnDisable()
        {
            input.Disable();
        }
    }

}

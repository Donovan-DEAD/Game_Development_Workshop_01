using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public static class InputHelper
{
    public static float Horizontal()
    {

#if ENABLE_INPUT_SYSTEM
        var kb = Keyboard.current;
        if (kb == null) return 0f;
        float x = 0f;
        if (kb.aKey.isPressed || kb.leftArrowKey.isPressed)  x -= 1;
        if (kb.dKey.isPressed || kb.rightArrowKey.isPressed)  x += 1;
        return x;
#else
        return Input.GetAxisRaw("Horizontal");
#endif
    }
    public static float Vertical()
    {

#if ENABLE_INPUT_SYSTEM
        var kb = Keyboard.current;
        if (kb == null) return 0f;
        float y = 0f;
        if (kb.wKey.isPressed || kb.downArrowKey.isPressed) y -= 1;
        if (kb.sKey.isPressed || kb.upArrowKey.isPressed) y += 1;
        return y;
#else
        return Input.GetAxisRaw("Vertical");
#endif
    }
    
    public static bool JumpPressed()
    {
#if ENABLE_INPUT_SYSTEM
        return Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;
#else
        return Input.GetButtonDown("Jump");
#endif
    }

    public static bool JumpReleased()
    {
#if ENABLE_INPUT_SYSTEM
        return Keyboard.current != null && Keyboard.current.spaceKey.wasReleasedThisFrame;
#else
        return Input.GetButtonUp("Jump");
#endif
    }

    public static Vector2 MouseScreenPosition()
    {
#if ENABLE_INPUT_SYSTEM
        return Mouse.current != null ? Mouse.current.position.ReadValue() : Vector2.zero;
#else
        return Input.mousePosition;
#endif
    }

}

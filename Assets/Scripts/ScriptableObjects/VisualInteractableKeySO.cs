using UnityEngine;
using System;
namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewVisualInteractableKeySO",menuName = "ScriptableObjects/VisualInteractableKey",order = 0)]
    public class VisualInteractableKeySO : ScriptableObject
    {
        public GamepadIcons Xbox;
        public GamepadIcons Ps4;
        public KeyboardIcons Keyboard;
        public Sprite BackgroundSpriteForKeyboard;
    }
    
       [Serializable]
        public class GamepadIcons
        {
            public Sprite buttonSouth;
            public Sprite buttonNorth;
            public Sprite buttonEast;
            public Sprite buttonWest;
            public Sprite startButton;
            public Sprite selectButton;
            public Sprite leftTrigger;
            public Sprite rightTrigger;
            public Sprite leftShoulder;
            public Sprite rightShoulder;
            public Sprite dpad;
            public Sprite dpadUp;
            public Sprite dpadDown;
            public Sprite dpadLeft;
            public Sprite dpadRight;
            public Sprite leftStick;
            public Sprite rightStick;
            public Sprite leftStickPress;
            public Sprite rightStickPress;

            public Sprite GetSprite(string controlPath)
            {
                // From the input system, we get the path of the control on device. So we can just
                // map from that to the sprites we have for gamepads.
                switch (controlPath)
                {
                    case "buttonSouth": return buttonSouth;
                    case "buttonNorth": return buttonNorth;
                    case "buttonEast": return buttonEast;
                    case "buttonWest": return buttonWest;
                    case "start": return startButton;
                    case "select": return selectButton;
                    case "leftTrigger": return leftTrigger;
                    case "rightTrigger": return rightTrigger;
                    case "leftShoulder": return leftShoulder;
                    case "rightShoulder": return rightShoulder;
                    case "dpad": return dpad;
                    case "dpad/up": return dpadUp;
                    case "dpad/down": return dpadDown;
                    case "dpad/left": return dpadLeft;
                    case "dpad/right": return dpadRight;
                    case "leftStick": return leftStick;
                    case "rightStick": return rightStick;
                    case "leftStickPress": return leftStickPress;
                    case "rightStickPress": return rightStickPress;
                }
                return null;
            }
        }

        [Serializable]
        public class KeyboardIcons
        {
            public Sprite enter;
            public Sprite escape;
            public Sprite tab;
            public Sprite ctrl;
            public Sprite space;
            public Sprite alt;
            public Sprite shift;
            public Sprite a;
            public Sprite b;
            public Sprite c;
            public Sprite d;
            public Sprite e;
            public Sprite f;
            public Sprite g;
            public Sprite h;
            public Sprite i;
            public Sprite j;
            public Sprite k;
            public Sprite l;
            public Sprite m;
            public Sprite n;
            public Sprite o;
            public Sprite p;
            public Sprite q;
            public Sprite r;
            public Sprite s;
            public Sprite t;
            public Sprite u;
            public Sprite v;
            public Sprite w;
            public Sprite x;
            public Sprite y;
            public Sprite z;
            public Sprite numpad1;
            public Sprite numpad2;
            public Sprite numpad3;
            public Sprite numpad4;
            public Sprite numpad5;
            public Sprite numpad6;
            public Sprite numpad7;
            public Sprite numpad8;
            public Sprite numpad9;
            public Sprite numpad0;
            
            public Sprite GetSprite(string controlPath)
            {
                // From the input system, we get the path of the control on device. So we can just
                // map from that to the sprites we have for gamepads.
                switch (controlPath)
                {
                    case "enter": return enter;
                    case "escape": return escape;
                    case "control": return ctrl;
                    case "left control": return ctrl;
                    case "right control": return ctrl;
                    case "tab": return tab;
                    case "space": return space;
                    case "alt": return alt;
                    case "right alt": return alt;
                    case "left alt": return alt;
                    case "a": return a;
                    case "b": return b;
                    case "c": return c;
                    case "d": return d;
                    case "e": return e;
                    case "f": return f;
                    case "g": return g;
                    case "h": return h;
                    case "i": return i;
                    case "j": return j;
                    case "k": return k;
                    case "l": return l;
                    case "m": return m;
                    case "n": return n;
                    case "o": return o;
                    case "p": return p;
                    case "q": return q;
                    case "r": return r;
                    case "s": return s;
                    case "t": return t;
                    case "u": return u;
                    case "v": return v;
                    case "w": return w;
                    case "x": return x;
                    case "y": return y;
                    case "z": return z;
                    case "1": return numpad1;
                    case "2": return numpad2;
                    case "3": return numpad3;
                    case "4": return numpad4;
                    case "5": return numpad5;
                    case "6": return numpad6;
                    case "7": return numpad7;
                    case "8": return numpad8;
                    case "9": return numpad9;
                    case "0": return numpad0;
                }
                return null;
            }
        }
}

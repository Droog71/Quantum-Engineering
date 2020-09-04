using UnityEngine;
using System.Collections;

public class cInputInit : MonoBehaviour 
{
	public GUISkin guiSkin;
    void Start()
    {
        cInput.Init();
        cInput.allowDuplicates = true;
        cInput.usePlayerPrefs = true;
        cGUI.cSkin = guiSkin;
        cGUI.windowMaxSize = new Vector2(1024, 600);
        cInput.SetKey("Walk Forward", "W", Keys.UpArrow);
        cInput.SetKey("Walk Backward", "S", Keys.DownArrow);
        cInput.SetKey("Strafe Left", "A", Keys.LeftArrow);
        cInput.SetKey("Strafe Right", "D", Keys.RightArrow);
        cInput.SetKey("Sprint", "LeftShift", Keys.LeftShift);
        cInput.SetKey("Jetpack", "Space", Keys.Space);
        cInput.SetKey("Fire", "Mouse0", Keys.Mouse0);
        cInput.SetKey("Interact", "E", Keys.E);
        cInput.SetKey("Build", "B", Keys.B);
        cInput.SetKey("Stop Building", "Q", Keys.Q);
        cInput.SetKey("Next Item", "Mouse Wheel Up", Keys.X);
        cInput.SetKey("Previous Item", "Mouse Wheel Down", Keys.Z);
        cInput.SetKey("Build Amount +", "RightBracket", Keys.RightBracket);
        cInput.SetKey("Build Amount  -", "LeftBracket", Keys.LeftBracket);
        cInput.SetKey("Place Object", "Mouse1", Keys.Mouse1);
        cInput.SetKey("Collect Object", "F", Keys.F);
        cInput.SetKey("Rotate Block", "R", Keys.R);
        cInput.SetKey("Build Axis", "V", Keys.V);
        cInput.SetKey("Inventory", "I", Keys.I);
        cInput.SetKey("Crafting", "C", Keys.C);
        cInput.SetKey("Market", "M", Keys.M);
        cInput.SetKey("Headlamp", "H", Keys.H);
        cInput.SetKey("Scanner", "F1", Keys.F1);
        cInput.SetKey("Laser Cannon", "F2", Keys.F2);
        cInput.SetKey("Tablet", "F3", Keys.F3);
        cInput.SetKey("Paint Gun", "P", Keys.P);
        cInput.SetKey("Paint Color", "O", Keys.O);
        cInput.SetKey("Crosshair", "T", Keys.T);
    }
}

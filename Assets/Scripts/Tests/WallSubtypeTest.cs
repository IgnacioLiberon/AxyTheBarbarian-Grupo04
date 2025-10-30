using UnityEngine;
using System;

public class WallSubtypeTest : MonoBehaviour
{
    public void RunTest()
    {
        Debug.Log("=== Wall Subtype Enum Test ===");

        string[] testSubtypes = {
            "North", "South", "East", "West",
            "CornerNE", "CornerNW", "CornerSE", "CornerSW",
            "cornerne", "CORNER_NW", "north ", "invalid"
        };

        foreach (var s in testSubtypes)
        {
            bool parsed = Enum.TryParse(s.Trim(), ignoreCase: true, out WallVariant variant);
            if (parsed)
                Debug.Log($"✅ Parsed '{s}' → {variant}");
            else
                Debug.LogWarning($"❌ Could NOT parse subtype '{s}'");
        }

        Debug.Log("=== Test Completed ===");
    }
}

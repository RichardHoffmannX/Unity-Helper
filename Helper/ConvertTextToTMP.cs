#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public static class ConvertTextToTMP
{
    [MenuItem("Tools/UI/Convert Selected Text to TextMeshPro")]
    private static void ConvertSelected()
    {
        GameObject[] selectedObjects = Selection.gameObjects;

        if (selectedObjects == null || selectedObjects.Length == 0)
        {
            Debug.LogWarning("No GameObjects selected.");
            return;
        }

        int convertedCount = 0;

        foreach (GameObject obj in selectedObjects)
        {
            Text oldText = obj.GetComponent<Text>();

            if (oldText == null)
            {
                Debug.LogWarning($"'{obj.name}' does not contain a Unity UI Text component.");
                continue;
            }

            // Save old properties before removing component
            string text = oldText.text;
            Color color = oldText.color;
            float fontSize = oldText.fontSize;

            TextAnchor alignment = oldText.alignment;

            bool raycastTarget = oldText.raycastTarget;
            bool enabled = oldText.enabled;

            FontStyle fontStyle = oldText.fontStyle;

            Undo.RegisterCompleteObjectUndo(obj, "Convert Text to TMP");

            // Remove legacy Text
            Undo.DestroyObjectImmediate(oldText);

            // Add TextMeshProUGUI
            TextMeshProUGUI tmp = Undo.AddComponent<TextMeshProUGUI>(obj);

            // Copy basic settings
            tmp.text = text;
            tmp.color = color;
            tmp.fontSize = fontSize;
            tmp.raycastTarget = raycastTarget;
            tmp.enabled = enabled;

            // Convert alignment
            tmp.alignment = ConvertAlignment(alignment);

            // Convert font style
            tmp.fontStyle = ConvertFontStyle(fontStyle);

            EditorUtility.SetDirty(obj);

            convertedCount++;

            Debug.Log($"Converted '{obj.name}' to TextMeshProUGUI.");
        }

        Debug.Log($"Finished converting {convertedCount} Text object(s) to TextMeshProUGUI.");
    }

    private static TextAlignmentOptions ConvertAlignment(TextAnchor alignment)
    {
        switch (alignment)
        {
            case TextAnchor.UpperLeft:
                return TextAlignmentOptions.TopLeft;

            case TextAnchor.UpperCenter:
                return TextAlignmentOptions.Top;

            case TextAnchor.UpperRight:
                return TextAlignmentOptions.TopRight;

            case TextAnchor.MiddleLeft:
                return TextAlignmentOptions.Left;

            case TextAnchor.MiddleCenter:
                return TextAlignmentOptions.Center;

            case TextAnchor.MiddleRight:
                return TextAlignmentOptions.Right;

            case TextAnchor.LowerLeft:
                return TextAlignmentOptions.BottomLeft;

            case TextAnchor.LowerCenter:
                return TextAlignmentOptions.Bottom;

            case TextAnchor.LowerRight:
                return TextAlignmentOptions.BottomRight;
        }

        return TextAlignmentOptions.Center;
    }

    private static FontStyles ConvertFontStyle(FontStyle style)
    {
        switch (style)
        {
            case FontStyle.Bold:
                return FontStyles.Bold;

            case FontStyle.Italic:
                return FontStyles.Italic;

            case FontStyle.BoldAndItalic:
                return FontStyles.Bold | FontStyles.Italic;

            default:
                return FontStyles.Normal;
        }
    }
}
#endif //UNITY_EDITOR
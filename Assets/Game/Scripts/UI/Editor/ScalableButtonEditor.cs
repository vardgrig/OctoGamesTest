using UnityEditor;
using UnityEditor.UI;

namespace Game.Scripts.UI.Editor
{
    [CustomEditor(typeof(ScalableButton), true), CanEditMultipleObjects]
    public class ScalableButtonEditor : ButtonEditor
    {
        private SerializedProperty _scaleFactorProperty;
        private SerializedProperty _scaleDurationProperty;
        private SerializedProperty _scaleEaseTypeProperty;

        protected override void OnEnable()
        {
            base.OnEnable();

            _scaleFactorProperty = serializedObject.FindProperty("scaleFactor");
            _scaleDurationProperty = serializedObject.FindProperty("scaleDuration");
            _scaleEaseTypeProperty = serializedObject.FindProperty("easeType");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            serializedObject.Update();

            EditorGUILayout.PropertyField(_scaleFactorProperty);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_scaleDurationProperty);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_scaleEaseTypeProperty);
            EditorGUILayout.Space();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
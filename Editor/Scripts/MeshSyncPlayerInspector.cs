using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.FilmInternalUtilities.Editor;
using Unity.MeshSync;
using UnityEditor;
using UnityEngine;

namespace Unity.MeshSync.Editor
{
    [CustomEditor(typeof(MeshSyncPlayer))]
    internal class MeshSyncPlayerInspector : UnityEditor.Editor {
                
       
//----------------------------------------------------------------------------------------------------------------------        
        public virtual void OnEnable() {
            m_asset = target as MeshSyncPlayer;
        }
  
//----------------------------------------------------------------------------------------------------------------------
        
        protected bool DrawPlayerSettings(MeshSyncPlayer t) 
        {
            bool changed   = false;
            var  styleFold = EditorStyles.foldout;
            styleFold.fontStyle = FontStyle.Bold;

            // Asset Sync Settings
            t.foldSyncSettings = EditorGUILayout.Foldout(t.foldSyncSettings, "Asset Sync Settings", true, styleFold);
            MeshSyncPlayerConfig playerConfig = m_asset.GetConfig();
            if (t.foldSyncSettings) {

                changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Sync Visibility",
                    guiFunc: () => EditorGUILayout.Toggle("Visibility", playerConfig.SyncVisibility), 
                    updateFunc: (bool toggle) => { playerConfig.SyncVisibility = toggle; }
                );

                changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Sync Transform",
                    guiFunc: () => EditorGUILayout.Toggle("Transform", playerConfig.SyncTransform), 
                    updateFunc: (bool toggle) => { playerConfig.SyncTransform = toggle; }
                );

                changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Sync Cameras",
                    guiFunc: () => EditorGUILayout.Toggle("Cameras", playerConfig.SyncCameras), 
                    updateFunc: (bool toggle) => { playerConfig.SyncCameras = toggle; }
                );
                
                if (playerConfig.SyncCameras)
                {
                    EditorGUI.indentLevel++;

                    changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Physical Camera Params",
                        guiFunc: () => EditorGUILayout.Toggle("Physical Camera Params", t.GetUsePhysicalCameraParams()), 
                        updateFunc: (bool toggle) => { t.SetUsePhysicalCameraParams(toggle); }
                    );
                    
                    //EditorGUILayout.PropertyField(so.FindProperty("m_useCustomCameraMatrices"), new GUIContent("Custom View/Proj Matrices"));
                    EditorGUI.indentLevel--;
                }
                
                changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Sync Lights",
                    guiFunc: () => EditorGUILayout.Toggle("Lights", playerConfig.SyncLights), 
                    updateFunc: (bool toggle) => { playerConfig.SyncLights = toggle; }
                );

                changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Sync Meshes",
                    guiFunc: () => EditorGUILayout.Toggle("Meshes", playerConfig.SyncMeshes), 
                    updateFunc: (bool toggle) => { playerConfig.SyncMeshes = toggle; }
                );
                

                EditorGUI.indentLevel++;
                changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Update Mesh Colliders",
                    guiFunc: () => EditorGUILayout.Toggle("Update Mesh Colliders", playerConfig.UpdateMeshColliders), 
                    updateFunc: (bool toggle) => { playerConfig.UpdateMeshColliders = toggle; }
                );
                EditorGUI.indentLevel--;

                //EditorGUILayout.PropertyField(so.FindProperty("m_syncPoints"), new GUIContent("Points"));
                changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Sync Materials",
                    guiFunc: () => EditorGUILayout.Toggle("Materials", playerConfig.SyncMaterials), 
                    updateFunc: (bool toggle) => { playerConfig.SyncMaterials = toggle; }
                );
                
                EditorGUI.indentLevel++;
                changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Find From Asset Database",
                    guiFunc: () => EditorGUILayout.Toggle("Find From AssetDatabase", playerConfig.FindMaterialFromAssets), 
                    updateFunc: (bool toggle) => { playerConfig.FindMaterialFromAssets = toggle; }
                );
                
                EditorGUI.indentLevel--;

                EditorGUILayout.Space();
            }

            // Import Settings
            t.foldImportSettings = EditorGUILayout.Foldout(t.foldImportSettings, "Import Settings", true, styleFold);
            if (t.foldImportSettings)
            {
                changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Animation Interpolation",
                    guiFunc: () => EditorGUILayout.Popup(new GUIContent("Animation Interpolation"), playerConfig.AnimationInterpolation, m_animationInterpolationEnums), 
                    updateFunc: (int val) => { playerConfig.AnimationInterpolation = val; }
                );
                

                changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Keyframe Reduction",
                    guiFunc: () => EditorGUILayout.Toggle("Keyframe Reduction", playerConfig.KeyframeReduction), 
                    updateFunc: (bool toggle) => { playerConfig.KeyframeReduction = toggle; }
                );
                
                if (playerConfig.KeyframeReduction)
                {
                    EditorGUI.indentLevel++;
                    
                    changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Threshold",
                        guiFunc: () => EditorGUILayout.FloatField("Threshold", playerConfig.ReductionThreshold), 
                        updateFunc: (float val) => { playerConfig.ReductionThreshold = val; }
                    );
                    
                    changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Erase Flat Curves",
                        guiFunc: () => EditorGUILayout.Toggle("Erase Flat Curves", playerConfig.ReductionEraseFlatCurves), 
                        updateFunc: (bool toggle) => { playerConfig.ReductionEraseFlatCurves = toggle; }
                    );
                    EditorGUI.indentLevel--;
                }
                
                changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Z-Up Correction",
                    guiFunc: () => EditorGUILayout.Popup(new GUIContent("Z-Up Correction"), playerConfig.ZUpCorrection, m_zUpCorrectionEnums), 
                    updateFunc: (int val) => { playerConfig.ZUpCorrection = val; }
                );

                EditorGUILayout.Space();
            }

            // Misc
            t.foldMisc = EditorGUILayout.Foldout(t.foldMisc, "Misc", true, styleFold);
            if (t.foldMisc)
            {
                changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Sync Material List",
                    guiFunc: () => EditorGUILayout.Toggle("Sync Material List", playerConfig.SyncMaterialList), 
                    updateFunc: (bool toggle) => { playerConfig.SyncMaterialList = toggle; }
                );

                changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Progressive Display",
                    guiFunc: () => EditorGUILayout.Toggle("Progressive Display", playerConfig.ProgressiveDisplay), 
                    updateFunc: (bool toggle) => { playerConfig.ProgressiveDisplay = toggle; }
                );
                
                
                changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Logging",
                    guiFunc: () => EditorGUILayout.Toggle("Logging", playerConfig.Logging), 
                    updateFunc: (bool toggle) => { playerConfig.Logging = toggle; }
                );

                changed |= EditorGUIDrawerUtility.DrawUndoableGUI(target,"MeshSync: Profiling",
                    guiFunc: () => EditorGUILayout.Toggle("Profiling", playerConfig.Profiling), 
                    updateFunc: (bool toggle) => { playerConfig.Profiling = toggle; }
                );
                
                EditorGUILayout.Space();
            }

            return changed;
        }

        public static void DrawMaterialList(MeshSyncPlayer t, bool allowFold = true)
        {
            Action drawInExportButton = () =>
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Import List", GUILayout.Width(110.0f)))
                {
                    var path = EditorUtility.OpenFilePanel("Import material list", "Assets", "asset");
                    t.ImportMaterialList(path);
                }
                if (GUILayout.Button("Export List", GUILayout.Width(110.0f)))
                {
                    var path = EditorUtility.SaveFilePanel("Export material list", "Assets", t.name + "_MaterialList", "asset");
                    t.ExportMaterialList(path);
                }
                GUILayout.EndHorizontal();
            };

            if (allowFold)
            {
                var styleFold = EditorStyles.foldout;
                styleFold.fontStyle = FontStyle.Bold;
                t.foldMaterialList = EditorGUILayout.Foldout(t.foldMaterialList, "Materials", true, styleFold);
                if (t.foldMaterialList)
                {
                    DrawMaterialListElements(t);
                    drawInExportButton();
                    if (GUILayout.Button("Open Material Window", GUILayout.Width(160.0f)))
                        MaterialWindow.Open(t);
                    EditorGUILayout.Space();
                }
            }
            else
            {
                GUILayout.Label("Materials", EditorStyles.boldLabel);
                DrawMaterialListElements(t);
                drawInExportButton();
            }
        }

        static void DrawMaterialListElements(MeshSyncPlayer t)
        {
            // calculate label width
            float labelWidth = 60; // minimum
            {
                var style = GUI.skin.box;
                foreach (var md in t.materialList)
                {
                    var size = style.CalcSize(new GUIContent(md.name));
                    labelWidth = Mathf.Max(labelWidth, size.x);
                }
                // 100: margin for color and material field
                labelWidth = Mathf.Min(labelWidth, EditorGUIUtility.currentViewWidth - 100);
            }

            foreach (var md in t.materialList)
            {
                var rect = EditorGUILayout.BeginHorizontal();
                EditorGUI.DrawRect(new Rect(rect.x, rect.y, 16, 16), md.color);
                EditorGUILayout.LabelField("", GUILayout.Width(16));
                EditorGUILayout.LabelField(md.name, GUILayout.Width(labelWidth));
                {
                    var tmp = EditorGUILayout.ObjectField(md.material, typeof(Material), true) as Material;
                    if (tmp != md.material)
                        t.AssignMaterial(md, tmp);
                }
                EditorGUILayout.EndHorizontal();
            }
        }


//----------------------------------------------------------------------------------------------------------------------        

        protected static bool DrawAnimationTweak(MeshSyncPlayer player) {
            bool changed = false;
            
            GUIStyle styleFold = EditorStyles.foldout;
            styleFold.fontStyle = FontStyle.Bold;
            player.foldAnimationTweak = EditorGUILayout.Foldout(player.foldAnimationTweak, "Animation Tweak", true, styleFold);
            if (player.foldAnimationTweak) {
                MeshSyncPlayerConfig config = player.GetConfig();
                AnimationTweakSettings animationTweakSettings = config.GetAnimationTweakSettings();
                
                float               frameRate = 30.0f;
                List<AnimationClip> clips     = player.GetAnimationClips();
                if (clips.Count > 0) {
                    frameRate = clips[0].frameRate;                    
                }

                {
                    // Override Frame Rate
                    GUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Override Frame Rate", EditorStyles.boldLabel);
                    EditorGUI.indentLevel++;
                    
                    changed |= EditorGUIDrawerUtility.DrawUndoableGUI(player,"MeshSync: Frame Rate",
                        guiFunc: () => EditorGUILayout.FloatField("Frame Rate", frameRate), 
                        updateFunc: (float val) => {
                            if (val > 0) {
                                ApplyFrameRate(clips, val);                                                    
                            }
                        }
                    );                                       
                    EditorGUI.indentLevel--;
                    GUILayout.EndVertical();                    
                }
                
                

                // Time Scale
                {
                    GUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Time Scale", EditorStyles.boldLabel);
                    EditorGUI.indentLevel++;
                    float prevTimeScale  = animationTweakSettings.TimeScale;
                    float prevTimeOffset = animationTweakSettings.TimeOffset;
                    
                    changed |= EditorGUIDrawerUtility.DrawUndoableGUI(player,"MeshSync: Scale",
                        guiFunc: () => EditorGUILayout.FloatField("Scale", animationTweakSettings.TimeScale), 
                        updateFunc: (float val) => { animationTweakSettings.TimeScale = val; }
                    );
                    
                    
                    changed |= EditorGUIDrawerUtility.DrawUndoableGUI(player,"MeshSync: Offset",
                        guiFunc: () => EditorGUILayout.FloatField("Offset", animationTweakSettings.TimeOffset), 
                        updateFunc: (float val) => { animationTweakSettings.TimeOffset = val; }
                    );                    
                    if (!Mathf.Approximately(prevTimeScale, animationTweakSettings.TimeScale) ||
                        !Mathf.Approximately(prevTimeOffset, animationTweakSettings.TimeOffset)
                    ) 
                    {                    
                        ApplyTimeScale(clips, animationTweakSettings.TimeScale, 
                            animationTweakSettings.TimeOffset
                        );                   
                    
                    }                    
                    EditorGUI.indentLevel--;
                    GUILayout.EndVertical();                    
                }

                // Drop Keyframes 
                {
                    GUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Drop Keyframes", EditorStyles.boldLabel);
                    EditorGUI.indentLevel++;
                    int prevDropStep = animationTweakSettings.DropStep;
                    
                    changed |= EditorGUIDrawerUtility.DrawUndoableGUI(player,"MeshSync: Step",
                        guiFunc: () => EditorGUILayout.IntField("Step", animationTweakSettings.DropStep), 
                        updateFunc: (int val) => { animationTweakSettings.DropStep = val; }
                    );
                    
                    if (prevDropStep != animationTweakSettings.DropStep && animationTweakSettings.DropStep > 1) {
                        ApplyDropKeyframes(clips, animationTweakSettings.DropStep);                                        
                    }
                    EditorGUI.indentLevel--;
                    GUILayout.EndVertical();                    
                }

                // Keyframe Reduction
                {
                    GUILayout.BeginVertical("Box");
                    EditorGUILayout.LabelField("Keyframe Reduction", EditorStyles.boldLabel);
                    EditorGUI.indentLevel++;
                    float prevReductionThreshold = animationTweakSettings.ReductionThreshold;
                    bool  prevEraseFlatCurves    = animationTweakSettings.EraseFlatCurves;
                    
                    changed |= EditorGUIDrawerUtility.DrawUndoableGUI(player,"MeshSync: Threshold",
                        guiFunc: () => EditorGUILayout.FloatField("Threshold", animationTweakSettings.ReductionThreshold), 
                        updateFunc: (float val) => {
                            animationTweakSettings.ReductionThreshold = val;
                            ApplyKeyframeReduction(clips, val, animationTweakSettings.EraseFlatCurves);                                        
                        }
                    );                   
                    
                    changed |= EditorGUIDrawerUtility.DrawUndoableGUI(player,"MeshSync: Erase Flat Curves",
                        guiFunc: () => EditorGUILayout.Toggle("Erase Flat Curves", animationTweakSettings.EraseFlatCurves), 
                        updateFunc: (bool toggle) => {
                            animationTweakSettings.EraseFlatCurves = toggle; 
                            ApplyKeyframeReduction(clips, animationTweakSettings.ReductionThreshold, toggle);                                                                    
                        }
                    );
                    
                    EditorGUI.indentLevel--;
                    GUILayout.EndVertical();                    
                }

                EditorGUILayout.Space();
            }

            return changed;
        }

//----------------------------------------------------------------------------------------------------------------------
        
        private static void ApplyFrameRate(IEnumerable<AnimationClip> clips, float frameRate) {
            foreach (AnimationClip clip in clips) {
                Undo.RegisterCompleteObjectUndo(clip, "ApplyFrameRate");
                clip.frameRate = frameRate;

                //Debug.Log("Applied frame rate to " + AssetDatabase.GetAssetPath(clip));
            }
            // repaint animation window
            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
        }

//----------------------------------------------------------------------------------------------------------------------

        private static void ApplyTimeScale(IEnumerable<AnimationClip> clips, float timeScale, float timeOffset) {
            foreach (AnimationClip clip in clips) {
                List<AnimationCurve>     curves   = new List<AnimationCurve>();
                List<EditorCurveBinding> bindings = new List<EditorCurveBinding>();
                AnimationEvent[]         events   = AnimationUtility.GetAnimationEvents(clip);

                // gather curves
                bindings.AddRange(AnimationUtility.GetCurveBindings(clip));
                bindings.AddRange(AnimationUtility.GetObjectReferenceCurveBindings(clip));
                foreach (var b in bindings)
                    curves.Add(AnimationUtility.GetEditorCurve(clip, b));

                int eventCount = events.Length;
                int curveCount = curves.Count;

                // transform keys/events
                foreach (AnimationCurve curve in curves)
                {
                    Keyframe[] keys = curve.keys;
                    int keyCount = keys.Length;
                    for (int ki = 0; ki < keyCount; ++ki)
                        keys[ki].time = keys[ki].time * timeScale + timeOffset;
                    curve.keys = keys;
                }
                for (int ei = 0; ei < eventCount; ++ei)
                    events[ei].time = events[ei].time * timeScale + timeOffset;

                // apply changes to clip
                Undo.RegisterCompleteObjectUndo(clip, "ApplyTimeScale");
                clip.frameRate = clip.frameRate / timeScale;
                clip.events = events;
                for (int ci = 0; ci < curveCount; ++ci)
                    Misc.SetCurve(clip, bindings[ci], curves[ci]);

                //Debug.Log("Applied time scale to " + AssetDatabase.GetAssetPath(clip));
            }

            // repaint animation window
            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
        }

//----------------------------------------------------------------------------------------------------------------------        
        private static void ApplyDropKeyframes(IEnumerable<AnimationClip> clips, int step) {
            Assert.IsTrue(step > 1);

            foreach (AnimationClip clip in clips)
            {
                List<AnimationCurve>     curves   = new List<AnimationCurve>();
                List<EditorCurveBinding> bindings = new List<EditorCurveBinding>();

                // gather curves
                bindings.AddRange(AnimationUtility.GetCurveBindings(clip));
                bindings.AddRange(AnimationUtility.GetObjectReferenceCurveBindings(clip));
                foreach (EditorCurveBinding b in bindings)
                    curves.Add(AnimationUtility.GetEditorCurve(clip, b));

                int curveCount = curves.Count;

                // transform keys/events
                foreach (var curve in curves)
                {
                    Keyframe[]     keys     = curve.keys;
                    int            keyCount = keys.Length;
                    List<Keyframe> newKeys  = new List<Keyframe>();
                    for (int ki = 0; ki < keyCount; ki += step)
                        newKeys.Add(keys[ki]);
                    curve.keys = newKeys.ToArray();
                }
 
                // apply changes to clip
                Undo.RegisterCompleteObjectUndo(clip, "ApplyDropKeyframes");
                for (int ci = 0; ci < curveCount; ++ci)
                    Misc.SetCurve(clip, bindings[ci], curves[ci]);

                //Debug.Log("Applied drop keyframes to " + AssetDatabase.GetAssetPath(clip));
            }

            // repaint animation window
            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
        }
        
//----------------------------------------------------------------------------------------------------------------------        

        private static void ApplyKeyframeReduction(IEnumerable<AnimationClip> clips, float threshold, bool eraseFlatCurves)
        {
            foreach (var clip in clips)
            {
                List<AnimationCurve>     curves   = new List<AnimationCurve>();
                List<EditorCurveBinding> bindings = new List<EditorCurveBinding>();

                // gather curves
                bindings.AddRange(AnimationUtility.GetCurveBindings(clip));
                bindings.AddRange(AnimationUtility.GetObjectReferenceCurveBindings(clip));
                foreach (var b in bindings)
                    curves.Add(AnimationUtility.GetEditorCurve(clip, b));

                int curveCount = curves.Count;

                // transform keys/events
                foreach (var curve in curves)
                    Misc.KeyframeReduction(curve, threshold, eraseFlatCurves);

                // apply changes to clip
                Undo.RegisterCompleteObjectUndo(clip, "ApplyKeyframeReduction");
                for (int ci = 0; ci < curveCount; ++ci)
                    Misc.SetCurve(clip, bindings[ci], curves[ci]);

                //Debug.Log("Applied keyframe reduction to " + AssetDatabase.GetAssetPath(clip));
            }

            // repaint animation window
            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
        }


        public static void DrawExportAssets(MeshSyncPlayer t)
        {
            var style = EditorStyles.foldout;
            style.fontStyle = FontStyle.Bold;
            t.foldExportAssets = EditorGUILayout.Foldout(t.foldExportAssets, "Export Assets", true, style);
            if (t.foldExportAssets)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Export Meshes", GUILayout.Width(160.0f)))
                    t.ExportMeshes();

                if (GUILayout.Button("Export Materials", GUILayout.Width(160.0f)))
                    t.ExportMaterials();
                GUILayout.EndHorizontal();
            }
            EditorGUILayout.Space();
        }

        public static void DrawPluginVersion() {
            EditorGUILayout.LabelField("Plugin Version: " + MeshSyncPlayer.GetPluginVersion());
        }

        
//----------------------------------------------------------------------------------------------------------------------

        private MeshSyncPlayer m_asset = null;
        private readonly string[] m_animationInterpolationEnums = System.Enum.GetNames( typeof( InterpolationMode ) );
        private readonly string[] m_zUpCorrectionEnums          = System.Enum.GetNames( typeof( ZUpCorrectionMode ) );

    }
} // end namespace

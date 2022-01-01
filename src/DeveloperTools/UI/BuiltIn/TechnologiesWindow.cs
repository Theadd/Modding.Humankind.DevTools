using Amplitude.Framework;
using Amplitude.Mercury.Data.Simulation;
using Amplitude.Mercury.Interop;
using Amplitude.Mercury.Runtime;
using Amplitude.Mercury.Sandbox;
using Amplitude.Mercury.Simulation;
using Amplitude.Mercury.UI;
using Amplitude.Mercury.UI.Helpers;
using System;
using Modding.Humankind.DevTools.Core;
using UnityEngine;

namespace Modding.Humankind.DevTools.DeveloperTools.UI
{
    public class TechnologiesWindow : UIToolWindow
    {
        public override string WindowGUIStyle { get; set; } = "PopupWindow";

        public override string WindowTitle { get; set; } = "TECHNOLOGIES";
        public override Rect WindowRect { get; set; } = new Rect(130f, 420f, 780f, 500f);
        
        private Vector2 scrollPosition;
        private bool enableTechnologyItemsDrag;
        private int originalLabelFontSize;

        protected Amplitude.Framework.Runtime.IRuntimeService RuntimeService { get; private set; }

        public override void OnGUIStyling()
        {
            base.OnGUIStyling();
            originalLabelFontSize = UIManager.DefaultSkin.label.fontSize;
            UIManager.DefaultSkin.label.fontSize = 13;
            GUI.backgroundColor = new Color32(255, 255, 255, 205);
        }

        public override void OnDrawUI()
        {
            WindowUtils.DrawWindowTitleBar(this);
            GUILayout.BeginVertical("Widget.ClientArea");
                
                OnDrawWindowContent();
                
                GUILayout.Space(10f);
            GUILayout.EndVertical();
            
            UIManager.DefaultSkin.label.fontSize = originalLabelFontSize;
        }


        protected void OnDrawWindowContent()
        {
            if (this.RuntimeService == null)
            {
                using (new GUILayout.VerticalScope((GUIStyle) "Widget.ClientArea", Array.Empty<GUILayoutOption>()))
                    GUILayout.Label("Waiting for the runtime service...");
                if (UnityEngine.Event.current.type != UnityEngine.EventType.Repaint)
                    return;
                this.RuntimeService = Services.GetService<Amplitude.Framework.Runtime.IRuntimeService>();
            }
            else if (this.RuntimeService.Runtime == null || !this.RuntimeService.Runtime.HasBeenLoaded ||
                     this.RuntimeService.Runtime.FiniteStateMachine.CurrentState == null)
            {
                using (new GUILayout.VerticalScope((GUIStyle) "Widget.ClientArea", Array.Empty<GUILayoutOption>()))
                    GUILayout.Label("Waiting for the runtime...");
            }
            else if (this.RuntimeService.Runtime.FiniteStateMachine.CurrentState.GetType() !=
                     typeof(RuntimeState_InGame))
            {
                using (new GUILayout.VerticalScope((GUIStyle) "Widget.ClientArea", Array.Empty<GUILayoutOption>()))
                    GUILayout.Label("Waiting for the runtime state...");
            }
            else if (!Amplitude.Mercury.Presentation.Presentation.HasBeenStarted)
            {
                using (new GUILayout.VerticalScope((GUIStyle) "Widget.ClientArea", Array.Empty<GUILayoutOption>()))
                    GUILayout.Label("Waiting for the presentation...");
            }
            else if (Snapshots.GameSnapshot == null || Snapshots.TechnologyScreenSnapshot == null)
            {
                using (new GUILayout.VerticalScope((GUIStyle) "Widget.ClientArea", Array.Empty<GUILayoutOption>()))
                    GUILayout.Label("Waiting for snapshots...");
            }
            else
            {
                if (!Snapshots.TechnologyScreenSnapshot.IsActive(
                    TechnologyScreenSnapshot.ActivationFlags.FloatingWindow))
                    Snapshots.TechnologyScreenSnapshot.Start(TechnologyScreenSnapshot.ActivationFlags.FloatingWindow);
                using (new GUILayout.VerticalScope((GUIStyle) "Widget.ClientArea", new GUILayoutOption[1]
                {
                    GUILayout.Height(500f)
                }))
                {
                    using (new GUILayout.HorizontalScope(Array.Empty<GUILayoutOption>()))
                    {
                        GUILayout.Label("Invest research ");
                        GUI.enabled = true;
                        if (GUILayout.Button("+1", (GUIStyle) "PopupWindow.ToolbarButton"))
                            SandboxManager.PostOrder((Order) new OrderInvestResearch()
                            {
                                Gain = 1
                            });
                        if (GUILayout.Button("+10", (GUIStyle) "PopupWindow.ToolbarButton"))
                            SandboxManager.PostOrder((Order) new OrderInvestResearch()
                            {
                                Gain = 10
                            });
                        if (GUILayout.Button("+100", (GUIStyle) "PopupWindow.ToolbarButton"))
                            SandboxManager.PostOrder((Order) new OrderInvestResearch()
                            {
                                Gain = 100
                            });
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("UnlockAll", (GUIStyle) "PopupWindow.ToolbarButton", GUILayout.Width(80f)))
                            SandboxManager.PostOrder((EditorOrder) new EditorOrderCompleteAllTechnology()
                            {
                                EmpireIndex = (int) Snapshots.GameSnapshot.PresentationData.LocalEmpireInfo.EmpireIndex
                            });
                    }

                    GUILayout.Space(5f);
                    this.scrollPosition = GUILayout.BeginScrollView(this.scrollPosition);
                    IDatabase<TechnologyDefinition> database = Databases.GetDatabase<TechnologyDefinition>();
                    TechnologyInfo[] technologyInfo1 =
                        Snapshots.TechnologyScreenSnapshot.PresentationData.TechnologyInfo;
                    int length = technologyInfo1.Length;
                    for (int index = 0; index < length; ++index)
                    {
                        TechnologyInfo technologyInfo2 = technologyInfo1[index];
                        using (new GUILayout.HorizontalScope(Array.Empty<GUILayoutOption>()))
                        {
                            GUI.enabled = technologyInfo2.TechnologyState != TechnologyStates.LockedByEra &&
                                          technologyInfo2.TechnologyState != TechnologyStates.LockedByTechnology;
                            if (database.GetValue(technologyInfo2.TechnologyDefinitionName).Visibility !=
                                TechnologyDefinition.VisibilityFlags.Never)
                            {
                                string localizedTitle =
                                    R.Text.GetLocalizedTitle(technologyInfo2.TechnologyDefinitionName);
                                GUILayout.Label(string.Format("{0} ({1})",
                                    (object) technologyInfo2.TechnologyDefinitionName.ToString(),
                                    (object) localizedTitle));
                                GUILayout.FlexibleSpace();
                                GUILayout.Label("| Cost: " + ((int) technologyInfo2.ResearchCost).ToString(),
                                    GUILayout.Width(130f));
                                GUILayout.Label(
                                    "| Progress: " +
                                    ((int) (technologyInfo2.ResearchInvestedInPercent * 100)).ToString() + "%",
                                    GUILayout.Width(100f));
                                GUILayout.Label("| " + technologyInfo2.TechnologyState.ToString(),
                                    GUILayout.Width(70f));
                                if (technologyInfo2.IsInQueue)
                                {
                                    GUI.enabled = true;
                                    if (GUILayout.Button("Remove", (GUIStyle) "PopupWindow.ToolbarButton",
                                        GUILayout.Width(50f)))
                                        SandboxManager.PostOrder((Order) new OrderRemoveTechnologyAt()
                                        {
                                            TechnologyIndexInQueue = technologyInfo2.IndexInQueue
                                        });
                                }
                                else
                                {
                                    GUI.enabled = technologyInfo2.TechnologyState == TechnologyStates.Available;
                                    if (GUILayout.Button("Queue", (GUIStyle) "PopupWindow.ToolbarButton",
                                        GUILayout.Width(50f)))
                                        SandboxManager.PostOrder((Order) new OrderEnqueueTechnology()
                                        {
                                            TechnologyName = technologyInfo2.TechnologyDefinitionName
                                        });
                                }

                                GUI.enabled = technologyInfo2.TechnologyState != TechnologyStates.Completed;
                                if (GUILayout.Button("Unlock", (GUIStyle) "PopupWindow.ToolbarButton",
                                    GUILayout.Width(50f)))
                                    SandboxManager.PostOrder((EditorOrder) new EditorOrderCompleteTechnology()
                                    {
                                        EmpireIndex = (int) Snapshots.GameSnapshot.PresentationData.LocalEmpireInfo
                                            .EmpireIndex,
                                        TechnologyName = technologyInfo2.TechnologyDefinitionName
                                    });
                            }
                        }
                    }

                    GUI.enabled = true;
                    GUILayout.EndScrollView();
                    GUILayout.Space(5f);
                    GUILayout.Label("Technology Screen Options");
                    TechnologyScreen window = WindowsUtils.GetWindow<TechnologyScreen>();
                    GUI.enabled = (UnityEngine.Object) window != (UnityEngine.Object) null;
                    bool flag = GUILayout.Toggle(this.enableTechnologyItemsDrag, "Enable TechnologyItem Drag");
                    if (flag != this.enableTechnologyItemsDrag)
                    {
                        window.SetEnableTechnologyItemsDrag(flag);
                        this.enableTechnologyItemsDrag = flag;
                    }

                    GUI.enabled = true;
                }
            }
        }
    }
}
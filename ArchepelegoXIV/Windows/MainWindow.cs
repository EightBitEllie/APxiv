using System;
using System.Numerics;
using ArchepelegoXIV;
using ArchepelegoXIV.Rando;
using Dalamud.Game.DutyState;
using Dalamud.Interface.Windowing;
using ImGuiNET;
using ImGuiScene;

namespace SamplePlugin.Windows;

public class MainWindow : Window, IDisposable
{
    private readonly ApState state;
    private readonly Plugin Plugin;

    public MainWindow(Plugin plugin, ApState state) : base(
        "Archipelego", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        this.SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = new Vector2(375, 330),
            MaximumSize = new Vector2(float.MaxValue, float.MaxValue)
        };

        this.state = state;
        this.Plugin = plugin;
    }

    public void Dispose()
    {
        
    }

    public override void Draw()
    {
        ImGui.Text($"The AP server is at {this.Plugin.Configuration.Connection} (Connected: {this.state.Connected})");

        if (ImGui.Button("Show Settings"))
        {
            this.Plugin.DrawConfigUI();
        }

        ImGui.Spacing();

        ImGui.Text($"DutyState: {state.DebugText}");
        ImGui.Text($"Current location in logic: {RegionContainer.CanReach(state, state.territoryName)}");
        ImGui.Text($"Available Checks:");
        ImGui.Indent(55);
        if (state.MissingLocations != null)
            foreach (var location in state.MissingLocations)
            {
                if (location.IsAccessible() && !location.Cleared)
                    ImGui.Text($"{location.Name}");
            }

        ImGui.Unindent(55);
    }
}

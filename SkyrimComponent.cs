﻿using LiveSplit.Model;
using LiveSplit.TimeFormatters;
using LiveSplit.UI.Components;
using LiveSplit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;
using System.Diagnostics;

namespace LiveSplit.Skyrim
{
    class SkyrimComponent : IComponent
    {
        public string ComponentName
        {
            get { return "Skyrim"; }
        }

        public IDictionary<string, Action> ContextMenuControls { get; protected set; }
        protected InfoTimeComponent InternalComponent { get; set; }
        public SkyrimSettings Settings { get; set; }

        public bool Disposed { get; private set; }
        public bool IsLayoutComponent { get; private set; }

        private TimerModel _timer;
        private GameMemory _gameMemory;
        private LiveSplitState _state;
        private GraphicsCache _cache;

        public SkyrimComponent(LiveSplitState state, bool isLayoutComponent)
        {
            _state = state;
            this.IsLayoutComponent = isLayoutComponent;

            this.Settings = new SkyrimSettings();
            this.ContextMenuControls = new Dictionary<String, Action>();
            this.InternalComponent = new InfoTimeComponent(null, null, new RegularTimeFormatter(TimeAccuracy.Hundredths));

            _cache = new GraphicsCache();
            _timer = new TimerModel { CurrentState = state };

            _gameMemory = new GameMemory(this.Settings);
            _gameMemory.OnFirstLevelLoading += gameMemory_OnFirstLevelLoading;
            _gameMemory.OnPlayerGainedControl += gameMemory_OnPlayerGainedControl;
            _gameMemory.OnLoadStarted += gameMemory_OnLoadStarted;
            _gameMemory.OnLoadFinished += gameMemory_OnLoadFinished;
            // _gameMemory.OnLoadScreenStarted += gameMemory_OnLoadScreenStarted;
            // _gameMemory.OnLoadScreenFinished += gameMemory_OnLoadScreenFinished;
            _gameMemory.OnSplitCompleted += gameMemory_OnSplitCompleted;
            state.OnReset += state_OnReset;
            _gameMemory.StartMonitoring();
        }

        public void Dispose()
        {
            this.Disposed = true;

            _state.OnReset -= state_OnReset;

            if (_gameMemory != null)
            {
                _gameMemory.Stop();
            }
        }

        void state_OnReset(object sender, TimerPhase e)
        {
            _gameMemory.resetSplitStates();
        }

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            if (!this.Settings.DrawWithoutLoads)
            {
                return;
            }

            this.InternalComponent.TimeValue =
                state.CurrentTime[state.CurrentTimingMethod == TimingMethod.GameTime
                    ? TimingMethod.RealTime : TimingMethod.GameTime];
            this.InternalComponent.InformationName = state.CurrentTimingMethod == TimingMethod.GameTime
                ? "Real Time" : "Without Loads";

            _cache.Restart();
            _cache["TimeValue"] = this.InternalComponent.ValueLabel.Text;
            _cache["TimingMethod"] = state.CurrentTimingMethod;
            if (invalidator != null && _cache.HasChanged)
            {
                invalidator.Invalidate(0f, 0f, width, height);
            }
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region region)
        {
            this.PrepareDraw(state);
            this.InternalComponent.DrawVertical(g, state, width, region);
        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region region)
        {
            this.PrepareDraw(state);
            this.InternalComponent.DrawHorizontal(g, state, height, region);
        }

        void PrepareDraw(LiveSplitState state)
        {
            this.InternalComponent.NameLabel.ForeColor = state.LayoutSettings.TextColor;
            this.InternalComponent.ValueLabel.ForeColor = state.LayoutSettings.TextColor;
            this.InternalComponent.NameLabel.HasShadow = this.InternalComponent.ValueLabel.HasShadow = state.LayoutSettings.DropShadows;
        }

        void gameMemory_OnFirstLevelLoading(object sender, EventArgs e)
        {
            if (this.Settings.AutoStart)
            {
                _timer.Reset();
            }
        }

        void gameMemory_OnPlayerGainedControl(object sender, EventArgs e)
        {
            if (this.Settings.AutoStart)
            {
                _timer.Start();
            }
        }

        void gameMemory_OnLoadStarted(object sender, EventArgs e)
        {
            _state.IsGameTimePaused = true;
        }

        void gameMemory_OnLoadFinished(object sender, EventArgs e)
        {
            _state.IsGameTimePaused = false;
        }

        // void gameMemory_OnLoadScreenStarted(object sender, EventArgs e)
        // {
        //     // Nothing to do
        // }

        // void gameMemory_OnLoadScreenFinished(object sender, EventArgs e)
        // {
        //     // Nothing to do
        // }

        void gameMemory_OnSplitCompleted(object sender, GameMemory.SplitArea split, uint frame)
        {
            Debug.WriteLineIf(split != GameMemory.SplitArea.None, String.Format("[NoLoads] Trying to split {0} with {1} preset, State: {2} - {3}", split, this.Settings.AnyPercentPreset, _gameMemory.splitStates[(int)split], frame));
            if (_state.CurrentPhase == TimerPhase.Running && !_gameMemory.splitStates[(int)split] &&
                ((split == GameMemory.SplitArea.Helgen && this.Settings.Helgen) ||
                (split == GameMemory.SplitArea.Whiterun && this.Settings.Whiterun) ||
                (split == GameMemory.SplitArea.ThalmorEmbassy && this.Settings.ThalmorEmbassy) ||
                (split == GameMemory.SplitArea.Esbern && this.Settings.Esbern) ||
                (split == GameMemory.SplitArea.Riverwood && this.Settings.Riverwood) ||
                (split == GameMemory.SplitArea.TheWall && this.Settings.TheWall) ||
                (split == GameMemory.SplitArea.Septimus && this.Settings.Septimus) ||
                (split == GameMemory.SplitArea.MzarkTower && this.Settings.MzarkTower) ||
                (split == GameMemory.SplitArea.ClearSky && this.Settings.ClearSky) ||
                (split == GameMemory.SplitArea.Alduin1 && this.Settings.Alduin1) ||
                (split == GameMemory.SplitArea.HighHrothgar && this.Settings.HighHrothgar) ||
                (split == GameMemory.SplitArea.Solitude && this.Settings.Solitude) ||
                (split == GameMemory.SplitArea.Windhelm && this.Settings.Windhelm) ||
                (split == GameMemory.SplitArea.Council && this.Settings.Council) ||
                (split == GameMemory.SplitArea.Odahviing && this.Settings.Odahviing) ||
                (split == GameMemory.SplitArea.EnterSovngarde && this.Settings.EnterSovngarde) ||
                (split == GameMemory.SplitArea.DarkBrotherhoodQuestlineCompleted && this.Settings.DarkBrotherhood) ||
                (split == GameMemory.SplitArea.CompanionsQuestlineCompleted && this.Settings.Companions) ||
                (split == GameMemory.SplitArea.CollegeQuestlineCompleted && this.Settings.CollegeOfWinterhold) ||
                (split == GameMemory.SplitArea.ThievesGuildQuestlineCompleted && this.Settings.ThievesGuild) ||
                (split == GameMemory.SplitArea.AlduinDefeated && this.Settings.AlduinDefeated)))
            {
                Trace.WriteLine(String.Format("[NoLoads] {0} Split with {2} preset - {1}", split, frame, this.Settings.AnyPercentPreset));
                _timer.Split();
                _gameMemory.splitStates[(int)split] = true;
            }
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            return this.Settings.GetSettings(document);
        }

        public Control GetSettingsControl(LayoutMode mode)
        {
            return this.Settings;
        }

        public void SetSettings(XmlNode settings)
        {
            this.Settings.SetSettings(settings);
        }

        public float VerticalHeight { get { return this.Settings.DrawWithoutLoads ? this.InternalComponent.VerticalHeight : 0; } }
        public float HorizontalWidth { get { return this.Settings.DrawWithoutLoads ? this.InternalComponent.HorizontalWidth : 0; } }
        public float MinimumWidth { get { return this.InternalComponent.MinimumWidth; } }
        public float MinimumHeight { get { return this.InternalComponent.MinimumHeight; } }
        public float PaddingLeft { get { return this.Settings.DrawWithoutLoads ? this.InternalComponent.PaddingLeft : 0; } }
        public float PaddingRight { get { return this.Settings.DrawWithoutLoads ? this.InternalComponent.PaddingRight : 0; } }
        public float PaddingTop { get { return this.Settings.DrawWithoutLoads ? this.InternalComponent.PaddingTop : 0; } }
        public float PaddingBottom { get { return this.Settings.DrawWithoutLoads ? this.InternalComponent.PaddingBottom : 0; } }
        public void RenameComparison(string oldName, string newName) { }
    }
}

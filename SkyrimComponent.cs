﻿using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.Skyrim
{
    public class SkyrimComponent : LogicComponent
    {
        public override string ComponentName
        {
            get { return "Skyrim"; }
        }

        public IComponent SoundComponent { get; set; }
        public SkyrimSettings Settings { get; set; }
        public Time BearCartSplit { get; private set; }

        public string BearCartDefaultSoundPath { get; private set; }

        private TimerModel _timer;
        private GameMemory _gameMemory;
        private LiveSplitState _state;
        private TimerPhase _prevPhase;

        public SkyrimComponent(LiveSplitState state)
        {
            bool debug = false;
#if DEBUG
            debug = true;
#endif
            Trace.WriteLine($"[NoLoads] Using LiveSplit.Skyrim component version {Assembly.GetExecutingAssembly().GetName().Version} {(debug ? "Debug" : "Release")} build");
            _state = state;

            this.Settings = new SkyrimSettings(this, state);

            _timer = new TimerModel { CurrentState = state };

            this.BearCartSplit = new Time();
            IComponentFactory soundFactory;
            if (ComponentManager.ComponentFactories.TryGetValue("LiveSplit.Sound.dll", out soundFactory))
            {
                SoundComponent = soundFactory.Create(_state) as SoundComponent;

                this.BearCartDefaultSoundPath = System.IO.Path.GetTempPath() + @"LiveSplit.Skyrim\bearcart.mp3";
                //extract embedded sound to temp folder
                try
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(BearCartDefaultSoundPath));
                    System.IO.File.WriteAllBytes(BearCartDefaultSoundPath, Properties.Resources.bearcart_short);
                }
                catch (System.IO.IOException) { Trace.WriteLine("[NoLoads] Error when extracting bear cart sound to temp folder."); }
            }

            _gameMemory = new GameMemory();
            _gameMemory.OnStartSaveLoad += gameMemory_OnStartSaveLoad;
            _gameMemory.OnLoadStarted += gameMemory_OnLoadStarted;
            _gameMemory.OnLoadFinished += gameMemory_OnLoadFinished;
            _gameMemory.OnSplitCompleted += gameMemory_OnSplitCompleted;
            _gameMemory.OnBearCart += gameMemory_OnBearCart;
            state.OnStart += State_OnStart;
            state.OnReset += State_OnReset;
            _gameMemory.StartMonitoring();
        }

        public override void Dispose()
        {
            _state.OnStart -= State_OnStart;
            _state.OnReset -= State_OnReset;

            _gameMemory?.Stop();
            SoundComponent?.Dispose();
        }

        void State_OnStart(object sender, EventArgs e)
        {
            _gameMemory.resetSplitStates();
            BearCartSplit = new Time();
        }

        void State_OnReset(object sender, TimerPhase e)
        {
            UpdateBearCartPB();
        }

        void gameMemory_OnStartSaveLoad(object sender, EventArgs e)
        {
            Stopwatch s = Stopwatch.StartNew();

            if (this.Settings.AutoReset)
            {
                UpdateBearCartPB(true);
                _timer.Reset();
            }

            if (this.Settings.AutoStart)
            {
                StartTimer(s.Elapsed);
            }
        }

        void StartTimer(TimeSpan time)
        {
            TimeSpan originalOffset = _state.Run.Offset;
            _state.Run.Offset = time;
            _timer.Start();
            _state.Run.Offset = originalOffset;
            Trace.WriteLine($"[NoLoads] Started timer at {time}");
        }
        void StartTimer() => StartTimer(TimeSpan.Zero);

        void gameMemory_OnLoadStarted(object sender, EventArgs e)
        {
            _state.IsGameTimePaused = true;
        }

        void gameMemory_OnLoadFinished(object sender, EventArgs e)
        {
            _state.IsGameTimePaused = false;
        }

        void gameMemory_OnSplitCompleted(object sender, SplitArea split, string[] templates, uint frame)
        {
            string templatesDbgStr = String.Empty;
            if (templates != null)
            {
                uint i = 0;
                foreach (string template in templates)
                {
                    templatesDbgStr += i > 0 ? " " : "";
                    templatesDbgStr += $"\"{template}\"";
                    templatesDbgStr += i + 1 != templates.Length ? "," : "";
                    i++;
                }
            }
            else
                templatesDbgStr = "Any";

            Debug.WriteLineIf(split != SplitArea.None, $"[NoLoads] Trying to split {split} with {templatesDbgStr} templates, State: {_gameMemory.splitStates[(int)split]} - {frame}");

            if (_state.CurrentPhase == TimerPhase.Running && !_gameMemory.splitStates[(int)split] && (templates == null || Array.IndexOf(templates, Settings.AnyPercentTemplate) >= 0) &&
                ((split == SplitArea.Helgen && this.Settings.Helgen) ||
                (split == SplitArea.Whiterun && this.Settings.Whiterun) ||
                (split == SplitArea.ThalmorEmbassy && this.Settings.ThalmorEmbassy) ||
                (split == SplitArea.Esbern && this.Settings.Esbern) ||
                (split == SplitArea.Riverwood && this.Settings.Riverwood) ||
                (split == SplitArea.Karthspire && this.Settings.Karthspire) ||
                (split == SplitArea.Septimus && this.Settings.Septimus) ||
                (split == SplitArea.TheWall && this.Settings.TheWall) ||
                (split == SplitArea.MzarkTower && this.Settings.MzarkTower) ||
                (split == SplitArea.ClearSky && this.Settings.ClearSky) ||
                (split == SplitArea.HorseClimb && this.Settings.HorseClimb) ||
                (split == SplitArea.CutsceneEnd && this.Settings.CutsceneEnd) ||
                (split == SplitArea.CutsceneStart && this.Settings.CutsceneStart) ||
                (split == SplitArea.Alduin1 && this.Settings.Alduin1) ||
                (split == SplitArea.HighHrothgar && this.Settings.HighHrothgar) ||
                (split == SplitArea.Solitude && this.Settings.Solitude) ||
                (split == SplitArea.Windhelm && this.Settings.Windhelm) ||
                (split == SplitArea.Council && this.Settings.Council) ||
                (split == SplitArea.Odahviing && this.Settings.Odahviing) ||
                (split == SplitArea.EnterSovngarde && this.Settings.EnterSovngarde) ||
                (split == SplitArea.CollegeOfWinterholdQuestlineCompleted && this.Settings.CollegeOfWinterhold) ||
                (split == SplitArea.CompanionsQuestlineCompleted && this.Settings.Companions) ||
                (split == SplitArea.DarkBrotherhoodQuestlineCompleted && this.Settings.DarkBrotherhood) ||
                (split == SplitArea.ThievesGuildQuestlineCompleted && this.Settings.ThievesGuild) ||
                (split == SplitArea.AlduinDefeated && this.Settings.AlduinDefeated)))
            {
                Trace.WriteLine($"[NoLoads] {split} Split with {templatesDbgStr} templates - {frame}");
                _timer.Split();
                _gameMemory.splitStates[(int)split] = true;
            }
        }

        void gameMemory_OnBearCart(object sender, EventArgs e)
        {
            if (BearCartSplit.RealTime == null && _state.CurrentPhase != TimerPhase.NotRunning)
            {
                BearCartSplit = _state.CurrentTime;
                Settings.IsBearCartSecret = false;
                Settings.SaveBearCartConfig();

                if (SoundComponent != null && (Settings.IsBearCartSecret || Settings.PlayBearCartSound)) //force play if it isn't unlocked in case the splits were shared
                {
                    if (Settings.IsBearCartSecret || !Settings.PlayBearCartSoundOnlyOnPB || IsBearCartPB(BearCartSplit))
                    {
                        if (String.IsNullOrEmpty(Settings.BearCartSoundPath))
                            ((SoundComponent)SoundComponent).PlaySound(BearCartDefaultSoundPath);
                        else
                            ((SoundComponent)SoundComponent).PlaySound(Settings.BearCartSoundPath);
                    }
                }
            }
        }

        bool IsBearCartPB(Time time)
        {
            return (time.GameTime < Settings.BearCartPB.GameTime || Settings.BearCartPB.GameTime == new TimeSpan(0));
        }

        void UpdateBearCartPB(bool silent = false)
        {
            if (BearCartSplit.RealTime == null)
                return;

            if (IsBearCartPB(BearCartSplit))
            {
                DialogResult result = DialogResult.Yes;
                if (Settings.BearCartPBNotification && !silent)
                {
                    string newTime = $"New time: Game Time: {BearCartSplit.GameTime.Value.ToString(@"mm\:ss\.fff")}, Real Time: {BearCartSplit.RealTime.Value.ToString(@"mm\:ss\.fff")}\n";
                    string oldTime = String.Empty;

                    if (Settings.BearCartPB.GameTime.Value != new TimeSpan(0))
                        oldTime = $"Previous time: Game Time: {Settings.BearCartPB.GameTime.Value.ToString(@"mm\:ss\.fff")}, Real Time: {Settings.BearCartPB.RealTime.Value.ToString(@"mm\:ss\.fff")}\n";

                    result = MessageBox.Show(_state.Form, newTime + oldTime + "\nDo you want to save your new Bear Cart Personal Best?",
                        "New Bear Cart Personal Best", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }
                if (result == DialogResult.Yes)
                {
                    Settings.BearCartPB = new Time(BearCartSplit.RealTime, BearCartSplit.GameTime); // give new pb to settings so it can be saved
                    Settings.SaveBearCartConfig();
                }
            }

            if (SoundComponent != null && !silent)
                ((SoundComponent)SoundComponent).Player.Stop();

            BearCartSplit = new Time();
        }

        public override XmlNode GetSettings(XmlDocument document)
        {
            return this.Settings.GetSettings(document);
        }

        public override Control GetSettingsControl(LayoutMode mode)
        {
            return this.Settings;
        }

        public override void SetSettings(XmlNode settings)
        {
            this.Settings.SetSettings(settings);
        }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            if (state.CurrentPhase != _prevPhase && state.CurrentPhase == TimerPhase.Ended)
            {
                UpdateBearCartPB(true);
            }

            _prevPhase = state.CurrentPhase;
        }
    }
}

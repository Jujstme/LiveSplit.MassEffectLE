using System;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;

namespace LiveSplit.MassEffectLE
{
    class Component : LogicComponent
    {
        public override string ComponentName => "Mass Effect: Legendary Edition - Autosplitter";
        private Settings settings { get; set; }
        private readonly TimerModel timer;
        private readonly System.Timers.Timer update_timer;
        private readonly SplittingLogic SplittingLogic;

        public Component(LiveSplitState state)
        {
            timer = new TimerModel { CurrentState = state };
            settings = new Settings();

            SplittingLogic = new SplittingLogic();
            SplittingLogic.OnStartTrigger += OnStartTrigger;
            SplittingLogic.OnSplitTrigger += OnSplitTrigger;
            SplittingLogic.OnGameTimeTrigger += OnGameTimeTrigger;

            update_timer = new System.Timers.Timer() { Enabled = true, Interval = 15, AutoReset = false };
            update_timer.Elapsed += updateTimer_Tick;
        }

        void updateTimer_Tick(object sender, EventArgs e)
        {
            SplittingLogic.Update();
            update_timer.Start();
        }

        void OnStartTrigger(object sender, byte gameID)
        {
            if (timer.CurrentState.CurrentPhase != TimerPhase.NotRunning) return;
            switch (gameID)
            {
                case 1: if (settings.runStart1) timer.Start(); break;
                case 2: if (settings.runStart2) timer.Start(); break;
                case 3: if (settings.runStart3) timer.Start(); break;
            }
        }

        void OnGameTimeTrigger(object sender, bool isGameTimePaused)
        {
            if (timer.CurrentState.CurrentPhase != TimerPhase.Running) return;
            timer.CurrentState.IsGameTimePaused = isGameTimePaused;
        }

        void OnSplitTrigger(object sender, SplitTrigger type)
        {
            if (timer.CurrentState.CurrentPhase != TimerPhase.Running) return;
            switch (type)
            {
                case SplitTrigger.ME1Prologue: if (settings.ME1Prologue) timer.Split(); break;
                case SplitTrigger.EdenPrime: if (settings.EdenPrime) timer.Split(); break;
                case SplitTrigger.Citadel: if (settings.Citadel) timer.Split(); break;
                case SplitTrigger.Noveria: if (settings.Noveria) timer.Split(); break;
                case SplitTrigger.Feros: if (settings.Feros) timer.Split(); break;
                case SplitTrigger.Therum: if (settings.Therum) timer.Split(); break;
                case SplitTrigger.Virmire: if (settings.Virmire) timer.Split(); break;
                case SplitTrigger.Ilos: if (settings.Ilos) timer.Split(); break;
                case SplitTrigger.Saren: if (settings.Saren) timer.Split(); break;
                case SplitTrigger.EscapeLazarus: if (settings.escapeLazarus) timer.Split(); break;
                case SplitTrigger.FreedomProgress: if (settings.freedomProgress) timer.Split(); break;
                case SplitTrigger.HorizonCompleted: if (settings.horizonCompleted) timer.Split(); break;
                case SplitTrigger.CollectorShip: if (settings.collectorShip) timer.Split(); break;
                case SplitTrigger.ReaperIFF: if (settings.reaperIFF) timer.Split(); break;
                case SplitTrigger.CrewAbduct: if (settings.crewAbuct) timer.Split(); break;
                case SplitTrigger.ME2Oculus: if (settings.ME2Oculus) timer.Split(); break;
                case SplitTrigger.ME2Valve: if (settings.ME2Valve) timer.Split(); break;
                case SplitTrigger.ME2Bubble: if (settings.ME2Bubble) timer.Split(); break;
                case SplitTrigger.ME2Ending: if (settings.ME2ending) timer.Split(); break;
                case SplitTrigger.N7_WMF: if (settings.N7_WMF) timer.Split(); break;
                case SplitTrigger.N7_ARS: if (settings.N7_ARS) timer.Split(); break;
                case SplitTrigger.N7_ADS: if (settings.N7_ADS) timer.Split(); break;
                case SplitTrigger.N7_MSVE: if (settings.N7_MSVE) timer.Split(); break;
                case SplitTrigger.N7_ESD: if (settings.N7_ESD) timer.Split(); break;
                case SplitTrigger.N7_ERS: if (settings.N7_ERS) timer.Split(); break;
                case SplitTrigger.N7_LO: if (settings.N7_LO) timer.Split(); break;
                case SplitTrigger.RecruitMordin: if (settings.recruitMordin) timer.Split(); break;
                case SplitTrigger.RecruitGarrus: if (settings.recruitGarrus) timer.Split(); break;
                case SplitTrigger.RecruitGrunt: if (settings.acquireGrunt) timer.Split(); break;
                case SplitTrigger.RecruitJack: if (settings.recruitJack) timer.Split(); break;
                case SplitTrigger.RecruitZaeed: if (settings.recruitZaeed) timer.Split(); break;
                case SplitTrigger.RecruitKasumi: if (settings.recruitKasumi) timer.Split(); break;
                case SplitTrigger.RecruitTali: if (settings.recruitTali) timer.Split(); break;
                case SplitTrigger.RecruitSamara: if (settings.recruitSamara) timer.Split(); break;
                case SplitTrigger.RecruitThane: if (settings.recruitThane) timer.Split(); break;
                case SplitTrigger.LoyaltyMiranda: if (settings.loyaltyMiranda) timer.Split(); break;
                case SplitTrigger.LoyaltyJacob: if (settings.loyaltyJacob) timer.Split(); break;
                case SplitTrigger.LoyaltyJack: if (settings.loyaltyJack) timer.Split(); break;
                case SplitTrigger.LoyaltyLegion: if (settings.loyaltyLegion) timer.Split(); break;
                case SplitTrigger.LoyaltyKasumi: if (settings.loyaltyKasumi) timer.Split(); break;
                case SplitTrigger.LoyaltyGarrus: if (settings.loyaltyGarrus) timer.Split(); break;
                case SplitTrigger.LoyaltyThane: if (settings.loyaltyThane) timer.Split(); break;
                case SplitTrigger.LoyaltyTali: if (settings.loyaltyTali) timer.Split(); break;
                case SplitTrigger.LoyaltyMordin: if (settings.loyaltyMordin) timer.Split(); break;
                case SplitTrigger.LoyaltyGrunt: if (settings.loyaltyGrunt) timer.Split(); break;
                case SplitTrigger.LoyaltySamara: if (settings.loyaltySamara) timer.Split(); break;
                case SplitTrigger.LoyaltyZaeed: if (settings.loyaltyZaeed) timer.Split(); break;
                case SplitTrigger.Prologue: if (settings.prologue) timer.Split(); break;
                case SplitTrigger.PriorityMars: if (settings.priorityMars) timer.Split(); break;
                case SplitTrigger.PriorityCitadel: if (settings.priorityCitadel) timer.Split(); break;
                case SplitTrigger.PriorityPalaven: if (settings.priorityPalaven) timer.Split(); break;
                case SplitTrigger.PrioritySurkesh: if (settings.prioritySurkesh) timer.Split(); break;
                case SplitTrigger.TurianPlatoon: if (settings.turianPlatoon) timer.Split(); break;
                case SplitTrigger.KroganRachni: if (settings.kroganRachni) timer.Split(); break;
                case SplitTrigger.PriorityTuchanka: if (settings.priorityTuchanka) timer.Split(); break;
                case SplitTrigger.PriorityBeforeThessia: if (settings.priorityBeforeThessia) timer.Split(); break;
                case SplitTrigger.PriorityGethDreadnought: if (settings.priorityGethDreadnought) timer.Split(); break;
                case SplitTrigger.AdmiralKoris: if (settings.admiralKoris) timer.Split(); break;
                case SplitTrigger.GethServer: if (settings.gethServer) timer.Split(); break;
                case SplitTrigger.PriorityRannoch: if (settings.priorityRannoch) timer.Split(); break;
                case SplitTrigger.PriorityThessia: if (settings.priorityThessia) timer.Split(); break;
                case SplitTrigger.PriorityHorizon: if (settings.priorityHorizon) timer.Split(); break;
                case SplitTrigger.PriorityCerberusHQ: if (settings.priorityCerberusHQ) timer.Split(); break;
                case SplitTrigger.PriorityEarth: if (settings.priorityEarth) timer.Split(); break;
                case SplitTrigger.PriorityEnding: if (settings.priorityEnding) timer.Split(); break;
            }
        }

        public override void Dispose()
        {
            settings.Dispose();
            update_timer?.Dispose();
        }

        public override XmlNode GetSettings(XmlDocument document) { return this.settings.GetSettings(document); }

        public override Control GetSettingsControl(LayoutMode mode) { return this.settings; }

        public override void SetSettings(XmlNode settings) { this.settings.SetSettings(settings); }

        public override void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode) { }
    }
}

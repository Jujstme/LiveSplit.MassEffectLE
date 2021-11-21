using System;
using System.Diagnostics;
using System.Linq;

namespace LiveSplit.MassEffectLE
{
    class SplittingLogic
    {
        private Process game;
        private Watchers watchers;
        private byte gameID;

        public delegate void StartTriggerEventHandler(object sender, byte type);
        public event StartTriggerEventHandler OnStartTrigger;

        public delegate void GameTimeTriggerEventHandler(object sender, bool type);
        public event GameTimeTriggerEventHandler OnGameTimeTrigger;

        public delegate void SplitTriggerEventHandler(object sender, SplitTrigger type);
        public event SplitTriggerEventHandler OnSplitTrigger;

        public void Update()
        {
            if (!VerifyOrHookGameProcess()) return;
            watchers.UpdateAll(game);
            switch (gameID)
            {
                case 1: UpdateLE1(); break;
                case 2: UpdateLE2(); break;
                case 3: UpdateLE3(); break;
            }
        }

        void UpdateLE1()
        {
            LE1_Start();
            LE1_GameTime();
            LE1_Split();
        }

        void UpdateLE2()
        {
            LE2_Start();
            LE2_LE3_GameTime();
            LE2_Split();
        }

        void UpdateLE3()
        {
            LE3_Start();
            LE2_LE3_GameTime();
            LE3_Split();
        }

        void LE1_Start()
        {
            if (watchers.XPOS.Old == 0xC5A25800 && watchers.YPOS.Old == 0x4627006B && watchers.ZPOS.Old == 0xC70CEDD8 && (watchers.XPOS.Changed || watchers.YPOS.Changed || watchers.ZPOS.Changed))
                OnStartTrigger?.Invoke(this, gameID);
        }

        void LE2_Start()
        {
            if (watchers.XPOS.Old == 0x43BC83FB && watchers.YPOS.Old == 0xC7035CE9 && (watchers.XPOS.Changed || watchers.YPOS.Changed) && watchers.LE2_Allowsplitting.Current)
                OnStartTrigger?.Invoke(this, gameID);
        }

        void LE3_Start()
        {
            if (watchers.XPOS.Old == 0xC74F2814 && watchers.YPOS.Old == 0x46C403A6 && watchers.ZPOS.Old == 0x466F8C9A && (watchers.XPOS.Changed || watchers.YPOS.Changed || watchers.ZPOS.Changed))
                OnStartTrigger?.Invoke(this, gameID);
        }

        void LE1_GameTime()
        {
            OnGameTimeTrigger?.Invoke(this, !watchers.IsLoading.Current || watchers.IsLoading2.Current);
        }

        void LE2_LE3_GameTime()
        {
            OnGameTimeTrigger?.Invoke(this, !watchers.IsLoading.Current);
        }

        void LE1_Split()
        {
            if (!watchers.LE1_AllowSplitting.Current || !watchers.LE1_AllowSplitting.Old) return;
            if (watchers.EdenPrimeStarted.Current && !watchers.EdenPrimeStarted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.ME1Prologue); }
            else if (watchers.EdenPrimeCompleted.Current && !watchers.EdenPrimeCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.EdenPrime); }
            else if (watchers.CitadelCompleted.Current && !watchers.CitadelCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.Citadel); }
            else if (watchers.NoveriaCompleted.Current && !watchers.NoveriaCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.Noveria); }
            else if (watchers.FerosCompleted.Current && !watchers.FerosCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.Feros); }
            else if (watchers.TherumCompleted.Current && !watchers.TherumCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.Therum); }
            else if (watchers.VirmireCompleted.Current && !watchers.VirmireCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.Virmire); }
            else if (watchers.IlosCompleted.Current && !watchers.IlosCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.Ilos); }
            else if (watchers.FinalBattleStarted.Current && watchers.FinalBattleStarted.Old && watchers.SarenDefeated.Current && !watchers.SarenDefeated.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.Saren); }
        }

        void LE2_Split()
        {
            if (!watchers.LE2_Allowsplitting.Current || !watchers.LE2_Allowsplitting.Old) return;
            if (watchers.LazarusCompleted.Current && !watchers.LazarusCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.EscapeLazarus); }
            else if (watchers.FreedomProgressCompleted.Current && !watchers.FreedomProgressCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.FreedomProgress); }
            else if (watchers.HorizonCompleted.Current && !watchers.HorizonCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.HorizonCompleted); }
            else if (watchers.CollectorShipCompleted.Current && !watchers.CollectorShipCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.CollectorShip); }
            else if (watchers.ReaperIFFCompleted.Current && !watchers.ReaperIFFCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.ReaperIFF); }
            else if (watchers.CrewAbductMissionComplete.Current && !watchers.CrewAbductMissionComplete.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.CrewAbduct); }
            else if (watchers.SuicideOculusDestroyed.Current && !watchers.SuicideOculusDestroyed.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.ME2Oculus); }
            else if (watchers.SuicideValveCompleted.Current && !watchers.SuicideValveCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.ME2Valve); }
            else if (watchers.SuicideBubbleCompleted.Current && !watchers.SuicideBubbleCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.ME2Bubble); }
            else if (watchers.SuicideMissionCompleted.Current && !watchers.SuicideMissionCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.ME2Ending); }
            else if (watchers.N7WMF_completed.Current && !watchers.N7WMF_completed.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.N7_WMF); }
            else if (watchers.N7ARS_completed.Current && !watchers.N7ARS_completed.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.N7_ARS); }
            else if (watchers.N7ADS_completed.Current && !watchers.N7ADS_completed.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.N7_ADS); }
            else if (watchers.N7MSVE_completed.Current && !watchers.N7MSVE_completed.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.N7_MSVE); }
            else if (watchers.N7ESD_completed.Current && !watchers.N7ESD_completed.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.N7_ESD); }
            else if (watchers.N7ERS_completed.Current && !watchers.N7ERS_completed.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.N7_ERS); }
            else if (watchers.N7LO_completed.Current && !watchers.N7LO_completed.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.N7_LO); }
            else if (watchers.MordinRecruited.Current && !watchers.MordinRecruited.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.RecruitMordin); }
            else if (watchers.GarrusRecruited.Current && !watchers.GarrusRecruited.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.RecruitGarrus); }
            else if (watchers.GruntTankRecovered.Current && !watchers.GruntTankRecovered.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.RecruitGrunt); }
            else if (watchers.JackRecruited.Current && !watchers.JackRecruited.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.RecruitJack); }
            else if (watchers.ZaeedRecruited.Current && !watchers.ZaeedRecruited.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.RecruitZaeed); }
            else if (watchers.KasumiRecruited.Current && !watchers.KasumiRecruited.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.RecruitKasumi); }
            else if (watchers.TaliRecruited.Current && !watchers.TaliRecruited.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.RecruitTali); }
            else if (watchers.SamaraRecruited.Current && !watchers.SamaraRecruited.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.RecruitSamara); }
            else if (watchers.ThaneRecruited.Current && !watchers.ThaneRecruited.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.RecruitThane); }
            else if (watchers.MirandaLoyaltyMissionCompleted.Current && !watchers.MirandaLoyaltyMissionCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.LoyaltyMiranda); }
            else if (watchers.JacobLoyaltyMissionCompleted.Current && !watchers.JacobLoyaltyMissionCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.LoyaltyJacob); }
            else if (watchers.JackLoyaltyMissionCompleted.Current && !watchers.JackLoyaltyMissionCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.LoyaltyJack); }
            else if (watchers.LegionLoyaltyMissionCompleted.Current && !watchers.LegionLoyaltyMissionCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.LoyaltyLegion); }
            else if (watchers.KasumiLoyaltyMissionCompleted.Current && !watchers.KasumiLoyaltyMissionCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.LoyaltyKasumi); }
            else if (watchers.GarrusLoyaltyMissionCompleted.Current && !watchers.GarrusLoyaltyMissionCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.LoyaltyGarrus); }
            else if (watchers.ThaneLoyaltyMissionCompleted.Current && !watchers.ThaneLoyaltyMissionCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.LoyaltyThane); }
            else if (watchers.TaliLoyaltyMissionCompleted.Current && !watchers.TaliLoyaltyMissionCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.LoyaltyTali); }
            else if (watchers.MordinLoyaltyMissionCompleted.Current && !watchers.MordinLoyaltyMissionCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.LoyaltyMordin); }
            else if (watchers.GruntLoyaltyMissionCompleted.Current && !watchers.GruntLoyaltyMissionCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.LoyaltyGrunt); }
            else if (watchers.SamaraLoyaltyMissionCompleted.Current && !watchers.SamaraLoyaltyMissionCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.LoyaltySamara); }
            else if (watchers.ZaeedLoyaltyMissionCompleted.Current && !watchers.ZaeedLoyaltyMissionCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.LoyaltyZaeed); }
        }

        void LE3_Split()
        {
            if (watchers.PrologueEarthCompleted.Current && !watchers.PrologueEarthCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.Prologue); }
            else if (watchers.PriorityMarsCompleted.Current && !watchers.PriorityMarsCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.PriorityMars); }
            else if (watchers.PriorityCitadelCompleted.Current && !watchers.PriorityCitadelCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.PriorityCitadel); }
            else if (watchers.PriorityPalavenCompleted.Current && !watchers.PriorityPalavenCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.PriorityPalaven); }
            else if (watchers.PrioritySurkeshCompleted.Current && !watchers.PrioritySurkeshCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.PrioritySurkesh); }
            else if (watchers.PriorityTurianPlatoonCompleted.Current && !watchers.PriorityTurianPlatoonCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.TurianPlatoon); }
            else if (watchers.PriorityKroganRachniCompleted.Current && !watchers.PriorityKroganRachniCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.KroganRachni); }
            else if (watchers.PriorityTuchankaCompleted.Current && !watchers.PriorityTuchankaCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.PriorityTuchanka); }
            else if (watchers.PriorityCerberusCitadelCompleted.Current && !watchers.PriorityCerberusCitadelCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.PriorityBeforeThessia); }
            else if (watchers.PriorityGethDreadCompleted.Current && !watchers.PriorityGethDreadCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.PriorityGethDreadnought); }
            else if (watchers.PriorityKorisCompleted.Current && !watchers.PriorityKorisCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.AdmiralKoris); }
            else if (watchers.PriorityGethServerCompleted.Current && !watchers.PriorityGethServerCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.GethServer); }
            else if (watchers.PriorityRannochCompleted.Current && !watchers.PriorityRannochCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.PriorityRannoch); }
            else if (watchers.PriorityThessiaCompleted.Current && !watchers.PriorityThessiaCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.PriorityThessia); }
            else if (watchers.PriorityHorizonME3Completed.Current && !watchers.PriorityHorizonME3Completed.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.PriorityHorizon); }
            else if (watchers.PriorityCerberusHQCompleted.Current && !watchers.PriorityCerberusHQCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.PriorityCerberusHQ); }
            else if (watchers.PriorityEarthCompleted.Current && !watchers.PriorityEarthCompleted.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.PriorityEarth); }
            else if (watchers.EndingReached.Current && !watchers.EndingReached.Old) { this.OnSplitTrigger?.Invoke(this, SplitTrigger.PriorityEnding); }
        }
        

        bool VerifyOrHookGameProcess()
        {
            // If the game is already hooked, return true and continue
            if (!(game == null || game.HasExited)) return true;

            foreach (var process in new string[] { "MassEffect1", "MassEffect2", "MassEffect3" })
            {
                game = Process.GetProcessesByName(process).OrderByDescending(x => x.StartTime).FirstOrDefault(x => !x.HasExited);
                if (game == null) continue;
                try
                {
                    gameID = Convert.ToByte(process.Substring(10, 1)); 
                    watchers = new Watchers(game, gameID);
                }
                catch
                {
                    game = null;
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}

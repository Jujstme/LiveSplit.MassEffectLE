using System;
using System.Diagnostics;
using LiveSplit.ComponentUtil;

namespace LiveSplit.MassEffectLE
{
    class Watchers : MemoryWatcherList
    {
        public MemoryWatcher<bool> IsLoading { get; }
        public MemoryWatcher<uint> XPOS { get; }
        public MemoryWatcher<uint> YPOS { get; }
        public MemoryWatcher<uint> ZPOS { get; }


        // Mass Effect 1 variables
        public MemoryWatcher<bool> IsLoading2 { get; }
        private MemoryWatcher<byte> BYTE_GameStarted { get; }
        private MemoryWatcher<byte> BYTE_EdenPrimeStart { get; }
        private MemoryWatcher<byte> BYTE_EdenPrimeCompleted { get; }
        private MemoryWatcher<byte> BYTE_StoryProgress { get; }
        private MemoryWatcher<byte> BYTE_Huddles { get; }
        private MemoryWatcher<byte> BYTE_FinalBattleStart { get; }
        public FakeMemoryWatcher<bool> LE1_AllowSplitting => new FakeMemoryWatcher<bool>(bitCheck(BYTE_GameStarted.Old, 1), bitCheck(BYTE_GameStarted.Current, 1));
        public FakeMemoryWatcher<bool> EdenPrimeStarted => new FakeMemoryWatcher<bool>(bitCheck(BYTE_EdenPrimeStart.Old, 5), bitCheck(BYTE_EdenPrimeStart.Current, 5));
        public FakeMemoryWatcher<bool> EdenPrimeCompleted => new FakeMemoryWatcher<bool>(bitCheck(BYTE_EdenPrimeCompleted.Old, 1), bitCheck(BYTE_EdenPrimeCompleted.Current, 1));
        public FakeMemoryWatcher<bool> CitadelCompleted => new FakeMemoryWatcher<bool>(bitCheck(BYTE_StoryProgress.Old, 2) || bitCheck(BYTE_StoryProgress.Old, 3) || bitCheck(BYTE_StoryProgress.Old, 4),
            bitCheck(BYTE_StoryProgress.Current, 2) || bitCheck(BYTE_StoryProgress.Current, 3) || bitCheck(BYTE_StoryProgress.Current, 4));
        public FakeMemoryWatcher<bool> NoveriaCompleted => new FakeMemoryWatcher<bool>(bitCheck(BYTE_Huddles.Old, 2), bitCheck(BYTE_Huddles.Current, 2));
        public FakeMemoryWatcher<bool> FerosCompleted => new FakeMemoryWatcher<bool>(bitCheck(BYTE_Huddles.Old, 1), bitCheck(BYTE_Huddles.Current, 1));
        public FakeMemoryWatcher<bool> TherumCompleted => new FakeMemoryWatcher<bool>(bitCheck(BYTE_Huddles.Old, 0), bitCheck(BYTE_Huddles.Current, 0));
        public FakeMemoryWatcher<bool> VirmireCompleted => new FakeMemoryWatcher<bool>(bitCheck(BYTE_Huddles.Old, 3), bitCheck(BYTE_Huddles.Current, 3));
        public FakeMemoryWatcher<bool> IlosCompleted => new FakeMemoryWatcher<bool>(bitCheck(BYTE_EdenPrimeCompleted.Old, 6), bitCheck(BYTE_EdenPrimeCompleted.Current, 6));
        public FakeMemoryWatcher<bool> FinalBattleStarted => new FakeMemoryWatcher<bool>(bitCheck(BYTE_FinalBattleStart.Old, 4), bitCheck(BYTE_FinalBattleStart.Current, 4));
        public FakeMemoryWatcher<bool> SarenDefeated => new FakeMemoryWatcher<bool>(XPOS.Old == 0xC60EC000 && YPOS.Old == 0xC5E50000 && ZPOS.Old == 0x476F1000, XPOS.Current == 0xC60EC000 && YPOS.Current == 0xC5E50000 && ZPOS.Current == 0x476F1000);


        // Mass Effect 2 variables
        private MemoryWatcher<byte> PlotPrologue { get; }
        private MemoryWatcher<byte> PlotCR0 { get; }
        private MemoryWatcher<byte> PlotCR123 { get; }
        private MemoryWatcher<byte> PlotIFF { get; }
        private MemoryWatcher<byte> CrewAbduct { get; }
        private MemoryWatcher<byte> SuicideOculus { get; }
        private MemoryWatcher<byte> SuicideValve { get; }
        private MemoryWatcher<byte> SuicideBubble { get; }
        private MemoryWatcher<byte> SuicideReaper { get; }
        private MemoryWatcher<byte> CrewAcq1 { get; }
        private MemoryWatcher<byte> CrewAcq2 { get; }
        private MemoryWatcher<byte> CrewAcq3 { get; }
        private MemoryWatcher<byte> CrewAcq4 { get; }
        private MemoryWatcher<byte> CrewAcq5 { get; }
        private MemoryWatcher<byte> CrewAcq6 { get; }
        private MemoryWatcher<byte> LoyaltyMissions1 { get; }
        private MemoryWatcher<byte> LoyaltyMissions2 { get; }
        private MemoryWatcher<byte> WMF { get; }
        private MemoryWatcher<byte> ARS { get; }
        private MemoryWatcher<byte> ADS { get; }
        private MemoryWatcher<byte> MSVE { get; }
        private MemoryWatcher<byte> ESD { get; }
        private MemoryWatcher<byte> LO { get; }
        private MemoryWatcher<byte> WakeUp { get; }
        public FakeMemoryWatcher<bool> LazarusCompleted => new FakeMemoryWatcher<bool>(bitCheck(PlotPrologue.Old, 5) && !bitCheck(PlotCR0.Old, 1), bitCheck(PlotPrologue.Current, 5) && !bitCheck(PlotCR0.Current, 1));
        public FakeMemoryWatcher<bool> FreedomProgressCompleted => new FakeMemoryWatcher<bool>(bitCheck(PlotCR0.Old, 1), bitCheck(PlotCR0.Current, 1));
        public FakeMemoryWatcher<bool> HorizonCompleted => new FakeMemoryWatcher<bool>(bitCheck(PlotCR123.Old, 0), bitCheck(PlotCR123.Current, 0));
        public FakeMemoryWatcher<bool> CollectorShipCompleted => new FakeMemoryWatcher<bool>(bitCheck(PlotCR123.Old, 1), bitCheck(PlotCR123.Current, 1));
        public FakeMemoryWatcher<bool> ReaperIFFCompleted => new FakeMemoryWatcher<bool>(bitCheck(PlotIFF.Old, 3), bitCheck(PlotIFF.Current, 3));
        public FakeMemoryWatcher<bool> CrewAbductMissionComplete => new FakeMemoryWatcher<bool>(bitCheck(CrewAbduct.Old, 1), bitCheck(CrewAbduct.Current, 1));
        public FakeMemoryWatcher<bool> SuicideOculusDestroyed => new FakeMemoryWatcher<bool>(bitCheck(SuicideOculus.Old, 4), bitCheck(SuicideOculus.Current, 4));
        public FakeMemoryWatcher<bool> SuicideValveCompleted => new FakeMemoryWatcher<bool>(bitCheck(SuicideValve.Old, 5), bitCheck(SuicideValve.Current, 5));
        public FakeMemoryWatcher<bool> SuicideBubbleCompleted => new FakeMemoryWatcher<bool>(bitCheck(SuicideBubble.Old, 0), bitCheck(SuicideBubble.Current, 0));
        public FakeMemoryWatcher<bool> SuicideMissionCompleted => new FakeMemoryWatcher<bool>(bitCheck(SuicideReaper.Old, 3) || bitCheck(SuicideReaper.Old, 5) || bitCheck(SuicideReaper.Old, 6),
            bitCheck(SuicideReaper.Current, 3) || bitCheck(SuicideReaper.Current, 5) || bitCheck(SuicideReaper.Current, 6));
        public FakeMemoryWatcher<bool> MordinRecruited => new FakeMemoryWatcher<bool>(bitCheck(CrewAcq1.Old, 6), bitCheck(CrewAcq1.Current, 6));
        public FakeMemoryWatcher<bool> GarrusRecruited => new FakeMemoryWatcher<bool>(bitCheck(CrewAcq1.Old, 5), bitCheck(CrewAcq1.Current, 5));
        public FakeMemoryWatcher<bool> JackRecruited => new FakeMemoryWatcher<bool>(bitCheck(CrewAcq1.Old, 3), bitCheck(CrewAcq1.Current, 3));
        public FakeMemoryWatcher<bool> GruntTankRecovered => new FakeMemoryWatcher<bool>(bitCheck(CrewAcq2.Old, 2), bitCheck(CrewAcq2.Current, 2));
        public FakeMemoryWatcher<bool> TaliRecruited => new FakeMemoryWatcher<bool>(bitCheck(CrewAcq1.Old, 7), bitCheck(CrewAcq1.Current, 7));
        public FakeMemoryWatcher<bool> ThaneRecruited => new FakeMemoryWatcher<bool>(bitCheck(CrewAcq3.Old, 1), bitCheck(CrewAcq3.Current, 1));
        public FakeMemoryWatcher<bool> SamaraRecruited => new FakeMemoryWatcher<bool>(bitCheck(CrewAcq4.Old, 4), bitCheck(CrewAcq4.Current, 4));
        public FakeMemoryWatcher<bool> N7WMF_completed => new FakeMemoryWatcher<bool>(bitCheck(WMF.Old, 0), bitCheck(WMF.Current, 0));
        public FakeMemoryWatcher<bool> N7ARS_completed => new FakeMemoryWatcher<bool>(bitCheck(ARS.Old, 6), bitCheck(ARS.Current, 6));
        public FakeMemoryWatcher<bool> N7ADS_completed => new FakeMemoryWatcher<bool>(bitCheck(ADS.Old, 0), bitCheck(ADS.Current, 0));
        public FakeMemoryWatcher<bool> N7MSVE_completed => new FakeMemoryWatcher<bool>(bitCheck(MSVE.Old, 2), bitCheck(MSVE.Current, 2));
        public FakeMemoryWatcher<bool> N7ESD_completed => new FakeMemoryWatcher<bool>(bitCheck(ESD.Old, 2), bitCheck(ESD.Current, 2));
        public FakeMemoryWatcher<bool> N7ERS_completed => new FakeMemoryWatcher<bool>(bitCheck(ESD.Old, 3), bitCheck(ESD.Current, 3));
        public FakeMemoryWatcher<bool> N7LO_completed => new FakeMemoryWatcher<bool>(bitCheck(LO.Old, 7), bitCheck(LO.Current, 7));
        public FakeMemoryWatcher<bool> ZaeedRecruited => new FakeMemoryWatcher<bool>(bitCheck(CrewAcq5.Old, 4), bitCheck(CrewAcq5.Current, 4));
        public FakeMemoryWatcher<bool> KasumiRecruited => new FakeMemoryWatcher<bool>(bitCheck(CrewAcq6.Old, 4), bitCheck(CrewAcq6.Current, 4));
        public FakeMemoryWatcher<bool> MirandaLoyaltyMissionCompleted => new FakeMemoryWatcher<bool>(bitCheck(LoyaltyMissions1.Old, 0), bitCheck(LoyaltyMissions1.Current, 0));
        public FakeMemoryWatcher<bool> JacobLoyaltyMissionCompleted => new FakeMemoryWatcher<bool>(bitCheck(LoyaltyMissions1.Old, 1), bitCheck(LoyaltyMissions1.Current, 1));
        public FakeMemoryWatcher<bool> JackLoyaltyMissionCompleted => new FakeMemoryWatcher<bool>(bitCheck(LoyaltyMissions1.Old, 2), bitCheck(LoyaltyMissions1.Current, 2));
        public FakeMemoryWatcher<bool> LegionLoyaltyMissionCompleted => new FakeMemoryWatcher<bool>(bitCheck(LoyaltyMissions1.Old, 3), bitCheck(LoyaltyMissions1.Current, 3));
        public FakeMemoryWatcher<bool> KasumiLoyaltyMissionCompleted => new FakeMemoryWatcher<bool>(bitCheck(LoyaltyMissions1.Old, 4), bitCheck(LoyaltyMissions1.Current, 4));
        public FakeMemoryWatcher<bool> GarrusLoyaltyMissionCompleted => new FakeMemoryWatcher<bool>(bitCheck(LoyaltyMissions1.Old, 5), bitCheck(LoyaltyMissions1.Current, 5));
        public FakeMemoryWatcher<bool> ThaneLoyaltyMissionCompleted => new FakeMemoryWatcher<bool>(bitCheck(LoyaltyMissions1.Old, 6), bitCheck(LoyaltyMissions1.Current, 6));
        public FakeMemoryWatcher<bool> TaliLoyaltyMissionCompleted => new FakeMemoryWatcher<bool>(bitCheck(LoyaltyMissions1.Old, 7), bitCheck(LoyaltyMissions1.Current, 7));
        public FakeMemoryWatcher<bool> MordinLoyaltyMissionCompleted => new FakeMemoryWatcher<bool>(bitCheck(LoyaltyMissions2.Old, 0), bitCheck(LoyaltyMissions2.Current, 0));
        public FakeMemoryWatcher<bool> GruntLoyaltyMissionCompleted => new FakeMemoryWatcher<bool>(bitCheck(LoyaltyMissions2.Old, 1), bitCheck(LoyaltyMissions2.Current, 1));
        public FakeMemoryWatcher<bool> SamaraLoyaltyMissionCompleted => new FakeMemoryWatcher<bool>(bitCheck(LoyaltyMissions2.Old, 2), bitCheck(LoyaltyMissions2.Current, 2));
        public FakeMemoryWatcher<bool> ZaeedLoyaltyMissionCompleted => new FakeMemoryWatcher<bool>(bitCheck(LoyaltyMissions2.Old, 3), bitCheck(LoyaltyMissions2.Current, 3));
        public FakeMemoryWatcher<bool> LE2_Allowsplitting => new FakeMemoryWatcher<bool>(bitCheck(WakeUp.Old, 7), bitCheck(WakeUp.Current, 7));


        // Mass Effect 3 variables
        private MemoryWatcher<byte> PrologueEarth { get; }
        private MemoryWatcher<byte> PriorityCitadel { get; }
        private MemoryWatcher<byte> PriorityPalaven { get; }
        private MemoryWatcher<byte> PrioritySurkesh { get; }
        private MemoryWatcher<byte> PreTuchanka { get; }
        private MemoryWatcher<byte> PriorityTuchanka { get; }
        private MemoryWatcher<byte> PriorityCitadelCerberus { get; }
        private MemoryWatcher<byte> RannochKoris { get; }
        private MemoryWatcher<byte> RannochGethServer { get; }
        private MemoryWatcher<byte> PriorityRannoch { get; }
        private MemoryWatcher<byte> PriorityThessia { get; }
        private MemoryWatcher<byte> PriorityHorizonME3 { get; }
        private MemoryWatcher<byte> PriorityCerberusHead { get; }
        private MemoryWatcher<byte> PriorityEarth { get; }
        private MemoryWatcher<byte> PriorityEndingME3 { get; }
        public FakeMemoryWatcher<bool> PrologueEarthCompleted => new FakeMemoryWatcher<bool>(bitCheck(PrologueEarth.Old, 1), bitCheck(PrologueEarth.Current, 1));
        public FakeMemoryWatcher<bool> PriorityMarsCompleted => new FakeMemoryWatcher<bool>(bitCheck(PrologueEarth.Old, 0), bitCheck(PrologueEarth.Current, 0));
        public FakeMemoryWatcher<bool> PriorityCitadelCompleted => new FakeMemoryWatcher<bool>(bitCheck(PriorityCitadel.Old, 5), bitCheck(PriorityCitadel.Current, 5));
        public FakeMemoryWatcher<bool> PriorityPalavenCompleted => new FakeMemoryWatcher<bool>(bitCheck(PriorityPalaven.Old, 3), bitCheck(PriorityPalaven.Current, 3));
        public FakeMemoryWatcher<bool> PrioritySurkeshCompleted => new FakeMemoryWatcher<bool>(bitCheck(PrioritySurkesh.Old, 7), bitCheck(PrioritySurkesh.Current, 7));
        public FakeMemoryWatcher<bool> PriorityTurianPlatoonCompleted => new FakeMemoryWatcher<bool>(bitCheck(PreTuchanka.Old, 2), bitCheck(PreTuchanka.Current, 2));
        public FakeMemoryWatcher<bool> PriorityKroganRachniCompleted => new FakeMemoryWatcher<bool>(bitCheck(PreTuchanka.Old, 3), bitCheck(PreTuchanka.Current, 3));
        public FakeMemoryWatcher<bool> PriorityTuchankaCompleted => new FakeMemoryWatcher<bool>(bitCheck(PriorityTuchanka.Old, 4), bitCheck(PriorityTuchanka.Current, 4));
        public FakeMemoryWatcher<bool> PriorityCerberusCitadelCompleted => new FakeMemoryWatcher<bool>(bitCheck(PriorityCitadelCerberus.Old, 0), bitCheck(PriorityCitadelCerberus.Current, 0));
        public FakeMemoryWatcher<bool> PriorityGethDreadCompleted => new FakeMemoryWatcher<bool>(bitCheck(PrioritySurkesh.Old, 5), bitCheck(PrioritySurkesh.Current, 5));
        public FakeMemoryWatcher<bool> PriorityKorisCompleted => new FakeMemoryWatcher<bool>(bitCheck(RannochKoris.Old, 6), bitCheck(RannochKoris.Current, 6));
        public FakeMemoryWatcher<bool> PriorityGethServerCompleted => new FakeMemoryWatcher<bool>(bitCheck(RannochGethServer.Old, 0), bitCheck(RannochGethServer.Current, 0));
        public FakeMemoryWatcher<bool> PriorityRannochCompleted => new FakeMemoryWatcher<bool>(bitCheck(PriorityRannoch.Old, 6), bitCheck(PriorityRannoch.Current, 6));
        public FakeMemoryWatcher<bool> PriorityThessiaCompleted => new FakeMemoryWatcher<bool>(bitCheck(PriorityThessia.Old, 2), bitCheck(PriorityThessia.Current, 2));
        public FakeMemoryWatcher<bool> PriorityHorizonME3Completed => new FakeMemoryWatcher<bool>(bitCheck(PriorityHorizonME3.Old, 7), bitCheck(PriorityHorizonME3.Current, 7));
        public FakeMemoryWatcher<bool> PriorityCerberusHQCompleted => new FakeMemoryWatcher<bool>(bitCheck(PriorityCerberusHead.Old, 2), bitCheck(PriorityCerberusHead.Current, 2));
        public FakeMemoryWatcher<bool> PriorityEarthCompleted => new FakeMemoryWatcher<bool>(bitCheck(PriorityEarth.Old, 0), bitCheck(PriorityEarth.Current, 0));
        public FakeMemoryWatcher<bool> EndingReached => new FakeMemoryWatcher<bool>(bitCheck(PriorityEndingME3.Old, 2), bitCheck(PriorityEndingME3.Current, 2));

        private bool bitCheck(byte plotEvent, int b)
        {
            return (plotEvent & (1 << b)) != 0;
        }

        public Watchers(Process game, byte gameID)
        {
            var scanner = new SignatureScanner(game, game.MainModuleWow64Safe().BaseAddress, game.MainModuleWow64Safe().ModuleMemorySize);
            IntPtr ptr;

            ptr = scanner.Scan(new SigScanTarget(4,
                "75 18",             // jne MassEffect1.exe+310FD6           // jne MassEffect2+475742               // jne MassEffect3.exe+480D12
                "8B 0D ????????",    // mov ecx,[MassEffect1.exe+16516B0]    // mov ecx,[MassEffect2.exe+16232F0]    // mov ecx,[MassEffect3.exe+1767AA0]  <----
                "85 C9"));           // test ecx,ecx                         // test ecx,ecx                         // test ecx,ecx
            if (ptr == IntPtr.Zero) throw new Exception();
            this.IsLoading = new MemoryWatcher<bool>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr))) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };      // This value is 0 while the game displays cutscenes, otherwise it stays at 1

            switch (gameID)
            {
                case 1:
                    // Mass Effect 1 uses an additional variable for loading messages
                    ptr = scanner.Scan(new SigScanTarget(2,
                        "83 3D ???????? 00",  // cmp dword ptr [MassEffect1.exe+17775B8],00  <----
                        "74 20",              // je MassEffect1.exe+2596F1
                        "48 8B 03"));         // mov rax,[rbx]
                    if (ptr == IntPtr.Zero) throw new Exception();
                    this.IsLoading2 = new MemoryWatcher<bool>(new DeepPointer(ptr + 5 + game.ReadValue<int>(ptr))) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull }; // This value is 1 in load messages, otherwise it's 0 

                    // Mass Effect 1 plot bools for automatic splitting
                    ptr = scanner.Scan(new SigScanTarget(11,
                        "4C 8B 30",             // mov r14,[rax]
                        "4D 85 F6",             // test r14,r14
                        "74 4A",                // je MassEffect1.exe+9D17CE
                        "48 8B 15 ????????"));  // mov rdx,[MassEffect1.exe+1B9F588]  <----
                    if (ptr == IntPtr.Zero) throw new Exception();
                    this.BYTE_GameStarted = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x174, 0xAFC, 0x60, 0x1B5)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.BYTE_EdenPrimeStart = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x174, 0xAFC, 0x60, 0x1BB)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.BYTE_EdenPrimeCompleted = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x174, 0xAFC, 0x60, 0x139)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.BYTE_StoryProgress = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x174, 0xAFC, 0x60, 0x362)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.BYTE_Huddles = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x174, 0xAFC, 0x60, 0x1FD)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.BYTE_FinalBattleStart = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x174, 0xAFC, 0x60, 0x184)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };

                    // XYZ position
                    ptr = scanner.Scan(new SigScanTarget(23,
                        "0F1F 44 00 00",        // nop dword ptr [rax+rax+00]
                        "85 DB",                // test ebx,ebx
                        "78 3A",                // js MassEffect1.exe+41BBDE
                        "3B 1D ????????",       // cmp ebx,[MassEffect1.exe+17B2308]
                        "7D 32",                // jnl MassEffect1.exe+41BBDE
                        "48 63 FB",             // movsxd rdi,ebx
                        "48 8B 05 ????????"));  // mov rax,[MassEffect1.exe+1782300]  <----
                    if (ptr == IntPtr.Zero) throw new Exception();
                    this.XPOS = new MemoryWatcher<uint>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x0, 0x78, 0x108)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.YPOS = new MemoryWatcher<uint>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x0, 0x78, 0x10C)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.ZPOS = new MemoryWatcher<uint>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x0, 0x78, 0x110)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };

                    this.AddRange(new MemoryWatcher[] { this.IsLoading, this.XPOS, this.YPOS, this.ZPOS,
                                                        this.IsLoading2, this.BYTE_GameStarted, this.BYTE_EdenPrimeStart, this.BYTE_EdenPrimeCompleted, this.BYTE_StoryProgress, this.BYTE_Huddles, this.BYTE_FinalBattleStart});

                    break;

                case 2:
                    // Mass Effect 2 plot bools for automatic splitting
                    ptr = scanner.Scan(new SigScanTarget(8,
                        "48 85 C0",             // test rax,rax
                        "74 42",                // je MassEffect2.exe+6DAA15
                        "4C 8B 05 ????????"));  // mov r8,[MassEffect2.exe+1B675B0]  <----
                    if (ptr == IntPtr.Zero) throw new Exception("Could not find address!");
                    this.PlotPrologue = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x14A)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.PlotCR0 = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x43)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.PlotCR123 = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x27)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.PlotIFF = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0xB3)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.CrewAbduct = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x12E)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.SuicideOculus = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0xFE)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.SuicideValve = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x16F)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.SuicideBubble = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x170)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.SuicideReaper = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x1BC)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.CrewAcq1 = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x278)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.CrewAcq2 = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x4D)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.CrewAcq3 = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x279)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.CrewAcq4 = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x47)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.CrewAcq5 = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x32)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.CrewAcq6 = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0xB9)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.LoyaltyMissions1 = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0xBB)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.LoyaltyMissions2 = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0xBC)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.WMF = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x1A8)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.ARS = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x225)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.ADS = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x18C)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.MSVE = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x156)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.ESD = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x28A)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.LO = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x288)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.WakeUp = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x16C, 0xA34, 0x60, 0x119)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };

                    // XYZ position
                    ptr = scanner.Scan(new SigScanTarget(21,
                        "41 8B DC",             // mov ebx,r12d
                        "85 DB",                // test ebx,ebx
                        "78 3A",                // js MassEffect2.exe+598505
                        "3B 1D ????????",       // cmp ebx,[MassEffect2.exe+1760018
                        "7D 32",                // jnl MassEffect2.exe+598505
                        "48 63 FB",             // movsxd rdi,ebx
                        "48 8B 05 ????????"));  // mov rax,[MassEffect2.exe+1760010]  <----
                    if (ptr == IntPtr.Zero) throw new Exception();
                    this.XPOS = new MemoryWatcher<uint>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x0, 0x78, 0x118)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.YPOS = new MemoryWatcher<uint>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x0, 0x78, 0x11C)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.ZPOS = new MemoryWatcher<uint>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x0, 0x78, 0x120)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };

                    this.AddRange(new MemoryWatcher[] { this.IsLoading, this.XPOS, this.YPOS, this.ZPOS,
                                                        this.PlotPrologue, this.PlotCR0, this.PlotCR123, this.PlotIFF, this.CrewAbduct, this.SuicideOculus, this.SuicideValve,
                                                        this.SuicideBubble, this.SuicideReaper, this.CrewAcq1, this.CrewAcq2, this.CrewAcq3, this.CrewAcq4, this.CrewAcq5, this.CrewAcq6,
                                                        this.LoyaltyMissions1, this.LoyaltyMissions2, this.WMF, this.ARS, this.ADS, this.MSVE, this.ESD, this.LO, this.WakeUp });
                    break;

                case 3:
                    // Mass Effect 3 plot bools for automatic splitting
                    ptr = scanner.Scan(new SigScanTarget(13,
                        "C3",                   // ret
                        "CC",                   // int 3
                        "CC",                   // int 3
                        "CC",                   // int 3
                        "CC",                   // int 3
                        "CC",                   // int 3
                        "48 83 EC 28",          // sub rsp,28
                        "48 8B 0D ????????",    // mov rcx,[MassEffect3.exe+1CBBC70]   <----
                        "48 85 C9",             // test rcx,rcx
                        "75 1F",                // jne MassEffect3.exe+AD241F
                        "48 8D 0D ????????"));  // lea rcx,[MassEffect3.exe+10AE638]
                    if (ptr == IntPtr.Zero) throw new Exception();
                    this.PrologueEarth = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x13C, 0xF0, 0x9ED)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.PriorityCitadel = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x13C, 0xF0, 0xA14)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.PriorityPalaven = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x13C, 0xF0, 0x924)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.PrioritySurkesh = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x13C, 0xF0, 0x8A9)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.PreTuchanka = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x13C, 0xF0, 0x940)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.PriorityTuchanka = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x13C, 0xF0, 0x8FA)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.PriorityCitadelCerberus = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x13C, 0xF0, 0x983)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.RannochKoris = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x13C, 0xF0, 0x934)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.RannochGethServer = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x13C, 0xF0, 0x935)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.PriorityRannoch = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x13C, 0xF0, 0x967)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.PriorityThessia = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x13C, 0xF0, 0x906)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.PriorityHorizonME3 = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x13C, 0xF0, 0x989)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.PriorityCerberusHead = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x13C, 0xF0, 0x9DD)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.PriorityEarth = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x13C, 0xF0, 0xA70)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.PriorityEndingME3 = new MemoryWatcher<byte>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x13C, 0xF0, 0xAF3)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };

                    // XYZ position
                    ptr = scanner.Scan(new SigScanTarget(23,
                        "0F1F 44 00 00",        // nop dword ptr [rax+rax+00]
                        "85 DB",                // test ebx,ebx
                        "78 3A",                // js MassEffect3.exe+59A75E
                        "3B 1D ????????",       // cmp ebx,[MassEffect3.exe+18B4248
                        "7D 32",                // jnl MassEffect3.exe+59A75E
                        "48 63 FB",             // movsxd rdi,ebx
                        "48 8B 05 ????????"));  // mov rax,[MassEffect3.exe+18B4240]  <----
                    if (ptr == IntPtr.Zero) throw new Exception();
                    this.XPOS = new MemoryWatcher<uint>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x0, 0x78, 0x108)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.YPOS = new MemoryWatcher<uint>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x0, 0x78, 0x10C)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };
                    this.ZPOS = new MemoryWatcher<uint>(new DeepPointer(ptr + 4 + game.ReadValue<int>(ptr), 0x0, 0x78, 0x110)) { FailAction = MemoryWatcher.ReadFailAction.SetZeroOrNull };

                    this.AddRange(new MemoryWatcher[] { this.IsLoading, this.XPOS, this.YPOS, this.ZPOS,
                                                        this.PrologueEarth, this.PriorityCitadel, this.PriorityPalaven, this.PrioritySurkesh, this.PreTuchanka, this.PriorityTuchanka,
                                                        this.PriorityCitadelCerberus, this.RannochKoris, this.RannochGethServer, this.PriorityRannoch, this.PriorityThessia,
                                                        this.PriorityHorizonME3, this.PriorityCerberusHead, this.PriorityEarth, this.PriorityEndingME3 });
                    break;
            }
        }
    }

    class FakeMemoryWatcher<T>
    {
        public T Current { get; set; }
        public T Old { get; set; }
        public bool Changed { get; }
        public FakeMemoryWatcher(T old, T current)
        {
            this.Old = old;
            this.Current = current;
            this.Changed = !old.Equals(current);
        }
    }

    enum SplitTrigger
    {
        // ME1 Split Triggers
        ME1Prologue,
        EdenPrime,
        Citadel,
        Noveria,
        Feros,
        Therum,
        Virmire,
        Ilos,
        Saren,

        // ME2 Split Triggers
        EscapeLazarus,
        FreedomProgress,
        HorizonCompleted,
        CollectorShip,
        ReaperIFF,
        CrewAbduct,
        ME2Oculus,
        ME2Valve,
        ME2Bubble,
        ME2Ending,
        N7_WMF,
        N7_ARS,
        N7_ADS,
        N7_MSVE,
        N7_ESD,
        N7_ERS,
        N7_LO,
        RecruitMordin,
        RecruitGarrus,
        RecruitGrunt,
        RecruitJack,
        RecruitZaeed,
        RecruitKasumi,
        RecruitTali,
        RecruitSamara,
        RecruitThane,
        LoyaltyMiranda,
        LoyaltyJacob,
        LoyaltyJack,
        LoyaltyLegion,
        LoyaltyKasumi,
        LoyaltyGarrus,
        LoyaltyThane,
        LoyaltyTali,
        LoyaltyMordin,
        LoyaltyGrunt,
        LoyaltySamara,
        LoyaltyZaeed,

        // LE3 Split Triggers
        Prologue,
        PriorityMars,
        PriorityCitadel,
        PriorityPalaven,
        PrioritySurkesh,
        TurianPlatoon,
        KroganRachni,
        PriorityTuchanka,
        PriorityBeforeThessia,
        PriorityGethDreadnought,
        AdmiralKoris,
        GethServer,
        PriorityRannoch,
        PriorityThessia,
        PriorityHorizon,
        PriorityCerberusHQ,
        PriorityEarth,
        PriorityEnding
    }
}

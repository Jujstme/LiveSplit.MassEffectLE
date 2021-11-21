using System;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.MassEffectLE
{
    public partial class Settings : UserControl
    {
        public bool runStart1 { get; set; }
        public bool runStart2 { get; set; }
        public bool runStart3 { get; set; }

        // Mass Effect 1 settings
        public bool ME1Prologue { get; set; }
        public bool EdenPrime { get; set; }
        public bool Citadel { get; set; }
        public bool Noveria { get; set; }
        public bool Feros { get; set; }
        public bool Therum { get; set; }
        public bool Virmire { get; set; }
        public bool Ilos { get; set; }
        public bool Saren { get; set; }

        // Mass Effect 2 settings
        public bool escapeLazarus { get; set; }
        public bool freedomProgress { get; set; }
        public bool horizonCompleted { get; set; }
        public bool collectorShip { get; set; }
        public bool reaperIFF { get; set; }
        public bool crewAbuct { get; set; }
        public bool ME2Oculus { get; set; }
        public bool ME2Valve { get; set; }
        public bool ME2Bubble { get; set; }
        public bool ME2ending { get; set; }
        public bool N7_WMF { get; set; }
        public bool N7_ARS { get; set; }
        public bool N7_ADS { get; set; }
        public bool N7_MSVE { get; set; }
        public bool N7_ESD { get; set; }
        public bool N7_ERS { get; set; }
        public bool N7_LO { get; set; }
        public bool recruitMordin { get; set; }
        public bool recruitGarrus { get; set; }
        public bool acquireGrunt { get; set; }
        public bool recruitJack { get; set; }
        public bool recruitZaeed { get; set; }
        public bool recruitKasumi { get; set; }
        public bool recruitTali { get; set; }
        public bool recruitSamara { get; set; }
        public bool recruitThane { get; set; }
        public bool loyaltyMiranda { get; set; }
        public bool loyaltyJacob { get; set; }
        public bool loyaltyJack { get; set; }
        public bool loyaltyLegion { get; set; }
        public bool loyaltyKasumi { get; set; }
        public bool loyaltyGarrus { get; set; }
        public bool loyaltyThane { get; set; }
        public bool loyaltyTali { get; set; }
        public bool loyaltyMordin { get; set; }
        public bool loyaltyGrunt { get; set; }
        public bool loyaltySamara { get; set; }
        public bool loyaltyZaeed { get; set; }

        // Mass Effect 3 settings
        public bool prologue { get; set; }
        public bool priorityMars { get; set; }
        public bool priorityCitadel { get; set; }
        public bool priorityPalaven { get; set; }
        public bool prioritySurkesh { get; set; }
        public bool turianPlatoon { get; set; }
        public bool kroganRachni { get; set; }
        public bool priorityTuchanka { get; set; }
        public bool priorityBeforeThessia { get; set; }
        public bool priorityGethDreadnought { get; set; }
        public bool admiralKoris { get; set; }
        public bool gethServer { get; set; }
        public bool priorityRannoch { get; set; }
        public bool priorityThessia { get; set; }
        public bool priorityHorizon { get; set; }
        public bool priorityCerberusHQ { get; set; }
        public bool priorityEarth { get; set; }
        public bool priorityEnding { get; set; }


        public Settings()
        {
            InitializeComponent();

            // General settings
            this.chkrunStart1.DataBindings.Add("Checked", this, "runStart1", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkrunStart2.DataBindings.Add("Checked", this, "runStart2", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkrunStart3.DataBindings.Add("Checked", this, "runStart3", false, DataSourceUpdateMode.OnPropertyChanged);

            // Mass effect 1
            this.chkME1Prologue.DataBindings.Add("Checked", this, "ME1Prologue", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkEdenPrime.DataBindings.Add("Checked", this, "EdenPrime", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCitadel.DataBindings.Add("Checked", this, "Citadel", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkNoveria.DataBindings.Add("Checked", this, "Noveria", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkFeros.DataBindings.Add("Checked", this, "Feros", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkTherum.DataBindings.Add("Checked", this, "Therum", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkVirmire.DataBindings.Add("Checked", this, "Virmire", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkIlos.DataBindings.Add("Checked", this, "Ilos", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkSaren.DataBindings.Add("Checked", this, "Saren", false, DataSourceUpdateMode.OnPropertyChanged);

            // Mass Effect 2
            this.chkescapeLazarus.DataBindings.Add("Checked", this, "escapeLazarus", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkfreedomProgress.DataBindings.Add("Checked", this, "freedomProgress", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkhorizonCompleted.DataBindings.Add("Checked", this, "horizonCompleted", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkcollectorShip.DataBindings.Add("Checked", this, "collectorShip", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkreaperIFF.DataBindings.Add("Checked", this, "reaperIFF", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkcrewAbuct.DataBindings.Add("Checked", this, "crewAbuct", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkME2Oculus.DataBindings.Add("Checked", this, "ME2Oculus", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkME2Valve.DataBindings.Add("Checked", this, "ME2Valve", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkME2Bubble.DataBindings.Add("Checked", this, "ME2Bubble", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkME2ending.DataBindings.Add("Checked", this, "ME2ending", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkN7_WMF.DataBindings.Add("Checked", this, "N7_WMF", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkN7_ARS.DataBindings.Add("Checked", this, "N7_ARS", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkN7_ADS.DataBindings.Add("Checked", this, "N7_ADS", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkN7_MSVE.DataBindings.Add("Checked", this, "N7_MSVE", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkN7_ESD.DataBindings.Add("Checked", this, "N7_ESD", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkN7_ERS.DataBindings.Add("Checked", this, "N7_ERS", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkN7_LO.DataBindings.Add("Checked", this, "N7_LO", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkrecruitMordin.DataBindings.Add("Checked", this, "recruitMordin", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkrecruitGarrus.DataBindings.Add("Checked", this, "recruitGarrus", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkacquireGrunt.DataBindings.Add("Checked", this, "acquireGrunt", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkrecruitJack.DataBindings.Add("Checked", this, "recruitJack", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkrecruitZaeed.DataBindings.Add("Checked", this, "recruitZaeed", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkrecruitKasumi.DataBindings.Add("Checked", this, "recruitKasumi", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkrecruitTali.DataBindings.Add("Checked", this, "recruitTali", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkrecruitSamara.DataBindings.Add("Checked", this, "recruitSamara", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkrecruitThane.DataBindings.Add("Checked", this, "recruitThane", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkloyaltyMiranda.DataBindings.Add("Checked", this, "loyaltyMiranda", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkloyaltyJacob.DataBindings.Add("Checked", this, "loyaltyJacob", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkloyaltyJack.DataBindings.Add("Checked", this, "loyaltyJack", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkloyaltyLegion.DataBindings.Add("Checked", this, "loyaltyLegion", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkloyaltyKasumi.DataBindings.Add("Checked", this, "loyaltyKasumi", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkloyaltyGarrus.DataBindings.Add("Checked", this, "loyaltyGarrus", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkloyaltyThane.DataBindings.Add("Checked", this, "loyaltyThane", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkloyaltyTali.DataBindings.Add("Checked", this, "loyaltyTali", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkloyaltyMordin.DataBindings.Add("Checked", this, "loyaltyMordin", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkloyaltyGrunt.DataBindings.Add("Checked", this, "loyaltyGrunt", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkloyaltySamara.DataBindings.Add("Checked", this, "loyaltySamara", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkloyaltyZaeed.DataBindings.Add("Checked", this, "loyaltyZaeed", false, DataSourceUpdateMode.OnPropertyChanged);

            // Mass Effect 3
            this.chkprologue.DataBindings.Add("Checked", this, "prologue", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkpriorityMars.DataBindings.Add("Checked", this, "priorityMars", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkpriorityCitadel.DataBindings.Add("Checked", this, "priorityCitadel", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkpriorityPalaven.DataBindings.Add("Checked", this, "priorityPalaven", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkprioritySurkesh.DataBindings.Add("Checked", this, "prioritySurkesh", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkturianPlatoon.DataBindings.Add("Checked", this, "turianPlatoon", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkkroganRachni.DataBindings.Add("Checked", this, "kroganRachni", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkpriorityTuchanka.DataBindings.Add("Checked", this, "priorityTuchanka", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkpriorityBeforeThessia.DataBindings.Add("Checked", this, "priorityBeforeThessia", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkpriorityGethDreadnought.DataBindings.Add("Checked", this, "priorityGethDreadnought", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkadmiralKoris.DataBindings.Add("Checked", this, "admiralKoris", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkgethServer.DataBindings.Add("Checked", this, "gethServer", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkpriorityRannoch.DataBindings.Add("Checked", this, "priorityRannoch", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkpriorityThessia.DataBindings.Add("Checked", this, "priorityThessia", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkpriorityHorizon.DataBindings.Add("Checked", this, "priorityHorizon", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkpriorityCerberusHQ.DataBindings.Add("Checked", this, "priorityCerberusHQ", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkpriorityEarth.DataBindings.Add("Checked", this, "priorityEarth", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkpriorityEnding.DataBindings.Add("Checked", this, "priorityEnding", false, DataSourceUpdateMode.OnPropertyChanged);

            //
            // Default Values
            //
            runStart1 = runStart2 = runStart3 = true;
            
            ME1Prologue = EdenPrime = Citadel = Noveria = Feros = Therum = Virmire = Ilos = Saren = true;

            escapeLazarus = freedomProgress = horizonCompleted = collectorShip = reaperIFF = crewAbuct = ME2Oculus = ME2Valve = ME2Bubble = ME2ending =
                N7_WMF = N7_ARS = N7_ADS = N7_MSVE = N7_ESD = N7_ERS = N7_LO = recruitMordin = recruitGarrus = acquireGrunt = recruitJack = recruitZaeed = recruitKasumi =
                recruitTali = recruitSamara = recruitThane = loyaltyMiranda = loyaltyJacob = loyaltyJack = loyaltyLegion = loyaltyKasumi = loyaltyGarrus = loyaltyThane =
                loyaltyTali = loyaltyMordin = loyaltyGrunt = loyaltySamara = loyaltyZaeed = true;

            prologue = priorityMars = priorityCitadel = priorityPalaven = prioritySurkesh = turianPlatoon = kroganRachni = priorityTuchanka = priorityBeforeThessia = priorityGethDreadnought =
                admiralKoris = gethServer = priorityRannoch = priorityThessia = priorityHorizon = priorityCerberusHQ = priorityEarth = priorityEnding = true;
        }

        public XmlNode GetSettings(XmlDocument doc)
        {
            XmlElement settingsNode = doc.CreateElement("settings");
            settingsNode.AppendChild(ToElement(doc, "runStart1", this.runStart1));
            settingsNode.AppendChild(ToElement(doc, "runStart2", this.runStart2));
            settingsNode.AppendChild(ToElement(doc, "runStart3", this.runStart3));
            settingsNode.AppendChild(ToElement(doc, "EdenPrime", this.EdenPrime));
            settingsNode.AppendChild(ToElement(doc, "ME1Prologue", this.ME1Prologue));
            settingsNode.AppendChild(ToElement(doc, "Citadel", this.Citadel));
            settingsNode.AppendChild(ToElement(doc, "Noveria", this.Noveria));
            settingsNode.AppendChild(ToElement(doc, "Feros", this.Feros));
            settingsNode.AppendChild(ToElement(doc, "Therum", this.Therum));
            settingsNode.AppendChild(ToElement(doc, "Virmire", this.Virmire));
            settingsNode.AppendChild(ToElement(doc, "Ilos", this.Ilos));
            settingsNode.AppendChild(ToElement(doc, "Saren", this.Saren));

            settingsNode.AppendChild(ToElement(doc, "escapeLazarus", this.escapeLazarus));
            settingsNode.AppendChild(ToElement(doc, "freedomProgress", this.freedomProgress));
            settingsNode.AppendChild(ToElement(doc, "horizonCompleted", this.horizonCompleted));
            settingsNode.AppendChild(ToElement(doc, "collectorShip", this.collectorShip));
            settingsNode.AppendChild(ToElement(doc, "reaperIFF", this.reaperIFF));
            settingsNode.AppendChild(ToElement(doc, "crewAbuct", this.crewAbuct));
            settingsNode.AppendChild(ToElement(doc, "ME2Oculus", this.ME2Oculus));
            settingsNode.AppendChild(ToElement(doc, "ME2Valve", this.ME2Valve));
            settingsNode.AppendChild(ToElement(doc, "ME2Bubble", this.ME2Bubble));
            settingsNode.AppendChild(ToElement(doc, "ME2ending", this.ME2ending));
            settingsNode.AppendChild(ToElement(doc, "N7_WMF", this.N7_WMF));
            settingsNode.AppendChild(ToElement(doc, "N7_ARS", this.N7_ARS));
            settingsNode.AppendChild(ToElement(doc, "N7_ADS", this.N7_ADS));
            settingsNode.AppendChild(ToElement(doc, "N7_MSVE", this.N7_MSVE));
            settingsNode.AppendChild(ToElement(doc, "N7_ESD", this.N7_ESD));
            settingsNode.AppendChild(ToElement(doc, "N7_ERS", this.N7_ERS));
            settingsNode.AppendChild(ToElement(doc, "N7_LO", this.N7_LO));
            settingsNode.AppendChild(ToElement(doc, "recruitMordin", this.recruitMordin));
            settingsNode.AppendChild(ToElement(doc, "recruitGarrus", this.recruitGarrus));
            settingsNode.AppendChild(ToElement(doc, "acquireGrunt", this.acquireGrunt));
            settingsNode.AppendChild(ToElement(doc, "recruitJack", this.recruitJack));
            settingsNode.AppendChild(ToElement(doc, "recruitZaeed", this.recruitZaeed));
            settingsNode.AppendChild(ToElement(doc, "recruitKasumi", this.recruitKasumi));
            settingsNode.AppendChild(ToElement(doc, "recruitTali", this.recruitTali));
            settingsNode.AppendChild(ToElement(doc, "recruitSamara", this.recruitSamara));
            settingsNode.AppendChild(ToElement(doc, "recruitThane", this.recruitThane));
            settingsNode.AppendChild(ToElement(doc, "loyaltyMiranda", this.loyaltyMiranda));
            settingsNode.AppendChild(ToElement(doc, "loyaltyJacob", this.loyaltyJacob));
            settingsNode.AppendChild(ToElement(doc, "loyaltyJack", this.loyaltyJack));
            settingsNode.AppendChild(ToElement(doc, "loyaltyLegion", this.loyaltyLegion));
            settingsNode.AppendChild(ToElement(doc, "loyaltyKasumi", this.loyaltyKasumi));
            settingsNode.AppendChild(ToElement(doc, "loyaltyGarrus", this.loyaltyGarrus));
            settingsNode.AppendChild(ToElement(doc, "loyaltyThane", this.loyaltyThane));
            settingsNode.AppendChild(ToElement(doc, "loyaltyTali", this.loyaltyTali));
            settingsNode.AppendChild(ToElement(doc, "loyaltyMordin", this.loyaltyMordin));
            settingsNode.AppendChild(ToElement(doc, "loyaltyGrunt", this.loyaltyGrunt));
            settingsNode.AppendChild(ToElement(doc, "loyaltySamara", this.loyaltySamara));
            settingsNode.AppendChild(ToElement(doc, "loyaltyZaeed", this.loyaltyZaeed));

            settingsNode.AppendChild(ToElement(doc, "prologue", this.prologue));
            settingsNode.AppendChild(ToElement(doc, "priorityMars", this.priorityMars));
            settingsNode.AppendChild(ToElement(doc, "priorityCitadel", this.priorityCitadel));
            settingsNode.AppendChild(ToElement(doc, "priorityPalaven", this.priorityPalaven));
            settingsNode.AppendChild(ToElement(doc, "prioritySurkesh", this.prioritySurkesh));
            settingsNode.AppendChild(ToElement(doc, "turianPlatoon", this.turianPlatoon));
            settingsNode.AppendChild(ToElement(doc, "kroganRachni", this.kroganRachni));
            settingsNode.AppendChild(ToElement(doc, "priorityTuchanka", this.priorityTuchanka));
            settingsNode.AppendChild(ToElement(doc, "priorityBeforeThessia", this.priorityBeforeThessia));
            settingsNode.AppendChild(ToElement(doc, "priorityGethDreadnought", this.priorityGethDreadnought));
            settingsNode.AppendChild(ToElement(doc, "admiralKoris", this.admiralKoris));
            settingsNode.AppendChild(ToElement(doc, "gethServer", this.gethServer));
            settingsNode.AppendChild(ToElement(doc, "priorityRannoch", this.priorityRannoch));
            settingsNode.AppendChild(ToElement(doc, "priorityThessia", this.priorityThessia));
            settingsNode.AppendChild(ToElement(doc, "priorityHorizon", this.priorityHorizon));
            settingsNode.AppendChild(ToElement(doc, "priorityCerberusHQ", this.priorityCerberusHQ));
            settingsNode.AppendChild(ToElement(doc, "priorityEarth", this.priorityEarth));
            settingsNode.AppendChild(ToElement(doc, "priorityEnding", this.priorityEnding));

            return settingsNode;
        }

        public void SetSettings(XmlNode settings)
        {
            this.runStart1 = ParseBool(settings, "runStart1", true);
            this.runStart2 = ParseBool(settings, "runStart2", true);
            this.runStart3 = ParseBool(settings, "runStart3", true);
            this.ME1Prologue = ParseBool(settings, "ME1Prologue", true);
            this.EdenPrime = ParseBool(settings, "EdenPrime", true);
            this.Citadel = ParseBool(settings, "Citadel", false);
            this.Noveria = ParseBool(settings, "Noveria", true);
            this.Feros = ParseBool(settings, "Feros", true);
            this.Therum = ParseBool(settings, "Therum", true);
            this.Virmire = ParseBool(settings, "Virmire", true);
            this.Ilos = ParseBool(settings, "Ilos", true);
            this.Saren = ParseBool(settings, "Saren", true);

            this.escapeLazarus = ParseBool(settings, "escapeLazarus", true);
            this.freedomProgress = ParseBool(settings, "freedomProgress", true);
            this.horizonCompleted = ParseBool(settings, "horizonCompleted", true);
            this.collectorShip = ParseBool(settings, "collectorShip", true);
            this.reaperIFF = ParseBool(settings, "reaperIFF", true);
            this.crewAbuct = ParseBool(settings, "crewAbuct", true);
            this.ME2Oculus = ParseBool(settings, "ME2Oculus", true);
            this.ME2Valve = ParseBool(settings, "ME2Valve", true);
            this.ME2Bubble = ParseBool(settings, "ME2Bubble", true);
            this.ME2ending = ParseBool(settings, "ME2ending", true);
            this.N7_WMF = ParseBool(settings, "N7_WMF", true);
            this.N7_ARS = ParseBool(settings, "N7_ARS", true);
            this.N7_ADS = ParseBool(settings, "N7_ADS", true);
            this.N7_MSVE = ParseBool(settings, "N7_MSVE", true);
            this.N7_ESD = ParseBool(settings, "N7_ESD", true);
            this.N7_ERS = ParseBool(settings, "N7_ERS", true);
            this.N7_LO = ParseBool(settings, "N7_LO", true);
            this.recruitMordin = ParseBool(settings, "recruitMordin", true);
            this.recruitGarrus = ParseBool(settings, "recruitGarrus", true);
            this.acquireGrunt = ParseBool(settings, "acquireGrunt", true);
            this.recruitJack = ParseBool(settings, "recruitJack", true);
            this.recruitZaeed = ParseBool(settings, "recruitZaeed", true);
            this.recruitKasumi = ParseBool(settings, "recruitKasumi", true);
            this.recruitTali = ParseBool(settings, "recruitTali", true);
            this.recruitSamara = ParseBool(settings, "recruitSamara", true);
            this.recruitThane = ParseBool(settings, "recruitThane", true);
            this.loyaltyMiranda = ParseBool(settings, "loyaltyMiranda", true);
            this.loyaltyJacob = ParseBool(settings, "loyaltyJacob", true);
            this.loyaltyJack = ParseBool(settings, "loyaltyJack", true);
            this.loyaltyLegion = ParseBool(settings, "loyaltyLegion", true);
            this.loyaltyKasumi = ParseBool(settings, "loyaltyKasumi", true);
            this.loyaltyGarrus = ParseBool(settings, "loyaltyGarrus", true);
            this.loyaltyThane = ParseBool(settings, "loyaltyThane", true);
            this.loyaltyTali = ParseBool(settings, "loyaltyTali", true);
            this.loyaltyMordin = ParseBool(settings, "loyaltyMordin", true);
            this.loyaltyGrunt = ParseBool(settings, "loyaltyGrunt", true);
            this.loyaltySamara = ParseBool(settings, "loyaltySamara", true);
            this.loyaltyZaeed = ParseBool(settings, "loyaltyZaeed", true);

            this.prologue = ParseBool(settings, "prologue", true);
            this.priorityMars = ParseBool(settings, "priorityMars", true);
            this.priorityCitadel = ParseBool(settings, "priorityCitadel", true);
            this.priorityPalaven = ParseBool(settings, "priorityPalaven", true);
            this.prioritySurkesh = ParseBool(settings, "prioritySurkesh", true);
            this.turianPlatoon = ParseBool(settings, "turianPlatoon", true);
            this.kroganRachni = ParseBool(settings, "kroganRachni", true);
            this.priorityTuchanka = ParseBool(settings, "priorityTuchanka", true);
            this.priorityBeforeThessia = ParseBool(settings, "priorityBeforeThessia", true);
            this.priorityGethDreadnought = ParseBool(settings, "priorityGethDreadnought", true);
            this.admiralKoris = ParseBool(settings, "admiralKoris", true);
            this.gethServer = ParseBool(settings, "gethServer", true);
            this.priorityRannoch = ParseBool(settings, "priorityRannoch", true);
            this.priorityThessia = ParseBool(settings, "priorityThessia", true);
            this.priorityHorizon = ParseBool(settings, "priorityHorizon", true);
            this.priorityCerberusHQ = ParseBool(settings, "priorityCerberusHQ", true);
            this.priorityEarth = ParseBool(settings, "priorityEarth", true);
            this.priorityEnding = ParseBool(settings, "priorityEnding", true);
        }

        static bool ParseBool(XmlNode settings, string setting, bool default_ = false)
        {
            bool val;
            return settings[setting] != null ? (Boolean.TryParse(settings[setting].InnerText, out val) ? val : default_) : default_;
        }

        static XmlElement ToElement<T>(XmlDocument document, string name, T value)
        {
            XmlElement str = document.CreateElement(name);
            str.InnerText = value.ToString();
            return str;
        }
    }
}

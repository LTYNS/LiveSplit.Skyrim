﻿using System;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.Skyrim
{
    public partial class SkyrimSettings : UserControl
    {
        public bool AutoStart { get; set; }
        public bool AlduinDefeated { get; set; }
        public bool Helgen { get; set; }
        public bool Whiterun { get; set; }
        public bool ThalmorEmbassy { get; set; }
        public bool Esbern { get; set; }
        public bool Riverwood { get; set; }
        public bool TheWall { get; set; }
        public bool Septimus { get; set; }
        public bool MzarkTower { get; set; }
        public bool ClearSky { get; set; }
        public bool HorseClimb { get; set; }
        public bool CutsceneStart { get; set; }
        public bool CutsceneEnd { get; set; }
        public bool Alduin1 { get; set; }
        public bool HighHrothgar{ get; set; }
        public bool Solitude { get; set; }
        public bool Windhelm { get; set; }
        public bool Council { get; set; }
        public bool Odahviing { get; set; }
        public bool EnterSovngarde{ get; set; }
        public bool CollegeOfWinterhold { get; set; }
        public bool Companions { get; set; }
        public bool DarkBrotherhood { get; set; }
        public bool ThievesGuild { get; set; }
        public string AnyPercentTemplate { get; set; }

        public const string TEMPLATE_MRWALRUS = "MrWalrus";
        public const string TEMPLATE_DRTCHOPS = "DrTChops";
        public const string TEMPLATE_GR3YSCALE = "gr3yscale";
        public const string TEMPLATE_DALLETH = "Dalleth";

        private const bool DEFAULT_DRAWWITHOUTLOADS = true;
        private const bool DEFAULT_AUTOSTART = true;
        private const bool DEFAULT_ALDUINDEFEATED = true;
        private const bool DEFAULT_HELGEN = false;
        private const bool DEFAULT_WHITERUN = false;
        private const bool DEFAULT_THALMOREMBASSY = false;
        private const bool DEFAULT_ESBERN = false;
        private const bool DEFAULT_RIVERWOOD = false;
        private const bool DEFAULT_THEWALL = false;
        private const bool DEFAULT_SEPTIMUS = false;
        private const bool DEFAULT_MZARKTOWER = false;
        private const bool DEFAULT_CLEARSKY = false;
        private const bool DEFAULT_HORSECLIMB = false;
        private const bool DEFAULT_CUTSCENESTART = false;
        private const bool DEFAULT_CUTSCENEEND = false;
        private const bool DEFAULT_ALDUIN1 = false;
        private const bool DEFAULT_HIGHHROTHGAR = false;
        private const bool DEFAULT_SOLITUDE = false;
        private const bool DEFAULT_WINDHELM = false;
        private const bool DEFAULT_COUNCIL = false;
        private const bool DEFAULT_ODAHVIING = false;
        private const bool DEFAULT_ENTERSOVNGARDE = false;
        private const bool DEFAULT_COLLEGEOFWINTERHOLD = false;
        private const bool DEFAULT_COMPANIONS = false;
        private const bool DEFAULT_DARKBROTHERHOOD = false;
        private const bool DEFAULT_THIEVESGUILD = false;
        private const string DEFAULT_ANYPERCENTTEMPLATE = TEMPLATE_MRWALRUS;

        public SkyrimSettings()
        {
            InitializeComponent();

            this.chkAutoStart.DataBindings.Add("Checked", this, "AutoStart", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkAlduinDefeated.DataBindings.Add("Checked", this, "AlduinDefeated", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkHelgen.DataBindings.Add("Checked", this, "Helgen", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkWhiterun.DataBindings.Add("Checked", this, "Whiterun", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkThalmorEmbassy.DataBindings.Add("Checked", this, "ThalmorEmbassy", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkEsbern.DataBindings.Add("Checked", this, "Esbern", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkRiverwood.DataBindings.Add("Checked", this, "Riverwood", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkTheWall.DataBindings.Add("Checked", this, "TheWall", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkSeptimus.DataBindings.Add("Checked", this, "Septimus", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkMzarkTower.DataBindings.Add("Checked", this, "MzarkTower", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkClearSky.DataBindings.Add("Checked", this, "ClearSky", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkHorseClimb.DataBindings.Add("Checked", this, "HorseClimb", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCutsceneStart.DataBindings.Add("Checked", this, "CutsceneStart", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCutsceneEnd.DataBindings.Add("Checked", this, "CutsceneEnd", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkAlduin1.DataBindings.Add("Checked", this, "Alduin1", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkHighHrothgar.DataBindings.Add("Checked", this, "HighHrothgar", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkSolitude.DataBindings.Add("Checked", this, "Solitude", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkWindhelm.DataBindings.Add("Checked", this, "Windhelm", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCouncil.DataBindings.Add("Checked", this, "Council", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkOdahviing.DataBindings.Add("Checked", this, "Odahviing", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkEnterSovngarde.DataBindings.Add("Checked", this, "EnterSovngarde", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCollegeOfWinterhold.DataBindings.Add("Checked", this, "CollegeOfWinterhold", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkCompanions.DataBindings.Add("Checked", this, "Companions", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkDarkBrotherhood.DataBindings.Add("Checked", this, "DarkBrotherhood", false, DataSourceUpdateMode.OnPropertyChanged);
            this.chkThievesGuild.DataBindings.Add("Checked", this, "ThievesGuild", false, DataSourceUpdateMode.OnPropertyChanged);

            // defaults
            this.AutoStart = DEFAULT_AUTOSTART;
            this.AlduinDefeated = DEFAULT_ALDUINDEFEATED;
            this.Helgen = DEFAULT_HELGEN;
            this.Whiterun = DEFAULT_WHITERUN;
            this.ThalmorEmbassy = DEFAULT_THALMOREMBASSY;
            this.Esbern = DEFAULT_ESBERN;
            this.Riverwood = DEFAULT_RIVERWOOD;
            this.TheWall = DEFAULT_THEWALL;
            this.Septimus = DEFAULT_SEPTIMUS;
            this.MzarkTower = DEFAULT_MZARKTOWER;
            this.ClearSky = DEFAULT_CLEARSKY;
            this.HorseClimb = DEFAULT_HORSECLIMB;
            this.CutsceneStart = DEFAULT_CUTSCENESTART;
            this.CutsceneEnd = DEFAULT_CUTSCENEEND;
            this.Alduin1 = DEFAULT_ALDUIN1;
            this.HighHrothgar = DEFAULT_HIGHHROTHGAR;
            this.Solitude = DEFAULT_SOLITUDE;
            this.Windhelm = DEFAULT_WINDHELM;
            this.Council = DEFAULT_COUNCIL;
            this.Odahviing = DEFAULT_ODAHVIING;
            this.EnterSovngarde = DEFAULT_ENTERSOVNGARDE;
            this.CollegeOfWinterhold = DEFAULT_COLLEGEOFWINTERHOLD;
            this.Companions = DEFAULT_COMPANIONS;
            this.DarkBrotherhood = DEFAULT_DARKBROTHERHOOD;
            this.ThievesGuild = DEFAULT_THIEVESGUILD;
            this.AnyPercentTemplate = DEFAULT_ANYPERCENTTEMPLATE;
        }

        public XmlNode GetSettings(XmlDocument doc)
        {
            XmlElement settingsNode = doc.CreateElement("Settings");

            settingsNode.AppendChild(ToElement(doc, "Version", Assembly.GetExecutingAssembly().GetName().Version.ToString(3)));

            settingsNode.AppendChild(ToElement(doc, "AutoStart", this.AutoStart));
            settingsNode.AppendChild(ToElement(doc, "AlduinDefeated", this.AlduinDefeated));
            settingsNode.AppendChild(ToElement(doc, "Helgen", this.Helgen));
            settingsNode.AppendChild(ToElement(doc, "Whiterun", this.Whiterun));
            settingsNode.AppendChild(ToElement(doc, "ThalmorEmbassy", this.ThalmorEmbassy));
            settingsNode.AppendChild(ToElement(doc, "Esbern", this.Esbern));
            settingsNode.AppendChild(ToElement(doc, "Riverwood", this.Riverwood));
            settingsNode.AppendChild(ToElement(doc, "TheWall", this.TheWall));
            settingsNode.AppendChild(ToElement(doc, "Septimus", this.Septimus));
            settingsNode.AppendChild(ToElement(doc, "MzarkTower", this.MzarkTower));
            settingsNode.AppendChild(ToElement(doc, "ClearSky", this.ClearSky));
            settingsNode.AppendChild(ToElement(doc, "HorseClimb", this.HorseClimb));
            settingsNode.AppendChild(ToElement(doc, "CutsceneStart", this.CutsceneStart));
            settingsNode.AppendChild(ToElement(doc, "CutsceneEnd", this.CutsceneEnd));
            settingsNode.AppendChild(ToElement(doc, "Alduin1", this.Alduin1));
            settingsNode.AppendChild(ToElement(doc, "HighHrothgar", this.HighHrothgar));
            settingsNode.AppendChild(ToElement(doc, "Solitude", this.Solitude));
            settingsNode.AppendChild(ToElement(doc, "Windhelm", this.Windhelm));
            settingsNode.AppendChild(ToElement(doc, "Council", this.Council));
            settingsNode.AppendChild(ToElement(doc, "Odahviing", this.Odahviing));
            settingsNode.AppendChild(ToElement(doc, "EnterSovngarde", this.EnterSovngarde));
            settingsNode.AppendChild(ToElement(doc, "CollegeOfWinterhold", this.CollegeOfWinterhold));
            settingsNode.AppendChild(ToElement(doc, "Companions", this.Companions));
            settingsNode.AppendChild(ToElement(doc, "DarkBrotherhood", this.DarkBrotherhood));
            settingsNode.AppendChild(ToElement(doc, "ThievesGuild", this.ThievesGuild));
            settingsNode.AppendChild(ToElement(doc, "AnyPercentTemplate", this.AnyPercentTemplate));

            return settingsNode;
        }

        public void SetSettings(XmlNode settings)
        {
            var element = (XmlElement)settings;

            this.DrawWithoutLoads = ParseBool(settings, "DrawWithoutLoads", DEFAULT_DRAWWITHOUTLOADS);
            this.AutoStart = ParseBool(settings, "AutoStart", DEFAULT_AUTOSTART);
            this.AlduinDefeated = ParseBool(settings, "AlduinDefeated", DEFAULT_ALDUINDEFEATED);
            this.Helgen = ParseBool(settings, "Helgen", DEFAULT_HELGEN);
            this.Whiterun = ParseBool(settings, "Whiterun", DEFAULT_WHITERUN);
            this.ThalmorEmbassy = ParseBool(settings, "ThalmorEmbassy", DEFAULT_THALMOREMBASSY);
            this.Esbern = ParseBool(settings, "Esbern", DEFAULT_ESBERN);
            this.Riverwood = ParseBool(settings, "Riverwood", DEFAULT_RIVERWOOD);
            this.TheWall = ParseBool(settings, "TheWall", DEFAULT_THEWALL);
            this.Septimus = ParseBool(settings, "Septimus", DEFAULT_SEPTIMUS);
            this.MzarkTower = ParseBool(settings, "MzarkTower", DEFAULT_RIVERWOOD);
            this.ClearSky = ParseBool(settings, "ClearSky", DEFAULT_CLEARSKY);
            this.HorseClimb = ParseBool(settings, "HorseClimb", DEFAULT_HORSECLIMB);
            this.CutsceneStart = ParseBool(settings, "CutsceneStart", DEFAULT_CUTSCENESTART);
            this.CutsceneEnd = ParseBool(settings, "CutsceneEnd", DEFAULT_CUTSCENEEND);
            this.Alduin1 = ParseBool(settings, "Alduin1", DEFAULT_ALDUIN1);
            this.HighHrothgar = ParseBool(settings, "HighHrothgar", DEFAULT_HIGHHROTHGAR);
            this.Solitude = ParseBool(settings, "Solitude", DEFAULT_SOLITUDE);
            this.Windhelm = ParseBool(settings, "Windhelm", DEFAULT_WINDHELM);
            this.Council = ParseBool(settings, "Council", DEFAULT_COUNCIL);
            this.Odahviing = ParseBool(settings, "Odahviing", DEFAULT_ODAHVIING);
            this.EnterSovngarde = ParseBool(settings, "EnterSovngarde", DEFAULT_ENTERSOVNGARDE);
            this.CollegeOfWinterhold = ParseBool(settings, "CollegeOfWinterhold", DEFAULT_COLLEGEOFWINTERHOLD);
            this.Companions = ParseBool(settings, "Companions", DEFAULT_COMPANIONS);
            this.DarkBrotherhood = ParseBool(settings, "DarkBrotherhood", DEFAULT_DARKBROTHERHOOD);
            this.ThievesGuild = ParseBool(settings, "ThievesGuild", DEFAULT_THIEVESGUILD);

            if (element["AnyPercentTemplate"] != null)
            {
                this.AnyPercentTemplate = element["AnyPercentTemplate"].InnerText.Equals(TEMPLATE_MRWALRUS) || element["AnyPercentTemplate"].InnerText.Equals(TEMPLATE_DRTCHOPS) ||
                    element["AnyPercentTemplate"].InnerText.Equals(TEMPLATE_GR3YSCALE)  || element["AnyPercentTemplate"].InnerText.Equals(TEMPLATE_DALLETH)
                        ? element["AnyPercentTemplate"].InnerText
                        : DEFAULT_ANYPERCENTTEMPLATE;
            }
            else
            {
                this.AnyPercentTemplate = DEFAULT_ANYPERCENTTEMPLATE;
            }

            this.rbMrwalrus.Checked = this.AnyPercentTemplate == TEMPLATE_MRWALRUS;
            this.rbDrtchops.Checked = this.AnyPercentTemplate == TEMPLATE_DRTCHOPS;
            this.rbGr3yscale.Checked = this.AnyPercentTemplate == TEMPLATE_GR3YSCALE;
            this.rbDalleth.Checked = this.AnyPercentTemplate == TEMPLATE_DALLETH;
            UpdateTemplate();
        }

        static bool ParseBool(XmlNode settings, string setting, bool default_ = false)
        {
            bool val;
            return settings[setting] != null ?
                (Boolean.TryParse(settings[setting].InnerText, out val) ? val : default_)
                : default_;
        }

        static XmlElement ToElement<T>(XmlDocument document, string name, T value)
        {
            XmlElement str = document.CreateElement(name);
            str.InnerText = value.ToString();
            return str;
        }

        private void UpdateTemplate()
        {
            if (rbMrwalrus.Checked)
            {
                this.AnyPercentTemplate = TEMPLATE_MRWALRUS;
            }
            else if (rbDrtchops.Checked)
            {
                this.AnyPercentTemplate = TEMPLATE_DRTCHOPS;
            }
            else if (rbDalleth.Checked)
            {
                this.AnyPercentTemplate = TEMPLATE_DALLETH;
            }
            else if (rbGr3yscale.Checked)
            {
                this.AnyPercentTemplate = TEMPLATE_GR3YSCALE;
            }
            this.chkHorseClimb.Enabled = this.AnyPercentTemplate == TEMPLATE_GR3YSCALE;
            this.chkCutsceneStart.Enabled = (this.AnyPercentTemplate == TEMPLATE_DRTCHOPS || this.AnyPercentTemplate == TEMPLATE_DALLETH);
            this.chkCutsceneEnd.Enabled = (this.AnyPercentTemplate == TEMPLATE_GR3YSCALE || this.AnyPercentTemplate == TEMPLATE_DALLETH);
        }

        private void templateRadioButtonChanged(object sender, EventArgs e)
        {
            UpdateTemplate();
        }

        private void llCheckAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!AreAllEnabledCheckboxChecked(flp_AnyPercentSplits.Controls))
            {
                checkAll(flp_AnyPercentSplits.Controls);
            }
            else
            {
                checkAll(flp_AnyPercentSplits.Controls, false);
            }
        }

        private bool AreAllEnabledCheckboxChecked(ControlCollection collection, bool state = true)
        {
            foreach (Control c in collection)
            {
                if (c.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)c;
                    if (checkBox.Checked != state && checkBox.Enabled)
	                {
                        return false;
	                }
                }
            }
            return true;
        }

        private void checkAll(ControlCollection collection, bool state = true)
        {
            foreach (Control c in collection)
            {
                if (c.GetType().Equals(typeof(CheckBox)))
                {
                    CheckBox checkBox = (CheckBox)c;
                    if (checkBox.Checked != state && checkBox.Enabled)
                    {
                        checkBox.Checked = state;
                    }
                }
            }
        }
    }
}

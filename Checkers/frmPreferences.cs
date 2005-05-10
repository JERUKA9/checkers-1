using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Checkers
{
  public class frmPreferences : System.Windows.Forms.Form
  {
    
    private CheckersSettings settings;
    private System.Windows.Forms.CheckBox chkMuteSounds;
    private System.Windows.Forms.Button btnDefault;
    private string [] sounds;
    
    #region API Imports
    
    [DllImport("winmm.dll", EntryPoint="PlaySound", SetLastError=true, CallingConvention=CallingConvention.Winapi)]
    static extern bool sndPlaySound( string pszSound, IntPtr hMod, SoundFlags sf );

    [Flags]
      public enum SoundFlags : int
    {
      SND_SYNC = 0x0000,  /* play synchronously (default) */
      SND_ASYNC = 0x0001,  /* play asynchronously */
      SND_NODEFAULT = 0x0002,  /* silence (!default) if sound not found */
      SND_MEMORY = 0x0004,  /* pszSound points to a memory file */
      SND_LOOP = 0x0008,  /* loop the sound until next sndPlaySound */
      SND_NOSTOP = 0x0010,  /* don't stop any currently playing sound */
      SND_NOWAIT = 0x00002000, /* don't wait if the driver is busy */
      SND_ALIAS = 0x00010000, /* name is a registry alias */
      SND_ALIAS_ID = 0x00110000, /* alias is a predefined ID */
      SND_FileName = 0x00020000, /* name is file name */
      SND_RESOURCE = 0x00040004  /* name is resource name or atom */
    }
    
    #endregion
    
    #region Class Controls
    
    private System.Windows.Forms.OpenFileDialog dlgOpenSound;
    private System.Windows.Forms.Label lblBoardBackColor;
    private System.Windows.Forms.PictureBox picBoardBackColor;
    private System.Windows.Forms.Label lblBackColor;
    private System.Windows.Forms.PictureBox picBackColor;
    private System.Windows.Forms.Label lblBoardForeColor;
    private System.Windows.Forms.PictureBox picBoardForeColor;
    private System.Windows.Forms.PictureBox picBoardGridColor;
    private System.Windows.Forms.Label lblBoardGridColor;
    private System.Windows.Forms.Panel panTitle;
    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.TabControl tabPreferences;
    private System.Windows.Forms.TabPage tabGeneral;
    private System.Windows.Forms.TabPage tabBoard;
    private System.Windows.Forms.TabPage tabSounds;
    private Pabo.MozBar.MozPane mozPreferences;
    private Pabo.MozBar.MozItem mozGeneral;
    private Pabo.MozBar.MozItem mozBoard;
    private Pabo.MozBar.MozItem mozSounds;
    private System.Windows.Forms.ImageList imlTabs;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.GroupBox grpNet;
    private System.Windows.Forms.CheckBox chkShowNetPanelOnMessage;
    private System.Windows.Forms.CheckBox chkFlashWindowOnTurn;
    private System.Windows.Forms.CheckBox chkFlashWindowOnMessage;
    private System.Windows.Forms.ColorDialog dlgColorDialog;
    private System.Windows.Forms.ListBox lstSounds;
    private System.Windows.Forms.Label lblSounds;
    private System.Windows.Forms.Button btnSoundFile;
    private System.Windows.Forms.TextBox txtSoundFile;
    private System.Windows.Forms.Button btnSoundPreview;
    private System.Windows.Forms.Label lblSoundFile;
    private System.ComponentModel.IContainer components;
    
    #endregion
    
    #region Class Construction
    
    public frmPreferences()
    {
      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();
    }
    
    #region Windows Form Designer generated code
    
    /// <summary> Clean up any resources being used. </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if(components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }
    
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmPreferences));
      this.mozPreferences = new Pabo.MozBar.MozPane();
      this.imlTabs = new System.Windows.Forms.ImageList(this.components);
      this.mozGeneral = new Pabo.MozBar.MozItem();
      this.mozBoard = new Pabo.MozBar.MozItem();
      this.mozSounds = new Pabo.MozBar.MozItem();
      this.tabPreferences = new System.Windows.Forms.TabControl();
      this.tabGeneral = new System.Windows.Forms.TabPage();
      this.grpNet = new System.Windows.Forms.GroupBox();
      this.chkFlashWindowOnTurn = new System.Windows.Forms.CheckBox();
      this.chkFlashWindowOnMessage = new System.Windows.Forms.CheckBox();
      this.chkShowNetPanelOnMessage = new System.Windows.Forms.CheckBox();
      this.tabBoard = new System.Windows.Forms.TabPage();
      this.lblBoardBackColor = new System.Windows.Forms.Label();
      this.picBoardBackColor = new System.Windows.Forms.PictureBox();
      this.lblBackColor = new System.Windows.Forms.Label();
      this.picBackColor = new System.Windows.Forms.PictureBox();
      this.lblBoardForeColor = new System.Windows.Forms.Label();
      this.picBoardForeColor = new System.Windows.Forms.PictureBox();
      this.picBoardGridColor = new System.Windows.Forms.PictureBox();
      this.lblBoardGridColor = new System.Windows.Forms.Label();
      this.tabSounds = new System.Windows.Forms.TabPage();
      this.lstSounds = new System.Windows.Forms.ListBox();
      this.lblSounds = new System.Windows.Forms.Label();
      this.txtSoundFile = new System.Windows.Forms.TextBox();
      this.btnSoundFile = new System.Windows.Forms.Button();
      this.btnSoundPreview = new System.Windows.Forms.Button();
      this.lblSoundFile = new System.Windows.Forms.Label();
      this.lblTitle = new System.Windows.Forms.Label();
      this.panTitle = new System.Windows.Forms.Panel();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnOK = new System.Windows.Forms.Button();
      this.dlgColorDialog = new System.Windows.Forms.ColorDialog();
      this.dlgOpenSound = new System.Windows.Forms.OpenFileDialog();
      this.chkMuteSounds = new System.Windows.Forms.CheckBox();
      this.btnDefault = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.mozPreferences)).BeginInit();
      this.mozPreferences.SuspendLayout();
      this.tabPreferences.SuspendLayout();
      this.tabGeneral.SuspendLayout();
      this.grpNet.SuspendLayout();
      this.tabBoard.SuspendLayout();
      this.tabSounds.SuspendLayout();
      this.panTitle.SuspendLayout();
      this.SuspendLayout();
      // 
      // mozPreferences
      // 
      this.mozPreferences.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left);
      this.mozPreferences.BackColor = System.Drawing.Color.White;
      this.mozPreferences.BorderColor = System.Drawing.Color.FromArgb(((System.Byte)(127)), ((System.Byte)(157)), ((System.Byte)(185)));
      this.mozPreferences.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid;
      this.mozPreferences.ImageList = this.imlTabs;
      this.mozPreferences.ItemBorderStyles.Focus = System.Windows.Forms.ButtonBorderStyle.Solid;
      this.mozPreferences.ItemBorderStyles.Normal = System.Windows.Forms.ButtonBorderStyle.None;
      this.mozPreferences.ItemBorderStyles.Selected = System.Windows.Forms.ButtonBorderStyle.Solid;
      this.mozPreferences.ItemColors.Background = System.Drawing.Color.White;
      this.mozPreferences.ItemColors.Border = System.Drawing.Color.Black;
      this.mozPreferences.ItemColors.Divider = System.Drawing.Color.FromArgb(((System.Byte)(127)), ((System.Byte)(157)), ((System.Byte)(185)));
      this.mozPreferences.ItemColors.FocusBackground = System.Drawing.Color.FromArgb(((System.Byte)(224)), ((System.Byte)(232)), ((System.Byte)(246)));
      this.mozPreferences.ItemColors.FocusBorder = System.Drawing.Color.FromArgb(((System.Byte)(152)), ((System.Byte)(180)), ((System.Byte)(226)));
      this.mozPreferences.ItemColors.SelectedBackground = System.Drawing.Color.FromArgb(((System.Byte)(193)), ((System.Byte)(210)), ((System.Byte)(238)));
      this.mozPreferences.ItemColors.SelectedBorder = System.Drawing.Color.FromArgb(((System.Byte)(49)), ((System.Byte)(106)), ((System.Byte)(197)));
      this.mozPreferences.ItemColors.Text = System.Drawing.Color.Black;
      this.mozPreferences.Items.AddRange(new Pabo.MozBar.MozItem[] {
                                                                     this.mozGeneral,
                                                                     this.mozBoard,
                                                                     this.mozSounds});
      this.mozPreferences.Location = new System.Drawing.Point(4, 4);
      this.mozPreferences.MaxSelectedItems = 1;
      this.mozPreferences.Name = "mozPreferences";
      this.mozPreferences.Padding.Horizontal = 2;
      this.mozPreferences.Padding.Vertical = 2;
      this.mozPreferences.Size = new System.Drawing.Size(76, 296);
      this.mozPreferences.Style = Pabo.MozBar.paneStyle.Vertical;
      this.mozPreferences.TabIndex = 0;
      this.mozPreferences.TabStop = false;
      this.mozPreferences.Toggle = false;
      // 
      // imlTabs
      // 
      this.imlTabs.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
      this.imlTabs.ImageSize = new System.Drawing.Size(32, 32);
      this.imlTabs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlTabs.ImageStream")));
      this.imlTabs.TransparentColor = System.Drawing.Color.Transparent;
      // 
      // mozGeneral
      // 
      this.mozGeneral.Images.Focus = -1;
      this.mozGeneral.Images.Normal = 0;
      this.mozGeneral.Images.Selected = -1;
      this.mozGeneral.ItemStyle = Pabo.MozBar.itemStyle.TextAndPicture;
      this.mozGeneral.Location = new System.Drawing.Point(2, 2);
      this.mozGeneral.Name = "mozGeneral";
      this.mozGeneral.Size = new System.Drawing.Size(72, 57);
      this.mozGeneral.TabIndex = 0;
      this.mozGeneral.TabStop = false;
      this.mozGeneral.Text = "General";
      this.mozGeneral.TextAlign = Pabo.MozBar.textAlign.Bottom;
      this.mozGeneral.Click += new System.EventHandler(this.mozGeneral_Click);
      // 
      // mozBoard
      // 
      this.mozBoard.Images.Focus = -1;
      this.mozBoard.Images.Normal = 1;
      this.mozBoard.Images.Selected = -1;
      this.mozBoard.ItemStyle = Pabo.MozBar.itemStyle.TextAndPicture;
      this.mozBoard.Location = new System.Drawing.Point(2, 61);
      this.mozBoard.Name = "mozBoard";
      this.mozBoard.Size = new System.Drawing.Size(72, 57);
      this.mozBoard.TabIndex = 1;
      this.mozBoard.TabStop = false;
      this.mozBoard.Text = "Appearance";
      this.mozBoard.TextAlign = Pabo.MozBar.textAlign.Bottom;
      this.mozBoard.Click += new System.EventHandler(this.mozBoard_Click);
      // 
      // mozSounds
      // 
      this.mozSounds.Images.Focus = -1;
      this.mozSounds.Images.Normal = 2;
      this.mozSounds.Images.Selected = -1;
      this.mozSounds.ItemStyle = Pabo.MozBar.itemStyle.TextAndPicture;
      this.mozSounds.Location = new System.Drawing.Point(2, 120);
      this.mozSounds.Name = "mozSounds";
      this.mozSounds.Size = new System.Drawing.Size(72, 57);
      this.mozSounds.TabIndex = 2;
      this.mozSounds.TabStop = false;
      this.mozSounds.Text = "Sounds";
      this.mozSounds.TextAlign = Pabo.MozBar.textAlign.Bottom;
      this.mozSounds.Click += new System.EventHandler(this.mozSounds_Click);
      // 
      // tabPreferences
      // 
      this.tabPreferences.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.tabPreferences.Appearance = System.Windows.Forms.TabAppearance.Buttons;
      this.tabPreferences.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                                 this.tabGeneral,
                                                                                 this.tabBoard,
                                                                                 this.tabSounds});
      this.tabPreferences.ImageList = this.imlTabs;
      this.tabPreferences.Location = new System.Drawing.Point(84, -12);
      this.tabPreferences.Multiline = true;
      this.tabPreferences.Name = "tabPreferences";
      this.tabPreferences.SelectedIndex = 0;
      this.tabPreferences.Size = new System.Drawing.Size(386, 316);
      this.tabPreferences.TabIndex = 2;
      this.tabPreferences.SelectedIndexChanged += new System.EventHandler(this.tabPreferences_SelectedIndexChanged);
      // 
      // tabGeneral
      // 
      this.tabGeneral.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                             this.grpNet});
      this.tabGeneral.ImageIndex = 0;
      this.tabGeneral.Location = new System.Drawing.Point(4, 42);
      this.tabGeneral.Name = "tabGeneral";
      this.tabGeneral.Size = new System.Drawing.Size(378, 270);
      this.tabGeneral.TabIndex = 0;
      this.tabGeneral.Text = "General";
      // 
      // grpNet
      // 
      this.grpNet.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.grpNet.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                         this.chkFlashWindowOnTurn,
                                                                         this.chkFlashWindowOnMessage,
                                                                         this.chkShowNetPanelOnMessage});
      this.grpNet.Location = new System.Drawing.Point(0, 12);
      this.grpNet.Name = "grpNet";
      this.grpNet.Size = new System.Drawing.Size(376, 92);
      this.grpNet.TabIndex = 0;
      this.grpNet.TabStop = false;
      this.grpNet.Text = "Net Settings";
      // 
      // chkFlashWindowOnTurn
      // 
      this.chkFlashWindowOnTurn.Location = new System.Drawing.Point(8, 24);
      this.chkFlashWindowOnTurn.Name = "chkFlashWindowOnTurn";
      this.chkFlashWindowOnTurn.Size = new System.Drawing.Size(360, 20);
      this.chkFlashWindowOnTurn.TabIndex = 0;
      this.chkFlashWindowOnTurn.Text = "Flash window when your turn";
      // 
      // chkFlashWindowOnMessage
      // 
      this.chkFlashWindowOnMessage.Location = new System.Drawing.Point(8, 44);
      this.chkFlashWindowOnMessage.Name = "chkFlashWindowOnMessage";
      this.chkFlashWindowOnMessage.Size = new System.Drawing.Size(360, 20);
      this.chkFlashWindowOnMessage.TabIndex = 1;
      this.chkFlashWindowOnMessage.Text = "Flash window when a message is received";
      // 
      // chkShowNetPanelOnMessage
      // 
      this.chkShowNetPanelOnMessage.Location = new System.Drawing.Point(8, 64);
      this.chkShowNetPanelOnMessage.Name = "chkShowNetPanelOnMessage";
      this.chkShowNetPanelOnMessage.Size = new System.Drawing.Size(360, 20);
      this.chkShowNetPanelOnMessage.TabIndex = 2;
      this.chkShowNetPanelOnMessage.Text = "Show Net Panel when message is received";
      // 
      // tabBoard
      // 
      this.tabBoard.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                           this.lblBoardBackColor,
                                                                           this.picBoardBackColor,
                                                                           this.lblBackColor,
                                                                           this.picBackColor,
                                                                           this.lblBoardForeColor,
                                                                           this.picBoardForeColor,
                                                                           this.picBoardGridColor,
                                                                           this.lblBoardGridColor});
      this.tabBoard.ImageIndex = 1;
      this.tabBoard.Location = new System.Drawing.Point(4, 42);
      this.tabBoard.Name = "tabBoard";
      this.tabBoard.Size = new System.Drawing.Size(378, 270);
      this.tabBoard.TabIndex = 1;
      this.tabBoard.Text = "Appearance";
      // 
      // lblBoardBackColor
      // 
      this.lblBoardBackColor.Location = new System.Drawing.Point(45, 56);
      this.lblBoardBackColor.Name = "lblBoardBackColor";
      this.lblBoardBackColor.Size = new System.Drawing.Size(324, 16);
      this.lblBoardBackColor.TabIndex = 9;
      this.lblBoardBackColor.Text = "Board Background Color";
      // 
      // picBoardBackColor
      // 
      this.picBoardBackColor.BackColor = System.Drawing.Color.White;
      this.picBoardBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picBoardBackColor.Location = new System.Drawing.Point(9, 48);
      this.picBoardBackColor.Name = "picBoardBackColor";
      this.picBoardBackColor.Size = new System.Drawing.Size(32, 32);
      this.picBoardBackColor.TabIndex = 7;
      this.picBoardBackColor.TabStop = false;
      this.picBoardBackColor.Click += new System.EventHandler(this.picColor_Click);
      // 
      // lblBackColor
      // 
      this.lblBackColor.Location = new System.Drawing.Point(45, 20);
      this.lblBackColor.Name = "lblBackColor";
      this.lblBackColor.Size = new System.Drawing.Size(324, 16);
      this.lblBackColor.TabIndex = 8;
      this.lblBackColor.Text = "Background Color";
      // 
      // picBackColor
      // 
      this.picBackColor.BackColor = System.Drawing.Color.White;
      this.picBackColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picBackColor.Location = new System.Drawing.Point(9, 12);
      this.picBackColor.Name = "picBackColor";
      this.picBackColor.Size = new System.Drawing.Size(32, 32);
      this.picBackColor.TabIndex = 4;
      this.picBackColor.TabStop = false;
      this.picBackColor.Click += new System.EventHandler(this.picColor_Click);
      // 
      // lblBoardForeColor
      // 
      this.lblBoardForeColor.Location = new System.Drawing.Point(45, 92);
      this.lblBoardForeColor.Name = "lblBoardForeColor";
      this.lblBoardForeColor.Size = new System.Drawing.Size(324, 16);
      this.lblBoardForeColor.TabIndex = 10;
      this.lblBoardForeColor.Text = "Board Foreground Color";
      // 
      // picBoardForeColor
      // 
      this.picBoardForeColor.BackColor = System.Drawing.Color.White;
      this.picBoardForeColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picBoardForeColor.Location = new System.Drawing.Point(9, 84);
      this.picBoardForeColor.Name = "picBoardForeColor";
      this.picBoardForeColor.Size = new System.Drawing.Size(32, 32);
      this.picBoardForeColor.TabIndex = 5;
      this.picBoardForeColor.TabStop = false;
      this.picBoardForeColor.Click += new System.EventHandler(this.picColor_Click);
      // 
      // picBoardGridColor
      // 
      this.picBoardGridColor.BackColor = System.Drawing.Color.White;
      this.picBoardGridColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.picBoardGridColor.Location = new System.Drawing.Point(9, 120);
      this.picBoardGridColor.Name = "picBoardGridColor";
      this.picBoardGridColor.Size = new System.Drawing.Size(32, 32);
      this.picBoardGridColor.TabIndex = 6;
      this.picBoardGridColor.TabStop = false;
      this.picBoardGridColor.Click += new System.EventHandler(this.picColor_Click);
      // 
      // lblBoardGridColor
      // 
      this.lblBoardGridColor.Location = new System.Drawing.Point(45, 128);
      this.lblBoardGridColor.Name = "lblBoardGridColor";
      this.lblBoardGridColor.Size = new System.Drawing.Size(324, 16);
      this.lblBoardGridColor.TabIndex = 11;
      this.lblBoardGridColor.Text = "Board Foreground Color";
      // 
      // tabSounds
      // 
      this.tabSounds.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                            this.chkMuteSounds,
                                                                            this.lstSounds,
                                                                            this.lblSounds,
                                                                            this.txtSoundFile,
                                                                            this.btnSoundFile,
                                                                            this.btnSoundPreview,
                                                                            this.lblSoundFile});
      this.tabSounds.ImageIndex = 2;
      this.tabSounds.Location = new System.Drawing.Point(4, 42);
      this.tabSounds.Name = "tabSounds";
      this.tabSounds.Size = new System.Drawing.Size(378, 270);
      this.tabSounds.TabIndex = 2;
      this.tabSounds.Text = "Sounds";
      // 
      // lstSounds
      // 
      this.lstSounds.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lstSounds.IntegralHeight = false;
      this.lstSounds.Location = new System.Drawing.Point(0, 32);
      this.lstSounds.Name = "lstSounds";
      this.lstSounds.Size = new System.Drawing.Size(376, 164);
      this.lstSounds.TabIndex = 1;
      this.lstSounds.SelectedIndexChanged += new System.EventHandler(this.lstSounds_SelectedIndexChanged);
      // 
      // lblSounds
      // 
      this.lblSounds.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lblSounds.Location = new System.Drawing.Point(0, 12);
      this.lblSounds.Name = "lblSounds";
      this.lblSounds.Size = new System.Drawing.Size(380, 20);
      this.lblSounds.TabIndex = 0;
      this.lblSounds.Text = "Game Sounds:";
      this.lblSounds.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // txtSoundFile
      // 
      this.txtSoundFile.Location = new System.Drawing.Point(0, 220);
      this.txtSoundFile.Name = "txtSoundFile";
      this.txtSoundFile.Size = new System.Drawing.Size(328, 20);
      this.txtSoundFile.TabIndex = 3;
      this.txtSoundFile.Text = "";
      this.txtSoundFile.TextChanged += new System.EventHandler(this.txtSoundFile_TextChanged);
      // 
      // btnSoundFile
      // 
      this.btnSoundFile.Location = new System.Drawing.Point(356, 220);
      this.btnSoundFile.Name = "btnSoundFile";
      this.btnSoundFile.Size = new System.Drawing.Size(20, 20);
      this.btnSoundFile.TabIndex = 5;
      this.btnSoundFile.Text = "..";
      this.btnSoundFile.Click += new System.EventHandler(this.btnSoundFile_Click);
      // 
      // btnSoundPreview
      // 
      this.btnSoundPreview.Location = new System.Drawing.Point(332, 220);
      this.btnSoundPreview.Name = "btnSoundPreview";
      this.btnSoundPreview.Size = new System.Drawing.Size(20, 20);
      this.btnSoundPreview.TabIndex = 4;
      this.btnSoundPreview.Text = "!!";
      this.btnSoundPreview.Click += new System.EventHandler(this.btnSoundPreview_Click);
      // 
      // lblSoundFile
      // 
      this.lblSoundFile.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lblSoundFile.Location = new System.Drawing.Point(0, 200);
      this.lblSoundFile.Name = "lblSoundFile";
      this.lblSoundFile.Size = new System.Drawing.Size(380, 20);
      this.lblSoundFile.TabIndex = 2;
      this.lblSoundFile.Text = "Sound File:";
      this.lblSoundFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lblTitle
      // 
      this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.lblTitle.BackColor = System.Drawing.SystemColors.Highlight;
      this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
      this.lblTitle.ForeColor = System.Drawing.SystemColors.HighlightText;
      this.lblTitle.Location = new System.Drawing.Point(0, 4);
      this.lblTitle.Name = "lblTitle";
      this.lblTitle.Size = new System.Drawing.Size(382, 24);
      this.lblTitle.TabIndex = 0;
      this.lblTitle.Text = "General";
      // 
      // panTitle
      // 
      this.panTitle.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
        | System.Windows.Forms.AnchorStyles.Right);
      this.panTitle.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                           this.lblTitle});
      this.panTitle.Location = new System.Drawing.Point(84, 0);
      this.panTitle.Name = "panTitle";
      this.panTitle.Size = new System.Drawing.Size(384, 28);
      this.panTitle.TabIndex = 1;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
      this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnCancel.Location = new System.Drawing.Point(378, 308);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(88, 36);
      this.btnCancel.TabIndex = 4;
      this.btnCancel.Text = "&Cancel";
      // 
      // btnOK
      // 
      this.btnOK.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnOK.Location = new System.Drawing.Point(282, 308);
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size(88, 36);
      this.btnOK.TabIndex = 3;
      this.btnOK.Text = "&OK";
      // 
      // dlgColorDialog
      // 
      this.dlgColorDialog.AnyColor = true;
      this.dlgColorDialog.FullOpen = true;
      // 
      // dlgOpenSound
      // 
      this.dlgOpenSound.DefaultExt = "wav";
      this.dlgOpenSound.Filter = "Wave Files (*.wav)|*.wav|All Files (*.*)|*.*";
      this.dlgOpenSound.ReadOnlyChecked = true;
      this.dlgOpenSound.Title = "Browse for Sound";
      // 
      // chkMuteSounds
      // 
      this.chkMuteSounds.Location = new System.Drawing.Point(0, 248);
      this.chkMuteSounds.Name = "chkMuteSounds";
      this.chkMuteSounds.Size = new System.Drawing.Size(376, 20);
      this.chkMuteSounds.TabIndex = 6;
      this.chkMuteSounds.Text = "Mute all Sounds";
      // 
      // btnDefault
      // 
      this.btnDefault.Location = new System.Drawing.Point(176, 308);
      this.btnDefault.Name = "btnDefault";
      this.btnDefault.Size = new System.Drawing.Size(88, 36);
      this.btnDefault.TabIndex = 5;
      this.btnDefault.Text = "&Default";
      this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
      // 
      // frmPreferences
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.CancelButton = this.btnCancel;
      this.ClientSize = new System.Drawing.Size(474, 351);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.btnDefault,
                                                                  this.btnCancel,
                                                                  this.btnOK,
                                                                  this.mozPreferences,
                                                                  this.panTitle,
                                                                  this.tabPreferences});
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Name = "frmPreferences";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Checkers Preferences";
      this.Load += new System.EventHandler(this.frmPreferences_Load);
      ((System.ComponentModel.ISupportInitialize)(this.mozPreferences)).EndInit();
      this.mozPreferences.ResumeLayout(false);
      this.tabPreferences.ResumeLayout(false);
      this.tabGeneral.ResumeLayout(false);
      this.grpNet.ResumeLayout(false);
      this.tabBoard.ResumeLayout(false);
      this.tabSounds.ResumeLayout(false);
      this.panTitle.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    
    #endregion

    private void tabPreferences_SelectedIndexChanged(object sender, System.EventArgs e)
    {
      if (tabPreferences.SelectedIndex == -1) return;
      lblTitle.Text = tabPreferences.SelectedTab.Text;
      mozPreferences.Items[tabPreferences.SelectedIndex].SelectItem();
    }
    
    #endregion
    
    #region Public Properties
    
    public CheckersSettings Settings
    {
      get { return settings; }
      set { settings = value; }
    }
    
    #endregion
    
    public new DialogResult ShowDialog (IWin32Window owner)
    {
      if (settings == null) settings = new CheckersSettings();
      
      // Show settings
      ShowSettings();
      
      // Show dialog
      DialogResult result = base.ShowDialog(owner);
      
      // Set properties
      if (result != DialogResult.Cancel)
      {
        settings = new CheckersSettings();
        // Net settings
        settings.FlashWindowOnTurn = chkFlashWindowOnTurn.Checked;
        settings.FlashWindowOnMessage = chkFlashWindowOnMessage.Checked;
        settings.ShowNetPanelOnMessage = chkShowNetPanelOnMessage.Checked;
        // Board appearance
        settings.BackColor = picBackColor.BackColor;
        settings.BoardBackColor = picBoardBackColor.BackColor;
        settings.BoardForeColor = picBoardForeColor.BackColor;
        settings.BoardGridColor = picBoardGridColor.BackColor;
        // Sounds
        settings.sounds = sounds;
        settings.MuteSounds = chkMuteSounds.Checked;
        // Save the settings
        settings.Save();
      }
      return result;
    }
    
    private void frmPreferences_Load (object sender, System.EventArgs e)
    {
      // Show first tab
      mozPreferences.Items[0].SelectItem();
    }
    
    private void btnDefault_Click (object sender, System.EventArgs e)
    {
      if (MessageBox.Show(this, "All settings will be lost. Reset to default settings?", "Checkers", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
      settings = new CheckersSettings();
      ShowSettings();
    }
    
    private void mozGeneral_Click (object sender, System.EventArgs e)
    { tabPreferences.SelectedTab = tabGeneral; }
    private void mozBoard_Click(object sender, System.EventArgs e)
    { tabPreferences.SelectedTab = tabBoard; }
    private void mozSounds_Click(object sender, System.EventArgs e)
    { tabPreferences.SelectedTab = tabSounds; }
    
    private void picColor_Click (object sender, System.EventArgs e)
    {
      dlgColorDialog.Color = ((PictureBox)sender).BackColor;
      if (dlgColorDialog.ShowDialog(this) == DialogResult.Cancel) return;
      ((PictureBox)sender).BackColor = dlgColorDialog.Color;
    }
    
    private void lstSounds_SelectedIndexChanged (object sender, System.EventArgs e)
    {
      if (lstSounds.SelectedItem == null) return;
      txtSoundFile.Text = sounds[(int)typeof(CheckersSounds).GetField((string)lstSounds.SelectedItem).GetValue(null)];
      //txtSoundFile.Text = (string)sounds.GetType().GetField((string)lstSounds.SelectedItem).GetValue(sounds);
    }
    private void txtSoundFile_TextChanged (object sender, System.EventArgs e)
    {
      if (lstSounds.SelectedIndex == -1) return;
      sounds[(int)typeof(CheckersSounds).GetField((string)lstSounds.SelectedItem).GetValue(null)] = txtSoundFile.Text;
      //sounds.GetType().GetField((string)lstSounds.SelectedItem).SetValue(sounds, txtSoundFile.Text);
    }
    private void btnSoundPreview_Click (object sender, System.EventArgs e)
    {
      if (lstSounds.SelectedIndex == -1) return;
      PlaySound(txtSoundFile.Text);
    }
    
    private void btnSoundFile_Click (object sender, System.EventArgs e)
    {
      if (lstSounds.SelectedIndex == -1) return;
      string soundsPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Sounds";
      string fileName = (( Path.IsPathRooted(txtSoundFile.Text) )?( txtSoundFile.Text ):( soundsPath + "\\" + txtSoundFile.Text ));
      
      if (File.Exists(fileName)) dlgOpenSound.FileName = fileName;
      else dlgOpenSound.InitialDirectory = Path.GetDirectoryName(fileName);
      // Show the dialog
      if (dlgOpenSound.ShowDialog(this) == DialogResult.Cancel) return;
      // Get the sound file
      string newFileName = dlgOpenSound.FileName;
      string common = Path.GetDirectoryName(newFileName);
      if (common.ToLower() == soundsPath.ToLower())
        newFileName = newFileName.Substring(common.Length+1);
      txtSoundFile.Text = newFileName;
    }
    
    private void ShowSettings ()
    {
      // Net settings
      chkFlashWindowOnTurn.Checked = settings.FlashWindowOnTurn;
      chkFlashWindowOnMessage.Checked = settings.FlashWindowOnMessage;
      chkShowNetPanelOnMessage.Checked = settings.ShowNetPanelOnMessage;
      // Board appearance
      picBackColor.BackColor = settings.BackColor;
      picBoardBackColor.BackColor = settings.BoardBackColor;
      picBoardForeColor.BackColor = settings.BoardForeColor;
      picBoardGridColor.BackColor = settings.BoardGridColor;
      // Sounds
      sounds = (string [])settings.sounds.Clone();
      lstSounds.Items.Clear();
      foreach (FieldInfo field in typeof(CheckersSounds).GetFields())
      {
        if ((!field.IsPublic) || (field.IsSpecialName)) continue;
        lstSounds.Items.Add(field.Name);
      }
      chkMuteSounds.Checked = settings.MuteSounds;
      if (lstSounds.Items.Count > 0) lstSounds.SelectedIndex = 0;
    }
    
    private void PlaySound (string soundFileName)
    {
      string fileName = (( Path.IsPathRooted(soundFileName) )?( soundFileName ):( Path.GetDirectoryName(Application.ExecutablePath) + "\\Sounds\\" + soundFileName ));
      // Play sound
      sndPlaySound(fileName, IntPtr.Zero, (SoundFlags.SND_FileName | SoundFlags.SND_ASYNC | SoundFlags.SND_NOWAIT));
    }
  }
}
namespace Bataille
{
    partial class frmGame
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGame));
            this.btnTools = new System.Windows.Forms.Button();
            this.pnlLocalCard = new System.Windows.Forms.Panel();
            this.pnlOpponentCard = new System.Windows.Forms.Panel();
            this.pnlOpponentDraw = new System.Windows.Forms.Panel();
            this.tmrAnimationLocal = new System.Windows.Forms.Timer(this.components);
            this.tmrAnimationOpponent = new System.Windows.Forms.Timer(this.components);
            this.pnlLocalDraw = new System.Windows.Forms.Panel();
            this.gbScore = new System.Windows.Forms.GroupBox();
            this.lblHisPoints = new System.Windows.Forms.Label();
            this.lblNone = new System.Windows.Forms.Label();
            this.lblMyPoints = new System.Windows.Forms.Label();
            this.lblMyPointsPlus = new System.Windows.Forms.Label();
            this.lblHisPointsPlus = new System.Windows.Forms.Label();
            this.lblBataille = new System.Windows.Forms.Label();
            this.lblAlert = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblMyPointsPlusSymbol = new System.Windows.Forms.Label();
            this.lblHisPointsPlusSymbol = new System.Windows.Forms.Label();
            this.lblTitleHoldingPoints = new System.Windows.Forms.Label();
            this.tmrWaiting4Player = new System.Windows.Forms.Timer(this.components);
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.gbScore.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTools
            // 
            this.btnTools.CausesValidation = false;
            this.btnTools.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTools.Image = ((System.Drawing.Image)(resources.GetObject("btnTools.Image")));
            this.btnTools.Location = new System.Drawing.Point(469, 371);
            this.btnTools.Name = "btnTools";
            this.btnTools.Size = new System.Drawing.Size(31, 30);
            this.btnTools.TabIndex = 2;
            this.btnTools.Text = " ";
            this.btnTools.UseVisualStyleBackColor = true;
            this.btnTools.Click += new System.EventHandler(this.btnTools_Click);
            // 
            // pnlLocalCard
            // 
            this.pnlLocalCard.BackColor = System.Drawing.Color.Snow;
            this.pnlLocalCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLocalCard.Location = new System.Drawing.Point(152, 148);
            this.pnlLocalCard.Name = "pnlLocalCard";
            this.pnlLocalCard.Size = new System.Drawing.Size(124, 168);
            this.pnlLocalCard.TabIndex = 3;
            this.pnlLocalCard.Visible = false;
            this.pnlLocalCard.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlLocalCard_Paint);
            // 
            // pnlOpponentCard
            // 
            this.pnlOpponentCard.BackColor = System.Drawing.Color.Snow;
            this.pnlOpponentCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOpponentCard.Location = new System.Drawing.Point(234, 96);
            this.pnlOpponentCard.Name = "pnlOpponentCard";
            this.pnlOpponentCard.Size = new System.Drawing.Size(124, 168);
            this.pnlOpponentCard.TabIndex = 3;
            this.pnlOpponentCard.Visible = false;
            this.pnlOpponentCard.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlOpponentCard_Paint);
            // 
            // pnlOpponentDraw
            // 
            this.pnlOpponentDraw.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlOpponentDraw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOpponentDraw.Enabled = false;
            this.pnlOpponentDraw.Location = new System.Drawing.Point(376, 97);
            this.pnlOpponentDraw.Name = "pnlOpponentDraw";
            this.pnlOpponentDraw.Size = new System.Drawing.Size(124, 168);
            this.pnlOpponentDraw.TabIndex = 5;
            // 
            // tmrAnimationLocal
            // 
            this.tmrAnimationLocal.Interval = 25;
            this.tmrAnimationLocal.Tick += new System.EventHandler(this.tmrAnimationLocal_Tick);
            // 
            // tmrAnimationOpponent
            // 
            this.tmrAnimationOpponent.Interval = 25;
            this.tmrAnimationOpponent.Tick += new System.EventHandler(this.tmrAnimationOpponent_Tick);
            // 
            // pnlLocalDraw
            // 
            this.pnlLocalDraw.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlLocalDraw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLocalDraw.Enabled = false;
            this.pnlLocalDraw.Location = new System.Drawing.Point(12, 148);
            this.pnlLocalDraw.Name = "pnlLocalDraw";
            this.pnlLocalDraw.Size = new System.Drawing.Size(124, 168);
            this.pnlLocalDraw.TabIndex = 4;
            this.pnlLocalDraw.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlLocalDraw_MouseClick);
            this.pnlLocalDraw.MouseLeave += new System.EventHandler(this.pnlLocalDraw_MouseLeave);
            this.pnlLocalDraw.MouseHover += new System.EventHandler(this.pnlLocalDraw_MouseHover);
            // 
            // gbScore
            // 
            this.gbScore.Controls.Add(this.lblHisPoints);
            this.gbScore.Controls.Add(this.lblNone);
            this.gbScore.Controls.Add(this.lblMyPoints);
            this.gbScore.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbScore.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbScore.Location = new System.Drawing.Point(12, 12);
            this.gbScore.Name = "gbScore";
            this.gbScore.Size = new System.Drawing.Size(137, 90);
            this.gbScore.TabIndex = 6;
            this.gbScore.TabStop = false;
            this.gbScore.Text = "Score";
            // 
            // lblHisPoints
            // 
            this.lblHisPoints.AutoSize = true;
            this.lblHisPoints.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHisPoints.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblHisPoints.Location = new System.Drawing.Point(72, 31);
            this.lblHisPoints.Name = "lblHisPoints";
            this.lblHisPoints.Size = new System.Drawing.Size(60, 42);
            this.lblHisPoints.TabIndex = 0;
            this.lblHisPoints.Text = "00";
            this.lblHisPoints.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNone
            // 
            this.lblNone.AutoSize = true;
            this.lblNone.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNone.Location = new System.Drawing.Point(56, 31);
            this.lblNone.Name = "lblNone";
            this.lblNone.Size = new System.Drawing.Size(30, 42);
            this.lblNone.TabIndex = 0;
            this.lblNone.Text = "-";
            // 
            // lblMyPoints
            // 
            this.lblMyPoints.AutoSize = true;
            this.lblMyPoints.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMyPoints.ForeColor = System.Drawing.Color.Green;
            this.lblMyPoints.Location = new System.Drawing.Point(6, 31);
            this.lblMyPoints.Name = "lblMyPoints";
            this.lblMyPoints.Size = new System.Drawing.Size(60, 42);
            this.lblMyPoints.TabIndex = 0;
            this.lblMyPoints.Text = "00";
            // 
            // lblMyPointsPlus
            // 
            this.lblMyPointsPlus.BackColor = System.Drawing.Color.Green;
            this.lblMyPointsPlus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMyPointsPlus.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMyPointsPlus.ForeColor = System.Drawing.Color.White;
            this.lblMyPointsPlus.Location = new System.Drawing.Point(12, 359);
            this.lblMyPointsPlus.Name = "lblMyPointsPlus";
            this.lblMyPointsPlus.Size = new System.Drawing.Size(73, 42);
            this.lblMyPointsPlus.TabIndex = 1;
            this.lblMyPointsPlus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHisPointsPlus
            // 
            this.lblHisPointsPlus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblHisPointsPlus.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblHisPointsPlus.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHisPointsPlus.ForeColor = System.Drawing.Color.White;
            this.lblHisPointsPlus.Location = new System.Drawing.Point(427, 12);
            this.lblHisPointsPlus.Name = "lblHisPointsPlus";
            this.lblHisPointsPlus.Size = new System.Drawing.Size(73, 45);
            this.lblHisPointsPlus.TabIndex = 1;
            this.lblHisPointsPlus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBataille
            // 
            this.lblBataille.AutoSize = true;
            this.lblBataille.BackColor = System.Drawing.Color.Blue;
            this.lblBataille.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBataille.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBataille.ForeColor = System.Drawing.Color.White;
            this.lblBataille.Location = new System.Drawing.Point(78, 168);
            this.lblBataille.Name = "lblBataille";
            this.lblBataille.Size = new System.Drawing.Size(353, 75);
            this.lblBataille.TabIndex = 7;
            this.lblBataille.Text = "BATAILLE!";
            this.lblBataille.Visible = false;
            // 
            // lblAlert
            // 
            this.lblAlert.BackColor = System.Drawing.Color.Red;
            this.lblAlert.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAlert.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlert.ForeColor = System.Drawing.Color.White;
            this.lblAlert.Location = new System.Drawing.Point(12, 168);
            this.lblAlert.Name = "lblAlert";
            this.lblAlert.Size = new System.Drawing.Size(488, 75);
            this.lblAlert.TabIndex = 8;
            this.lblAlert.Text = "ALERT!";
            this.lblAlert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAlert.Visible = false;
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.Blue;
            this.lblMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(12, 168);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(488, 75);
            this.lblMessage.TabIndex = 9;
            this.lblMessage.Text = "CONNECTING";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMyPointsPlusSymbol
            // 
            this.lblMyPointsPlusSymbol.AutoSize = true;
            this.lblMyPointsPlusSymbol.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMyPointsPlusSymbol.ForeColor = System.Drawing.Color.Green;
            this.lblMyPointsPlusSymbol.Location = new System.Drawing.Point(91, 359);
            this.lblMyPointsPlusSymbol.Name = "lblMyPointsPlusSymbol";
            this.lblMyPointsPlusSymbol.Size = new System.Drawing.Size(40, 42);
            this.lblMyPointsPlusSymbol.TabIndex = 0;
            this.lblMyPointsPlusSymbol.Text = "+";
            this.lblMyPointsPlusSymbol.Visible = false;
            // 
            // lblHisPointsPlusSymbol
            // 
            this.lblHisPointsPlusSymbol.AutoSize = true;
            this.lblHisPointsPlusSymbol.Font = new System.Drawing.Font("Arial", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHisPointsPlusSymbol.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblHisPointsPlusSymbol.Location = new System.Drawing.Point(381, 15);
            this.lblHisPointsPlusSymbol.Name = "lblHisPointsPlusSymbol";
            this.lblHisPointsPlusSymbol.Size = new System.Drawing.Size(40, 42);
            this.lblHisPointsPlusSymbol.TabIndex = 0;
            this.lblHisPointsPlusSymbol.Text = "+";
            this.lblHisPointsPlusSymbol.Visible = false;
            // 
            // lblTitleHoldingPoints
            // 
            this.lblTitleHoldingPoints.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(241)))), ((int)(((byte)(240)))));
            this.lblTitleHoldingPoints.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTitleHoldingPoints.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblTitleHoldingPoints.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitleHoldingPoints.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblTitleHoldingPoints.Location = new System.Drawing.Point(308, 371);
            this.lblTitleHoldingPoints.Name = "lblTitleHoldingPoints";
            this.lblTitleHoldingPoints.Size = new System.Drawing.Size(160, 30);
            this.lblTitleHoldingPoints.TabIndex = 10;
            this.lblTitleHoldingPoints.Text = "BATTLE ZONE ";
            this.lblTitleHoldingPoints.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tmrWaiting4Player
            // 
            this.tmrWaiting4Player.Interval = 2000;
            this.tmrWaiting4Player.Tick += new System.EventHandler(this.tmrWaiting4Player_Tick);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // frmGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 401);
            this.Controls.Add(this.lblTitleHoldingPoints);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblAlert);
            this.Controls.Add(this.lblHisPointsPlusSymbol);
            this.Controls.Add(this.lblMyPointsPlusSymbol);
            this.Controls.Add(this.lblBataille);
            this.Controls.Add(this.lblHisPointsPlus);
            this.Controls.Add(this.lblMyPointsPlus);
            this.Controls.Add(this.gbScore);
            this.Controls.Add(this.pnlLocalDraw);
            this.Controls.Add(this.pnlLocalCard);
            this.Controls.Add(this.pnlOpponentDraw);
            this.Controls.Add(this.btnTools);
            this.Controls.Add(this.pnlOpponentCard);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(520, 440);
            this.MinimumSize = new System.Drawing.Size(520, 440);
            this.Name = "frmGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmGame_FormClosing);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmGame_KeyPress);
            this.gbScore.ResumeLayout(false);
            this.gbScore.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTools;
        private System.Windows.Forms.Panel pnlLocalCard;
        private System.Windows.Forms.Panel pnlOpponentCard;
        private System.Windows.Forms.Timer tmrAnimationLocal;
        private System.Windows.Forms.Timer tmrAnimationOpponent;
        private System.Windows.Forms.Panel pnlLocalDraw;
        private System.Windows.Forms.Panel pnlOpponentDraw;
        private System.Windows.Forms.GroupBox gbScore;
        private System.Windows.Forms.Label lblHisPoints;
        private System.Windows.Forms.Label lblNone;
        private System.Windows.Forms.Label lblMyPoints;
        private System.Windows.Forms.Label lblMyPointsPlus;
        private System.Windows.Forms.Label lblHisPointsPlus;
        private System.Windows.Forms.Label lblBataille;
        private System.Windows.Forms.Label lblAlert;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblMyPointsPlusSymbol;
        private System.Windows.Forms.Label lblHisPointsPlusSymbol;
        private System.Windows.Forms.Label lblTitleHoldingPoints;
        private System.Windows.Forms.Timer tmrWaiting4Player;
        private System.IO.Ports.SerialPort serialPort1;

    }
}


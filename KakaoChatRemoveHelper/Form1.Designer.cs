
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WinApi;

namespace KakaoChatRemoveHelper
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SetFormSize = new System.Windows.Forms.ToolStripMenuItem();
            this.FormClose = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_KeyState = new System.Windows.Forms.TextBox();
            this.btn_KeySetting = new System.Windows.Forms.Button();
            this.lbl_KeyStateMessage = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetFormSize,
            this.FormClose});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(99, 48);
            // 
            // SetFormSize
            // 
            this.SetFormSize.Name = "SetFormSize";
            this.SetFormSize.Size = new System.Drawing.Size(98, 22);
            this.SetFormSize.Text = "열기";
            this.SetFormSize.Click += new System.EventHandler(this.SetFormSize_Click);
            // 
            // FormClose
            // 
            this.FormClose.Name = "FormClose";
            this.FormClose.Size = new System.Drawing.Size(98, 22);
            this.FormClose.Text = "종료";
            this.FormClose.Click += new System.EventHandler(this.FormClose_Click);
            // 
            // txt_KeyState
            // 
            this.txt_KeyState.Location = new System.Drawing.Point(12, 12);
            this.txt_KeyState.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_KeyState.Name = "txt_KeyState";
            this.txt_KeyState.ReadOnly = true;
            this.txt_KeyState.Size = new System.Drawing.Size(174, 23);
            this.txt_KeyState.TabIndex = 1;
            // 
            // btn_KeySetting
            // 
            this.btn_KeySetting.Location = new System.Drawing.Point(192, 11);
            this.btn_KeySetting.Name = "btn_KeySetting";
            this.btn_KeySetting.Size = new System.Drawing.Size(75, 23);
            this.btn_KeySetting.TabIndex = 2;
            this.btn_KeySetting.Text = "키 설정";
            this.btn_KeySetting.UseVisualStyleBackColor = true;
            this.btn_KeySetting.Click += new System.EventHandler(this.btn_KeySetting_Click);
            // 
            // lbl_KeyStateMessage
            // 
            this.lbl_KeyStateMessage.AutoSize = true;
            this.lbl_KeyStateMessage.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbl_KeyStateMessage.Location = new System.Drawing.Point(12, 48);
            this.lbl_KeyStateMessage.Name = "lbl_KeyStateMessage";
            this.lbl_KeyStateMessage.Size = new System.Drawing.Size(224, 20);
            this.lbl_KeyStateMessage.TabIndex = 3;
            this.lbl_KeyStateMessage.Text = "카카오톡 채팅 삭제 도우미 v0.4\r\n";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "1. 삭제할 채팅 위에 마우스를 올린다.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "2. 지정한 키를 누른다.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(275, 153);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_KeyStateMessage);
            this.Controls.Add(this.btn_KeySetting);
            this.Controls.Add(this.txt_KeyState);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Ver 0.4";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem FormClose;
        private ToolStripMenuItem SetFormSize;
        private TextBox txt_KeyState;
        private Button btn_KeySetting;
        private Label lbl_KeyStateMessage;
        private Label label2;
        private Label label3;
    }
}


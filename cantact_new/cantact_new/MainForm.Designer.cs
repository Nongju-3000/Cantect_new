
namespace cantact_new
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbox_ip = new System.Windows.Forms.TextBox();
            this.tbox_controller_ip = new System.Windows.Forms.TextBox();
            this.btn_setting = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.timer_send_image = new System.Windows.Forms.Timer(this.components);
            this.noti = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(156, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP주소";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(104, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "컨트롤러 IP주소";
            // 
            // tbox_ip
            // 
            this.tbox_ip.Location = new System.Drawing.Point(200, 46);
            this.tbox_ip.Name = "tbox_ip";
            this.tbox_ip.Size = new System.Drawing.Size(226, 21);
            this.tbox_ip.TabIndex = 2;
            // 
            // tbox_controller_ip
            // 
            this.tbox_controller_ip.Location = new System.Drawing.Point(200, 105);
            this.tbox_controller_ip.Name = "tbox_controller_ip";
            this.tbox_controller_ip.ReadOnly = true;
            this.tbox_controller_ip.Size = new System.Drawing.Size(226, 21);
            this.tbox_controller_ip.TabIndex = 3;
            // 
            // btn_setting
            // 
            this.btn_setting.Location = new System.Drawing.Point(452, 46);
            this.btn_setting.Name = "btn_setting";
            this.btn_setting.Size = new System.Drawing.Size(75, 23);
            this.btn_setting.TabIndex = 4;
            this.btn_setting.Text = "설정";
            this.btn_setting.UseVisualStyleBackColor = true;
            this.btn_setting.Click += new System.EventHandler(this.btn_setting_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Enabled = false;
            this.btn_ok.Location = new System.Drawing.Point(452, 105);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 5;
            this.btn_ok.Text = "허가";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // timer_send_image
            // 
            this.timer_send_image.Tick += new System.EventHandler(this.timer_send_image_Tick);
            // 
            // noti
            // 
            this.noti.Text = "notifyIcon1";
            this.noti.Visible = true;
            this.noti.DoubleClick += new System.EventHandler(this.noti_DoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 222);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_setting);
            this.Controls.Add(this.tbox_controller_ip);
            this.Controls.Add(this.tbox_ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "Cantect";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbox_ip;
        private System.Windows.Forms.TextBox tbox_controller_ip;
        private System.Windows.Forms.Button btn_setting;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Timer timer_send_image;
        private System.Windows.Forms.NotifyIcon noti;
    }
}


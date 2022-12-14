
namespace PRO2_PolygonFiller
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.canvas = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxDiffuse = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sliderDiffuse = new System.Windows.Forms.TrackBar();
            this.groupBoxSpecular = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.sliderSpecular = new System.Windows.Forms.TrackBar();
            this.groupBoxShininess = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.sliderShininess = new System.Windows.Forms.TrackBar();
            this.groupBoxLightHeight = new System.Windows.Forms.GroupBox();
            this.labelLightHeight = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.sliderLightHeight = new System.Windows.Forms.TrackBar();
            this.groupBoxLightRotation = new System.Windows.Forms.GroupBox();
            this.checkBoxLightMovement = new System.Windows.Forms.CheckBox();
            this.groupBoxTexture = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonTexture = new System.Windows.Forms.Button();
            this.buttonSetColor = new System.Windows.Forms.Button();
            this.buttonShowColor = new System.Windows.Forms.Button();
            this.groupBoxInterpolationType = new System.Windows.Forms.GroupBox();
            this.radioButtonInterpolationPoint = new System.Windows.Forms.RadioButton();
            this.radioButtonInterpolationColor = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxNormalMap = new System.Windows.Forms.CheckBox();
            this.buttonNormalMap = new System.Windows.Forms.Button();
            this.buttonNewModel = new System.Windows.Forms.Button();
            this.checkBoxPaintEdges = new System.Windows.Forms.CheckBox();
            this.timerAnimation = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBoxDiffuse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderDiffuse)).BeginInit();
            this.groupBoxSpecular.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderSpecular)).BeginInit();
            this.groupBoxShininess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderShininess)).BeginInit();
            this.groupBoxLightHeight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderLightHeight)).BeginInit();
            this.groupBoxLightRotation.SuspendLayout();
            this.groupBoxTexture.SuspendLayout();
            this.groupBoxInterpolationType.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.Controls.Add(this.canvas, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(677, 661);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // canvas
            // 
            this.canvas.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.canvas.Location = new System.Drawing.Point(3, 3);
            this.canvas.Name = "canvas";
            this.canvas.Size = new System.Drawing.Size(501, 655);
            this.canvas.TabIndex = 0;
            this.canvas.TabStop = false;
            this.canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.canvas_Paint);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.groupBoxDiffuse, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBoxSpecular, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.groupBoxShininess, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.groupBoxLightHeight, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.groupBoxLightRotation, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.groupBoxTexture, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.groupBoxInterpolationType, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.buttonNewModel, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.checkBoxPaintEdges, 0, 8);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(510, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 10;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(164, 655);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // groupBoxDiffuse
            // 
            this.groupBoxDiffuse.Controls.Add(this.label2);
            this.groupBoxDiffuse.Controls.Add(this.label1);
            this.groupBoxDiffuse.Controls.Add(this.sliderDiffuse);
            this.groupBoxDiffuse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDiffuse.Location = new System.Drawing.Point(3, 3);
            this.groupBoxDiffuse.Name = "groupBoxDiffuse";
            this.groupBoxDiffuse.Size = new System.Drawing.Size(158, 63);
            this.groupBoxDiffuse.TabIndex = 0;
            this.groupBoxDiffuse.TabStop = false;
            this.groupBoxDiffuse.Text = "Diffuse Coefficient (kd)";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "1";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "0";
            // 
            // sliderDiffuse
            // 
            this.sliderDiffuse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sliderDiffuse.Location = new System.Drawing.Point(3, 19);
            this.sliderDiffuse.Maximum = 100;
            this.sliderDiffuse.Name = "sliderDiffuse";
            this.sliderDiffuse.Size = new System.Drawing.Size(152, 41);
            this.sliderDiffuse.TabIndex = 0;
            this.sliderDiffuse.Value = 75;
            this.sliderDiffuse.ValueChanged += new System.EventHandler(this.sliderDiffuse_ValueChanged);
            // 
            // groupBoxSpecular
            // 
            this.groupBoxSpecular.Controls.Add(this.label4);
            this.groupBoxSpecular.Controls.Add(this.label3);
            this.groupBoxSpecular.Controls.Add(this.sliderSpecular);
            this.groupBoxSpecular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSpecular.Location = new System.Drawing.Point(3, 72);
            this.groupBoxSpecular.Name = "groupBoxSpecular";
            this.groupBoxSpecular.Size = new System.Drawing.Size(158, 63);
            this.groupBoxSpecular.TabIndex = 1;
            this.groupBoxSpecular.TabStop = false;
            this.groupBoxSpecular.Text = "Specular Coefficient (ks)";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(139, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "1";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "0";
            // 
            // sliderSpecular
            // 
            this.sliderSpecular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sliderSpecular.Location = new System.Drawing.Point(3, 19);
            this.sliderSpecular.Maximum = 100;
            this.sliderSpecular.Name = "sliderSpecular";
            this.sliderSpecular.Size = new System.Drawing.Size(152, 41);
            this.sliderSpecular.TabIndex = 0;
            this.sliderSpecular.Value = 75;
            this.sliderSpecular.ValueChanged += new System.EventHandler(this.sliderSpecular_ValueChanged);
            // 
            // groupBoxShininess
            // 
            this.groupBoxShininess.Controls.Add(this.label6);
            this.groupBoxShininess.Controls.Add(this.label5);
            this.groupBoxShininess.Controls.Add(this.sliderShininess);
            this.groupBoxShininess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxShininess.Location = new System.Drawing.Point(3, 141);
            this.groupBoxShininess.Name = "groupBoxShininess";
            this.groupBoxShininess.Size = new System.Drawing.Size(158, 63);
            this.groupBoxShininess.TabIndex = 2;
            this.groupBoxShininess.TabStop = false;
            this.groupBoxShininess.Text = "Surface Shininess (m)";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(91, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Less (128)";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "More (1)";
            // 
            // sliderShininess
            // 
            this.sliderShininess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sliderShininess.Location = new System.Drawing.Point(3, 19);
            this.sliderShininess.Maximum = 128;
            this.sliderShininess.Minimum = 1;
            this.sliderShininess.Name = "sliderShininess";
            this.sliderShininess.Size = new System.Drawing.Size(152, 41);
            this.sliderShininess.TabIndex = 1;
            this.sliderShininess.Value = 64;
            this.sliderShininess.ValueChanged += new System.EventHandler(this.sliderShininess_ValueChanged);
            // 
            // groupBoxLightHeight
            // 
            this.groupBoxLightHeight.Controls.Add(this.labelLightHeight);
            this.groupBoxLightHeight.Controls.Add(this.label8);
            this.groupBoxLightHeight.Controls.Add(this.label7);
            this.groupBoxLightHeight.Controls.Add(this.sliderLightHeight);
            this.groupBoxLightHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxLightHeight.Location = new System.Drawing.Point(3, 210);
            this.groupBoxLightHeight.Name = "groupBoxLightHeight";
            this.groupBoxLightHeight.Size = new System.Drawing.Size(158, 63);
            this.groupBoxLightHeight.TabIndex = 3;
            this.groupBoxLightHeight.TabStop = false;
            this.groupBoxLightHeight.Text = "Light Height";
            // 
            // labelLightHeight
            // 
            this.labelLightHeight.AutoSize = true;
            this.labelLightHeight.Location = new System.Drawing.Point(91, 1);
            this.labelLightHeight.Name = "labelLightHeight";
            this.labelLightHeight.Size = new System.Drawing.Size(28, 15);
            this.labelLightHeight.TabIndex = 7;
            this.labelLightHeight.Text = "2.00";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(28, 15);
            this.label8.TabIndex = 6;
            this.label8.Text = "0.01";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(127, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 15);
            this.label7.TabIndex = 5;
            this.label7.Text = "4.0";
            // 
            // sliderLightHeight
            // 
            this.sliderLightHeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sliderLightHeight.Location = new System.Drawing.Point(3, 19);
            this.sliderLightHeight.Maximum = 400;
            this.sliderLightHeight.Minimum = 1;
            this.sliderLightHeight.Name = "sliderLightHeight";
            this.sliderLightHeight.Size = new System.Drawing.Size(152, 41);
            this.sliderLightHeight.TabIndex = 2;
            this.sliderLightHeight.Value = 200;
            this.sliderLightHeight.ValueChanged += new System.EventHandler(this.sliderLightHeight_ValueChanged);
            // 
            // groupBoxLightRotation
            // 
            this.groupBoxLightRotation.Controls.Add(this.checkBoxLightMovement);
            this.groupBoxLightRotation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxLightRotation.Location = new System.Drawing.Point(3, 279);
            this.groupBoxLightRotation.Name = "groupBoxLightRotation";
            this.groupBoxLightRotation.Size = new System.Drawing.Size(158, 42);
            this.groupBoxLightRotation.TabIndex = 4;
            this.groupBoxLightRotation.TabStop = false;
            this.groupBoxLightRotation.Text = "Light Movement";
            // 
            // checkBoxLightMovement
            // 
            this.checkBoxLightMovement.AutoSize = true;
            this.checkBoxLightMovement.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxLightMovement.Location = new System.Drawing.Point(3, 19);
            this.checkBoxLightMovement.Name = "checkBoxLightMovement";
            this.checkBoxLightMovement.Size = new System.Drawing.Size(152, 20);
            this.checkBoxLightMovement.TabIndex = 0;
            this.checkBoxLightMovement.Text = "Stop Light Movement";
            this.checkBoxLightMovement.UseVisualStyleBackColor = true;
            this.checkBoxLightMovement.CheckedChanged += new System.EventHandler(this.checkBoxLightMovement_CheckedChanged);
            // 
            // groupBoxTexture
            // 
            this.groupBoxTexture.Controls.Add(this.label9);
            this.groupBoxTexture.Controls.Add(this.buttonTexture);
            this.groupBoxTexture.Controls.Add(this.buttonSetColor);
            this.groupBoxTexture.Controls.Add(this.buttonShowColor);
            this.groupBoxTexture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTexture.Location = new System.Drawing.Point(3, 404);
            this.groupBoxTexture.Name = "groupBoxTexture";
            this.groupBoxTexture.Size = new System.Drawing.Size(158, 97);
            this.groupBoxTexture.TabIndex = 5;
            this.groupBoxTexture.TabStop = false;
            this.groupBoxTexture.Text = "Choose Color or Texture";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(65, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 15);
            this.label9.TabIndex = 3;
            this.label9.Text = "OR";
            // 
            // buttonTexture
            // 
            this.buttonTexture.Location = new System.Drawing.Point(7, 66);
            this.buttonTexture.Name = "buttonTexture";
            this.buttonTexture.Size = new System.Drawing.Size(145, 23);
            this.buttonTexture.TabIndex = 2;
            this.buttonTexture.Text = "Load Image";
            this.buttonTexture.UseVisualStyleBackColor = true;
            this.buttonTexture.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonTexture_MouseClick);
            // 
            // buttonSetColor
            // 
            this.buttonSetColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetColor.Location = new System.Drawing.Point(74, 22);
            this.buttonSetColor.Name = "buttonSetColor";
            this.buttonSetColor.Size = new System.Drawing.Size(78, 23);
            this.buttonSetColor.TabIndex = 1;
            this.buttonSetColor.Text = "Set Color";
            this.buttonSetColor.UseVisualStyleBackColor = true;
            this.buttonSetColor.Click += new System.EventHandler(this.buttonSetColor_Click);
            // 
            // buttonShowColor
            // 
            this.buttonShowColor.Enabled = false;
            this.buttonShowColor.Location = new System.Drawing.Point(7, 22);
            this.buttonShowColor.Name = "buttonShowColor";
            this.buttonShowColor.Size = new System.Drawing.Size(52, 23);
            this.buttonShowColor.TabIndex = 0;
            this.buttonShowColor.UseVisualStyleBackColor = true;
            // 
            // groupBoxInterpolationType
            // 
            this.groupBoxInterpolationType.Controls.Add(this.radioButtonInterpolationPoint);
            this.groupBoxInterpolationType.Controls.Add(this.radioButtonInterpolationColor);
            this.groupBoxInterpolationType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxInterpolationType.Location = new System.Drawing.Point(3, 327);
            this.groupBoxInterpolationType.Name = "groupBoxInterpolationType";
            this.groupBoxInterpolationType.Size = new System.Drawing.Size(158, 71);
            this.groupBoxInterpolationType.TabIndex = 6;
            this.groupBoxInterpolationType.TabStop = false;
            this.groupBoxInterpolationType.Text = "Interpolation Method";
            // 
            // radioButtonInterpolationPoint
            // 
            this.radioButtonInterpolationPoint.AutoSize = true;
            this.radioButtonInterpolationPoint.Checked = true;
            this.radioButtonInterpolationPoint.Location = new System.Drawing.Point(6, 47);
            this.radioButtonInterpolationPoint.Name = "radioButtonInterpolationPoint";
            this.radioButtonInterpolationPoint.Size = new System.Drawing.Size(124, 19);
            this.radioButtonInterpolationPoint.TabIndex = 1;
            this.radioButtonInterpolationPoint.TabStop = true;
            this.radioButtonInterpolationPoint.Text = "Point Interpolation";
            this.radioButtonInterpolationPoint.UseVisualStyleBackColor = true;
            // 
            // radioButtonInterpolationColor
            // 
            this.radioButtonInterpolationColor.AutoSize = true;
            this.radioButtonInterpolationColor.Location = new System.Drawing.Point(6, 22);
            this.radioButtonInterpolationColor.Name = "radioButtonInterpolationColor";
            this.radioButtonInterpolationColor.Size = new System.Drawing.Size(125, 19);
            this.radioButtonInterpolationColor.TabIndex = 0;
            this.radioButtonInterpolationColor.Text = "Color Interpolation";
            this.radioButtonInterpolationColor.UseVisualStyleBackColor = true;
            this.radioButtonInterpolationColor.CheckedChanged += new System.EventHandler(this.radioButtonInterpolationColor_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxNormalMap);
            this.groupBox1.Controls.Add(this.buttonNormalMap);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 507);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 72);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Normal Maps";
            // 
            // checkBoxNormalMap
            // 
            this.checkBoxNormalMap.AutoSize = true;
            this.checkBoxNormalMap.Checked = true;
            this.checkBoxNormalMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxNormalMap.Location = new System.Drawing.Point(7, 22);
            this.checkBoxNormalMap.Name = "checkBoxNormalMap";
            this.checkBoxNormalMap.Size = new System.Drawing.Size(115, 19);
            this.checkBoxNormalMap.TabIndex = 1;
            this.checkBoxNormalMap.Text = "Use Normal Map";
            this.checkBoxNormalMap.UseVisualStyleBackColor = true;
            this.checkBoxNormalMap.CheckedChanged += new System.EventHandler(this.checkBoxNormalMap_CheckedChanged);
            // 
            // buttonNormalMap
            // 
            this.buttonNormalMap.Location = new System.Drawing.Point(6, 44);
            this.buttonNormalMap.Name = "buttonNormalMap";
            this.buttonNormalMap.Size = new System.Drawing.Size(143, 23);
            this.buttonNormalMap.TabIndex = 0;
            this.buttonNormalMap.Text = "Load Normal Map";
            this.buttonNormalMap.UseVisualStyleBackColor = true;
            this.buttonNormalMap.Click += new System.EventHandler(this.buttonNormalMap_Click);
            // 
            // buttonNewModel
            // 
            this.buttonNewModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonNewModel.Location = new System.Drawing.Point(3, 611);
            this.buttonNewModel.Name = "buttonNewModel";
            this.buttonNewModel.Size = new System.Drawing.Size(158, 41);
            this.buttonNewModel.TabIndex = 7;
            this.buttonNewModel.Text = "Load New Model";
            this.buttonNewModel.UseVisualStyleBackColor = true;
            this.buttonNewModel.Click += new System.EventHandler(this.buttonNewModel_Click);
            // 
            // checkBoxPaintEdges
            // 
            this.checkBoxPaintEdges.AutoSize = true;
            this.checkBoxPaintEdges.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.checkBoxPaintEdges.Location = new System.Drawing.Point(3, 586);
            this.checkBoxPaintEdges.Name = "checkBoxPaintEdges";
            this.checkBoxPaintEdges.Size = new System.Drawing.Size(158, 19);
            this.checkBoxPaintEdges.TabIndex = 9;
            this.checkBoxPaintEdges.Text = "Show Edges and Vertices";
            this.checkBoxPaintEdges.UseVisualStyleBackColor = true;
            this.checkBoxPaintEdges.CheckedChanged += new System.EventHandler(this.checkBoxPaintEdges_CheckedChanged);
            // 
            // timerAnimation
            // 
            this.timerAnimation.Enabled = true;
            this.timerAnimation.Interval = 33;
            this.timerAnimation.Tick += new System.EventHandler(this.timerAnimation_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(677, 661);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Illumination Simulator";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.canvas)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.groupBoxDiffuse.ResumeLayout(false);
            this.groupBoxDiffuse.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderDiffuse)).EndInit();
            this.groupBoxSpecular.ResumeLayout(false);
            this.groupBoxSpecular.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderSpecular)).EndInit();
            this.groupBoxShininess.ResumeLayout(false);
            this.groupBoxShininess.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderShininess)).EndInit();
            this.groupBoxLightHeight.ResumeLayout(false);
            this.groupBoxLightHeight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderLightHeight)).EndInit();
            this.groupBoxLightRotation.ResumeLayout(false);
            this.groupBoxLightRotation.PerformLayout();
            this.groupBoxTexture.ResumeLayout(false);
            this.groupBoxTexture.PerformLayout();
            this.groupBoxInterpolationType.ResumeLayout(false);
            this.groupBoxInterpolationType.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Timer timerAnimation;
        private System.Windows.Forms.GroupBox groupBoxDiffuse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar sliderDiffuse;
        private System.Windows.Forms.GroupBox groupBoxSpecular;
        private System.Windows.Forms.TrackBar sliderSpecular;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBoxShininess;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar sliderShininess;
        private System.Windows.Forms.GroupBox groupBoxLightHeight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TrackBar sliderLightHeight;
        private System.Windows.Forms.GroupBox groupBoxLightRotation;
        private System.Windows.Forms.GroupBox groupBoxTexture;
        private System.Windows.Forms.Button buttonSetColor;
        private System.Windows.Forms.Button buttonShowColor;
        private System.Windows.Forms.GroupBox groupBoxInterpolationType;
        private System.Windows.Forms.RadioButton radioButtonInterpolationPoint;
        private System.Windows.Forms.RadioButton radioButtonInterpolationColor;
        private System.Windows.Forms.PictureBox canvas;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonTexture;
        private System.Windows.Forms.Button buttonNewModel;
        private System.Windows.Forms.Label labelLightHeight;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxNormalMap;
        private System.Windows.Forms.Button buttonNormalMap;
        private System.Windows.Forms.CheckBox checkBoxPaintEdges;
        private System.Windows.Forms.CheckBox checkBoxLightMovement;
    }
}


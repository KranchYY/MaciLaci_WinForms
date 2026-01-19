using MaciLaci_WinForms.Model;
using MaciLaci_WinForms.Persistance;

namespace MaciLaci_WinForms
{
    public partial class GameForm : Form
    {
        private GameModel model = null!;
        private System.Windows.Forms.Timer timer = null!;
        private int size;
        private Button[,] buttonGrid = null!;
        private bool isPaused;

        public GameForm()
        {
            InitializeComponent();

            model = new GameModel(15);
            model.FieldChanged += new EventHandler<MaciLaciFieldEventArgs>(Game_FieldChanged);
            model.GameOver += new EventHandler<MaciLaciEventArgs>(Game_GameOver);
            isPaused = true;
            pause.Visible = false;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(Timer_Tick);
            label1.Visible = false;
            label2.Visible = false;
            KeyPreview = true;
            KeyDown += new KeyEventHandler(MaciMozgo);
        }

        private void MaciMozgo(object? sender, KeyEventArgs e)
        {
            if (isPaused) return;
            e.SuppressKeyPress = true;
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
            {
                model.MoveMaci(Dir.UP); return;
            }
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                model.MoveMaci(Dir.LEFT); return;
            }
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
            {
                model.MoveMaci(Dir.DOWN); return;
            }
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                model.MoveMaci(Dir.RIGHT); return;
            }
        }

        private void Game_GameOver(object? sender, MaciLaciEventArgs e)
        {
            timer.Stop();
            pause.Enabled = false;
            isPaused = true;
            if (e.IsWon) // győzelemtől függő üzenet megjelenítése
            {
                MessageBox.Show("Gratulálok, győztél!" + Environment.NewLine +
                                TimeSpan.FromSeconds(e.GameTime).ToString("g") + " ideig játszottál.",
                                "MaciLaci játék",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Sajnálom, vesztettél!",
                                "MaciLaci játék",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Asterisk);
            }
        }


        private void Timer_Tick(Object? sender, EventArgs e)
        {
            model.AdvanceTime(); // játék léptetése
            label1.Text = "Eltelt Idő: " + model.GameTime;
            label2.Text = "Megszerzett kosarak: " + model.Macilaci.Kosarak.ToString();
        }
        private void Game_FieldChanged(object? sender, MaciLaciFieldEventArgs e)
        {
            SetUpTable();
        }
        private void GenerateTable2()
        {
            // előző gombok törlése
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();

            tableLayoutPanel1.RowCount = size;
            tableLayoutPanel1.ColumnCount = size;

            for (int i = 0; i < size; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / size));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / size));
            }

            buttonGrid = new Button[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    buttonGrid[i, j] = new Button();
                    buttonGrid[i, j].Dock = DockStyle.Fill; // kitölti a cellát
                    buttonGrid[i, j].Size = new Size(40, 40); // méret
                    buttonGrid[i, j].Margin = new Padding(0);
                    buttonGrid[i, j].Font = new Font(FontFamily.GenericSansSerif, 17, FontStyle.Bold);
                    buttonGrid[i, j].Enabled = false;
                    buttonGrid[i, j].TabIndex = 100 + i * size + j;
                    buttonGrid[i, j].FlatStyle = FlatStyle.Flat;

                    // a gombot a (j,i) cellába rakjuk
                    tableLayoutPanel1.Controls.Add(buttonGrid[i, j], j, i);
                }
            }
        }

        public void SetUpTable()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    string c = " ";
                    switch (model.Forest.Table[j, i])
                    {
                        case ForestField.Empty:
                            c = " ";
                            break;
                        case ForestField.MaciLaci:
                            c = "m";
                            break;
                        case ForestField.Hunter:
                            c = "h";
                            break;
                        case ForestField.Tree:
                            c = "t";
                            break;
                        case ForestField.Border:
                            c = "b";
                            break;
                        case ForestField.Kosar:
                            c = "k";
                            break;
                    }
                    buttonGrid[j, i].Text = c;
                }
            }
        }
        private void newgame_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an item from the list!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            size = Convert.ToInt32(comboBox1.Text.Split('x')[0]);
            if (checkBox1.Checked)
            { //tested maps only
                model = new GameModel(comboBox1.Text + ".txt");
            }
            else
            {
                model = new GameModel(size);
            }
            model.FieldChanged += new EventHandler<MaciLaciFieldEventArgs>(Game_FieldChanged);
            model.GameOver += new EventHandler<MaciLaciEventArgs>(Game_GameOver);
            GenerateTable2();
            SetUpTable();
            timer.Start();
            isPaused = false;
            pause.Visible = true;
            pause.Enabled = true;
            label1.Visible = true;
            label2.Visible = true;
        }
        private void pause_Click(object sender, EventArgs e)
        {
            if (isPaused)
            {
                isPaused = false;
                pause.Text = "Pause";

                timer.Start();

            }
            else
            {
                timer.Stop();
                isPaused = true;
                pause.Text = "Resume";
            }
        }

    }
}

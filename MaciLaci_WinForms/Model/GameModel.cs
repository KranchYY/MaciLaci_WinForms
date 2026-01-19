using MaciLaci_WinForms.Persistance;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaciLaci_WinForms.Model
{
    public class GameModel
    {
        private int size;
        private int hunter_count;
        private int kosar_count;
        private Persistance.Forest forest = null!;
        private Maci macilaci = new Maci();
        private List<Hunter> hunters = new List<Hunter>();
        private static Random rnd = new Random();
        private int gameTime;

        public Persistance.Forest Forest { get => forest; set => forest = value; }
        public Maci Macilaci { get => macilaci; set => macilaci = value; }
        public List<Hunter> Hunters { get => hunters; set => hunters = value; }
        public int Kosar_count { get => kosar_count; set => kosar_count = value; }
        public int GameTime { get => gameTime; set => gameTime = value; }

        public GameModel(int size, int hunter_count, int kosar_count)
        {
            GameTime = 0;
            this.size = size;
            this.hunter_count = hunter_count;
            this.kosar_count = kosar_count;
            forest = new Persistance.Forest(size);
            forest.Table[1, 1] = ForestField.MaciLaci;
            int tree_count = rnd.Next(1, size * 2);
            for (int i = 0; i < tree_count; i++)
            {
                int x, y;
                do
                {
                    x = rnd.Next(1, size);
                    y = rnd.Next(1, size);
                }
                while (forest.Table[y, x] != ForestField.Empty);
                forest.Table[y, x] = ForestField.Tree;
            }
            for (int i = 0; i < hunter_count; i++)
            {
                int x, y;
                do
                {
                    x = rnd.Next(1, size);
                    y = rnd.Next(1, size);
                }
                while (forest.Table[y, x] != ForestField.Empty || (x == 2 && y == 2 || x == 1 && y == 2 || x == 2 && y == 1));
                hunters.Add(new Hunter(x, y));
                forest.Table[y, x] = ForestField.Hunter;
            }
            for (int i = 0; i < kosar_count; i++)
            {
                int x, y;
                do
                {
                    x = rnd.Next(1, size);
                    y = rnd.Next(1, size);
                }
                while (forest.Table[y, x] != ForestField.Empty);
                forest.Table[y, x] = ForestField.Kosar;
            }
        }
        public GameModel(string filePath)
        {
            forest = new Persistance.Forest(filePath);
            size = forest.Size;
            GameTime = 0;
            kosar_count = 0;
            hunter_count = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (forest.Table[j, i] == ForestField.Kosar) kosar_count++;
                    else if (forest.Table[j, i] == ForestField.Hunter)
                    {
                        hunter_count++;
                        hunters.Add(new Hunter(i, j));
                    }
                }
            }

        }
        public GameModel(int size) : this(size, rnd.Next(5, size), rnd.Next(3, size * 2)) { }
        public void MoveHunters()
        {
            for (int i = 0; i < hunter_count; i++)
            {
                int x = hunters[i].Position_X;
                int y = hunters[i].Position_Y;
                if (hunters[i].Direction)
                {
                    if (forest.Table[y, x + hunters[i].Way] != ForestField.MaciLaci && forest.Table[y, x + hunters[i].Way] != ForestField.Empty)
                    {
                        hunters[i].Way *= -1;
                    }
                    else
                    {
                        hunters[i].Move();
                        forest.Table[y, x] = ForestField.Empty;
                        OnFieldChanged(x, y, ForestField.Empty);
                        forest.Table[y, hunters[i].Position_X] = ForestField.Hunter;
                        OnFieldChanged(x, y, ForestField.Hunter);
                    }
                }
                else
                {
                    if (forest.Table[y + hunters[i].Way, x] != ForestField.MaciLaci && forest.Table[y + hunters[i].Way, x] != ForestField.Empty)
                    {
                        hunters[i].Way *= -1;
                    }
                    else
                    {
                        hunters[i].Move();
                        forest.Table[y, x] = ForestField.Empty;
                        OnFieldChanged(x, y, ForestField.Empty);
                        forest.Table[hunters[i].Position_Y, x] = ForestField.Hunter;
                        OnFieldChanged(x, y, ForestField.Hunter);
                    }
                }
            }
            for (int x = macilaci.Position_X - 1; x <= macilaci.Position_X + 1; x++)
            {
                for (int y = macilaci.Position_Y - 1; y <= macilaci.Position_Y + 1; y++)
                {
                    if (Forest.Table[y, x] == ForestField.Hunter)
                    {
                        //GAME OVER (LOSE)
                        OnGameOver(false);
                        return;
                    }
                }
            }
        }
        public event EventHandler<MaciLaciFieldEventArgs>? FieldChanged;
        public event EventHandler<MaciLaciEventArgs>? GameOver;

        private void OnFieldChanged(int x, int y, ForestField field)
        {
            FieldChanged?.Invoke(this, new MaciLaciFieldEventArgs(x, y, field));
        }
        private void OnGameOver(bool isWon)
        {
            GameOver?.Invoke(this, new MaciLaciEventArgs(gameTime, isWon));
        }
        public void AdvanceTime()
        {

            GameTime++;
            MoveHunters();


        }
        public void MoveMaci(Dir dir)
        {
            int x = macilaci.Position_X;
            int y = macilaci.Position_Y;
            switch (dir)
            {
                case Dir.UP:
                    y -= 1;
                    break;
                case Dir.DOWN:
                    y += 1;
                    break;
                case Dir.LEFT:
                    x -= 1;
                    break;
                case Dir.RIGHT:
                    x += 1;
                    break;
            }
            ForestField hely = forest.Table[y, x];
            if (hely == ForestField.Empty)
            {
                forest.Table[macilaci.Position_Y, macilaci.Position_X] = ForestField.Empty;
                OnFieldChanged(macilaci.Position_X, macilaci.Position_Y, ForestField.Empty);
                forest.Table[y, x] = ForestField.MaciLaci;
                OnFieldChanged(x, y, ForestField.MaciLaci);
                macilaci.Position_X = x;
                macilaci.Position_Y = y;
            }
            else if (hely == ForestField.Kosar)
            {
                forest.Table[macilaci.Position_Y, macilaci.Position_X] = ForestField.Empty;
                OnFieldChanged(macilaci.Position_X, macilaci.Position_Y, ForestField.Empty);
                macilaci.Position_X = x;
                macilaci.Position_Y = y;
                macilaci.Kosar_begyujt();
                kosar_count--;
                forest.Table[y, x] = ForestField.MaciLaci;
                OnFieldChanged(x, y, ForestField.MaciLaci);
                if (kosar_count == 0)
                {
                    //GAME OVER (WIN)
                    OnGameOver(true);
                }
            }
            for (int i = macilaci.Position_X - 1; i <= macilaci.Position_X + 1; i++)
            {
                for (int j = macilaci.Position_Y - 1; j <= macilaci.Position_Y + 1; j++)
                {
                    if (Forest.Table[j, i] == ForestField.Hunter)
                    {
                        //GAME OVER (LOSE)
                        OnGameOver(false);
                        return;
                    }
                }
            }
        }


    }
    public enum Dir { UP, LEFT, RIGHT, DOWN }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzobotiClanek
{
    class Program
    {
        static void Main(string[] args)
        {
            parseFile(@"C:\Users\VSk\OneDrive\MFF UK\MGR\OzobotiClanek\OzobotiClanek\bin\Debug\clanek\1-robust\1.txt");
        }

        static void  parseFile(string fileName)
        {
            StreamReader sr = new StreamReader(fileName);          

            string line;
            int counter = 0;

            while ((line = sr.ReadLine()) != null)
            {
                string[] parts = line.Split(' ');
                cord[] nodes = new cord[parts.Length];

                for (int i = 0; i < parts.Length; i++)
                {
                    int nodeV = int.Parse(parts[i]) - 1;
                    nodes[i] = new cord(nodeV % 8, nodeV / 8);
                }

                string outFileName = fileName + counter+".txt";
                WriteOutput(outFileName, nodes);

                counter++;
            }
        }

        public static void WriteOutput(string fileName, cord[] path)
        {
            StreamWriter sw = new StreamWriter(fileName);
            View v = new RightView();

            for (int i = 0; i < path.Length - 1; i++)
            {
                sw.WriteLine((int)v.translateMove(path[i], path[i + 1], ref v));
            }

            sw.Close();     
        }
    }

    class cord
    {
        public cord(int y, int x)
        {
            this.y = y;
            this.x = x;
        }
        public int x;
        public int y;
    }

    /// <summary>
    /// Child classes represent possible view on the grid (like ozobot). The name of the child specifies the direction ozobot is looking from the beginning.
    /// </summary>
    class View
    {
        public static View detectView(cord from, cord to)
        {
            int first = to.x - from.x;
            int second = to.y - from.y;

            if (first > 0)
            {
                return new DownView();
            }
            else if (first < 0)
            {
                return new TopView();
            }
            else if (second > 0)
            {
                return new RightView();
            }
            else
            {
                return new LeftView();
            }
        }

        public virtual DIRECTION translateMove(cord from, cord to, ref View v)
        {
            return 0;
        }

    }

    class LeftView : View
    {
        public override DIRECTION translateMove(cord from, cord to, ref View v)
        {
            int second = to.x - from.x;
            int first = to.y - from.y;

            if (first > 0) // dolu
            {
                v = new DownView();
                return DIRECTION.DIRECTION_LEFT;
            }
            else if (first < 0) // nahoru
            {
                v = new TopView();
                return DIRECTION.DIRECTION_RIGHT;
            }
            else if (second > 0) // doprava
            {
                v = new RightView();
                return DIRECTION.DIRECTION_BACKWARD;
            }
            else if (second < 0) // doleva
            {
                return DIRECTION.DIRECTION_FORWARD;
            }
            else
            {
                return DIRECTION.WAIT;
            }
        }
    }

    class RightView : View
    {
        public override DIRECTION translateMove(cord from, cord to, ref View v)
        {
            int second = to.x - from.x;
            int first = to.y - from.y;

            if (first > 0) // dolu
            {
                v = new DownView();
                return DIRECTION.DIRECTION_RIGHT;
            }
            else if (first < 0) // nahoru
            {
                v = new TopView();
                return DIRECTION.DIRECTION_LEFT;
            }
            else if (second > 0) // doprava
            {
                return DIRECTION.DIRECTION_FORWARD;
            }
            else if (second < 0) // doleva
            {
                v = new LeftView();
                return DIRECTION.DIRECTION_BACKWARD;
            }
            else
            {
                return DIRECTION.WAIT;
            }
        }
    }

    class TopView : View
    {
        public override DIRECTION translateMove(cord from, cord to, ref View v)
        {
            int second = to.x - from.x;
            int first = to.y - from.y;

            if (first > 0) // dolu
            {
                v = new DownView();
                return DIRECTION.DIRECTION_BACKWARD;
            }
            else if (first < 0) // nahoru
            {
                return DIRECTION.DIRECTION_FORWARD;
            }
            else if (second > 0) // doprava
            {
                v = new RightView();
                return DIRECTION.DIRECTION_RIGHT;
            }
            else if (second < 0)// doleva
            {
                v = new LeftView();
                return DIRECTION.DIRECTION_LEFT;
            }
            else
            {
                return DIRECTION.WAIT;
            }
        }
    }

    class DownView : View
    {
        public override DIRECTION translateMove(cord from, cord to, ref View v)
        {
            int second = to.x - from.x;
            int first = to.y - from.y;

            if (first > 0) // dolu
            {
                return DIRECTION.DIRECTION_FORWARD;
            }
            else if (first < 0) // nahoru
            {
                v = new TopView();
                return DIRECTION.DIRECTION_BACKWARD;
            }
            else if (second > 0) // doprava
            {
                v = new RightView();
                return DIRECTION.DIRECTION_LEFT;
            }
            else if (second < 0) // doleva
            {
                v = new LeftView();
                return DIRECTION.DIRECTION_RIGHT;
            }
            else
            {
                return DIRECTION.WAIT;
            }
        }

    }

    enum DIRECTION
    {
        DIRECTION_LEFT = 2, DIRECTION_RIGHT = 4, DIRECTION_FORWARD = 1, DIRECTION_BACKWARD = 8, WAIT = 0
    }
}

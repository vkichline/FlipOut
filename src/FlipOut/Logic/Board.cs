using System;
using System.Collections.Generic;

namespace FlipOut.Logic {
    public class Board {
        private bool[] _board = new bool[64];
        private Random _rand = new Random(System.DateTime.Now.Millisecond);

        public Board() {
            for (int i = 0; i < 64; i++) {
                _board[i] = (_rand.Next() & 1) == 1;
            }
        }

        // Copy Constructor
        public Board(Board orig) {
            for (int i = 0; i < 64; i++) {
                this._board[i] = orig._board[i];
            }
        }

        // Indexer
        public bool this[int row, int col] {
            get {
                return _board[row * 8 + col];
            }
        }

        public override string ToString() {
            string s = "";
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    int x = i * 8 + j;
                    s += (_board[x] ? "1 " : "0 ");
                }
                s += "\n";
            }
            return s;
        }

        // The board has six zones, each with even or odd parity,
        // yeilding a 6 bit binary number (0-63)
        // Zone 1 is rows 0-3
        // Zone 2 is row 0-1 and 4-5
        // Zone 3 is rows 0, 2, 4, and 6
        // Zone 4 is columns 0-3
        // Zone 5 is columns 0-1 and 4-5
        // Zone 6 is columns 0, 2, 4, and 6
        public int Value {
            get {
                int row = 0, col = 0, zone1 = 0, zone2 = 0, zone3 = 0, zone4 = 0, zone5 = 0, zone6 = 0;
                for (row = 0; row < 8; row++) {
                    for (col = 0; col < 8; col++) {
                        bool val = _board[row * 8 + col];
                        if (row < 4 && val) zone1++;
                        if ((row < 2 || (row >= 4 && row < 6)) && val) zone2++;
                        if (((row & 1) == 0) && val) zone3++;
                        if ((col < 4) && val) zone4++;
                        if ((col < 2 || (col >= 4 && col < 6)) && val) zone5++;
                        if (((col & 1) == 0) && val) zone6++;
                    }
                }
                return ((zone1 & 1) << 5) + ((zone2 & 1) << 4) + ((zone3 & 1) << 3) + ((zone4 & 1) << 2) + ((zone5 & 1) << 1) + (zone6 & 1);
            }
        }

        private string IndexToRowCol(int index)
        {
            string s = (index / 8).ToString() + "," + (index % 8).ToString();
            return s;
        }

        public string TryAll() {
            int[] pos = new int[64];

            for (int i = 0; i < 64; i++) {
                Board b = new Board(this);
                b._board[i] = !b._board[i];
                pos[b.Value] = i;
            }
            string s = "\nSelect\tFlip\n";
            for (int i = 0; i < 64; i++)
            {
                s += IndexToRowCol(i) + "\t" + IndexToRowCol(pos[i]) + "\n";
            }
            return s;
        }
    }
}
// PersonalPuzzle.cs
using System;
using System.Text;

namespace MultiToolLib
{
    // KẾT QUẢ TRẢ VỀ
    public class PersonalPuzzleResult
    {
        public string Encoded { get; set; }
        public string AsciiArt { get; set; }
        public string Signature { get; set; }
    }

    // LỚP CHÍNH: nhận Input, Key -> Compute()
    public class PersonalPuzzle
    {
        private string _input;
        private string _key;
        private PersonalPuzzleResult _lastResult;

        public PersonalPuzzle()
        {
            _input = string.Empty;
            _key = string.Empty;
            _lastResult = null;
        }

        // Thuộc tính input
        public string Input
        {
            get { return _input; }
            set { _input = value ?? string.Empty; }
        }

        // Thuộc tính key
        public string Key
        {
            get { return _key; }
            set { _key = value ?? string.Empty; }
        }

        // Đọc kết quả gần nhất
        public PersonalPuzzleResult LastResult
        {
            get { return _lastResult; }
        }

        // Hàm chính: tính toán kết quả, trả về chuỗi tóm tắt
        public string Compute()
        {
            string norm = Normalize(_input);
            string encoded = CombinedEncode(norm, _key);
            string art = CreateAsciiArt(encoded);
            string signature = CreateSignature();

            _lastResult = new PersonalPuzzleResult()
            {
                Encoded = encoded,
                AsciiArt = art,
                Signature = signature
            };

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("=== Personal Puzzle Result ===");
            sb.AppendLine("Encoded: " + encoded);
            sb.AppendLine();
            sb.AppendLine("ASCII Art:");
            sb.AppendLine(art);
            sb.AppendLine();
            sb.AppendLine("Signature: " + signature);
            return sb.ToString();
        }

        // Chuẩn hoá input: trim, collapse spaces
        private string Normalize(string s)
        {
            if (s == null) return string.Empty;
            string t = s.Trim();
            StringBuilder sb = new StringBuilder();
            bool lastSpace = false;
            foreach (char c in t)
            {
                if (char.IsWhiteSpace(c))
                {
                    if (!lastSpace)
                    {
                        sb.Append(' ');
                        lastSpace = true;
                    }
                }
                else
                {
                    sb.Append(c);
                    lastSpace = false;
                }
            }
            return sb.ToString();
        }

        // Kết hợp mã hoá: Vigenere-like + Caesar derived from key
        private string CombinedEncode(string text, string key)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;

            if (string.IsNullOrEmpty(key)) key = "bé yêu";

            int[] kseq = new int[key.Length];
            for (int i = 0; i < key.Length; i++) kseq[i] = (int)key[i];

            int caesar = 0;
            for (int i = 0; i < kseq.Length; i++) caesar += kseq[i];
            caesar = caesar % 13;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                int k = kseq[i % kseq.Length];
                int v = (int)c;
                int x = (v ^ k) + caesar;
                x = RotateRight(x, 3);
                int printable = 32 + (Math.Abs(x) % 95);
                sb.Append((char)printable);
            }
            return sb.ToString();
        }

        private int RotateRight(int value, int bits)
        {
            int mask = 0xFFFF;
            int v = value & mask;
            bits = bits % 16;
            return ((v >> bits) | (v << (16 - bits))) & mask;
        }

        // Tạo ASCII art từ chuỗi encoded
        private string CreateAsciiArt(string encoded)
        {
            if (string.IsNullOrEmpty(encoded)) return "(no art)";

            StringBuilder sb = new StringBuilder();
            int width = 40;
            int seed = 0;
            for (int i = 0; i < encoded.Length; i++) seed += (int)encoded[i];

            RandomPseudo rnd = new RandomPseudo(seed);

            int rows = Math.Max(6, Math.Min(18, encoded.Length / 2));
            for (int r = 0; r < rows; r++)
            {
                StringBuilder line = new StringBuilder();
                for (int c = 0; c < width; c++)
                {
                    int pick = rnd.Next(encoded.Length + 5);
                    char ch;
                    if (pick < encoded.Length)
                    {
                        char src = encoded[pick];
                        int selector = ((int)src + r + c) % 6;
                        ch = Palette(selector);
                    }
                    else
                    {
                        ch = Palette((r + c + seed) % 6);
                    }
                    line.Append(ch);
                }
                sb.AppendLine(line.ToString());
            }

            string sig = CreateSignature();
            string[] artLines = sb.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            int midLine = artLines.Length / 2;
            if (midLine >= 0 && midLine < artLines.Length)
            {
                string old = artLines[midLine];
                if (old.Length >= sig.Length)
                {
                    int pos = Math.Max(0, (old.Length - sig.Length) / 2);
                    char[] chars = old.ToCharArray();
                    for (int i = 0; i < sig.Length && pos + i < chars.Length; i++)
                    {
                        chars[pos + i] = sig[i];
                    }
                    artLines[midLine] = new string(chars);
                }
            }

            return string.Join(Environment.NewLine, artLines);
        }

        private char Palette(int selector)
        {
            switch (selector % 6)
            {
                case 0: return '.';
                case 1: return '*';
                case 2: return '+';
                case 3: return 'o';
                case 4: return '#';
                default: return '~';
            }
        }

        private string CreateSignature()
        {
            string tag = "bé yêu";
            string ts = DateTime.Now.ToString("yyyyMMddHHmm");
            return "[" + tag + " - " + ts + "]";
        }

        // Pseudo-random deterministic
        private class RandomPseudo
        {
            private int _state;
            public RandomPseudo(int seed)
            {
                _state = seed & 0x7fffffff;
                if (_state == 0) _state = 12345;
            }
            public int Next()
            {
                _state = (214013 * _state + 2531011) & 0x7fffffff;
                return (_state >> 16) & 0x7fff;
            }
            public int Next(int max)
            {
                if (max <= 0) return 0;
                return Next() % max;
            }
        }
    }
}

// Program.cs
using System;
using MultiToolLib;

namespace PuzzleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Personal Puzzle Console - bé yêu edition";

            Console.WriteLine("=== Personal Puzzle Console ===");
            Console.Write("Nhap chuoi (Input): ");
            string input = Console.ReadLine();
            Console.Write("Nhap khoa (Key) [Enter để dùng mặc định]: ");
            string key = Console.ReadLine();

            PersonalPuzzle p = new PersonalPuzzle();
            p.Input = input;
            p.Key = key;
            string output = p.Compute();

            Console.WriteLine();
            Console.WriteLine(output);

            Console.WriteLine();
            Console.Write("Ban muon luu ket qua vao file? (y/N): ");
            string ans = Console.ReadLine();
            if (!string.IsNullOrEmpty(ans) && (ans.ToLower() == "y"))
            {
                try
                {
                    string filename = "puzzle_result_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                    System.IO.File.WriteAllText(filename, output);
                    Console.WriteLine("Đã lưu: " + filename);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi lưu: " + ex.Message);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Nhấn Enter để kết thúc...");
            Console.ReadLine();
        }
    }
}

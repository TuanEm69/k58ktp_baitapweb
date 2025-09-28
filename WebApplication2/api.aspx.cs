using System;
using System.Web;
using System.Web.UI;
using System.Text;
using MultiToolLib;

public partial class api : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Chỉ cho phép POST
        if (Request.HttpMethod != "POST")
        {
            Response.StatusCode = 405; // Method Not Allowed
            Response.End();
            return;
        }

        string input = Request.Form["input"];
        if (input == null) input = "";

        string key = Request.Form["key"];
        if (key == null) key = "";

        PersonalPuzzle p = new PersonalPuzzle();
        p.Input = input;
        p.Key = key;
        p.Compute();

        PersonalPuzzleResult res = p.LastResult;

        string encoded = "";
        string art = "";
        string signature = "";

        if (res != null)
        {
            encoded = JsonEscape(res.Encoded);
            art = JsonEscape(res.AsciiArt);
            signature = JsonEscape(res.Signature);
        }

        string json = "{\"encoded\":\"" + encoded + "\",\"asciiArt\":\"" + art + "\",\"signature\":\"" + signature + "\"}";

        Response.Clear();
        Response.ContentType = "application/json; charset=utf-8";
        Response.Write(json);
        Response.End();
    }

    private string JsonEscape(string s)
    {
        if (s == null) return "";
        StringBuilder sb = new StringBuilder();
        foreach (char c in s)
        {
            switch (c)
            {
                case '\\':
                    sb.Append("\\\\");
                    break;
                case '"':
                    sb.Append("\\\"");
                    break;
                case '\n':
                    sb.Append("\\n");
                    break;
                case '\r':
                    sb.Append("\\r");
                    break;
                case '\t':
                    sb.Append("\\t");
                    break;
                default:
                    if (c < 32)
                    {
                        sb.Append("\\u" + ((int)c).ToString("X4"));
                    }
                    else
                    {
                        sb.Append(c);
                    }
                    break;
            }
        }
        return sb.ToString();
    }
}

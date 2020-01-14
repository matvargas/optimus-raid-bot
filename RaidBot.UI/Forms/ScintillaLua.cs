using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.UI.Forms
{
    public class ScintillaLua
    {
        public static void InitSyntax(Scintilla textArea)
        {
            textArea.WrapMode = WrapMode.None;
            textArea.IndentationGuides = IndentView.LookBoth;
            var alphaChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var numericChars = "0123456789";
            var accentedChars = "ŠšŒœŸÿÀàÁáÂâÃãÄäÅåÆæÇçÈèÉéÊêËëÌìÍíÎîÏïÐðÑñÒòÓóÔôÕõÖØøÙùÚúÛûÜüÝýÞþßö";

            // Configuring the default style with properties
            // we have common to every lexer style saves time.
            textArea.StyleResetDefault();
            textArea.Styles[Style.Default].Font = "Consolas";
            textArea.Styles[Style.Default].Size = 10;
            textArea.StyleClearAll();

            // Configure the Lua lexer styles
            textArea.Styles[Style.Lua.Default].ForeColor = Color.Silver;
            textArea.Styles[Style.Lua.Comment].ForeColor = Color.Green;
            textArea.Styles[Style.Lua.CommentLine].ForeColor = Color.Green;
            textArea.Styles[Style.Lua.Number].ForeColor = Color.Olive;
            textArea.Styles[Style.Lua.Word].ForeColor = Color.Blue;
            textArea.Styles[Style.Lua.Word2].ForeColor = Color.BlueViolet;
            textArea.Styles[Style.Lua.Word3].ForeColor = Color.DarkSlateBlue;
            textArea.Styles[Style.Lua.Word4].ForeColor = Color.DarkSlateBlue;
            textArea.Styles[Style.Lua.String].ForeColor = Color.Red;
            textArea.Styles[Style.Lua.Character].ForeColor = Color.Red;
            textArea.Styles[Style.Lua.LiteralString].ForeColor = Color.Red;
            textArea.Styles[Style.Lua.StringEol].BackColor = Color.Pink;
            textArea.Styles[Style.Lua.Operator].ForeColor = Color.Purple;
            textArea.Styles[Style.Lua.Preprocessor].ForeColor = Color.Maroon;
            textArea.Lexer = Lexer.Lua;
            textArea.WordChars = alphaChars + numericChars + accentedChars;

            // Console.WriteLine(textArea.DescribeKeywordSets());

            // Keywords
            textArea.SetKeywords(0, "and break do else elseif end for function if in local nil not or repeat return then until while" + " false true" + " goto");
            // Basic Functions
            textArea.SetKeywords(1, "assert collectgarbage dofile error _G getmetatable ipairs loadfile next pairs pcall print rawequal rawget rawset setmetatable tonumber tostring type _VERSION xpcall string table math coroutine io os debug" + " getfenv gcinfo load loadlib loadstring require select setfenv unpack _LOADED LUA_PATH _REQUIREDNAME package rawlen package bit32 utf8 _ENV");
            // String Manipulation & Mathematical
            textArea.SetKeywords(2, "string.byte string.char string.dump string.find string.format string.gsub string.len string.lower string.rep string.sub string.upper table.concat table.insert table.remove table.sort math.abs math.acos math.asin math.atan math.atan2 math.ceil math.cos math.deg math.exp math.floor math.frexp math.ldexp math.log math.max math.min math.pi math.pow math.rad math.random math.randomseed math.sin math.sqrt math.tan" + " string.gfind string.gmatch string.match string.reverse string.pack string.packsize string.unpack table.foreach table.foreachi table.getn table.setn table.maxn table.pack table.unpack table.move math.cosh math.fmod math.huge math.log10 math.modf math.mod math.sinh math.tanh math.maxinteger math.mininteger math.tointeger math.type math.ult" + " bit32.arshift bit32.band bit32.bnot bit32.bor bit32.btest bit32.bxor bit32.extract bit32.replace bit32.lrotate bit32.lshift bit32.rrotate bit32.rshift" + " utf8.char utf8.charpattern utf8.codes utf8.codepoint utf8.len utf8.offset");
            // Input and Output Facilities and System Facilities
            textArea.SetKeywords(3, "coroutine.create coroutine.resume coroutine.status coroutine.wrap coroutine.yield io.close io.flush io.input io.lines io.open io.output io.read io.tmpfile io.type io.write io.stdin io.stdout io.stderr os.clock os.date os.difftime os.execute os.exit os.getenv os.remove os.rename os.setlocale os.time os.tmpname" + " coroutine.isyieldable coroutine.running io.popen module package.loaders package.seeall package.config package.searchers package.searchpath" + " require package.cpath package.loaded package.loadlib package.path package.preload");
            // Custom

            // Instruct the lexer to calculate folding
            textArea.SetProperty("fold", "1");
            textArea.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            textArea.Margins[2].Type = MarginType.Symbol;
            textArea.Margins[2].Mask = Marker.MaskFolders;
            textArea.Margins[2].Sensitive = true;
            textArea.Margins[2].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                textArea.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                textArea.Markers[i].SetBackColor(SystemColors.ControlDark);
            }

            // Configure folding markers with respective symbols
            textArea.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            textArea.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            textArea.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            textArea.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            textArea.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            textArea.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            textArea.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;
            // Enable automatic folding
            textArea.Margins[0].Width = 16;
            textArea.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

        }
    }
}

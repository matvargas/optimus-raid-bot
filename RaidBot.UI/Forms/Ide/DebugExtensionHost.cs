using MoonSharp.Interpreter;
using RaidBot.Engine.Bot;
using RaidBot.Engine.Bot.Extension;
using RaidBot.Engine.Bot.Managers;
using RaidBot.Engine.Bot.Managers.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.UI.Forms.Ide
{
    public class DebugExtensionHost : IExtensionHost
    {
        public event Action<int, String> SyntaxError;
        public event Action<ScriptRuntimeException> RunTimeError;
        Brain brain;

        public DebugExtensionHost(String content, Brain bot)
        {
            brain = bot;
            mContent = content;
        }

        String mContent;
        public string GetContent()
        {
            return mContent;
        }

        public ExtensionDomain ParseContent()
        {
            try
            {
                ExtensionDomain domain = new ExtensionDomain(ExtensionType.Trajet, brain);
                domain.Load(mContent);
                return domain;
            }
            catch (MoonSharp.Interpreter.SyntaxErrorException e)
            {
                if (SyntaxError != null)
                    SyntaxError(-1, e.DecoratedMessage);
            }
            catch (ScriptRuntimeException e)
            {
                if (RunTimeError != null)
                    RunTimeError(e);
            }
            return null;
        }
    }
}

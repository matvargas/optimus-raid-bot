using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBot.Engine.Functionality.Script.Machine.Link
{
    public class MouvementsLink:ILink
    {
        public event EventHandler<MouvementAddedEventArgs> MouvementAdded;

        public void AddMouvement(MouvementAddedEventArgs mouvements)
        {
            if (MouvementAdded != null)
                MouvementAdded(this, mouvements);
        }
    }
}

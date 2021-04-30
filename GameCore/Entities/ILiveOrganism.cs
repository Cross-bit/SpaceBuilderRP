using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*namespace strG.Assets.Scripts.GameCore.LiveOrganisms
{*/
public interface ILiveOrganism
{
    string OrganismName { get; set; }
    float OrganismAge { get; set; }
    int OrganismHealth { get; set; }
}
//}

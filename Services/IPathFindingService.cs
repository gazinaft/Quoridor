using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Strategy;
namespace Services
{
    public class IPathFindingService
    {
        IAlgorithm SelectedAlgorithm { get; set; }
    }
}

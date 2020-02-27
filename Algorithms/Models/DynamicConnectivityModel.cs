using Algorithms.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Algorithms.Models
{
    public class DynamicConnectivityModel
    {
        [Display(Name = "Elements Count")]
        public int elementsCount { get; set; }
        public int[] collection { get; set; }
        public string unions { get; set; }
        public int firstNode { get; set; }
        public int secondNode { get; set; }
        public bool connected { get; set; }
        public InlineImageResult graphic { get; set; }
    }
}

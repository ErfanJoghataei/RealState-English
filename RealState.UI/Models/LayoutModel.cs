using RealState.Dal.Entities;

namespace RealState.UI.Models
{
    public class LayoutModel
    {
        public EmailErorModel EmailEror { get; set; }
        public EmailModel EmailModel { get; set; }
        public List< Properties> properties { get; set; }
    }
}
